using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab17.Models
{
	public class StaffDbContext : DbContext
	{
		static StaffDbContext()
		{
			Database.SetInitializer<StaffDbContext>(new StaffDbContextInitializer());
		}

		public DbSet<Staff> Workers { get; set; }
		public DbSet<Company> Companies { get; set; }
	}
}