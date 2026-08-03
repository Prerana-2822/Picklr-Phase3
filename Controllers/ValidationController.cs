using Microsoft.AspNetCore.Mvc;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class ValidationController : Controller
    {
        private readonly PicklrContext context;

        public ValidationController(PicklrContext ctx)
        {
            context = ctx;
        }

        // ----------------------------
        // Program Remote Validation
        // ----------------------------
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckProgramName(string name, int programID)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(true);
            }

            bool exists = context.Programs.Any(p =>
                p.Name.ToLower().Trim() == name.ToLower().Trim() &&
                p.ProgramID != programID);

            if (exists)
            {
                return Json("Program name already exists.");
            }

            return Json(true);
        }


        // ----------------------------
        // Club Remote Validation
        // ----------------------------
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckClubName(string name, int clubID)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(true);
            }

            bool exists = context.Clubs.Any(c =>
                c.Name.ToLower().Trim() == name.ToLower().Trim() &&
                c.ClubID != clubID);

            if (exists)
            {
                return Json("Club name already exists.");
            }

            return Json(true);
        }


        // ----------------------------
        // User Remote Validation
        // ----------------------------
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckEmail(string email, int userID)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(true);
            }

            bool exists = context.Users.Any(u =>
                u.Email.ToLower().Trim() == email.ToLower().Trim() &&
                u.UserID != userID);

            if (exists)
            {
                return Json("Email address already exists.");
            }

            return Json(true);
        }
    }
}