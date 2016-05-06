using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework2.Models.ViewHelpers
{
	public class DeviceCreationContext
	{
		public bool DevIsBrightable { get; set; }
		public bool DevHasThermostat { get; set; }
		public string DevType { get; set; }
		public string DevTypeTranslation { get; set; }
	}
}