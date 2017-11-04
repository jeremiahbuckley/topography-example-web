using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Comment
	{
		public int Id { get; set; }
		public int TopicId { get; set; }
		public int ThreadId { get; set; }
		public int UserId { get; set; }
		public int? ReplyToCommentId { get; set; }
		public string CommentStr { get; set; }
		public DateTime DateTimeAdded { get; set; }
		public byte[] Version { get; set; }

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public Comment ReplyToComment { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public Thread Thread { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public Topic Topic { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public User User { get; set; }
	}
}
