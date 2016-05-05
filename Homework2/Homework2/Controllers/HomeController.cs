using Homework2.Models;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Homework2.Controllers
{
	public class HomeController : Controller
	{
		const string increase = "up";
		const string decrease = "down";

		Assembly modelAssembly = Assembly.Load("SmartHouse");

		public ActionResult Index()
		{
			ViewBag.Title = "Smart House MVC";

			SmartHouseContext shContext = LoadContext();

			return View(shContext as object);
		}

		public ActionResult TogglePower(string id)
		{
			ViewBag.Title = "Smart House MVC";

			SmartHouseContext shContext = LoadContext();

			switch(shContext.SmartHouse[id].State)
			{
				case EPowerState.On:
					shContext.SmartHouse[id].State = EPowerState.Off;
					break;
				case EPowerState.Off:
					shContext.SmartHouse[id].State = EPowerState.On;
					break;
			}

			return View("Index", shContext as object);
		}

		public ActionResult ToggleOpenClose(string id)
		{
			ViewBag.Title = "Smart House MVC";

			SmartHouseContext shContext = LoadContext();

			ISmartDevice dev = shContext.SmartHouse[id];
			if (dev is IOpenCloseable)
			{
				(dev as IOpenCloseable).IsOpened ^= true;
			}

			return View("Index", shContext as object);
		}

		public ActionResult Delete(string id)
		{
			ViewBag.Title = "Smart House MVC";

			SmartHouseContext shContext = LoadContext();

			shContext.SmartHouse.RemoveDevice(id);

			return View("Index", shContext as object);
		}

		public ActionResult AdjustTemperature(string id, string direction)
		{
			ViewBag.Title = "Smart House MVC";

			SmartHouseContext shContext = LoadContext();

			ISmartDevice dev = shContext.SmartHouse[id];

			if (dev is IHaveThermostat)
			{
				IHaveThermostat thermo = dev as IHaveThermostat;
				switch (direction)
				{
					case increase:
						thermo.IncreaseTemperature();
						break;
					case decrease:
						thermo.DecreaseTemperature();
						break;
				}
			}

			return View("Index", shContext as object);
		}

		public ActionResult AdjustBrightness(string id, string direction)
		{
			ViewBag.Title = "Smart House MVC";

			SmartHouseContext shContext = LoadContext();

			ISmartDevice dev = shContext.SmartHouse[id];

			if (dev is IBrightable)
			{
				IBrightable thermo = dev as IBrightable;
				switch (direction)
				{
					case increase:
						thermo.IncreaseBrightness();
						break;
					case decrease:
						thermo.DecreaseBrightness();
						break;
				}
			}

			return View("Index", shContext as object);
		}

		private SmartHouseContext LoadContext()
		{
			ISmartHouse sh;
			SmartHouseContext shContext = new SmartHouseContext();
			ISmartHouseCreator shc = Manufacture.GetManufacture(modelAssembly);

			if (Session["SmartHouse"] == null)
			{
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

				Session.Add("SmartHouse", sh);
			}
			else
			{
				sh = Session["SmartHouse"] as ISmartHouse;
			}

			shContext.SmartHouse = sh;
			shContext.TypesAvailable = LoadAvailableDevTypes();

			return shContext;
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
	}
}
