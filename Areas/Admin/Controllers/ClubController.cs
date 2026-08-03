using Microsoft.AspNetCore.Mvc;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private readonly PicklrContext context;

        public ClubController(PicklrContext ctx)
        {
            context = ctx;
        }

        // GET: /Admin/Club
        [HttpGet]
        public IActionResult Index()
        {
            List<Club> clubs = context.Clubs
                .OrderBy(c => c.Name)
                .ToList();

            return View("List", clubs);
        }


        // Keeps /Admin/Club/List links working.
        [HttpGet]
        public IActionResult List()
        {
            return RedirectToAction(nameof(Index));
        }


        // GET: /Admin/Club/AddEdit
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            Club club;

            if (id == null)
            {
                club = new Club();
                ViewBag.Action = "Add";
            }
            else
            {
                Club? existingClub = context.Clubs.Find(id.Value);

                if (existingClub == null)
                {
                    return NotFound();
                }

                club = existingClub;
                ViewBag.Action = "Edit";
            }

            return View(club);
        }


        // POST: /Admin/Club/AddEdit
        [HttpPost]
        public IActionResult AddEdit(Club club)
        {
            if (ModelState.IsValid)
            {
                if (club.ClubID == 0)
                {
                    context.Clubs.Add(club);
                }
                else
                {
                    Club? existingClub =
                        context.Clubs.Find(club.ClubID);

                    if (existingClub == null)
                    {
                        return NotFound();
                    }

                    existingClub.Name = club.Name;
                    existingClub.Location = club.Location;
                    existingClub.Description = club.Description;
                }

                context.SaveChanges();

                TempData["message"] =
                    $"'{club.Name}' was saved successfully.";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action =
                club.ClubID == 0 ? "Add" : "Edit";

            return View(club);
        }


        // GET: /Admin/Club/Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Club? club = context.Clubs.Find(id);

            if (club == null)
            {
                return NotFound();
            }

            bool hasPrograms = context.Programs
                .Any(p => p.ClubID == id);

            ViewBag.HasPrograms = hasPrograms;

            return View(club);
        }


        // POST: /Admin/Club/Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int clubID)
        {
            Club? club = context.Clubs.Find(clubID);

            if (club == null)
            {
                return NotFound();
            }

            bool hasPrograms = context.Programs
                .Any(p => p.ClubID == clubID);

            if (hasPrograms)
            {
                TempData["message"] =
                    $"'{club.Name}' cannot be deleted because programs are assigned to it.";

                return RedirectToAction(nameof(Index));
            }

            string clubName = club.Name;

            context.Clubs.Remove(club);
            context.SaveChanges();

            TempData["message"] =
                $"'{clubName}' was deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}