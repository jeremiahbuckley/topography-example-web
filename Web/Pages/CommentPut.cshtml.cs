using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
	public class CommentPutModel : PageModel
	{
		private readonly Web.Context.ICommentRepository context;

		public CommentPutModel(Web.Context.ICommentRepository context)
		{
			this.context = context;
		}

		[BindProperty]
		public Comment Comment { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var Commentlist = await context.GetComment(HttpContext, id);

			if (Commentlist == null || Commentlist.Count == 0)
			{
				return NotFound();
			}
			Comment = Commentlist[0];
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var id = await context.PostComment(HttpContext, Comment);

			return RedirectToPage("./Comments");
		}
	}
}
