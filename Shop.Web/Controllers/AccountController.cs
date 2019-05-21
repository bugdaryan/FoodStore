using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Models;
using Shop.Web.DataMapper;
using Shop.Web.Models.Account;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Mapper _mapper;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrder _orderService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ShoppingCart shoppingCart, IOrder orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = new Mapper();
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var model = _mapper.ApplicationUserToAccountProfileModel(user, _orderService);
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
                var user = _mapper.AccountRegisterModelToApplicationUser(register);
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    var loginModel = new AccountLoginModel
                    {
                        Email = register.Email,
                        Password = register.Password,
                    };
                    return RedirectToAction("Login", new { login = loginModel });
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
            _shoppingCart.ClearCart();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var model = _mapper.ApplicationUserToAccountProfileModel(user, _orderService);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveProfile(AccountProfileModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            _mapper.AccountProfileModelToApplicationUser(model, user);
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Profile");
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
    }
}