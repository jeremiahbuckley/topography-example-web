using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Comment
{
    public class IndexModel : PageModel
    {
        //private readonly Web.Models.CommentContext _context;

        //public IndexModel(Web.Models.CommentContext context)
        //{
        //    _context = context;
        //}

        public IList<Web.Models.Comment> Comment { get;set; }

        public async Task OnGetAsync()
		{
			await new Task(() => { int x = 0; });

			//Comment = await _context.Comment.ToListAsync();
        }
    }
}
