using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Picklr.Models
{
    // Named AppUser to avoid future conflicts if ASP.NET Identity is added.
    public class AppUser
    {
        [Key]
        public int UserID { get; set; }


        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(50,
            ErrorMessage = "First name cannot exceed 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(50,
            ErrorMessage = "Last name cannot exceed 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100,
            ErrorMessage = "Email cannot exceed 100 characters.")]
        [Remote(
            action: "CheckEmail",
            controller: "Validation",
            areaName: "",
            AdditionalFields = nameof(UserID),
            ErrorMessage = "Email address already exists.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select a role.")]
        [RegularExpression(
            "Client|Admin",
            ErrorMessage = "Please select a valid role.")]
        public string Role { get; set; } = "Client";


        // Computed display name for use in views
        public string FullName => $"{FirstName} {LastName}";
    }
}