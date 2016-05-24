using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab17.Models
{
	public class StaffDbContextInitializer : DropCreateDatabaseAlways<StaffDbContext>
	{
		protected override void Seed(StaffDbContext context)
		{
			Hobby robbery = new Hobby { Name = "Гоп-стоп", Workers = new List<Staff>() };
			Hobby chess = new Hobby { Name = "Шахматы", Workers = new List<Staff>() };
			Hobby strangeNames = new Hobby { Name = "Странные имена", Workers = new List<Staff>() };
			context.Hobbies.Add(robbery);
			context.Hobbies.Add(chess);
			context.Hobbies.Add(strangeNames);

			Company c1 = new Company();
			c1.Name = "Кантора Братев Бобры";
			Company c2 = new Company();
			c2.Name = "SpaceRangers inc.";

			Staff[] s = new Staff[6];
			s[0] = new Staff { Id = 1, Name = "Тимоха", Surname = "Бобры", Position = "Бугай", Company = c1 };
			s[1] = new Staff { Id = 1, Name = "Митроха", Surname = "Бобры", Position = "Бугай", Company = c1 };
			s[2] = new Staff { Id = 1, Name = "Николай Николаевич", Surname = "Бобры", Position = "Мозги", Company = c1 };
			s[3] = new Staff { Id = 1, Name = "Угон", Surname = "Камазов", Position = "Сторож", Company = c2 };
			s[4] = new Staff { Id = 1, Name = "Бидон", Surname = "Помоев", Position = "Племянник директора", Company = c2 };
			s[5] = new Staff { Id = 1, Name = "Рулон", Surname = "Обоев", Position = "Директор", Company = c2 };

			for (int i = 0; i < 3; i++)
			{
				robbery.Workers.Add(s[i]);
			}
			chess.Workers.Add(s[2]);

			for (int i = 3; i < 6; i++)
			{
				strangeNames.Workers.Add(s[i]);
			}

			foreach (Staff worker in s)
			{
				context.Workers.Add(worker);
			}

			context.SaveChanges();
		}
	}
}