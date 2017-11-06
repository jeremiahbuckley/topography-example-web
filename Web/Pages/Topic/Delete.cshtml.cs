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
    public class DeleteModel : PageModel
    {
        //private readonly Web.Models.TopicContext _context;

        //public DeleteModel(Web.Models.TopicContext context)
        //{
        //    _context = context;
        //}

        [BindProperty]
        public Web.Models.Topic Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			await new Task(() => { int x = 0; });
			//Topic = await _context.Topic.SingleOrDefaultAsync(m => m.Id == id);

			if (Topic == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			await new Task(() => { int x = 0; });

			//Topic = await _context.Topic.FindAsync(id);

			//if (Topic != null)
			//{
			//    _context.Topic.Remove(Topic);
			//    await _context.SaveChangesAsync();
			//}

			return RedirectToPage("./Index");
        }
    }
}
