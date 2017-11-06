using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
    public class UserSettingsModel : PageModel
    {
        private readonly Web.Context.IUserRepository context;

        public UserSettingsModel(Web.Context.IUserRepository context)
        {
            this.context = context;
        }

        public Web.Models.User User { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var Userlist = await context.GetUser(HttpContext, id);

			if (Userlist == null || Userlist.Count == 0)
			{
				return NotFound();
			}
			User = Userlist[0];
			return Page();
		}
	}
}
