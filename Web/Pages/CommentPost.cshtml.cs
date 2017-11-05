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
    public class CommentPostModel : PageModel
    {
        private readonly Web.Context.ICommentRepository context;

        public CommentPostModel(Web.Context.ICommentRepository context)
        {
            this.context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var id = await context.PostComment(HttpContext, Comment);

			return RedirectToPage("./Threads");
		}
	}
}