using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages.Comment
{
    public class CreateModel : PageModel
    {
		private readonly Web.Context.ICommentRepository context;

		public CreateModel(Web.Context.ICommentRepository context)
		{
			this.context = context;
		}

		public IActionResult OnGet(int threadId, int topicId, int? replyToCommentId)
		{
			Comment = new Web.Models.Comment();
			Comment.ThreadId = threadId;
			Comment.TopicId = topicId;
			Comment.ReplyToCommentId = replyToCommentId;
			return Page();
		}

		[BindProperty]
		public Web.Models.Comment Comment { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var id = await context.PostComment(HttpContext, Comment);

			return RedirectToPage("/Index");
		}
	}
}