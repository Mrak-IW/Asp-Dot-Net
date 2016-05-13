using System.Web.Http;

using Homework2.Config;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers
{
	public class PowerStateController : GeneralSmartHomeController
	{
		// GET api/PowerState/5
		public IHttpActionResult Get(string id)
		{
			ISmartHouse sh = LoadFromStorage();
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
			ISmartHouse sh = LoadFromStorage();
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
				SaveToStorage(sh);
			}

			return result;
		}
	}
}
