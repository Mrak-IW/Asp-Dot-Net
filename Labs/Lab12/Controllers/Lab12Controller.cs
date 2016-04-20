using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab12.Models;

namespace Lab12.Controllers
{
	public class Lab12Controller : Controller
	{
		public ActionResult Index(string routeName, string parameter)
		{
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;
			output.someText = parameter;
			return View(output);
		}

		public ActionResult Task3(string routeName)
		{
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;
			return View("Index", output);
		}

		public ActionResult Task7(string routeName)
		{
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;

			output.someText = string.Format("controller = {0}; action = {1}", RouteData.Values["controller"], RouteData.Values["action"]);

			return View("Index", output);
		}

		public ActionResult Task8(string routeName, string param1, string param2)
		{
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;
			output.someText = string.Format("param1 = {0}; param2 = {1}", param1, param2);
			return View("Index", output);
		}

		public ActionResult Task9(string routeName, string catchall = "")
		{
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;
			output.someText = string.Format("params = \"{0}\"", catchall);
			return View("Index", output);
		}
	}


}