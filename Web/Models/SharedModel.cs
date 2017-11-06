using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SharedModel
    {
		public int? CurrentTopicId { get; set; }
		public bool TopicEnabled { get; set; }
		public int? CurrentThreadId { get; set; }
		public bool ThreadEnabled { get; set; }
		public int? CurrentCommentId { get; set; }
    }
}
