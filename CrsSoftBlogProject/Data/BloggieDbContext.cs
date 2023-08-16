using CrsSoftBlogProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrsSoftBlogProject.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPostDomain> BlogPosts { get; set; }
        public DbSet<TagDomain> Tags { get; set; }
        public DbSet<ContactFormDomain> ContactForm { get; set; }
        public DbSet<CategoriesDomain> Categories { get; set; }
        public DbSet<BlogPostLikeDomain> BlogPostLike { get; set;}

        public DbSet<BlogPostCommentDomain> BlogPostComment { get; set; }


    }
}
