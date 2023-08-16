using CrsSoftBlogProject.Models.Domain;
using CrsSoftBlogProject.Models.ViewModels;
using CrsSoftBlogProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrsSoftBlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikesRepository blogPostLikes;

        public BlogPostLikeController(IBlogPostLikesRepository blogPostLikes) 
        {
            this.blogPostLikes = blogPostLikes;
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequestViewModel addLikeRequest)
        {
            var model = new BlogPostLikeDomain
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UsersId = addLikeRequest.UsersId
            };

            await blogPostLikes.AddLikeForBlog(model);

            return Ok("Add");


        }
    }
}
