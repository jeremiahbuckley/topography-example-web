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
	public interface ICommentRepository
	{
		Task<IList<Comment>> GetAll();
		Task<IList<Comment>> GetComment(int? id);
		Task<int> PostComment(Comment thread);
		Task<int> PutComment(Comment comment);
	}

	public class CommentRepository : ICommentRepository
	{
		HttpClient client;
		string uri = "Comment";
		string baseUri;
		public CommentRepository()
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

		public Task<IList<Comment>> GetAll()
		{
			return GetComment(null);
		}

		public async Task<IList<Comment>> GetComment(int? id)
		{
			HttpResponseMessage response = await client.GetAsync(id.HasValue ? "/" + id.ToString() : "");
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var Comments = JsonConvert.DeserializeObject<List<Comment>>(data);
				return Comments;
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}

		public async Task<int> PostComment(Comment thread)
		{
			string body = JsonConvert.SerializeObject(thread);
			HttpResponseMessage response = await client.PostAsync("", new StringContent(body, Encoding.UTF8, "application/json"));
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var Comments = JsonConvert.DeserializeObject<List<Comment>>(data);
				return Int32.Parse(data);
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}

		public async Task<int> PutComment(Comment comment)
		{
			string body = JsonConvert.SerializeObject(comment);
			HttpResponseMessage response = await client.PostAsync("/" + comment.Id.ToString(), new StringContent(body, Encoding.UTF8, "application/json"));
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var Comments = JsonConvert.DeserializeObject<List<Comment>>(data);
				return Int32.Parse(data);
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}
	}
}
