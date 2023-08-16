using CrsSoftBlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.ViewModels;
using Azure;
using CrsSoftBlogProject.Repositories;
using CrsSoftBlogProject.Models.Domain;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrsSoftBlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BloggieDbContext bloggieDbContext, ILogger<HomeController> logger)
        {
            this.bloggieDbContext = bloggieDbContext;
            this._logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Home/Index page visited.");
            var blogPosts = await bloggieDbContext.BlogPosts.ToListAsync();
            return View(blogPosts);
        }

        public async Task<IActionResult> News()
        {
            _logger.LogInformation("News page visited.");

            var blogPosts = await bloggieDbContext.BlogPosts.ToListAsync();
            return View(blogPosts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred. RequestId: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
