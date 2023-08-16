using CrsSoftBlogProject.Data;
using CrsSoftBlogProject.Models.Domain;
using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrsSoftBlogProject.Controllers
{
    public class ContactController : Controller
    {
        private BloggieDbContext bloggieDbContext;
        private ILogger<ContactController> _logger;

        public ContactController(BloggieDbContext bloggieDbContext, ILogger<ContactController> logger)
        {
            this.bloggieDbContext = bloggieDbContext;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ContactForm()
        {
            _logger.LogInformation("Contact page visited.");

            return View();
        }


        [HttpPost]
        [ActionName("ContactForm")]

        public async Task<IActionResult> ContactForm(AddContactFormViewModel addContactForm)
        {
            var contactForm = new ContactFormDomain
            {
                FirstName = addContactForm.FirstName,
                LastName = addContactForm.LastName,
                Email = addContactForm.Email,
                Subject = addContactForm.Subject
            };
            bloggieDbContext.ContactForm.Add(contactForm);
            await bloggieDbContext.SaveChangesAsync();
            _logger.LogInformation("Contact form submitted: FirstName={FirstName}, LastName={LastName}, Email={Email}, Subject={Subject}",
                  addContactForm.FirstName, addContactForm.LastName, addContactForm.Email, addContactForm.Subject);

            return View("ContactForm");
        }

    }
}
