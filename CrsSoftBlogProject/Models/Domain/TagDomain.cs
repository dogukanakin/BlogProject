namespace CrsSoftBlogProject.Models.Domain
{
    public class TagDomain
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogPostDomain> BlogPosts { get; set; }
    }
}
