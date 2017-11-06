using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.Thread
{
    public class EditModel : PageModel
    {
        //private readonly Web.Models.ThreadContext _context;

        //public EditModel(Web.Models.ThreadContext context)
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
			await new Task(() => { int x = 0; });

			//Thread = await _context.Thread.SingleOrDefaultAsync(m => m.Id == id);

            if (Thread == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Thread).State = EntityState.Modified;

            try
            {
				await new Task(() => { int x = 0; });
				//await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("./Index");
        }
    }
}
