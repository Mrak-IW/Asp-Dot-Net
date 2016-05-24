using Lab16.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

		[HttpGet]
		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Add(Staff worker)
		{
			db.Workers.Add(worker);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}
			Staff worker = db.Workers.Find(id);
			if (worker == null)
			{
				return HttpNotFound();
			}
			return View(worker);
		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Staff worker = db.Workers.Find(id);
			if (worker == null)
			{
				return HttpNotFound();
			}
			db.Workers.Remove(worker);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			return View(db.Workers.Where(worker => worker.Id == id).FirstOrDefault());
		}

		[HttpPost]
		public ActionResult Edit(Staff worker)
		{
			db.Entry(worker).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
