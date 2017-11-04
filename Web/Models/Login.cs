using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class Login
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
    }

	public class AuthResponse
	{
		public string UserName { get; set; }
		public string AuthToken { get; set; }
	}
}
