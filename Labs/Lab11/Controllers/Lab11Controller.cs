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
	}
}