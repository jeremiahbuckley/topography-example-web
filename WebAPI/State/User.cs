using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.State
{
    public class User
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public DateTime DateTimeJoined { get; set; }

		public Dictionary<int, Comment> Comments { get; set; }
	}
}
