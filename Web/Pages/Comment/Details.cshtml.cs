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
    public class DetailsModel : PageModel
    {
        //private readonly Web.Models.CommentContext _context;

        //public DetailsModel(Web.Models.CommentContext context)
        //{
        //    _context = context;
        //}

        public Web.Models.Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			await new Task(() => { int x = 0; });

			//Comment = await _context.Comment.SingleOrDefaultAsync(m => m.Id == id);

            if (Comment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
