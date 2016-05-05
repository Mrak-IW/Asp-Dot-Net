using Homework2.Models;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
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
		Assembly modelAssembly = Assembly.Load("SmartHouse");

		public ActionResult Index()
		{
			SmartHouseLayout shl;
			ViewBag.Title = "Home Page";

			shl = LoadLayout();

			return View(shl as object);
		}

		private SmartHouseLayout LoadLayout()
		{
			ISmartHouse sh;
			SmartHouseLayout shl = new SmartHouseLayout();
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

			shl.SmartHouse = sh;
			shl.TypesAvailable = LoadAvailableDevTypes();

			return shl;
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
