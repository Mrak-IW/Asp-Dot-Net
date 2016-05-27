using System.Data.Entity;

namespace ConsoleEntityFramework
{
	class DatabaseInitializer : CreateDatabaseIfNotExists<DatabaseCont>
	{
		protected override void Seed(DatabaseCont context)
		{
			//context.Books.Add(new Book
			//{
			//	Id = 1,
			//	Name = "Main
			//principles of object model", Author = "Grady Booch", Price
			//= 100
			//});
			//context.Books.Add(new Book
			//{
			//	Id = 2,
			//	Name = "Design
			//principles SOLID", Author = "Robert Martin", Price =
			//100
			//});
			context.Cars.Add(new Car { Name = "Антилопа", NumberOfWheels = 5, Speed = 20 });
			context.Cars.Add(new Car { Name = "кравчучка", NumberOfWheels = 2, Speed = 1 });
			context.Cars.Add(new Car { Name = "Джипорожец", NumberOfWheels = 4, Speed = 50 });
			context.Ships.Add(new Ship { Name = "Беда", Sinking = 30, Speed = 30 });
			System.Console.WriteLine("Инициализиция БД завершена");
			context.SaveChanges();
		}
	}
}
