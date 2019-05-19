using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.Models.Account;
using Shop.Web.Models.Order;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOrder _orderService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOrder orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var model = BuildProfileModel(user);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string returnUrl = "/")
        {
            returnUrl = returnUrl.Replace("%2F", "/");

            var model = new AccountLoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public IActionResult Register(string returnUrl = "/")
        {
            returnUrl = returnUrl.Replace("%2F", "/");

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Cancel(string returnUrl = "/")
        {
            returnUrl = returnUrl.Replace("%2F", "/");

            return Redirect(returnUrl);
        }

        private ApplicationUser AccountRegisterModelToApplicationUser(AccountRegisterModel login)
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
                PhoneNumber = login.PhoneNumber,
            };
        }

        private AccountProfileModel ApplicationUserAccountProfileModel(ApplicationUser user)
        {
            return new AccountProfileModel
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                Balance = user.Balance,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                ImageUrl = user.ImageUrl,
                LastName = user.LastName,
                MemberSince = user.MemberSince,
                PhoneNumber = user.PhoneNumber,
                Orders = Enumerable.Empty<OrderIndexModel>(),

            }; 
        }
    }
}