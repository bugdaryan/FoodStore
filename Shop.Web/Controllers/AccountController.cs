using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Login;
using System;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByNameAsync(login.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                {
                    _context.Update(user);
                    user.IsActive = true;
                    _context.SaveChanges();
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(login.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Username/Password not found");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = login.UserName, UserName = login.UserName, MemberSince = DateTime.Now };
                var result = await _userManager.CreateAsync(user, login.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            _context.Update(user);
            user.IsActive = false;
            _context.SaveChanges();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}