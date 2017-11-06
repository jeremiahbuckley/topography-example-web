using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Pages
{
    public class ThreadPostModel : PageModel
    {
        private readonly Web.Context.IThreadRepository context;

        public ThreadPostModel(Web.Context.IThreadRepository context)
        {
            this.context = context;
        }

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

            var id = await context.PostThread(HttpContext, Thread);

            return RedirectToPage("./Threads");
        }
    }
}