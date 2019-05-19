using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Account;
using System;
using System.Linq;
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
            var model = new AccountLoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);

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
            ModelState.AddModelError("IncorrectInput", "Username or Password is incorrect");
            return View(login);
        }

        public IActionResult Register(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new AccountRegisterModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterModel register)
        {
            var u = await _userManager.FindByEmailAsync(register.Email ?? "");
            if (u != null)
            {
                ModelState.AddModelError("Email", "Email is already taken");
            }
            else if (ModelState.IsValid)
            {
                var user = BuildUser(register);
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(register);
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

        public IActionResult Cancel(string returnUrl="/")
        {
            returnUrl = returnUrl.Replace("%2F", "/");
            return Redirect(returnUrl);
        }

        private ApplicationUser BuildUser(AccountRegisterModel login)
        {
            return new ApplicationUser
            {
                FirstName = login.FirstName,
                AddressLine1 = login.AddressLine1,
                AddressLine2 = login.AddressLine2,
                City = login.City,
                Country = login.Country,
                Email = login.Email,
                ImageUrl = login.ImageUrl,
                MemberSince = DateTime.Now,
                Balance = 0,
                LastName = login.LastName,
                UserName = login.Email,
                Orders = Enumerable.Empty<Order>(),
                PhoneNumber = login.PhoneNumber
            };
        }
    }
}