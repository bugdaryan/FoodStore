using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Data;
using Shop.Data.Enums;
using Shop.Data.Models;
using Shop.Web.DataMapper;
using Shop.Web.Models.Order;

namespace Shop.Web.Pages
{
    public class ArchiveModel : PageModel
    {
        private readonly IOrder _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Mapper _mapper;

        [BindProperty(SupportsGet = true)]
        public OrderArchiveModel Order { get; set; }

        // [BindProperty(SupportsGet = true)]
        // public int PageNumber { get; set; }

        public ArchiveModel(IOrder orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _mapper = new Mapper();
        }

        public async Task<IActionResult> OnGetAsync(int? pageNumber)
        {
            ApplicationUser user;
            if (!string.IsNullOrEmpty(Order.UserId) && User.IsInRole("Admin"))
            {
                user = await _userManager.FindByIdAsync(Order.UserId);
            }
            else
            {
                user = await _userManager.GetUserAsync(User);
            }

            if(!pageNumber.HasValue)
            {
                pageNumber= 1;
            }

            int orderInPage = 5;
            int count = _orderService.GetByUserId(user.Id).Count();
            int pageCount = (int)Math.Ceiling(count / (double)orderInPage);
            var orders = _orderService.GetFilteredOrders(user.Id, OrderBy.None, (pageNumber.Value - 1) * orderInPage, orderInPage).ToList();
            var models = _mapper.OrdersToOrderIndexModels(orders);

            if(Order.PageCount <= 0)
            {
                Order=(OrderArchiveModel)ViewData["Order"];
                Order = new OrderArchiveModel
                {
                    Orders = models,
                    Page = pageNumber.Value,
                    PageCount = pageCount,
                    UserId = user.Id,
                };
            }
            else
            {
                Order.Orders = models;
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Order.ZipCode = "2222";
            ViewData["Order"]= Order;

            return RedirectToPage("Archive", new {pageNumber = 1});
        }
    }
}