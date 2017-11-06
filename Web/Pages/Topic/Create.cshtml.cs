using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages.Topic
{
    public class CreateModel : PageModel
    {
        //private readonly Web.Models.TopicContext _context;

        //public CreateModel(Web.Models.TopicContext context)
        //{
        //    _context = context;
        //}

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Web.Models.Topic Topic { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Topic.Add(Topic);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}