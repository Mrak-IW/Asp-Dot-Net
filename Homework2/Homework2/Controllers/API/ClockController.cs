using System.Web.Http;

using Homework2.Controllers.API.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace Homework2.Controllers.API
{
	public class ClockController : GeneralSmartHomeController
	{
		// GET api/Clock/5
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
				if (dev is IHaveClock)
				{
					result = Ok(new { hour = (dev as IHaveClock).Time.Hour, minute = (dev as IHaveClock).Time.Minute });
				}
				else
				{
					result = BadRequest(string.Format("Устройство {0} не поддерживает интерфейс IHaveClock", dev.Name));
				}
			}

			return result;
		}
	}
}
