using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Thread
{
    public class DeleteModel : PageModel
    {
        //private readonly Web.Models.ThreadContext _context;

        //public DeleteModel(Web.Models.ThreadContext context)
        //{
        //    _context = context;
        //}

        [BindProperty]
        public Web.Models.Thread Thread { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			Thread = null; // await _context.Thread.SingleOrDefaultAsync(m => m.Id == id);

            if (Thread == null)
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

			Thread = null; // await _context.Thread.FindAsync(id);

            //if (Thread != null)
            //{
            //    _context.Thread.Remove(Thread);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
