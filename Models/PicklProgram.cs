using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Picklr.Models
{
    public class PicklProgram
    {
        [Key]
        public int ProgramID { get; set; }

        [Required(ErrorMessage = "Please select a club.")]
        [Display(Name = "Club")]
        public int ClubID { get; set; }

        [ValidateNever]
        public Club Club { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a program name.")]
        [Remote(
            action: "CheckProgramName",
            controller: "Validation",
            areaName: "",
            AdditionalFields = nameof(ProgramID),
            ErrorMessage = "Program name already exists.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(200,
            MinimumLength = 10,
            ErrorMessage = "Description must be between 10 and 200 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the fee.")]
        [Range(1, 10000,
            ErrorMessage = "Fee must be between $1 and $10,000.")]
        public decimal Fee { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        [AtLeastOneDay(ErrorMessage = "Please select at least one training day.")]
        [NotMapped]
        public bool ValidateDays => true;

        [NotMapped]
        public string AvailableDays
        {
            get
            {
                List<string> days = new();

                if (Monday) days.Add("Monday");
                if (Tuesday) days.Add("Tuesday");
                if (Wednesday) days.Add("Wednesday");
                if (Thursday) days.Add("Thursday");
                if (Friday) days.Add("Friday");
                if (Saturday) days.Add("Saturday");
                if (Sunday) days.Add("Sunday");

                return string.Join(", ", days);
            }
        }
    }
}