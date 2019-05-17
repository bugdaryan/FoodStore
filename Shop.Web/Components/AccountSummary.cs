using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Models;
using Shop.Web.Models.Account;
using Shop.Web.Models.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Components
{
	public class AccountSummary : ViewComponent
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountSummary(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> Invoke()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if(user != null)
			{
				var model = new AccountSummaryModel
				{
					ImageUrl = user.ImageUrl,
					Name = user.UserName
				};

				return View(model);
			}
			return View();
		}
	}
}
