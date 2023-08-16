namespace CrsSoftBlogProject.Models.Domain
{
    public class CategoriesDomain
    {
        public Guid Id { get; set; }
        public string CategoriesName { get; set; }

        public ICollection<BlogPostDomain> BlogPosts { get; set; }

    }
}
