using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab17.Models
{
	public class Staff
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public int? CompanyID { get; set; }
		public virtual Company Company { get; set; }
	}
}