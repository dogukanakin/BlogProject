using CrsSoftBlogProject.Models.Domain;

namespace CrsSoftBlogProject.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostCommentDomain> AddAsync(BlogPostCommentDomain blogPostComment);

        Task<IEnumerable<BlogPostCommentDomain>> GetCommentByBLogIdAsync(Guid blogPostId);

    }
    
}
