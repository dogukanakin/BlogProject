using Microsoft.AspNetCore.Mvc;

namespace CrsSoftBlogProject.Controllers
{
    public class RoleBasedController : Controller
    {
        public IActionResult UserRolesList()
        {
            return View();
        }
    }
}
