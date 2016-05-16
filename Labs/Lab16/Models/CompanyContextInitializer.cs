using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab16.Models
{
	public class CompanyContextInitializer : CreateDatabaseIfNotExists<CompanyContext>
	{
		protected override void Seed(CompanyContext context)
		{
			context.Workers.Add(new Staff { Id = 1, Name = "Угон", Surname = "Камазов", Position = "Сторож" });
			context.Workers.Add(new Staff { Id = 1, Name = "Бидон", Surname = "Помоев", Position = "Племянник директора" });
			context.Workers.Add(new Staff { Id = 1, Name = "Рулон", Surname = "Обоев", Position = "Директор" });
			context.SaveChanges();
		}
	}
}