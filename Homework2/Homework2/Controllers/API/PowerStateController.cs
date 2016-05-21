using System.Web.Http;

using Homework2.Config;
using Homework2.Controllers.API.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers.API
{
	public class PowerStateController : GeneralSmartHomeController
	{
		// GET api/PowerState/5
		public IHttpActionResult Get(string id)
		{
			ISmartHouse sh = LoadSmartHouse();
			ISmartDevice dev = sh[id];
			IHttpActionResult result;

			if (dev == null)
			{
				result = NotFound();
			}
			else
			{
				result = Ok(dev.State.ToString().ToLower());
			}

			return result;
		}

		// PUT api/PowerState/5
		public IHttpActionResult Put(string id, [FromBody]string value)
		{
			ISmartHouse sh = LoadSmartHouse();
			ISmartDevice dev = sh[id];
			IHttpActionResult result;

			if (dev == null)
			{
				result = NotFound();
			}
			else
			{
				switch (value)
				{
					case "on":
						dev.State = EPowerState.On;
						break;
					default:
						dev.State = EPowerState.Off;
						break;
				}

				result = Ok(dev.State.ToString().ToLower());
				SaveSmartHouse(sh);
			}

			return result;
		}
	}
}
