using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System.Collections.Generic;

namespace Homework2.Views.ViewHelpers
{
	public class SmartHouseContext
	{
		public ISmartHouse SmartHouse { get; set; }
		public IList<DevTypesDescription> TypesAvailable { get; set; }
		public DeviceCreationContext DevCreationContext { get; set; }
	}
}