using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Models;

namespace Shop.Data.Extensions
{
	public static class HttpContextExtensions
	{
		public static async Task RefreshLoginAsync(this HttpContext context)
		{
			if (context.User == null)
				return;

			var userManager = context.RequestServices
				.GetRequiredService<UserManager<ApplicationUser>>();
			var signInManager = context.RequestServices
				.GetRequiredService<SignInManager<ApplicationUser>>();

			ApplicationUser user = await userManager.GetUserAsync(context.User);

			if(signInManager.IsSignedIn(context.User))
			{
				await signInManager.RefreshSignInAsync(user);
			}
		}
	}
}