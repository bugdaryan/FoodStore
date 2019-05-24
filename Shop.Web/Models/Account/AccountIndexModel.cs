using System.Collections.Generic;

namespace Shop.Web.Models.Account
{
	public class AccountIndexModel
	{
		public IEnumerable<AccountProfileModel> Accounts { get; set; }
		public string SearchQuery { get; set; }
	}
}