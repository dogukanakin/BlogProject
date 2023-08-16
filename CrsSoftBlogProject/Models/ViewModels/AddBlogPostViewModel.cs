using CrsSoftBlogProject.Models.Domain;

namespace CrsSoftBlogProject.Models.ViewModels
{
    public class AddBlogPostViewModel
    {
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string SelectedTag { get; set; }
        public string CategoriesName { get; set; }
        public int SelectedCategoryId { get; set; }

        public int? TotalLikes { get; set; }

        public string FeaturedImageUpload { get; set; }

        public List<TagDomain> Tags { get; set; }
        public List<CategoriesDomain> Categories { get; set; }

    }
}
