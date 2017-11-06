using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Thread
{
    public class DetailsModel : PageModel
    {
		private readonly Web.Context.IThreadRepository context;

		public DetailsModel(Web.Context.IThreadRepository context)
		{
			this.context = context;
		}


		public Web.Models.Thread Thread { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var Threadlist = await context.GetThread(HttpContext, id);

			if (Threadlist == null || Threadlist.Count == 0)
			{
				return NotFound();
			}
			Thread = Threadlist[0];
			return Page();
		}
	}
}
