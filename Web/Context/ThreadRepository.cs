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
	public interface IThreadRepository
	{
		Task<IList<Thread>> GetAll();
		Task<IList<Thread>> GetThread(int? id);
		Task<int> PostThread(Thread thread);
		Task<int> PutThread(Thread thread);
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
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public Task<IList<Thread>> GetAll()
		{
			return GetThread(null);
		}

		public async Task<IList<Thread>> GetThread(int? id)
		{
			HttpResponseMessage response = await client.GetAsync(id.HasValue ? "/" + id.ToString() : "");
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

		public async Task<int> PostThread(Thread thread)
		{
			string body = JsonConvert.SerializeObject(thread);
			HttpResponseMessage response = await client.PostAsync("", new StringContent(body, Encoding.UTF8, "application/json"));
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

		public async Task<int> PutThread(Thread thread)
		{
			string body = JsonConvert.SerializeObject(thread);
			HttpResponseMessage response = await client.PutAsync("/" + thread.Id, new StringContent(body, Encoding.UTF8, "application/json"));
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
