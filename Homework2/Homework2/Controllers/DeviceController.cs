using System.Web.Http;

using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers
{
    public class DeviceController : GeneralSmartHomeController
	{
        // GET: api/Device/5
        public IHttpActionResult Get(string id)
        {
			IHttpActionResult result;

			ISmartHouse sh = LoadFromStorage();
			ISmartDevice dev = sh[id];

			if (dev == null)
			{
				result = NotFound();
			}
			else
			{
				result = Ok(dev.ToString());
			}

            return result;
        }

        // POST: api/Device
        public void Post([FromBody]string value)
        {
        }

        // DELETE: api/Device/5
        public void Delete(int id)
        {
        }
    }
}
