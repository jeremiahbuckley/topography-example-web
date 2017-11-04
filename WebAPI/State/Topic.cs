using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.State
{
    public class Topic
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public byte[] Version { get; set; }

		public Dictionary<int, Thread> Threads { get; set; }
	}
}
