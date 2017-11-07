using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SharedModel
    {
		public Topic CurrentTopic { get; set; }
		public Thread CurrentThread { get; set; }
		public Comment CurrentComment { get; set; }
    }
}
