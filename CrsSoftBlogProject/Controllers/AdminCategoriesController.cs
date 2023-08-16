using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.Domain;
using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace CrsSoftBlogProject.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        private readonly ILogger<AdminCategoriesController> _logger;

        public AdminCategoriesController(BloggieDbContext bloggieDbContext, ILogger<AdminCategoriesController> logger)
        {
            this.bloggieDbContext = bloggieDbContext;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult AddCategories()
        {
            _logger.LogInformation("Add Categories page visited.");
            return View();
        }

        [HttpPost]
        [ActionName("AddCategories")]
        public IActionResult AddCategories(AddCategoriesViewModel addCategoriesViewModel)
        {
            try
            {
                _logger.LogInformation("Category added: {CategoriesName}", addCategoriesViewModel.CategoriesName);

                var categoryName = new CategoriesDomain
                {
                    CategoriesName = addCategoriesViewModel.CategoriesName
                };

                bloggieDbContext.Categories.Add(categoryName);
                bloggieDbContext.SaveChanges();

                _logger.LogInformation("Category added: CategoriesName={CategoriesName}", addCategoriesViewModel.CategoriesName);
                return View("AddCategories");
            }
             catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding a category.");
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult CategoriesList()
        {
            _logger.LogInformation("Categories list viewed.");

            var categoriesLists = bloggieDbContext.Categories.FromSqlRaw("EXEC GetCategories").ToList();
            return View(categoriesLists);
        }



        [HttpGet]
        public IActionResult CategoriesEdit(Guid id)
        {
            var categoriesList = bloggieDbContext.Categories.SingleOrDefault(t => t.Id == id);

            if (categoriesList != null)
            {
                var editCategories = new EditCategoriesViewModel
                {
                    Id = categoriesList.Id,
                    CategoriesName = categoriesList.CategoriesName
                };
                return View(editCategories);
            }
            return RedirectToAction("CategoriesEdit", "AdminCategories");
        }

        [HttpPost]
        public IActionResult CategoriesEdit(EditCategoriesViewModel editCategories)
        {
            if (ModelState.IsValid)
            {
                var categoriesList = bloggieDbContext.Categories.SingleOrDefault(x => x.Id == editCategories.Id);

                if (categoriesList != null)
                {
                    var parameters = new[]
                    {
                new SqlParameter("@CategoryId", categoriesList.Id),
                new SqlParameter("@NewCategoryName", editCategories.CategoriesName)
            };

                    int rowsAffected = bloggieDbContext.Database.ExecuteSqlRaw("EXEC UpdateCategory @CategoryId, @NewCategoryName", parameters);

                    if (rowsAffected == 1)
                    {
                        _logger.LogInformation("Category edited. Id: {CategoriesName}, NewName: {NewName}", editCategories.CategoriesName, editCategories.CategoriesName);
                        return RedirectToAction("CategoriesEdit", "AdminCategories");
                    }
                    else
                    {
                        _logger.LogWarning("Category edit failed. Category not found. Id: {CategoryId}", editCategories.Id);
                        return RedirectToAction("CategoriesEdit", "AdminCategories");
                    }
                }
                else
                {
                    _logger.LogWarning("Category edit failed. Category not found. Id: {CategoryId}", editCategories.Id);
                    return RedirectToAction("CategoriesEdit", "AdminCategories");
                }
            }
            return View(editCategories);
        }

        [HttpPost]
        public IActionResult DeleteCategory(EditCategoriesViewModel editCategories)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@CategoryId", editCategories.Id)
                  };

                int rowsAffected = bloggieDbContext.Database.ExecuteSqlRaw("EXEC DeleteCategory @CategoryId", parameters);

                if (rowsAffected == 1)
                {
                    _logger.LogInformation("Category deleted. Id: {CategoryId}", editCategories.Id);
                    return RedirectToAction("CategoriesList");
                }
                else
                {
                    _logger.LogWarning("Category delete failed. Category not found. Id: {CategoryId}", editCategories.Id);
                    return RedirectToAction("CategoriesEdit", new { id = editCategories.Id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting category. Id: {CategoryId}", editCategories.Id);
                return View("Error");
            }
        }

    }
}
