using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab17.Models
{
	public class StaffDbContextInitializer : CreateDatabaseIfNotExists<StaffDbContext>
	{
		protected override void Seed(StaffDbContext context)
		{
			Company c1 = new Company();
			c1.Name = "Кантора Братев Бобры";
			Company c2 = new Company();
			c2.Name = "SpaceRangers inc.";

			context.Workers.Add(new Staff { Id = 1, Name = "Тимоха", Surname = "Бобры", Position = "Бугай", Company = c1 });
			context.Workers.Add(new Staff { Id = 1, Name = "Митроха", Surname = "Бобры", Position = "Бугай", Company = c1 });
			context.Workers.Add(new Staff { Id = 1, Name = "Николай Николаевич", Surname = "Бобры", Position = "Мозги", Company = c1 });

			context.Workers.Add(new Staff { Id = 1, Name = "Угон", Surname = "Камазов", Position = "Сторож", Company = c2 });
			context.Workers.Add(new Staff { Id = 1, Name = "Бидон", Surname = "Помоев", Position = "Племянник директора", Company = c2 });
			context.Workers.Add(new Staff { Id = 1, Name = "Рулон", Surname = "Обоев", Position = "Директор", Company = c2 });
			context.SaveChanges();
		}
	}
}