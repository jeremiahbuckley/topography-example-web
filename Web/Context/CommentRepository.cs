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
	public interface ICommentRepository
	{
		Task<IList<Comment>> GetAll(HttpContext httpContext);
		Task<IList<Comment>> GetComment(HttpContext httpContext, int? id);
		Task<int> PostComment(HttpContext httpContext, Comment comment);
		Task<int> PutComment(HttpContext httpContext, Comment comment);
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
		}

		public Task<IList<Comment>> GetAll(HttpContext httpContext)
		{
			return GetComment(httpContext, null);
		}

		public async Task<IList<Comment>> GetComment(HttpContext httpContext, int? id)
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
				var Comments = JsonConvert.DeserializeObject<List<Comment>>(data);
				return Comments;
			}
			else
			{
				throw new Exception(string.Format("{0}: {1}", response.StatusCode, response.ReasonPhrase));
			}
		}

		public async Task<int> PostComment(HttpContext httpContext, Comment comment)
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.Headers.Add("x-jb-api-username", httpContext.Request.Cookies["username"]);
			request.Headers.Add("x-jb-api-authtoken", httpContext.Request.Cookies["authtoken"]);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Method = HttpMethod.Post;
			request.Content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.SendAsync(request);
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

		public async Task<int> PutComment(HttpContext httpContext, Comment comment)
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.Headers.Add("x-jb-api-username", httpContext.Request.Cookies["username"]);
			request.Headers.Add("x-jb-api-authtoken", httpContext.Request.Cookies["authtoken"]);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.RequestUri = new Uri(client.BaseAddress + "/" + comment.Id.ToString());
			request.Method = HttpMethod.Put;
			request.Content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.SendAsync(request);
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
