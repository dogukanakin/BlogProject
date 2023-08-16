namespace CrsSoftBlogProject.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }

        public ICollection<TagDomain> Tags { get; set; }
    }


}