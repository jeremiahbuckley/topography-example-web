using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	public class LoginInfo
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
	public interface IAuthorizationTokenController
	{
		IActionResult Post([FromBody] LoginInfo login);
	}

	[Produces("application/json")]
    [Route("api/AuthorizationToken")]
    public class AuthorizationTokenController : Controller, IAuthorizationTokenController
	{
		IDataLayer dataLayer;

		public class AuthResponse
		{
			public string UserName { get; set; }
			public string AuthToken { get; set; }
		}


		public AuthorizationTokenController(IDataLayer dataLayer)
		{
			this.dataLayer = dataLayer;
		}
		// POST: api/AuthorizationToken
		[HttpPost]
        public IActionResult Post([FromBody]LoginInfo login)
        {
			string username = login.Username;
			string password = login.Password;
			// TODO: improve this...
			if (username != null && password != null &&
				(string.Compare(username, password, StringComparison.InvariantCultureIgnoreCase) == 0) &&
				(dataLayer.UserReadByName(username) != null))
			{
				var auth = new AuthResponse { UserName = username, AuthToken = username.GetHashCode().ToString() };
				return Ok(auth);
			} else
			{
				return new UnauthorizedResult();
			}
        }
    }
}
