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

namespace Homework2.Controllers
{
	public class ValuesController : ApiController
	{
		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		public string Get(string id)
		{
			ISmartHouse sh = LoadFromStorage();

			return sh[id].ToString();
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}

		private ISmartHouse LoadFromStorage()
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

		private bool SaveToStorage(ISmartHouse sh)
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

		private SmartHouseConfig GetConfig()
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
			SmartHouseConfig shSection = (SmartHouseConfig)config.GetSection(SmartHouseConfig.SectionName);
			return shSection;
		}
	}
}
