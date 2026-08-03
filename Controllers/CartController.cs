using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class CartController : Controller
    {
        private readonly PicklrContext context;

        public CartController(PicklrContext ctx)
        {
            context = ctx;
        }


        public ViewResult Index()
        {
            var session =
                new PicklrSession(HttpContext.Session);


            List<CartItem> cartItems =
                session.GetCartItems();


            List<int> programIDs =
                cartItems
                    .Select(item => item.ProgramID)
                    .Distinct()
                    .ToList();


            List<PicklProgram> programs =
                context.Programs
                    .Include(p => p.Club)
                    .Where(p =>
                        programIDs.Contains(p.ProgramID))
                    .ToList();



            List<CartProgramViewModel> cartPrograms =
                new List<CartProgramViewModel>();


            foreach (CartItem cartItem in cartItems)
            {
                PicklProgram? program =
                    programs.FirstOrDefault(
                        p => p.ProgramID == cartItem.ProgramID);


                if (program != null)
                {
                    cartPrograms.Add(
                        new CartProgramViewModel
                        {
                            Program = program,
                            Date = cartItem.Date
                        });
                }
            }



            ProgramsViewModel model =
                new ProgramsViewModel
                {
                    ActiveClub =
                        session.GetActiveClub(),

                    ActiveDate =
                        session.GetActiveDate(),

                    CartPrograms =
                        cartPrograms
                            .OrderBy(item => item.Date)
                            .ThenBy(item =>
                                item.Program.ProgramID)
                            .ToList()
                };


            return View(model);
        }




        [HttpPost]
        public RedirectToActionResult Add(
            int id,
            DateTime date)
        {
            var session =
                new PicklrSession(HttpContext.Session);


            List<CartItem> cartItems =
                session.GetCartItems();


            PicklProgram? program =
                context.Programs
                    .FirstOrDefault(
                        p => p.ProgramID == id);



            if (program == null)
            {
                TempData["message"] =
                    "The selected program could not be found.";

                return RedirectToHome(
                    session.GetActiveClub(),
                    date);
            }



            bool alreadyAdded =
                cartItems.Any(
                    item =>
                        item.ProgramID == id &&
                        item.Date.Date == date.Date);



            if (!alreadyAdded)
            {
                cartItems.Add(
                    new CartItem
                    {
                        ProgramID = id,
                        Date = date.Date
                    });


                session.SetCartItems(cartItems);


                TempData["message"] =
                    $"{program.Name} added to your cart.";
            }
            else
            {
                TempData["message"] =
                    $"{program.Name} is already in your cart.";
            }


            return RedirectToHome(
                session.GetActiveClub(),
                date);
        }





        [HttpPost]
        public RedirectToActionResult Remove(
            int id,
            DateTime date)
        {
            var session =
                new PicklrSession(HttpContext.Session);


            List<CartItem> cartItems =
                session.GetCartItems();


            CartItem? item =
                cartItems.FirstOrDefault(
                    x =>
                        x.ProgramID == id &&
                        x.Date.Date == date.Date);



            if (item != null)
            {
                cartItems.Remove(item);

                session.SetCartItems(cartItems);


                TempData["message"] =
                    "Program removed from cart.";
            }


            return RedirectToAction(nameof(Index));
        }





        [HttpPost]
        public RedirectToActionResult Clear()
        {
            var session =
                new PicklrSession(HttpContext.Session);


            session.RemoveCartItems();


            TempData["message"] =
                "All programs removed from cart.";


            return RedirectToAction(nameof(Index));
        }







        // Step 1:
        // Receive selected programs from Cart page
        [HttpPost]
        public IActionResult Confirm(List<int> selectedPrograms)
        {
            var session =
                new PicklrSession(HttpContext.Session);



            if (selectedPrograms == null ||
                selectedPrograms.Count == 0)
            {
                TempData["message"] =
                    "Please select at least one program.";

                return RedirectToAction(nameof(Index));
            }



            List<CartItem> cartItems =
                session.GetCartItems();



            List<CartItem> selectedItems =
                cartItems
                    .Where(item =>
                        selectedPrograms.Contains(
                            item.ProgramID))
                    .ToList();



            if (selectedItems.Count == 0)
            {
                TempData["message"] =
                    "Selected programs were not found.";

                return RedirectToAction(nameof(Index));
            }



            session.SetConfirmItems(selectedItems);


            return RedirectToAction(nameof(Review));
        }






        // Step 2:
        // Display confirmation page
        public IActionResult Review()
        {
            var session =
                new PicklrSession(HttpContext.Session);



            List<CartItem> confirmItems =
                session.GetConfirmItems();



            if (confirmItems.Count == 0)
            {
                TempData["message"] =
                    "No programs selected.";

                return RedirectToAction(nameof(Index));
            }



            List<int> ids =
                confirmItems
                    .Select(item => item.ProgramID)
                    .ToList();



            List<PicklProgram> programs =
                context.Programs
                    .Include(p => p.Club)
                    .Where(p =>
                        ids.Contains(p.ProgramID))
                    .ToList();




            List<CartProgramViewModel> cartPrograms =
                new List<CartProgramViewModel>();



            foreach (CartItem item in confirmItems)
            {
                PicklProgram? program =
                    programs.FirstOrDefault(
                        p =>
                        p.ProgramID == item.ProgramID);



                if (program != null)
                {
                    cartPrograms.Add(
                        new CartProgramViewModel
                        {
                            Program = program,
                            Date = item.Date
                        });
                }
            }



            ProgramsViewModel model =
                new ProgramsViewModel
                {
                    CartPrograms = cartPrograms
                };


            return View(model);
        }







        // Step 3:
        // Final confirmation
        [HttpPost]
        public RedirectToActionResult Complete()
        {
            var session =
                new PicklrSession(HttpContext.Session);



            List<CartItem> confirmItems =
                session.GetConfirmItems();



            if (confirmItems.Count == 0)
            {
                TempData["message"] =
                    "No reservation found.";

                return RedirectToAction(nameof(Index));
            }



            List<CartItem> cartItems =
                session.GetCartItems();



            foreach (CartItem item in confirmItems)
            {
                cartItems.Remove(
                    cartItems.First(
                        x =>
                        x.ProgramID == item.ProgramID &&
                        x.Date == item.Date));
            }



            session.SetCartItems(cartItems);

            session.RemoveConfirmItems();



            TempData["message"] =
                "Reservation confirmed successfully.";



            return RedirectToAction(nameof(Index));
        }







        private RedirectToActionResult RedirectToHome(
            string activeClub,
            DateTime date)
        {
            return RedirectToAction(
                "Index",
                "Home",
                new
                {
                    ClubId = activeClub,
                    Date = date.ToString("yyyy-MM-dd")
                });
        }
    }
}