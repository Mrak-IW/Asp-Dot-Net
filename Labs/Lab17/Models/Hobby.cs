using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab17.Models
{
	public class Hobby
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Staff> Workers { get; set; }
	}
}