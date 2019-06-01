using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Data.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Data;
using Shop.Data.Models;
using Shop.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Shop.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("MSSqlConnection")));
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
			}
			else
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
			}

			services.AddIdentity<ApplicationUser, IdentityRole>(
			   options =>
			   {
				   options.Password.RequireDigit = false;
				   options.Password.RequiredLength = 6;
				   options.Password.RequireLowercase = false;
				   options.Password.RequireUppercase = false;
				   options.Password.RequireNonAlphanumeric = false;
			   }).AddEntityFrameworkStores<ApplicationDbContext>()
			    // .AddDefaultUI(Microsoft.AspNetCore.Identity.UI.UIFramework.Bsootstrap4)
			   .AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                    options.LogoutPath = new PathString("/Account/Logout");
                });

			services.AddTransient<ICategory, CategoryService>();
			services.AddTransient<IFood, FoodService>();
			services.AddTransient<IOrder, OrderService>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped(sp => ShoppingCart.GetCart(sp));

			services.AddMvc();
			services.AddMemoryCache();
			services.AddSession();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			DbInitializer.Seed(app);

			app.UseSession();
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();

			SeedRoles.CreateRoles(serviceProvider, Configuration).Wait();
		}
	}
}
