using System.Web.Http;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;
using Homework2.Models;
using Homework2.Constants;

namespace Homework2.Controllers
{
	public class DeviceController : GeneralSmartHomeController
	{
		// GET: api/Device/5
		public IHttpActionResult Get(string id)
		{
			IHttpActionResult result;

			ISmartHouse sh = LoadSmartHouse();
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

		public IHttpActionResult Post([FromBody]string value)
		{
			IHttpActionResult result = BadRequest();
			Dictionary<string, string> fields;

			DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
			using (Stream str = GenerateStreamFromString(value))
			{
				fields = (Dictionary<string, string>)js.ReadObject(str);
				//Я в курсе, что этот способ выглядит странным, но я проверял - если попробовать принять
				//Dictionary<string, string> или KeyValuePair<string, string>[] через параметр метода,
				//получим вариацию на тему null
			}

			ISmartHouse sh = LoadSmartHouse();

			if (sh[fields[CreateDeviceFields.name]] == null)
			{
				Assembly modelAssembly = Assembly.Load("SmartHouse");
				ISmartHouseCreator shc = Manufacture.GetManufacture(modelAssembly);

				ISmartDevice dev = shc.CreateDevice(fields[CreateDeviceFields.devType], fields[CreateDeviceFields.name]);
				if (dev != null)
				{
					if (dev is IBrightable)
					{
						IBrightable idev = dev as IBrightable;
						idev.BrightnessMax = int.Parse(fields[CreateDeviceFields.brightnessMax]);
						idev.BrightnessMin = int.Parse(fields[CreateDeviceFields.brightnessMin]);
						idev.BrightnessStep = int.Parse(fields[CreateDeviceFields.brightnessStep]);
					}

					if (dev is IHaveThermostat)
					{
						IHaveThermostat idev = dev as IHaveThermostat;
						idev.TempMax = int.Parse(fields[CreateDeviceFields.temperatureMax]);
						idev.TempMin = int.Parse(fields[CreateDeviceFields.temperatureMin]);
						idev.TempStep = int.Parse(fields[CreateDeviceFields.temperatureStep]);
					}

					sh.AddDevice(dev);
					SaveSmartHouse(sh);
					result = Ok();
				}
			}
			else
			{
				result = BadRequest(string.Format("Устройство {0} уже есть в системе", fields[CreateDeviceFields.name]));
			}
			return result;
		}

		// DELETE: api/Device/5
		public IHttpActionResult Delete(string id)
		{
			IHttpActionResult result;

			ISmartHouse sh = LoadSmartHouse();
			ISmartDevice dev = sh[id];

			if (dev == null)
			{
				result = NotFound();
			}
			else
			{
				sh.RemoveDevice(id);
				SaveSmartHouse(sh);
				result = Ok();
			}

			return result;
		}

		public Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
