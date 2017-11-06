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
    public class IndexModel : PageModel
    {
        //private readonly Web.Models.UserContext _context;

        //public IndexModel(Web.Models.UserContext context)
        //{
        //    _context = context;
        //}

        public IList<Web.Models.User> User { get;set; }

        public async Task OnGetAsync()
        {
			await new Task(() => { int x = 0; });

			//User = await _context.User.ToListAsync();
        }
    }
}
