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
		public DbSet<Hobby> Hobbies { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Hobby>()
			.HasMany(c => c.Workers)
			.WithMany(s => s.Hobbies)
			.Map(t => t.MapLeftKey("HobbyId")
			.MapRightKey("WorkerId")
			.ToTable("WorkersHobbies"));
		}
	}
}