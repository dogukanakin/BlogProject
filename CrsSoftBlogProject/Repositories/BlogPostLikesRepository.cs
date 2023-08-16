using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CrsSoftBlogProject.Repositories
{
    public class BlogPostLikesRepository : IBlogPostLikesRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikesRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLikeDomain> AddLikeForBlog(BlogPostLikeDomain blogPostLike)
        {
            await bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public Task<BlogPostLikeDomain> AddLikeForBlog(BlogPostDomain blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostLikeDomain> AddLikeForBlog(BlogPostLikesRepository blogPostLikesRepository)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPostLikeDomain>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike
                .CountAsync(x => x.BlogPostId == blogPostId);
        }

    }
}
