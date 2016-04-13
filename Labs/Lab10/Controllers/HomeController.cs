using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab10.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Task1(int param = 0, string message = null)
		{
			if (param > 0)
			{
				return View("Task1_pos");
			}
			else if (param < 0)
			{
				return View("Task1_neg");
			}

			if (message != null)
			{
				ViewBag.message = message;
			}

			return View();
		}

		public ActionResult Task2(int border1 = 0, int border2 = 10, bool typedView = false)
		{
			int min, max;
			int[] arr;

			if (border1 > border2)
			{
				min = border2;
				max = border1;
			}
			else
			{
				min = border1;
				max = border2;
			}

			arr = new int[max - min + 1];
			for (int i = 0; i <= max - min; i++)
			{
				arr[i] = i + min;
			}

			if (!typedView)
			{
				ViewBag.arr = arr;
				return View();
			}

			return View("Task2_typed", arr);
		}

		public ActionResult Task4()
		{
			return View();
		}

		public ActionResult Task4Partial1(string message)
		{
			return View((object)message);
		}
	}
}