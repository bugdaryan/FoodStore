using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Data;
using Shop.Data.Extensions;
using Shop.Data.Models;
using Shop.Web.DataMapper;
using Shop.Web.Models.Account;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Mapper _mapper;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrder _orderService;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ShoppingCart shoppingCart, IOrder orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = new Mapper();
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ApplicationUser> users = _userManager.Users;
            var models = await _mapper.ApplicationUsersToAccountProfileModelsAsync(users, _orderService, _userManager);

            var model = new AccountIndexModel
            {
                Accounts = models
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if(string.IsNullOrWhiteSpace(searchQuery))
            {
                return RedirectToAction("Index");
            }

            var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower().Split(" ");
            var models = await _mapper.ApplicationUsersToAccountProfileModelsAsync(_userManager.Users.Where(user => queries.Any(query => (user.FirstName + " "+ user.LastName + " "+ user.Email).ToLower().Contains(query))),_orderService,_userManager);
            
            var model = new AccountIndexModel
            {
                Accounts = models,
                SearchQuery = searchQuery
            };

            return View("Index",model);
        } 

        [Authorize]
        public async Task<IActionResult> Profile(string userId)
        {
            ApplicationUser user;
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("Admin"))
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            else
            {
                user = await _userManager.GetUserAsync(User);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (user != null)
            {
                var model = _mapper.ApplicationUserToAccountProfileModel(user, _orderService, roles.FirstOrDefault());
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminProfile(string userId)
        {
            return RedirectToAction("Profile", new { userId });
        }

        public IActionResult Login(string returnUrl = "/")
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
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

            if (_signInManager.IsSignedIn(User) && !User.IsInRole("Admin"))
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
                    await _userManager.AddToRoleAsync(user, "Customer");
                    if(!_signInManager.IsSignedIn(User) )
                    {
                        await _signInManager.PasswordSignInAsync(user, register.Password, false,false);
                    }
                    if(!string.IsNullOrEmpty(register.ReturnUrl))
                    {
                        return Redirect(register.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(register);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if(_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
                _shoppingCart.ClearCart();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Settings(string userId)
        {
            ApplicationUser user;
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("Admin"))
            {
                user = await _userManager.FindByIdAsync(userId);
                GetRoles();
            }
            else
            {
                user = await _userManager.GetUserAsync(User);
            }

            if (user != null)
            {
                string roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                string roleId = _roleManager.Roles.FirstOrDefault(role => role.Name == roleName).Id;
                var model = _mapper.ApplicationUserToAccountSettingsModel(user, roleId);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        private void GetRoles()
        {
            var roles = _roleManager.Roles.Select(role => new RoleDropdownModel
            {
                Id = role.Id,
                Name = role.Name
            });
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveProfile(AccountSettingsModel model)
        {
            GetRoles();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                _mapper.AccountSettingsModelToApplicationUser(model, user);

                if (!string.IsNullOrEmpty(model.OldPassword)
                    || !string.IsNullOrEmpty(model.NewPassword)
                    || !string.IsNullOrEmpty(model.NewPasswordConfirmation))
                {
                    if (string.IsNullOrEmpty(model.OldPassword))
                    {
                        ModelState.AddModelError("OldPassword", "Enter your current password to change it");
                        return View("Settings", model);
                    }
                    if (string.IsNullOrEmpty(model.NewPassword))
                    {
                        ModelState.AddModelError("NewPassword", "Enter your new password to change it");
                        return View("Settings", model);
                    }

                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword ?? "", model.NewPassword ?? "");
                    if (!result.Succeeded)
                    { 
                        ModelState.AddModelError("OldPassword", "Incorrect password, please enter your current password to change it");
                        return View("Settings", model);
                    }
                }

                var role = _roleManager.Roles.First(r => r.Id == model.RoleId).Name;
                var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                if(role != userRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                    await _userManager.AddToRoleAsync(user, role);
                }


                await _userManager.UpdateAsync(user);

                await HttpContext.RefreshLoginAsync();

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Profile", new { userId = user.Id });
                }
                return RedirectToAction("Profile");
            }
            return View("Settings", model);
        }

        public async Task<IActionResult> Deactivate(string userId)
        {
            ApplicationUser user;
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("Admin"))
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            else
            {
                user = await _userManager.GetUserAsync(User);
            }

            if(user!=null)
            {
                if(string.IsNullOrEmpty(userId) || !User.IsInRole("Admin"))
                {
                    await _signInManager.SignOutAsync();
                    _shoppingCart.ClearCart();
                }
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index", "Home");
            }
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