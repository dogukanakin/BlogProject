using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrsSoftBlogProject.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext) 
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPostCommentDomain> AddAsync(BlogPostCommentDomain blogPostComment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostCommentDomain>> GetCommentByBLogIdAsync(Guid blogPostId)
        {
           return await bloggieDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
