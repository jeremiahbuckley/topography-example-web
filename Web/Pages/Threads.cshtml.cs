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
    public class ThreadsModel : PageModel
    {
        private readonly Web.Context.IThreadRepository context;

        public ThreadsModel(Web.Context.IThreadRepository context)
        {
            this.context = context;
        }

        public IList<Thread> Thread { get;set; }

        public async Task OnGetAsync()
        {
            Thread = await context.GetAll();
        }
    }
}
