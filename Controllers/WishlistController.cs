using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class WishlistController : Controller
    {

        private PicklrContext context { get; set; }



        public WishlistController(PicklrContext ctx)
        {
            context = ctx;
        }





        // SHOW WISHLIST
        public IActionResult Index()
        {

            var session =
                new PicklrSession(HttpContext.Session);



            List<WishlistItem> wishlistItems =
                session.GetWishlistItems();



            return View(wishlistItems);

        }







        // ADD PROGRAM TO WISHLIST
        public IActionResult Add(int id)
        {

            var session =
                new PicklrSession(HttpContext.Session);



            List<WishlistItem> wishlistItems =
                session.GetWishlistItems();



            PicklProgram? program =
                context.Programs
                .Include(p => p.Club)
                .FirstOrDefault(
                    p => p.ProgramID == id
                );



            if (program != null)
            {


                bool exists =
                    wishlistItems.Any(
                        x => x.ProgramID == id
                    );



                if (!exists)
                {

                    WishlistItem item =
                        new WishlistItem
                        {

                            ProgramID =
                                program.ProgramID,


                            Name =
                                program.Name,


                            Description =
                                program.Description,


                            Fee =
                                program.Fee,


                            ClubName =
                                program.Club?.Name ?? ""

                        };



                    wishlistItems.Add(item);

                }

            }



            session.SetWishlistItems(
                wishlistItems
            );



            TempData["message"] =
                "Program added to your wishlist.";



            return RedirectToAction(
                "Index",
                "Home"
            );

        }









        // MOVE WISHLIST ITEM TO CART
        [HttpPost]
        public IActionResult MoveToCart(int id)
        {

            var session =
                new PicklrSession(HttpContext.Session);



            List<WishlistItem> wishlistItems =
                session.GetWishlistItems();



            WishlistItem? wishlistItem =
                wishlistItems.FirstOrDefault(
                    x => x.ProgramID == id
                );



            if (wishlistItem != null)
            {


                List<CartItem> cartItems =
                    session.GetCartItems();



                bool alreadyInCart =
                    cartItems.Any(
                        x => x.ProgramID == id
                    );



                if (!alreadyInCart)
                {


                    CartItem cartItem =
                        new CartItem
                        {

                            ProgramID =
                                wishlistItem.ProgramID,


                            Date =
                                DateTime.Today

                        };



                    cartItems.Add(cartItem);



                    session.SetCartItems(
                        cartItems
                    );

                }



                // Remove from wishlist

                wishlistItems.Remove(
                    wishlistItem
                );



                session.SetWishlistItems(
                    wishlistItems
                );



                TempData["message"] =
                    wishlistItem.Name +
                    " moved to your cart.";

            }



            return RedirectToAction(
                "Index"
            );

        }









        // REMOVE FROM WISHLIST
        [HttpPost]
        public IActionResult Remove(int id)
        {

            var session =
                new PicklrSession(HttpContext.Session);



            List<WishlistItem> wishlistItems =
                session.GetWishlistItems();



            WishlistItem? item =
                wishlistItems.FirstOrDefault(
                    x => x.ProgramID == id
                );



            if (item != null)
            {

                wishlistItems.Remove(item);

            }



            session.SetWishlistItems(
                wishlistItems
            );



            TempData["message"] =
                "Program removed from wishlist.";



            return RedirectToAction(
                "Index"
            );

        }


    }
}