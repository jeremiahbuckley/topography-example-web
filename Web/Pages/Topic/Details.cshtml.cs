using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Topic
{
    public class DetailsModel : PageModel
    {
		private readonly Web.Context.ITopicRepository context;

		public DetailsModel(Web.Context.ITopicRepository context)
		{
			this.context = context;
		}

		public Web.Models.Topic Topic { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var Topiclist = await context.GetTopic(HttpContext, id);

			if (Topiclist == null || Topiclist.Count == 0)
			{
				return NotFound();
			}
			Topic = Topiclist[0];
			return Page();
		}
	}
}
