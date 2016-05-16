using Lab16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab16.Controllers
{
	public class HomeController : Controller
	{
		private CompanyContext db = new CompanyContext();
		public ActionResult Index()
		{
			return View(db.Workers);
		}
	}
}
