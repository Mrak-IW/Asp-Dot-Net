using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab17.Models
{
	public class Company
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Staff> Workers { get; set; }
	}
}