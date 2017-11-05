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
	public interface ITopicRepository
	{
		Task<IList<Topic>> GetAll(HttpContext httpContext);
		Task<IList<Topic>> GetTopic(HttpContext httpContext, int? id);
	}

	public class TopicRepository : ITopicRepository
	{
		HttpClient client;
		string uri = "Topic";
		string baseUri;
		public TopicRepository()
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

		public Task<IList<Topic>> GetAll(HttpContext httpContext)
		{
			return GetTopic(httpContext, null);
		}

		public async Task<IList<Topic>> GetTopic(HttpContext httpContext, int? id)
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
				var data = await response.Content.ReadAsStringAsync();
				var Topics = JsonConvert.DeserializeObject<List<Topic>>(data);
				return Topics;
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}
	}
}
