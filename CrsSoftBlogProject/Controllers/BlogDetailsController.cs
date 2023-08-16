using CrsSoftBlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.ViewModels;
using CrsSoftBlogProject.Repositories;
using CrsSoftBlogProject.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrsSoftBlogProject.Controllers
{
    public class BlogDetailsController : Controller
    {
        private BloggieDbContext bloggieDbContext;
        private readonly IBlogPostLikesRepository blogPostLikesRepository;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<BlogDetailsController> _logger;

        public BlogDetailsController(BloggieDbContext bloggieDbContext,
            IBlogPostLikesRepository blogPostLikesRepository,
            IBlogPostCommentRepository blogPostCommentRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<BlogDetailsController> logger)
        {
            this.bloggieDbContext = bloggieDbContext;
            this.blogPostLikesRepository = blogPostLikesRepository;
            this.blogPostCommentRepository = blogPostCommentRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Technology()
        {
            _logger.LogInformation("Technology page visited.");

            string categoriesName = "Technology";
            var blogPosts = bloggieDbContext.BlogPosts
                .Where(b => b.CategoriesName == categoriesName)
                .ToList();

            return View(blogPosts);
        }

        [HttpGet]
        public IActionResult Phone()
        {
            _logger.LogInformation("Phone page visited.");

            string categoriesName = "Phone";
            var blogPosts = bloggieDbContext.BlogPosts
                .Where(b => b.CategoriesName == categoriesName)
                .ToList();

            return View(blogPosts);
        }

        [HttpGet]
        public IActionResult Computer()
        {
            _logger.LogInformation("Computer page visited.");

            string categoriesName = "Computer";
            var blogPosts = bloggieDbContext.BlogPosts
                .Where(b => b.CategoriesName == categoriesName)
                .ToList();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Blog(Guid id)
        {
            var liked = false;

            var blogPost = bloggieDbContext.BlogPosts.SingleOrDefault(t => t.Id == id);
            var editBlogPost = new EditBlogPostViewModel();

            if (blogPost != null)
            {
                var totalLikes = await blogPostLikesRepository.GetTotalLikes(blogPost.Id);
                var blogPostCommentDomainModel = await blogPostCommentRepository.GetCommentByBLogIdAsync(blogPost.Id);
                var blogCommentsForView = new List<BlogPostCommentDomain>();

                foreach (var blogPostComment in blogPostCommentDomainModel)
                {
                    blogCommentsForView.Add(new BlogPostCommentDomain
                    {
                        Description = blogPostComment.Description,
                        CreatedDate = blogPostComment.CreatedDate,
                        UserId = blogPostComment.UserId,
                    });
                }

                editBlogPost = new EditBlogPostViewModel
                {
                    Id = blogPost.Id,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    SelectedTag = blogPost.SelectedTag,
                    PageContent = blogPost.PageContent,
                    Description = blogPost.Description,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    CategoriesName = blogPost.CategoriesName,
                    Liked = liked,
                    Comments = blogCommentsForView
                };

                _logger.LogInformation("Blog post viewed: Id={Id}, Title={Title}", blogPost.Id, blogPost.PageTitle);

                return View(editBlogPost);
            }
            else
            {
                _logger.LogWarning("Blog post not found: Id={Id}", id);
                return RedirectToAction("Blog", "BlogDetails");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Blog(EditBlogPostViewModel editBlogPost)
        {
            try
            {
                if (blogPostCommentRepository != null)
                {
                    var domainModel = new BlogPostCommentDomain
                    {
                        BlogPostId = editBlogPost.Id,
                        Description = editBlogPost.CommentDescription,
                        UserId = editBlogPost.Id,
                        CreatedDate = DateTime.Now,
                        //UserId = (await userManager.GetUserIdAsync(userManager.GetUserId(User.Identity.Name))).Id,
                    };

                    await blogPostCommentRepository.AddAsync(domainModel);

                    _logger.LogInformation("Comment added to blog post: BlogId={BlogId}, Comment={Comment}",
                        editBlogPost.Id, editBlogPost.CommentDescription);

                    return RedirectToAction("Blog", "BlogDetails", new { urlHandle = editBlogPost.UrlHandle });
                }

                _logger.LogWarning("BlogPostCommentRepository is null.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a comment to a blog post.");
                return View("Error");
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteComment(EditBlogPostViewModel editBlogPostView)
        {
            try
            {
                var comment = bloggieDbContext.BlogPostComment.Find(editBlogPostView.Id);
                if (comment != null)
                {
                    bloggieDbContext.BlogPostComment.Remove(comment);
                    bloggieDbContext.SaveChanges();

                    return RedirectToAction("List");


                }

                //_logger.LogInformation("Comment deleted: CommentId={CommentId}", commentId);

                // Return a success response
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting a comment.");
                return BadRequest("An error occurred while deleting the comment.");
            }
        }

    }
}
