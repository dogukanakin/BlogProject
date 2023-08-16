using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.Domain;
using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CrsSoftBlogProject.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggieDbContext bloggieDbContext;
        private ILogger<AdminTagsController> _logger;

        public AdminTagsController(BloggieDbContext bloggieDbContext, ILogger<AdminTagsController> logger)
        {
            this.bloggieDbContext = bloggieDbContext;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogInformation("Add Tag page visited.");
            return View();
        }

        [HttpPost]
        [ActionName("Add")]

        public IActionResult Add(AddTagRequestViewModel addTagRequest)
        {
            try
            {
                var tag = new TagDomain
                {
                    Name = addTagRequest.Name,
                    DisplayName = addTagRequest.DisplayName
                };

                bloggieDbContext.Tags.Add(tag);
                bloggieDbContext.SaveChanges();
                _logger.LogInformation("Tag added: Name={Name}, DisplayName={DisplayName}", addTagRequest.Name, addTagRequest.DisplayName);


                return View("Add");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while adding a tag.");
                return View("Error");
            }
          
        }

        [HttpGet]
        public IActionResult List()
        {
            _logger.LogInformation("Tags list viewed.");

            var tags = bloggieDbContext.Tags.FromSqlRaw("EXEC GetTags").ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = bloggieDbContext.Tags.SingleOrDefault(t => t.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequestViewModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return RedirectToAction("List", "AdminTags");
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequestViewModel editTagRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parameters = new[]
                    {
                new SqlParameter("@TagId", editTagRequest.Id),
                new SqlParameter("@NewTagName", editTagRequest.Name),
                new SqlParameter("@NewTagDisplayName", editTagRequest.DisplayName)
            };

                    int rowsAffected = bloggieDbContext.Database.ExecuteSqlRaw("EXEC EditTag @TagId, @NewTagName, @NewTagDisplayName", parameters);

                    if (rowsAffected == 1)
                    {
                        _logger.LogInformation("Tag edited: Id={Id}, NewName={NewName}",
                            editTagRequest.Id, editTagRequest.Name);

                        return RedirectToAction("List", "AdminTags");
                    }
                    else
                    {
                        return RedirectToAction("List", "AdminTags");
                    }
                }

                return View(editTagRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while editing a tag.");
                return View("Error");
            }
        }


        [HttpPost]
        public IActionResult Delete(EditTagRequestViewModel editTagRequest)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@TagId", editTagRequest.Id)
        };

                int rowsAffected = bloggieDbContext.Database.ExecuteSqlRaw("EXEC DeleteTag @TagId", parameters);

                if (rowsAffected == 1)
                {
                    _logger.LogInformation("Tag deleted: Id={Id}", editTagRequest.Id);
                    return RedirectToAction("List");
                }
                else
                {
                    _logger.LogWarning("Tag delete failed. Tag not found. Id: {TagId}", editTagRequest.Id);
                    return RedirectToAction("Edit", new { id = editTagRequest.Id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting a tag. Id: {TagId}", editTagRequest.Id);
                return View("Error");
            }
        }

    }
}
