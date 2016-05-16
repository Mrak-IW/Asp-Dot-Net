using System.Web.Http;

using Homework2.Config;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers
{
	public class IsOpenedController : GeneralSmartHomeController
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
				if (dev is IOpenCloseable)
				{
					result = Ok((dev as IOpenCloseable).IsOpened);
				}
				else
				{
					result = BadRequest(string.Format("Устройство {0} не поддерживает интерфейс IOpenClosable", dev.Name));
				}
			}

			return result;
		}

		// PUT api/IsOpenedController/5
		public IHttpActionResult Put(string id, [FromBody]bool value)
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
				if (dev is IOpenCloseable)
				{
					(dev as IOpenCloseable).IsOpened = value;

					result = Ok((dev as IOpenCloseable).IsOpened);
					SaveToStorage(sh);
				}
				else
				{
					result = BadRequest(string.Format("Устройство {0} не поддерживает интерфейс IOpenClosable", dev.Name));
				}
			}

			return result;
		}
	}
}
