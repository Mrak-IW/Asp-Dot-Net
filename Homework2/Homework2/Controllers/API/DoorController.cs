using System.Web.Http;

using Homework2.Config;
using Homework2.Controllers.API.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers.API
{
	public class DoorController : GeneralSmartHomeController
	{
		// GET api/Door/5
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

		// PUT api/Door/5
		public IHttpActionResult Put(string id, [FromBody]bool value)
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
				if (dev is IOpenCloseable)
				{
					(dev as IOpenCloseable).IsOpened = value;

					result = Ok((dev as IOpenCloseable).IsOpened);
					SaveSmartHouse(sh);
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
