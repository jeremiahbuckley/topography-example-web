using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
		private readonly Web.Context.ITopicRepository context;
		private readonly IViewComponentHelper viewComponentHelper;
		public IndexModel(Web.Context.ITopicRepository context, IViewComponentHelper viewComponentHelper)
		{
			this.context = context;
			this.viewComponentHelper = viewComponentHelper;
			this.SharedModel = new SharedModel();
		}

		public IList<Web.Models.Topic> Topic { get; set; }

		public SharedModel SharedModel { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
			Topic = await context.GetAll(HttpContext);
			foreach (Web.Models.Topic t in Topic)
			{
				SharedModel.CurrentTopic = t;
				break;
			}
			return Page();
		}

		public async Task<string> ChangeSelected(int id)
		{
			if (ModelState.IsValid)
			{
				foreach(Web.Models.Topic t in Topic)
				{
					if (t.Id == id)
					{
						SharedModel.CurrentTopic = t;
						break;
					}
				}
				var content = await viewComponentHelper.InvokeAsync("Thread", new { sharedModel = SharedModel });
				var writer = new System.IO.StringWriter();
				content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

				return writer.ToString();
			}
			return null;
		}
	}
}
