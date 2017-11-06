﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;

namespace Web.Pages.Access
{
	//public class Login
	//{
	//	public int Id { get; set; }
	//	public string UserName { get; set; }
	//	public string Password { get; set; }
	//}

	//public class AuthResponse
	//{
	//	public string UserName { get; set; }
	//	public string AuthToken { get; set; }
	//}
	public class IndexModel : PageModel
	{
		private readonly Web.Context.ILoginRepository context;

		public IndexModel(Web.Context.ILoginRepository context)
		{
			this.context = context;
		}

		public IActionResult OnGet()
		{
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
				//await Task.Run(() => { int x = 0; });
				var authTokens = await context.PostLogin(Login);
				this.HttpContext.Response.Cookies.Append("username", authTokens.UserName);
				this.HttpContext.Response.Cookies.Append("authtoken", authTokens.AuthToken);

			}
			catch (Exception ex)
			{
				return Page();
			}

			return RedirectToPage("../Index");
		}
	}
}