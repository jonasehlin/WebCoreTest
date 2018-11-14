using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebCoreTest.Pages
{
	public class IndexModel : PageModel
	{
		public string Text { get; set; }

		public void OnGet()
		{
			Text = "Hello world!";
		}
	}
}