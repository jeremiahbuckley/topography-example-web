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
using Microsoft.AspNetCore.Http;

namespace Web.Context
{
	public interface IThreadRepository
	{
		Task<IList<Thread>> GetAll(HttpContext httpContext);
		Task<IList<Thread>> GetThread(HttpContext httpContext, int? id);
		Task<int> PostThread(HttpContext httpContext, Thread thread);
		Task<int> PutThread(HttpContext httpContext, Thread thread);
	}

	public class ThreadRepository : IThreadRepository
	{
		HttpClient client;
		string uri = "Thread";
		string baseUri;
		public ThreadRepository()
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

		public Task<IList<Thread>> GetAll(HttpContext httpContext)
		{
			return GetThread(httpContext, null);
		}

		public async Task<IList<Thread>> GetThread(HttpContext httpContext, int? id)
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
				var Threads = JsonConvert.DeserializeObject<List<Thread>>(data);
				return Threads;
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}

		public async Task<int> PostThread(HttpContext httpContext, Thread thread)
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.Headers.Add("x-jb-api-username", httpContext.Request.Cookies["username"]);
			request.Headers.Add("x-jb-api-authtoken", httpContext.Request.Cookies["authtoken"]);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Method = HttpMethod.Post;
			request.Content = new StringContent(JsonConvert.SerializeObject(thread), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var Threads = JsonConvert.DeserializeObject<List<Thread>>(data);
				return Int32.Parse(data);
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}

		public async Task<int> PutThread(HttpContext httpContext, Thread thread)
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.Headers.Add("x-jb-api-username", httpContext.Request.Cookies["username"]);
			request.Headers.Add("x-jb-api-authtoken", httpContext.Request.Cookies["authtoken"]);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.RequestUri = new Uri(client.BaseAddress + "/" + thread.Id.ToString());
			request.Method = HttpMethod.Put;
			request.Content = new StringContent(JsonConvert.SerializeObject(thread), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var Threads = JsonConvert.DeserializeObject<List<Thread>>(data);
				return Int32.Parse(data);
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}
	}
}
