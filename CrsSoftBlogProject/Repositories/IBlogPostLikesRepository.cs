using CrsSoftBlogProject.Models.Domain;
namespace CrsSoftBlogProject.Repositories
{
    public interface IBlogPostLikesRepository
    {
        Task <int> GetTotalLikes (Guid blogPostId);

        Task<IEnumerable<BlogPostLikeDomain>> GetLikesForBlog(Guid blogPostId);

        Task<BlogPostLikeDomain> AddLikeForBlog (BlogPostLikeDomain blogPostLike);
    }
}
