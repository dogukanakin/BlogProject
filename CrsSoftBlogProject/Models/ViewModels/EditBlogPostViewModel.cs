using CrsSoftBlogProject.Models.Domain;


namespace CrsSoftBlogProject.Models.ViewModels
{
    public class EditBlogPostViewModel
    {
        public Guid Id { get; set; }
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public string? Description { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? Author { get; set; }
        public string? SelectedTag { get; set; }
        public string? CategoriesName { get; set; }

        public bool Liked { get; set; }

        public int? TotalLikes { get; set; }

        public string? FeaturedImageUpload { get; set; }
        public string? UrlHandle { get; set; }

        public string? CommentDescription { get; set; }

        public IEnumerable<BlogPostCommentDomain>? Comments { get; set; }

    }
}
