using System.ComponentModel.DataAnnotations;

namespace CrsSoftBlogProject.Models.ViewModels
{
    public class AddContactFormViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}
