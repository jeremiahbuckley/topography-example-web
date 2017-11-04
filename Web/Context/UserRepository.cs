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

namespace Web.Context
{
	public interface IUserRepository
	{
		Task<IList<User>> GetAll();
		Task<IList<User>> GetUser(int? id);
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
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public Task<IList<User>> GetAll()
		{
			return GetUser(null);
		}

		public async Task<IList<User>> GetUser(int? id)
		{
			HttpResponseMessage response = await client.GetAsync(id.HasValue ? "/" + id.ToString() : "");
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
