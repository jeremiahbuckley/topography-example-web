using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Pages
{
    public class CommentsModel : PageModel
    {
        private readonly Web.Context.ICommentRepository context;

        public CommentsModel(Web.Context.ICommentRepository context)
        {
			this.context = context;
        }

        public IList<Comment> Comment { get;set; }

        public async Task OnGetAsync()
        {
            Comment = await context.GetAll(HttpContext);
        }
    }
}
