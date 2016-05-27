using System.Collections.Generic;
using System.Data.Entity;

namespace ConsoleEntityFramework
{
	class DatabaseCont : DbContext
	{
		static DatabaseCont()
		{
			Database.SetInitializer<DatabaseCont>(new DatabaseInitializer());
		}
		public DbSet<Ship> Ships { get; set; }
		public DbSet<Car> Cars { get; set; }

		public IEnumerable<ITransport> Vehicles
		{
			get
			{
				List<ITransport> list = new List<ITransport>();

				foreach (ITransport t in Ships)
				{
					list.Add(t);
				}

				foreach (ITransport t in Cars)
				{
					list.Add(t);
				}

				return list;
			}
		}
	}
}
