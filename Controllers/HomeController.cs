using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class HomeController : Controller
    {
        private readonly PicklrContext context;

        public HomeController(PicklrContext ctx)
        {
            context = ctx;
        }


        public ViewResult Index(ProgramsViewModel model)
        {
            var session =
                new PicklrSession(HttpContext.Session);


            string activeClub;


            if (!string.IsNullOrEmpty(model.ActiveClub) &&
                model.ActiveClub != "all")
            {
                activeClub = model.ActiveClub;

                session.SetActiveClub(activeClub);
            }
            else
            {
                activeClub =
                    session.GetActiveClub();
            }



            DateTime selectedDate;


            if (model.ActiveDate.HasValue)
            {
                selectedDate =
                    model.ActiveDate.Value.Date;

                session.SetActiveDate(
                    selectedDate);
            }
            else
            {
                selectedDate =
                    session.GetActiveDate();
            }



            model.ActiveClub =
                activeClub;


            model.ActiveDate =
                selectedDate;



            model.Clubs =
                context.Clubs
                    .OrderBy(c => c.Name)
                    .ToList();



            IQueryable<PicklProgram> query =
                context.Programs
                    .Include(p => p.Club);



            if (activeClub != "all" &&
                int.TryParse(
                    activeClub,
                    out int clubId))
            {
                query =
                    query.Where(
                        p => p.ClubID == clubId);
            }



            switch (selectedDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    query =
                        query.Where(
                            p => p.Monday);
                    break;


                case DayOfWeek.Tuesday:
                    query =
                        query.Where(
                            p => p.Tuesday);
                    break;


                case DayOfWeek.Wednesday:
                    query =
                        query.Where(
                            p => p.Wednesday);
                    break;


                case DayOfWeek.Thursday:
                    query =
                        query.Where(
                            p => p.Thursday);
                    break;


                case DayOfWeek.Friday:
                    query =
                        query.Where(
                            p => p.Friday);
                    break;


                case DayOfWeek.Saturday:
                    query =
                        query.Where(
                            p => p.Saturday);
                    break;


                case DayOfWeek.Sunday:
                    query =
                        query.Where(
                            p => p.Sunday);
                    break;
            }



            model.Programs =
                query
                    .OrderBy(
                        p => p.ProgramID)
                    .ToList();



            return View(model);
        }




        public ViewResult Details(
            int id,
            DateTime? date = null)
        {
            var session =
                new PicklrSession(HttpContext.Session);



            DateTime selectedDate =
                date?.Date
                ?? session.GetActiveDate();



            if (date.HasValue)
            {
                session.SetActiveDate(
                    selectedDate);
            }



            PicklProgram? program =
                context.Programs
                    .Include(
                        p => p.Club)
                    .FirstOrDefault(
                        p => p.ProgramID == id);



            if (program == null)
            {
                Response.StatusCode = 404;


                return View(
                    new ProgramsViewModel
                    {
                        ActiveClub =
                            session.GetActiveClub(),

                        ActiveDate =
                            selectedDate
                    });
            }



            var model =
                new ProgramsViewModel
                {
                    Program =
                        program,

                    ActiveClub =
                        session.GetActiveClub(),

                    ActiveDate =
                        selectedDate
                };



            return View(model);
        }



        public ContentResult About()
        {
            return Content(
                "About page — under construction.");
        }



        public ContentResult Club()
        {
            return Content(
                "Club page — under construction.");
        }



        public ContentResult Program()
        {
            return Content(
                "Program page — under construction.");
        }



        public ContentResult Shop()
        {
            return Content(
                "Shop page — under construction.");
        }
    }
}