using Microsoft.AspNetCore.Mvc;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly PicklrContext context;

        public UserController(PicklrContext ctx)
        {
            context = ctx;
        }


        // GET: /Admin/User
        [HttpGet]
        public IActionResult Index()
        {
            List<AppUser> users = context.Users
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();

            return View("List", users);
        }


        // Keeps older links working.
        [HttpGet]
        public IActionResult List()
        {
            return RedirectToAction(nameof(Index));
        }


        // GET: /Admin/User/AddEdit
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            AppUser user;

            if (id == null)
            {
                user = new AppUser();
                ViewBag.Action = "Add";
            }
            else
            {
                AppUser? existingUser =
                    context.Users.Find(id.Value);

                if (existingUser == null)
                {
                    return NotFound();
                }

                user = existingUser;
                ViewBag.Action = "Edit";
            }

            return View(user);
        }


        // POST: /Admin/User/AddEdit
        [HttpPost]
        public IActionResult AddEdit(AppUser user)
        {
            if (ModelState.IsValid)
            {
                if (user.UserID == 0)
                {
                    context.Users.Add(user);
                }
                else
                {
                    AppUser? existingUser =
                        context.Users.Find(user.UserID);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.FirstName =
                        user.FirstName;

                    existingUser.LastName =
                        user.LastName;

                    existingUser.Email =
                        user.Email;

                    existingUser.Role =
                        user.Role;
                }


                context.SaveChanges();


                TempData["message"] =
                    $"'{user.FullName}' was saved successfully.";


                return RedirectToAction(nameof(Index));
            }


            ViewBag.Action =
                user.UserID == 0
                ? "Add"
                : "Edit";


            return View(user);
        }



        // GET: /Admin/User/Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            AppUser? user =
                context.Users.Find(id);


            if (user == null)
            {
                return NotFound();
            }


            return View(user);
        }



        // POST: /Admin/User/Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int userID)
        {
            AppUser? user =
                context.Users.Find(userID);


            if (user == null)
            {
                return NotFound();
            }


            string fullName =
                user.FullName;


            context.Users.Remove(user);

            context.SaveChanges();


            TempData["message"] =
                $"'{fullName}' was deleted successfully.";


            return RedirectToAction(nameof(Index));
        }
    }
}