using Hospital.Data.Entities;
using Hospital.WebProject.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebProject.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                Email = model.Email,
                UserName = model.FirstName + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.Role == "Doctor" || model.Role == "Nurse" || model.Role == "Patient" || model.Role=="Admin")
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                }
                return RedirectToAction("Login", "User");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await this.signManager.PasswordSignInAsync(user, model.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        //[AllowAnonymous]
        //public async Task<IActionResult> SeedRoles()
        //{
        //    string[] roles = { "Admin", "Doctor", "Nurse", "Patient" };
        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        //        }
        //    }
        //    return Content("Roles seeded (created if missing).");
        //}
    }
}
