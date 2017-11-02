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

					var topics = new List<Topic>();
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						while (reader.Read())
						{
							Topic t = new Topic();
							t.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							t.Name = reader.GetString(reader.GetOrdinal("Name"));
							t.Enabled = reader.GetBoolean(reader.GetOrdinal("Enabled"));
							topics.Add(t);
						}
					}

					return topics;
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

	}
}
