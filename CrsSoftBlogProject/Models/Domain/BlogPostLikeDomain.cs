namespace CrsSoftBlogProject.Models.Domain
{
    public class BlogPostLikeDomain
    {
        public Guid Id { get; set; }

        public Guid BlogPostId { get; set; }

        public Guid UsersId { get; set; }
    }
}
