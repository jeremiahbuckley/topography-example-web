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
	public interface ITopicRepository
	{
		Task<IList<Topic>> GetAll();
		Task<IList<Topic>> GetTopic(int? id);
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
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public Task<IList<Topic>> GetAll()
		{
			return GetTopic(null);
		}

		public async Task<IList<Topic>> GetTopic(int? id)
		{
			HttpResponseMessage response = await client.GetAsync(id.HasValue ? "/" + id.ToString() : "");
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
