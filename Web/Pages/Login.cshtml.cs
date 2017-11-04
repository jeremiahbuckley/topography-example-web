using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Web.Context.ILoginRepository context;

        public LoginModel(Web.Context.ILoginRepository context)
        {
            this.context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Login Login { get; set; }

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

			return RedirectToPage("./Threads");
		}
	}
}