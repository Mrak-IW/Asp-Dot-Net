using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleEntityFramework
{
	class Ship : ITransport, IFloating
	{
		[Key]
		public string Name { get; set; }
		public int Speed { get; set; }

		public int Sinking { get; set; }

		[NotMapped]
		public int CrazyValue
		{
			get
			{
				return Speed * Sinking;
			}
		}
	}
}
