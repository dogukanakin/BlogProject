using System.ComponentModel.DataAnnotations;

namespace CrsSoftBlogProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }


    }
}


