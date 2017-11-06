using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;


namespace Web.ViewComponents
{
    public class CommentViewComponent : ViewComponent
    {
		private readonly Web.Context.ICommentRepository context;

		public CommentViewComponent(Web.Context.ICommentRepository context)
		{
			this.context = context;
		}

		public IList<Web.Models.Comment> Comment { get; set; }

		public SharedModel SharedModel { get; set; }

		public async Task<IViewComponentResult> InvokeAsync(SharedModel sharedModel)
		{
			SharedModel = sharedModel;
			Comment = await context.GetAll(HttpContext);
			return View(this);

		}
	}
}
