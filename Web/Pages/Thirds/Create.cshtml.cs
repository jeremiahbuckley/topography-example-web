using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages.Thirds
{
    public class CreateModel : PageModel
    {
        //private readonly Web.Models.ThreadContext _context;

        //public CreateModel(Web.Models.ThreadContext context)
        //{
        //    _context = context;
        //}

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Web.Models.Thread Thread { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Thread.Add(Thread);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}