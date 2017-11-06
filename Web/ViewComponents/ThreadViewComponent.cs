using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;


namespace Web.ViewComponents
{
    public class ThreadViewComponent : ViewComponent
    {
		private readonly Web.Context.IThreadRepository context;

		public ThreadViewComponent(Web.Context.IThreadRepository context)
		{
			this.context = context;
		}

		public IList<Web.Models.Thread> Thread { get; set; }

		public async Task<IViewComponentResult> InvokeAsync()
		{
			Thread = await context.GetAll(HttpContext);
			return View<IEnumerable<Web.Models.Thread>>(Thread);
		}
	}
}
