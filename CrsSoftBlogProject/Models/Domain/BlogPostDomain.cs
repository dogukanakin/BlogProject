namespace CrsSoftBlogProject.Models.Domain
{
    public class BlogPostDomain
    {
        public Guid Id { get; set; }
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? SelectedTag { get; set; }
        public string? CategoriesName { get; set; }
        public string? FeaturedImageUpload { get; set; } 
        public int? TotalLikes { get; set; }

        public ICollection<TagDomain> Tags { get; set; }

        public ICollection<BlogPostLikeDomain> Likes { get; set; }


        public ICollection<CategoriesDomain> Categories { get; set; }


        public ICollection<BlogPostCommentDomain> Comments { get; set; }


    }
}


