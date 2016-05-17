using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using System.Web.Configuration;
using System.IO;
using System.Web.Hosting;

using Homework2.Config;
using Homework2.Models;
using Homework2.Constants;
using Homework2.Views.ViewHelpers;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace Homework2.Controllers
{
	public class HomeController : Controller
	{
		const string title = "Smart House MVC";

		Assembly modelAssembly = Assembly.Load("SmartHouse");

		public ActionResult Index()
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			return View(shContext as object);
		}

		public ActionResult ShowCreateDeviceForm(string selectDeviceType)
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			shContext.DevCreationContext = GetDeviceCreationContext(selectDeviceType);

			return View("Index", shContext as object);
		}

		public ActionResult CreateDeviceFormPartial(string devType)
		{
			return PartialView("Parts/CreateDeviceForm/CreateDeviceForm", GetDeviceCreationContext(devType));
		}

		public ActionResult CreateDevice()
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();
			ISmartHouseCreator devCreator = Manufacture.GetManufacture(modelAssembly);

			string devType = Request.Params[CreateDeviceFields.devType];

			string devName = Request.Params[CreateDeviceFields.name];
			if (devName == "" || devName == null)
			{
				devName = CreateDevicePlaceholders.name;
			}

			ISmartDevice dev = devCreator.CreateDevice(devType, devName);

			if (dev != null)
			{
				FillIHaveThermostat(dev);
				FillIBrightable(dev);

				shContext.SmartHouse.AddDevice(dev);
			}

			SmartHouseConfig shConfig = GetConfig();
			if (!shConfig.UseSession)
			{
				SaveToStorage(shContext.SmartHouse);
			}

			return View("Index", shContext as object);
		}

		public ActionResult TogglePower(string id)
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			switch (shContext.SmartHouse[id].State)
			{
				case EPowerState.On:
					shContext.SmartHouse[id].State = EPowerState.Off;
					break;
				case EPowerState.Off:
					shContext.SmartHouse[id].State = EPowerState.On;
					break;
			}

			SmartHouseConfig shConfig = GetConfig();
			if (!shConfig.UseSession)
			{
				SaveToStorage(shContext.SmartHouse);
			}

			return View("Index", shContext as object);
		}

		public ActionResult ToggleOpenClose(string id)
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			ISmartDevice dev = shContext.SmartHouse[id];
			if (dev is IOpenCloseable)
			{
				(dev as IOpenCloseable).IsOpened ^= true;
			}

			SmartHouseConfig shConfig = GetConfig();
			if (!shConfig.UseSession)
			{
				SaveToStorage(shContext.SmartHouse);
			}

			return View("Index", shContext as object);
		}

		public ActionResult Delete(string id)
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			shContext.SmartHouse.RemoveDevice(id);

			SmartHouseConfig shConfig = GetConfig();
			if (!shConfig.UseSession)
			{
				SaveToStorage(shContext.SmartHouse);
			}

			return View("Index", shContext as object);
		}

		public ActionResult AdjustTemperature(string id, string direction)
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			ISmartDevice dev = shContext.SmartHouse[id];

			if (dev is IHaveThermostat)
			{
				IHaveThermostat thermo = dev as IHaveThermostat;
				switch (direction)
				{
					case AdjustDirections.increase:
						thermo.IncreaseTemperature();
						break;
					case AdjustDirections.decrease:
						thermo.DecreaseTemperature();
						break;
				}
			}

			SmartHouseConfig shConfig = GetConfig();
			if (!shConfig.UseSession)
			{
				SaveToStorage(shContext.SmartHouse);
			}

			return View("Index", shContext as object);
		}

		public ActionResult AdjustBrightness(string id, string direction)
		{
			ViewBag.Title = title;

			SmartHouseContext shContext = LoadContext();

			ISmartDevice dev = shContext.SmartHouse[id];

			if (dev is IBrightable)
			{
				IBrightable thermo = dev as IBrightable;
				switch (direction)
				{
					case AdjustDirections.increase:
						thermo.IncreaseBrightness();
						break;
					case AdjustDirections.decrease:
						thermo.DecreaseBrightness();
						break;
				}
			}

			SmartHouseConfig shConfig = GetConfig();
			if (!shConfig.UseSession)
			{
				SaveToStorage(shContext.SmartHouse);
			}

			return View("Index", shContext as object);
		}

		private SmartHouseContext LoadContext()
		{
			ISmartHouse sh;
			SmartHouseContext shContext = new SmartHouseContext();
			SmartHouseConfig shConfig = GetConfig();

			if (shConfig.UseSession)
			{
				sh = Session["SmartHouse"] as ISmartHouse;
			}
			else
			{
				sh = LoadFromStorage();
			}

			if (sh == null)
			{
				sh = CreateTestSet();
				if (shConfig.UseSession)
				{
					Session.Add("SmartHouse", sh);
				}
				else
				{
					SaveToStorage(sh);
				}
			}

			shContext.SmartHouse = sh;
			shContext.TypesAvailable = LoadAvailableDevTypes();
			shContext.DevCreationContext = null;

			return shContext;
		}

		private ISmartHouse CreateTestSet()
		{
			ISmartHouseCreator shc = Manufacture.GetManufacture(modelAssembly);

			ISmartHouse sh = null;

			ISmartDevice dev;
			IBrightable ibri;
			IHaveThermostat iterm;

			sh = shc.CreateSmartHouse();

			dev = shc.CreateDevice("SmartLamp", "l1");

			ibri = dev as IBrightable;
			ibri.BrightnessMax = 100;
			ibri.BrightnessMin = 10;
			ibri.BrightnessStep = 10;
			ibri.Brightness = ibri.BrightnessMax;
			sh.AddDevice(dev);

			dev = shc.CreateDevice("SmartLamp", "l2");

			ibri = dev as IBrightable;
			ibri.BrightnessMax = 100;
			ibri.BrightnessMin = 10;
			ibri.BrightnessStep = 15;
			ibri.Brightness = ibri.BrightnessMax;
			sh.AddDevice(dev);

			dev = shc.CreateDevice("Fridge", "fr1");

			iterm = dev as IHaveThermostat;
			iterm.TempMax = 0;
			iterm.TempMin = -5;
			iterm.TempStep = 1;
			dev.On();
			iterm.DecreaseTemperature();
			sh.AddDevice(dev);

			dev = shc.CreateDevice("Clock", "clk1");

			dev.On();
			sh.AddDevice(dev);

			return sh;
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

		private IList<DevTypesDescription> LoadAvailableDevTypes()
		{
			IList<DevTypesDescription> result = new List<DevTypesDescription>();
			ISmartDevice dev;

			var devTypes = from t in modelAssembly.GetTypes()
						   where t.GetInterfaces().Contains(typeof(ISmartDevice)) && !t.IsAbstract
						   select t;

			foreach (var type in devTypes)
			{

				object[] constructorParams = new object[1];
				constructorParams[0] = "dummy";
				dev = Activator.CreateInstance(type, constructorParams) as ISmartDevice;

				DevTypesDescription li = new DevTypesDescription();
				li.className = type.Name;
				li.nameTranslation = dev.DeviceType;

				result.Add(li);
			}
			return result;
		}

		private void FillIHaveThermostat(ISmartDevice dev)
		{
			if (dev is IHaveThermostat)
			{
				IHaveThermostat idev = dev as IHaveThermostat;

				int max;
				int min;
				int step;

				if (!int.TryParse(Request.Params[CreateDeviceFields.temperatureMax], out max))
				{
					max = CreateDevicePlaceholders.temperatureMax;
				}
				if (!int.TryParse(Request.Params[CreateDeviceFields.temperatureMin], out min))
				{
					min = CreateDevicePlaceholders.temperatureMin;
				}
				if (!int.TryParse(Request.Params[CreateDeviceFields.temperatureStep], out step))
				{
					step = CreateDevicePlaceholders.temperatureStep;
				}

				idev.TempMin = min;
				idev.TempMax = max;
				idev.TempStep = step;
			}
		}

		private void FillIBrightable(ISmartDevice dev)
		{
			if (dev is IBrightable)
			{
				IBrightable idev = dev as IBrightable;

				int max;
				int min;
				int step;

				if (!int.TryParse(Request.Params[CreateDeviceFields.brightnessMax], out max))
				{
					max = CreateDevicePlaceholders.brightnessMax;
				}
				if (!int.TryParse(Request.Params[CreateDeviceFields.brightnessMin], out min))
				{
					min = CreateDevicePlaceholders.brightnessMin;
				}
				if (!int.TryParse(Request.Params[CreateDeviceFields.brightnessStep], out step))
				{
					step = CreateDevicePlaceholders.brightnessStep;
				}

				idev.BrightnessMin = min;
				idev.BrightnessMax = max;
				idev.BrightnessStep = step;
			}
		}

		private SmartHouseConfig GetConfig()
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
			SmartHouseConfig shSection = (SmartHouseConfig)config.GetSection(SmartHouseConfig.SectionName);
			return shSection;
		}

		private DeviceCreationContext GetDeviceCreationContext(string devType)
		{
			DeviceCreationContext dcc = null;
			ISmartHouseCreator devCreator = Manufacture.GetManufacture(modelAssembly);

			ISmartDevice dev = devCreator.CreateDevice(devType, "dummy");

			if (dev != null)
			{
				dcc = new DeviceCreationContext();
				dcc.DevTypeTranslation = dev.DeviceType;
				dcc.DevType = devType;
				dcc.DevIsBrightable = dev is IBrightable;
				dcc.DevHasThermostat = dev is IHaveThermostat;
			}

			return dcc;
		}
	}
}
