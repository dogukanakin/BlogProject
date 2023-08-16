using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


public class UserListController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly AuthDbContext authDbContext;
    private readonly RoleManager<IdentityRole> roleManager;
    private ILogger<UserListController> _logger;
    private IMemoryCache _memoryCache;

    public UserListController(UserManager<IdentityUser> userManager, AuthDbContext dbContext, RoleManager<IdentityRole> roleManager, ILogger<UserListController> logger, IMemoryCache memoryCache)
    {
        this.userManager = userManager;
        this.authDbContext = dbContext;
        this.roleManager = roleManager;
        this._logger = logger;
        _memoryCache = memoryCache;

    }


    [HttpGet]
    public async Task<IActionResult> Users()
    {
        try
        {
            _logger.LogInformation("User list viewed.");

            var cachedUserList = await _memoryCache.GetOrCreateAsync("UserList", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30); 
                return await LoadUsersFromDatabase();
            });

            ViewBag.UserManager = userManager;

            return View(cachedUserList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the user list.");
            return View("Error");
        }
    }

    private async Task<List<IdentityUser>> LoadUsersFromDatabase()
    {
        return await authDbContext.Users.FromSqlRaw("EXEC GetUsers").ToListAsync();
    }

    [HttpPost]
    // create controller for edting user

    [HttpPost]
    public async Task<IActionResult> DeleteUsers(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var userRoles = authDbContext.UserRoles.Where(ur => ur.UserId == userId).ToList();
                authDbContext.UserRoles.RemoveRange(userRoles);
                await authDbContext.SaveChangesAsync();

                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Users");
            }
        }
        return NotFound();
    }


    [HttpGet]
    public IActionResult EditUser(Guid userID) 
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(string userId, string email, string password)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.Email = email;
            user.UserName = email;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Users");
            }
        }
        return NotFound();
    }


}
