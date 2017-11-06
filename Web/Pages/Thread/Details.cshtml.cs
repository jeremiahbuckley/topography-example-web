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
    public class DetailsModel : PageModel
    {
        //private readonly Web.Models.ThreadContext _context;

        //public DetailsModel(Web.Models.ThreadContext context)
        //{
        //    _context = context;
        //}

        public Web.Models.Thread Thread { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Thread = await _context.Thread.SingleOrDefaultAsync(m => m.Id == id);

            //if (Thread == null)
            //{
            //    return NotFound();
            //}
            return Page();
        }
    }
}
