namespace CrsSoftBlogProject.Models.Domain
{
    public class BlogPostCommentDomain
    {
        public Guid Id { get; set; } 
        public string Description { get; set; }

        public Guid BlogPostId { get; set; }

        public Guid UserId { get; set; }


        public DateTime CreatedDate { get; set; }
    }
}
