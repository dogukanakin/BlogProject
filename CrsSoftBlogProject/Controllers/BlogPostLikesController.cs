using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrsSoftBlogProject.Repositories;
using CrsSoftBlogProject.Models.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CrsSoftBlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikesController : ControllerBase
    {
        private readonly IBlogPostLikesRepository blogPostLikes;
        private readonly ILogger<BlogPostLikesController> _logger;

        public BlogPostLikesController(IBlogPostLikesRepository blogPostLikes, ILogger<BlogPostLikesController> logger)
        {
            this.blogPostLikes = blogPostLikes;
            this._logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequestViewModel addLikeRequest)
        {
            try
            {
                var model = new BlogPostLikeDomain
                {
                    BlogPostId = addLikeRequest.BlogPostId,
                    UsersId = addLikeRequest.UsersId
                };

                await blogPostLikes.AddLikeForBlog(model);

                _logger.LogInformation("Like added for BlogPostId={BlogPostId}, UsersId={UsersId}", addLikeRequest.BlogPostId, addLikeRequest.UsersId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a like for BlogPostId={BlogPostId}, UsersId={UsersId}", addLikeRequest.BlogPostId, addLikeRequest.UsersId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding a like");
            }
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            try
            {
                var totalLikes = await blogPostLikes.GetTotalLikes(blogPostId);

                _logger.LogInformation("Total likes retrieved for BlogPostId={BlogPostId}: {TotalLikes}", blogPostId, totalLikes);

                return Ok(totalLikes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving total likes for BlogPostId={BlogPostId}", blogPostId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving total likes");
            }
        }
    }
}
