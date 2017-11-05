using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
    public class TopicModel : PageModel
    {
		private readonly Web.Context.ITopicRepository context;

        public TopicModel(Web.Context.ITopicRepository context)
        {
            this.context = context;
        }

        public IList<Topic> Topic { get;set; }

        public async Task OnGetAsync()
        {
			Topic = await context.GetAll(HttpContext);
        }
    }
}
