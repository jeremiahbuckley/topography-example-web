using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters
{
    public class AuthenticatedUserAttribute : ActionFilterAttribute
    {
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.HttpContext.Request.Headers.ContainsKey("x-jb-api-username") || !context.HttpContext.Request.Headers.ContainsKey("x-jb-api-authtoken"))
			{
				context.Result = new UnauthorizedResult();
			}
			var username = context.HttpContext.Request.Headers["x-jb-api-username"][0];
			var authtoken = context.HttpContext.Request.Headers["x-jb-api-authtoken"][0];
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(authtoken) || authtoken != username.GetHashCode().ToString())
			{
				context.Result = new UnauthorizedResult();
			}
		}
    }
}
