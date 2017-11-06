using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
		public Web.Context.ICommentRepository CommentContext { get; }
		public Web.Context.IUserRepository UserContext { get; }

		public Web.Pages.CommentsModel CommentModel { get; private set; }
		public Web.Pages.UserSettingsModel UserModel { get; private set; }

		public IndexModel(Context.ICommentRepository commentContext,
			Context.IUserRepository userContext)
		{
			this.CommentContext = commentContext;
			this.UserContext = userContext;

			CommentModel = new Pages.CommentsModel(CommentContext);
			UserModel = new UserSettingsModel(UserContext);
		}

		public void OnGet()
        {

        }
    }
}
