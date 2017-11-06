using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Thread
{
    public class EditModel : PageModel
    {
		private readonly Web.Context.IThreadRepository context;

		public EditModel(Web.Context.IThreadRepository context)
		{
			this.context = context;
		}

		[BindProperty]
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

		public async Task<IActionResult> OnPostAsync()
        {
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var id = await context.PostThread(HttpContext, Thread);

			return RedirectToPage("./Index");
		}
	}
}
