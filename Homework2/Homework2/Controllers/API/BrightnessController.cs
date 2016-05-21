using System.Web.Http;

using Homework2.Config;
using Homework2.Controllers.API.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers.API
{
	public class BrightnessController : GeneralSmartHomeController
	{
		// GET api/Brightness/5
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
				if (dev is IBrightable)
				{
					result = Ok((dev as IBrightable).Brightness);
				}
				else
				{
					result = BadRequest(string.Format("Устройство {0} не поддерживает интерфейс IHaveThermostat", dev.Name));
				}
			}

			return result;
		}

		// PUT api/Brightness/5
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
				if (dev is IBrightable)
				{
					switch (value)
					{
						case "+":
							(dev as IBrightable).IncreaseBrightness();
							break;
						case "-":
							(dev as IBrightable).DecreaseBrightness();
							break;
						default:
							break;
					}

					result = Ok((dev as IBrightable).Brightness);
					SaveSmartHouse(sh);
				}
				else
				{
					result = BadRequest(string.Format("Устройство {0} не поддерживает интерфейс IHaveThermostat", dev.Name));
				}
			}

			return result;
		}
	}
}
