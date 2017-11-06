using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages.User
{
    public class DeleteModel : PageModel
    {
        //private readonly Web.Models.UserContext _context;

        //public DeleteModel(Web.Models.UserContext context)
        //{
        //    _context = context;
        //}

        [BindProperty]
        public Web.Models.User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			await new Task(() => { int x = 0; });

			//User = await _context.User.SingleOrDefaultAsync(m => m.Id == id);

            if (User == null)
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
			//User = await _context.User.FindAsync(id);

            //if (User != null)
            //{
            //    _context.User.Remove(User);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
