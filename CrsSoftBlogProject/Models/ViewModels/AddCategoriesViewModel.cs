using CrsSoftBlogProject.Models.Domain;

namespace CrsSoftBlogProject.Models.ViewModels
{
    public class AddCategoriesViewModel
    {
        public string CategoriesName { get; set; }

        public List<BlogPostDomain> BlogPosts { get; set; }
    }
}
