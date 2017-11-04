using Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Web.Context
{
	public interface ILoginRepository
	{
		Task<AuthResponse> PostLogin(Login login);
	}
	public class LoginRepository : ILoginRepository
	{
		HttpClient client;
		string uri = "AuthorizationToken";
		string baseUri;
		public LoginRepository()
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

		public async Task<AuthResponse> PostLogin(Login login)
		{
			string body = JsonConvert.SerializeObject(login);
			HttpResponseMessage response = await client.PostAsync("", new StringContent(body, Encoding.UTF8, "application/json"));
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var authtokens = JsonConvert.DeserializeObject<AuthResponse>(data);
				return authtokens;
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}
	}
}
