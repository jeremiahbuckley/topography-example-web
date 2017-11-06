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
    public class IndexModel : PageModel
    {
        //private readonly Web.Models.TopicContext _context;

        //public IndexModel(Web.Models.TopicContext context)
        //{
        //    _context = context;
        //}

        public IList<Web.Models.Topic> Topic { get;set; }

        public async Task OnGetAsync()
        {
			await new Task(() => { int x = 0; });

			//Topic = await _context.Topic.ToListAsync();
        }
    }
}
