using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages.Login
{
	public class IndexModel : PageModel
	{
		private readonly Web.Context.ILoginRepository context;

		//public IndexModel(Web.Context.ILoginRepository context)
		//{
		//	this.context = context;
		//}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnGetAsync()
		{
			Login = await Task.Run(() => new Web.Models.Login());
			return Page();
		}

		[BindProperty]
		public Web.Models.Login Login { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			try
			{
				var authTokens = await context.PostLogin(Login);
				this.HttpContext.Response.Cookies.Append("username", authTokens.UserName);
				this.HttpContext.Response.Cookies.Append("authtoken", authTokens.AuthToken);

			}
			catch (Exception ex)
			{
				return Page();
			}

			return RedirectToPage("./Index");
		}
	}
}