using Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Context
{
	public interface IUserRepository
	{
		Task<IList<User>> GetAll(HttpContext httpContext);
		Task<IList<User>> GetUser(HttpContext httpContext, int? id);
	}

	public class UserRepository : IUserRepository
	{
		HttpClient client;
		string uri = "User";
		string baseUri;
		public UserRepository()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot configuration = builder.Build();
#if DEBUG
			baseUri = configuration["APIs:ForumDebug"];
#else
			baseUri = configuration["APIs:ForumDev"];
#endif

			client = new HttpClient();
			client.BaseAddress = new Uri(baseUri + uri);
			client.DefaultRequestHeaders.Accept.Clear();
		}

		public Task<IList<User>> GetAll(HttpContext httpContext)
		{
			return GetUser(httpContext, null);
		}

		public async Task<IList<User>> GetUser(HttpContext httpContext, int? id)
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.Headers.Add("x-jb-api-username", httpContext.Request.Cookies["username"]);
			request.Headers.Add("x-jb-api-authtoken", httpContext.Request.Cookies["authtoken"]);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			if (id.HasValue)
			{
				request.RequestUri = new Uri(client.BaseAddress + "/" + id.ToString());
			}
			request.Method = HttpMethod.Get;
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var data = response.Content.ReadAsStringAsync().Result;
				var Users = JsonConvert.DeserializeObject<List<User>>(data);
				return Users;
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}
    }
}
