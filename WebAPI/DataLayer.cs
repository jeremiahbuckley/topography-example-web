using WebAPI.State;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
	public interface IDataLayer
	{
		Task<int> TopicCreate(string name, bool enabled);
		Task<IList<Topic>> TopicRead(int? id);
		Task<IList<Topic>> TopicReadAll();
		Task<int> TopicUpdate(int id, string name, bool enabled);
		Task<int> TopicDelete(int id);
		Task<int> ThreadCreate(string name, int topicId, bool enabled, bool pinned, int? pinOrder);
		Task<IList<Thread>> ThreadRead(int? id);
		Task<IList<Thread>> ThreadReadAll();
		Task<int> ThreadUpdate(int id, string name, int topicId, bool enabled, bool pinned, int? pinOrder);
		Task<int> ThreadDelete(int id);
		Task<int> UserCreate(string name, bool enabled);
		Task<IList<User>> UserRead(int? id);
		Task<IList<User>> UserReadAll();
		Task<int> UserUpdate(int id, string name, bool enabled);
		Task<int> UserDelete(int id);
		Task<int> CommentCreate(int topicId, int threadId, int userId, string comment, int? replyToCommentId);
		Task<IList<Comment>> CommentRead(int? id);
		Task<IList<Comment>> CommentReadAll();
		Task<int> CommentUpdate(int id, int topicId, int threadId, int userId, string comment, int? replyToCommentId);
		Task<int> CommentDelete(int id);
	}

	public class DataLayer : IDataLayer
    {
		string connectionString;
		public DataLayer()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			IConfigurationRoot configuration = builder.Build();
			connectionString = configuration["ConnectionStrings:DevConnection"];
		}

		#region Topic
		public async Task<int> TopicCreate(string name, bool enabled)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Topic_Insert", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "enabled", DbType = DbType.Boolean, Value = enabled });

					return (int) await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<IList<Topic>> TopicRead(int? id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Topic_Select", conn))
				{
					if (id.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id.Value });
					}

					var results = new List<Topic>();
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							var result = new Topic();
							result.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							result.Name = reader.GetString(reader.GetOrdinal("Name"));
							result.Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"));
							results.Add(result);
						}
					}

					return results;
				}
			}
		}

		public async Task<IList<Topic>> TopicReadAll()
		{
			return await TopicRead(null);
		}

		public async Task<int> TopicUpdate(int id, string name, bool enabled)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Topic_Update", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "enabled", DbType = DbType.Boolean, Value = enabled });

					return (int) await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<int> TopicDelete(int id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Topic_Delete", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					return await cmd.ExecuteNonQueryAsync();
				}
			}
		}
		#endregion

		#region Thread
		public async Task<int> ThreadCreate(string name, int topicId, bool enabled, bool pinned, int? pinOrder)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Thread_Insert", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "pinned", DbType = DbType.Boolean, Value = pinned });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "topicid", DbType = DbType.Int32, Value = topicId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "enabled", DbType = DbType.Boolean, Value = enabled });
					if (pinOrder.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "pinorder", DbType = DbType.String, Value = pinOrder });
					}

					return (int)await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<IList<Thread>> ThreadRead(int? id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Thread_Select", conn))
				{
					if (id.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id.Value });
					}

					var results = new List<Thread>();
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							var result = new Thread();
							result.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							result.Name = reader.GetString(reader.GetOrdinal("Name"));
							result.Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"));
							result.Pinned = reader.GetBoolean(reader.GetOrdinal("Pinned"));
							if (!reader.IsDBNull(reader.GetOrdinal("PinOrder")))
							{
								result.PinOrder = reader.GetInt32(reader.GetOrdinal("PinOrder"));
							}
							results.Add(result);
						}
					}

					return results;
				}
			}
		}

		public async Task<IList<Thread>> ThreadReadAll()
		{
			return await ThreadRead(null);
		}

		public async Task<int> ThreadUpdate(int id, string name, int topicId, bool enabled, bool pinned, int? pinOrder)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Thread_Update", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "pinned", DbType = DbType.Boolean, Value = pinned });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "topicid", DbType = DbType.Int32, Value = topicId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "enabled", DbType = DbType.Boolean, Value = enabled });
					if (pinOrder.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "pinorder", DbType = DbType.String, Value = pinOrder });
					}

					return (int)await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<int> ThreadDelete(int id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Thread_Delete", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					return await cmd.ExecuteNonQueryAsync();
				}
			}
		}
		#endregion

		#region User
		public async Task<int> UserCreate(string name, bool enabled)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("User_Insert", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "enabled", DbType = DbType.Boolean, Value = enabled });

					return (int)await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<IList<User>> UserRead(int? id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("User_Select", conn))
				{
					if (id.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id.Value });
					}

					var results = new List<User>();
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							var result = new User();
							result.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							result.Name = reader.GetString(reader.GetOrdinal("Name"));
							result.Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"));
							result.DateTimeJoined = reader.GetDateTime(reader.GetOrdinal("DateTimeJoined"));
							results.Add(result);
						}
					}

					return results;
				}
			}
		}

		public async Task<IList<User>> UserReadAll()
		{
			return await UserRead(null);
		}

		public async Task<int> UserUpdate(int id, string name, bool enabled)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("User_Update", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "enabled", DbType = DbType.Boolean, Value = enabled });

					return (int)await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<int> UserDelete(int id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("User_Delete", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					return await cmd.ExecuteNonQueryAsync();
				}
			}
		}
		#endregion

		#region Comment
		public async Task<int> CommentCreate(int topicId, int threadId, int userId, string comment, int? replyToCommentId)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Comment_Insert", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "topicid", DbType = DbType.Int32, Value = topicId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "threadid", DbType = DbType.Int32, Value = threadId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "userid", DbType = DbType.Int32, Value = userId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = comment });
					if (replyToCommentId.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "replytocommentid", DbType = DbType.Int32, Value = replyToCommentId });
					}

					return (int)await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<IList<Comment>> CommentRead(int? id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Comment_Select", conn))
				{
					if (id.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id.Value });
					}

					var results = new List<Comment>();
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							var result = new Comment();
							result.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							result.TopicId = reader.GetInt32(reader.GetOrdinal("TopicId"));
							result.ThreadId = reader.GetInt32(reader.GetOrdinal("ThreadId"));
							result.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
							if (!reader.IsDBNull(reader.GetOrdinal("ReplyToCommentId")))
							{
								result.ReplyToCommentId = reader.GetInt32(reader.GetOrdinal("ReplyToCommentId"));
							}
							result.CommentStr = reader.GetString(reader.GetOrdinal("Comment"));
							result.DateTimeAdded = reader.GetDateTime(reader.GetOrdinal("DateTimeAdded"));
							results.Add(result);
						}
					}

					return results;
				}
			}
		}

		public async Task<IList<Comment>> CommentReadAll()
		{
			return await CommentRead(null);
		}

		public async Task<int> CommentUpdate(int id, int topicId, int threadId, int userId, string comment, int? replyToCommentId)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Comment_Update", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "topicid", DbType = DbType.Int32, Value = topicId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "threadid", DbType = DbType.Int32, Value = threadId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "userid", DbType = DbType.Int32, Value = userId });
					cmd.Parameters.Add(new SqlParameter { ParameterName = "string", DbType = DbType.String, Value = comment });
					if (replyToCommentId.HasValue)
					{
						cmd.Parameters.Add(new SqlParameter { ParameterName = "replytocommentid", DbType = DbType.Int32, Value = replyToCommentId });
					}

					return (int)await cmd.ExecuteScalarAsync();
				}
			}
		}

		public async Task<int> CommentDelete(int id)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("Comment_Delete", conn))
				{
					cmd.Parameters.Add(new SqlParameter { ParameterName = "id", DbType = DbType.String, Value = id });
					return await cmd.ExecuteNonQueryAsync();
				}
			}
		}
		#endregion

	}
}
