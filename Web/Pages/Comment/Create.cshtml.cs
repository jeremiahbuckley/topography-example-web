using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages.Comment
{
    public class CreateModel : PageModel
    {
        //private readonly Web.Models.CommentContext _context;

        //public CreateModel(Web.Models.CommentContext context)
        //{
        //    _context = context;
        //}

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Web.Models.Comment Comment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
			await new Task(() => { int x = 0; });

			//_context.Comment.Add(Comment);
			//await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
        }
    }
}