using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;

using Homework2.Config;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers
{
	public class PowerStateController : ApiController
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
				result = Ok(dev.State.ToString());
			}

			return result;
		}

		// PUT api/PowerState/5
		public IHttpActionResult Put(string id, [FromBody]EPowerState value)
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
				dev.State = value;
				result = Ok(dev.State);
			}

			return result;
		}

		protected ISmartHouse LoadFromStorage()
		{
			ISmartHouse result = null;

			SmartHouseConfig shConfig = GetConfig();
			string storagePath = Path.Combine(shConfig.StorageFilePath, shConfig.StorageFileName);
			storagePath = HostingEnvironment.MapPath(storagePath);
			FileInfo fi = null;

			try
			{
				fi = new FileInfo(storagePath);
			}
			catch { }

			if (fi != null && fi.Exists)
			{
				using (FileStream fs = fi.Open(FileMode.Open))
				{
					try
					{
						BinaryFormatter bf = new BinaryFormatter();
						result = bf.Deserialize(fs) as ISmartHouse;
					}
					catch { }
				}
			}

			return result;
		}

		protected bool SaveToStorage(ISmartHouse sh)
		{
			bool saved = false;

			SmartHouseConfig shConfig = GetConfig();
			string storagePath = Path.Combine(shConfig.StorageFilePath, shConfig.StorageFileName);
			storagePath = HostingEnvironment.MapPath(storagePath);
			FileInfo fi = null;

			try
			{
				fi = new FileInfo(storagePath);
			}
			catch { }

			if (fi != null)
			{
				using (FileStream fs = fi.Open(FileMode.Create))
				{
					try
					{
						BinaryFormatter bf = new BinaryFormatter();
						bf.Serialize(fs, sh);
						saved = true;
					}
					catch { }
				}
			}

			return saved;
		}

		protected SmartHouseConfig GetConfig()
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
			SmartHouseConfig shSection = (SmartHouseConfig)config.GetSection(SmartHouseConfig.SectionName);
			return shSection;
		}
	}
}
