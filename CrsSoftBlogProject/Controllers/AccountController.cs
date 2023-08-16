using CrsSoftBlogProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;


namespace CrsSoftBlogProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._logger = logger;
        }


        [HttpGet]
        public async Task <IActionResult> Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {

                _logger.LogInformation("Attempting to register user: {Username}", loginViewModel.Username);
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password, false, false);

            if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
            {
                _logger.LogWarning("User logged in: {Username}", loginViewModel.Username);

                return Redirect(loginViewModel.ReturnUrl);
            }
            else if (signInResult.Succeeded)
            {
                _logger.LogInformation("User logged in: {Username}", loginViewModel.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Username or password is incorrect.");
                _logger.LogWarning("Login failed for user: {Username}", loginViewModel.Username);
                return View();
            }
        }

        [HttpGet]
        public async Task <IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Attempting to register user: {Username}", registerViewModel.Username);

                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    _logger.LogInformation("User registered successfully: {Username}", registerViewModel.Username);

                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                    if (roleIdentityResult.Succeeded)
                    {
                        _logger.LogInformation("User added to 'User' role: {Username}", registerViewModel.Username);
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    _logger.LogWarning("Registration failed for user: {Username}. Errors: {Errors}",
                        registerViewModel.Username, string.Join(", ", identityResult.Errors));
                }
            }
            else
            {
                _logger.LogWarning("Invalid registration data.");
                ModelState.AddModelError("", "Please fill in all required fields and follow the specified format.");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;

            await signInManager.SignOutAsync();

            if (!string.IsNullOrEmpty(username))
            {
                _logger.LogInformation("User logged out: {Username}", username);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}