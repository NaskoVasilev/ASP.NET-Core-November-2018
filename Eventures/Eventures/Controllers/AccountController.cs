using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Models;
using Eventures.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Eventures.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            List<AuthenticationScheme> ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            LoginViewModel model = new LoginViewModel() { ExternalLogins = ExternalLogins };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                List<AuthenticationScheme> ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                model.ExternalLogins = model.ExternalLogins;
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password!");
                List<AuthenticationScheme> ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                model.ExternalLogins = model.ExternalLogins;
                return View(model);
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UniqueCitizenNumber = model.UniqueCitizenNumber,
                Email = model.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = await userManager.AddToRoleAsync(user, "User");
                if (roleResult.Errors.Any())
                {
                    return View(model);
                }

                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}