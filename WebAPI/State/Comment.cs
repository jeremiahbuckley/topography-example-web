using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.State
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

		public Comment ReplyToComment { get; set; }
		public Thread Thread { get; set; }
		public Topic Topic { get; set; }
		public User User { get; set; }
	}
}
