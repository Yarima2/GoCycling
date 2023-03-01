using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoCycling.Pages
{
    public class IndexModel : PageModel
    {

		public async void OnGet(string code)
		{
			var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;

			if (claimsIdentity != null)
			{
				var c = claimsIdentity.FindFirst("Id");

				if (c != null)
				{
					string test = c.Value;
				}
			}
		}
	}
}
