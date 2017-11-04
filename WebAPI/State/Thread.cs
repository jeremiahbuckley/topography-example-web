using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.State
{
    public class Thread
    {
		public int Id { get; set; }
		public int TopicId { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public bool Pinned { get; set; }
		public int? PinOrder { get; set; }
		public byte[] Version { get; set; }

		public Dictionary<int, Comment> Comments { get; set; }
		public Thread Topic { get; set; }
	}
}
