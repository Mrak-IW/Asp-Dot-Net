using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEntityFramework
{
	class Program
	{

		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

			DatabaseCont db = new DatabaseCont();

			//db.Vehicles.First().Speed = 1;
			//db.SaveChanges();

			foreach (ITransport t in db.Vehicles)
			{
				Console.WriteLine("Имя:\t{0}\nСкорость:\t{1}", t.Name, t.Speed);
				if (t is IWheeled)
				{
					Console.WriteLine("Колёс:\t{0}", (t as IWheeled).NumberOfWheels);
				}
				if (t is IFloating)
				{
					Console.WriteLine("Осадка:\t{0}", (t as IFloating).Sinking);
				}

				Console.WriteLine("\n\n");
			}
		}
	}
}
