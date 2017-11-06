using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Thirds
{
    public class IndexModel : PageModel
    {
        //private readonly Web.Models.ThreadContext _context;

        //public IndexModel(Web.Models.ThreadContext context)
        //{
        //    _context = context;
        //}

        public IList<Web.Models.Thread> Thread { get;set; }

        public async Task OnGetAsync()
		{
			await new Task(() => { int x = 0; });

			//Thread = await _context.Thread.ToListAsync();
        }
    }
}
