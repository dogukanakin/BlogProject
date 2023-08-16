using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.Domain;
using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace CrsSoftBlogProject.Controllers
{
    public class BlogPostController : Controller
    {

        private BloggieDbContext bloggieDbContext;
        private ILogger<UserListController> _logger;
        private IMemoryCache _memoryCache;

        public BlogPostController(BloggieDbContext bloggieDbContext, ILogger<UserListController> logger, IMemoryCache memoryCache)
        {
            this.bloggieDbContext = bloggieDbContext;
            this._logger = logger;
            _memoryCache = memoryCache;

        }

        [HttpGet]
        public IActionResult BlogList()
        {
            try
            {
                string author = User.Identity.Name;

                var blogPosts = bloggieDbContext.BlogPosts
                .FromSqlRaw("EXEC GetBlogPostsByAuthor @Author", new SqlParameter("@Author", author))
                .ToList();

                _logger.LogInformation("Blog list viewed.");

                return View(blogPosts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the blog list.");
                return View("Error");
            }
        }


        [HttpGet]
        public IActionResult CreatePost()
        {
            try
            {
                var tags = bloggieDbContext.Tags.ToList();
                var categories = bloggieDbContext.Categories.ToList();

                ViewBag.Tags = tags;
                ViewBag.Categories = categories;

                _logger.LogInformation("Create post page accessed.");

                return View(new AddBlogPostViewModel { Tags = tags, Categories = categories });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing create post page.");
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("CreatePost")]
        public IActionResult CreatePost(AddBlogPostViewModel addBlogPost)
        {
            try
            {
                var blogPost = new BlogPostDomain
                {
                    PageTitle = addBlogPost.PageTitle,
                    PageContent = addBlogPost.PageContent,
                    Description = addBlogPost.Description,
                    Author = addBlogPost.Author,
                    FeaturedImageUrl = addBlogPost.FeaturedImageUrl,
                    SelectedTag = addBlogPost.SelectedTag,
                    CategoriesName = addBlogPost.CategoriesName,
                    FeaturedImageUpload = addBlogPost.FeaturedImageUpload
                };
                bloggieDbContext.BlogPosts.Add(blogPost);
                bloggieDbContext.SaveChanges();

                _logger.LogInformation("New blog post created: PageTitle={PageTitle}, Author={Author}", addBlogPost.PageTitle, addBlogPost.Author);

                return RedirectToAction("CreatePost");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new blog post.");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult EditPost(Guid id)
        {
            try
            {
                var blogPost = bloggieDbContext.BlogPosts.SingleOrDefault(x => x.Id == id);

                if (blogPost != null)
                {
                    var editBlogPost = new EditBlogPostViewModel
                    {
                        Id = blogPost.Id,
                        PageTitle = blogPost.PageTitle,
                        PageContent = blogPost.PageContent,
                        Description = blogPost.Description,
                        FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    };
                    _logger.LogInformation("Edit blog post page accessed: BlogPostId={BlogPostId}", id);
                    return View(editBlogPost);
                }
                return RedirectToAction("BlogList", "BlogPost");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing the edit blog post page.");
                return View("Error");
            }
        }



        [HttpPost]
        public IActionResult EditPost(EditBlogPostViewModel editBlogPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parameters = new[]
                    {
                new SqlParameter("@BlogPostID", editBlogPost.Id),
                new SqlParameter("@NewPageTitle", editBlogPost.PageTitle),
                new SqlParameter("@NewPageContent", editBlogPost.PageContent),
                new SqlParameter("@NewDescription", editBlogPost.Description),
                new SqlParameter("@NewFeaturedImageUrl", editBlogPost.FeaturedImageUrl)
            };

                    int rowsAffected = bloggieDbContext.Database.ExecuteSqlRaw("EXEC UpdateBlogPost @BlogPostID, @NewPageTitle, @NewPageContent, @NewDescription, @NewFeaturedImageUrl", parameters);

                    if (rowsAffected == 1)
                    {
                        _logger.LogInformation("Blog post edited: Id={Id}, NewTitle={NewTitle}",
                            editBlogPost.Id, editBlogPost.PageTitle);

                        return RedirectToAction("BlogList", "BlogPost");
                    }
                    else
                    {
                        return RedirectToAction("BlogList", "BlogPost");
                    }
                }

                return View(editBlogPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while editing a blog post.");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult DeletePost(EditBlogPostViewModel editBlogPost)
        {
            try
            {
                var blogPost = bloggieDbContext.BlogPosts.Find(editBlogPost.Id);
                if (blogPost != null)
                {
                    bloggieDbContext.BlogPosts.Remove(blogPost);
                    bloggieDbContext.SaveChanges();

                    _logger.LogInformation("Blog post deleted: BlogPostId={BlogPostId}", editBlogPost.Id);

                    return RedirectToAction("BlogList");
                }

                return View("EditPost", new { id = editBlogPost.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the blog post.");
                return View("Error");
            }
        }
    }
}