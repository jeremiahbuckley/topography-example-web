using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.ViewComponents
{
	//public class Top
	//{
	//	public string Name { get; set; }
	//}

	public class TopicViewComponent : ViewComponent
	{
		private readonly Web.Context.ITopicRepository context;

		public TopicViewComponent(Web.Context.ITopicRepository context)
		{
			this.context = context;
		}

		public IList<Topic> Topic { get; set; }

		public async Task OnGetAsync()
		{
			Topic = await context.GetAll(HttpContext);
		}
		public async Task<IViewComponentResult> InvokeAsync(string filter)
		{
			Topic = await context.GetAll(HttpContext);
			//var ts = new Top[Topic.Count];
			//int i = 0;
			//foreach (Topic tpic in Topic)
			//{
			//	var t = new Top { Name = tpic.Name };
			//	ts[i++] = t;
			//}
			return View<IList<Web.Models.Topic>>(Topic);
		}
	}
}
