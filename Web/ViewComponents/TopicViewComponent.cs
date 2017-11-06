using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
	public class TopicViewComponent : ViewComponent
	{
		private readonly Web.Context.ITopicRepository context;

		public TopicViewComponent(Web.Context.ITopicRepository context)
		{
			this.context = context;
			this.SharedModel = new SharedModel();
		}

		public IList<Topic> Topic { get; set; }

		public SharedModel SharedModel { get; set; }

		public async Task OnGetAsync()
		{
			Topic = await context.GetAll(HttpContext);
			foreach(Web.Models.Topic t in Topic)
			{
				SharedModel.CurrentTopicId = t.Id;
				break;
			}
		}
		public async Task<IViewComponentResult> InvokeAsync(string filter)
		{
			Topic = await context.GetAll(HttpContext);
			return View(this);
		}
	}
}
