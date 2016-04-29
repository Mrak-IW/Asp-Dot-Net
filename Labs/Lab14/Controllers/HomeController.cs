using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab14.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

		public PartialViewResult Select(string[] list, int? id)
		{
			ViewBag.id = id;
			return PartialView(list);
		}
	}
}
