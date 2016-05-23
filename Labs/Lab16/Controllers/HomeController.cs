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

		public ActionResult Add()
		{
			return View();
		}

		public ActionResult Edit(int id)
		{
			return View(db.Workers.Where(worker => worker.Id == id).FirstOrDefault());
		}
	}
}
