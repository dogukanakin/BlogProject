using System.ComponentModel.DataAnnotations;

namespace CrsSoftBlogProject.Models.ViewModels
{
    public class AddTagRequestViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}