using System.Web.Http;

using Homework2.Config;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers
{
	public class IHaveThermostatController : GeneralSmartHomeController
	{
		// GET api/IsOpenedController/5
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
				if (dev is IHaveThermostat)
				{
					result = Ok((dev as IHaveThermostat).Temperature);
				}
				else
				{
					result = BadRequest(string.Format("Устройство {0} не поддерживает интерфейс IHaveThermostat", dev.Name));
				}
			}

			return result;
		}

		// PUT api/IsOpenedController/5
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
				if (dev is IHaveThermostat)
				{
					switch (value)
					{
						case "+":
							(dev as IHaveThermostat).IncreaseTemperature();
							break;
						case "-":
							(dev as IHaveThermostat).DecreaseTemperature();
							break;
						default:
							break;
					}

					result = Ok((dev as IHaveThermostat).Temperature);
					SaveToStorage(sh);
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
