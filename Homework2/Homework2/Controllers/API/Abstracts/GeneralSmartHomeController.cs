﻿using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;
using System.Web;
using System.Reflection;
using System.Data.Entity;

using Homework2.Config;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;
using Homework2.Models;

namespace Homework2.Controllers.API.Abstracts
{
	public abstract class GeneralSmartHomeController : ApiController
	{
		Assembly modelAssembly = Assembly.Load("SmartHouse");

		protected ISmartHouse LoadSmartHouse()
		{
			ISmartHouse sh = null;
			SmartHouseConfig shConfig = GetConfig();
			ISmartHouseCreator shc = Manufacture.GetManufacture(modelAssembly);
			Type smartHouseType = shc.SmartHouseType;

			if (smartHouseType.IsSubclassOf(typeof(DbContext)))
			{
				sh = shc.CreateSmartHouse();
			}
			else
			{
				if (shConfig.UseSession)
				{
					var Session = HttpContext.Current.Session;
					sh = Session["SmartHouse"] as ISmartHouse;
				}
				else
				{
					sh = LoadFromStorage();
				}
			}
			return sh;
		}

		protected void SaveSmartHouse(ISmartHouse sh)
		{
			SmartHouseConfig shConfig = GetConfig();
			if (sh is DbContext)
			{
				(sh as DbContext).SaveChanges();
			}
			else
			{
				if (shConfig.UseSession)
				{
					var Session = HttpContext.Current.Session;
					Session.Add("SmartHouse", sh);
				}
				else
				{
					SaveToStorage(sh);
				}
			}
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

		protected SmartHouseConfig GetConfig()
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
			SmartHouseConfig shSection = (SmartHouseConfig)config.GetSection(SmartHouseConfig.SectionName);
			return shSection;
		}
	}
}
