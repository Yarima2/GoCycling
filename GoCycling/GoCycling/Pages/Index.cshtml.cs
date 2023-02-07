using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoCycling.Pages
{
    public class IndexModel : PageModel
    {
		public bool hasCode { get; set; }
		public string code { get; set; }
		public void OnGet(string code)
		{
			hasCode = code != null;
			Console.WriteLine(code);
			this.code = code;
		}
	}
}
