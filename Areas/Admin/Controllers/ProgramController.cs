using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private readonly PicklrContext context;

        public ProgramController(PicklrContext ctx)
        {
            context = ctx;
        }


        // GET: /Admin/Program
        [HttpGet]
        public IActionResult Index()
        {
            List<PicklProgram> programs = context.Programs
                .Include(p => p.Club)
                .OrderBy(p => p.ProgramID)
                .ToList();

            return View("List", programs);
        }


        // Keeps /Admin/Program/List working.
        [HttpGet]
        public IActionResult List()
        {
            return RedirectToAction(nameof(Index));
        }


        // GET: /Admin/Program/AddEdit
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            PicklProgram program;

            if (id == null)
            {
                program = new PicklProgram();
                ViewBag.Action = "Add";
            }
            else
            {
                PicklProgram? existingProgram =
                    context.Programs.Find(id.Value);

                if (existingProgram == null)
                {
                    return NotFound();
                }

                program = existingProgram;
                ViewBag.Action = "Edit";
            }


            LoadClubs(program.ClubID);

            return View(program);
        }



        // POST: /Admin/Program/AddEdit
        [HttpPost]
        public IActionResult AddEdit(PicklProgram program)
        {
            if (ModelState.IsValid)
            {
                if (program.ProgramID == 0)
                {
                    context.Programs.Add(program);
                }
                else
                {
                    PicklProgram? existingProgram =
                        context.Programs.Find(program.ProgramID);

                    if (existingProgram == null)
                    {
                        return NotFound();
                    }


                    existingProgram.Name =
                        program.Name;

                    existingProgram.ClubID =
                        program.ClubID;

                    existingProgram.Description =
                        program.Description;

                    existingProgram.Fee =
                        program.Fee;


                    existingProgram.Monday =
                        program.Monday;

                    existingProgram.Tuesday =
                        program.Tuesday;

                    existingProgram.Wednesday =
                        program.Wednesday;

                    existingProgram.Thursday =
                        program.Thursday;

                    existingProgram.Friday =
                        program.Friday;

                    existingProgram.Saturday =
                        program.Saturday;

                    existingProgram.Sunday =
                        program.Sunday;
                }


                context.SaveChanges();


                TempData["message"] =
                    $"'{program.Name}' was saved successfully.";


                return RedirectToAction(nameof(Index));
            }


            ViewBag.Action =
                program.ProgramID == 0
                ? "Add"
                : "Edit";


            LoadClubs(program.ClubID);


            return View(program);
        }




        // GET: /Admin/Program/Delete/2
        [HttpGet]
        public IActionResult Delete(int id)
        {
            PicklProgram? program =
                context.Programs
                    .Include(p => p.Club)
                    .FirstOrDefault(
                        p => p.ProgramID == id);


            if (program == null)
            {
                return NotFound();
            }


            return View(program);
        }




        // POST: /Admin/Program/Delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int programID)
        {
            PicklProgram? program =
                context.Programs.Find(programID);


            if (program == null)
            {
                return NotFound();
            }


            string programName =
                program.Name;


            context.Programs.Remove(program);

            context.SaveChanges();


            TempData["message"] =
                $"'{programName}' was deleted successfully.";


            return RedirectToAction(nameof(Index));
        }




        private void LoadClubs(int selectedClubID = 0)
        {
            List<Club> clubs =
                context.Clubs
                    .OrderBy(c => c.Name)
                    .ToList();


            ViewBag.Clubs =
                new SelectList(
                    clubs,
                    "ClubID",
                    "Name",
                    selectedClubID);
        }
    }
}