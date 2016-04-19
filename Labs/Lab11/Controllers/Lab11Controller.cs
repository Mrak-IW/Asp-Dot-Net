using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab11.Controllers
{
	public class Lab11Controller : Controller
	{
		public ActionResult Task1(int[] arr)
		{
			return View(arr);
		}

		[HttpGet]
		public ActionResult Task2()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Task2(int dummy = 0)
		{
			List<string> result = new List<string>();

			foreach (var p in Request.Form)
			{
				result.Add(p + " = " + Request.Form[p.ToString()]);
			}

			return View(result);
		}
	}
}