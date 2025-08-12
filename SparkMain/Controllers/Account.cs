using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SparkMain.Data;
using SparkMain.Models;
using SparkMain.ViewModels;

namespace SparkMain.Controllers
{
    public class Account : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public Account(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager) 
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName!, model.Password!, model.RememberMe!,false);
                if (result.Succeeded) 
                { 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);

            }
            return View(model);

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid) 
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Name,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,true);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


    }
}
