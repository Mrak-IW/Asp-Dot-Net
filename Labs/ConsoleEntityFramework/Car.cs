using System.ComponentModel.DataAnnotations;

namespace ConsoleEntityFramework
{
	class Car : ITransport, IWheeled
	{
		[Key]
		public string Name { get; set; }
		public int Speed { get; set; }

		public int NumberOfWheels { get; set; }
	}
}
