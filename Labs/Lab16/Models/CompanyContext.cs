using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab16.Models
{
	public class CompanyContext : DbContext
	{
		static CompanyContext()
		{
			Database.SetInitializer<CompanyContext>(new CompanyContextInitializer());
		}

		public DbSet<Staff> Workers { get; set; }
	}
}