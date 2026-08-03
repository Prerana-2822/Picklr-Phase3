using System;
using System.Collections.Generic;
using System.Linq;

namespace Picklr.Models
{
    public class ProgramsViewModel
    {
        // The club currently selected by the user.
        // "all" indicates that all clubs should be displayed.
        public string ActiveClub { get; set; } = "all";

        // The reservation date selected by the user.
        // Defaults to today's date if not supplied.
        public DateTime? ActiveDate { get; set; }

        // A single program used by the Details page.
        public PicklProgram Program { get; set; } =
            new PicklProgram();

        // The list of programs displayed on the Home page.
        public List<PicklProgram> Programs { get; set; } =
            new List<PicklProgram>();

        // Programs currently stored in the user's reservation cart.
        public List<CartProgramViewModel> CartPrograms { get; set; } =
            new List<CartProgramViewModel>();

        // All clubs used to populate the Club filter.
        public List<Club> Clubs { get; set; } =
            new List<Club>();

        // Returns "active" when the supplied club matches
        // the currently selected club.
        public string CheckActiveClub(string clubID)
        {
            return string.Equals(
                clubID,
                ActiveClub,
                StringComparison.OrdinalIgnoreCase)
                ? "active"
                : "";
        }

        // Calculates the total reservation fee.
        public decimal CartTotal =>
            CartPrograms.Sum(item => item.Program.Fee);
    }
}