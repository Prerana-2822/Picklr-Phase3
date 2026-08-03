using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Picklr.Models
{
    public class Club
    {
        public int ClubID { get; set; }

        [Required(ErrorMessage = "Please enter a club name.")]
        [StringLength(100,
            ErrorMessage = "Club name cannot exceed 100 characters.")]
        [Remote(
            action: "CheckClubName",
            controller: "Validation",
            areaName: "",
            AdditionalFields = nameof(ClubID),
            ErrorMessage = "Club name already exists.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(100,
            ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; } = string.Empty;

        [StringLength(300,
            ErrorMessage = "Description cannot exceed 300 characters.")]
        public string Description { get; set; } = string.Empty;

        [ValidateNever]
        public ICollection<PicklProgram> Programs { get; set; }
            = new List<PicklProgram>();
}
}