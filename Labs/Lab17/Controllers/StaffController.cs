using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab17.Models;

namespace Lab17.Controllers
{
	public class StaffController : Controller
	{
		private StaffDbContext db = new StaffDbContext();

		// GET: Staff
		public ActionResult Index()
		{
			var workers = db.Workers.Include(s => s.Company);
			return View(workers.ToList());
		}

		// GET: Staff/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Staff staff = db.Workers.Find(id);
			if (staff == null)
			{
				return HttpNotFound();
			}
			return View(staff);
		}

		// GET: Staff/Create
		public ActionResult Create()
		{
			ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name");
			return View();
		}

		// POST: Staff/Create
		[HttpPost]
		public ActionResult Create([Bind(Include = "Id,Name,Surname,Position,CompanyID")] Staff staff)
		{
			if (ModelState.IsValid)
			{
				db.Workers.Add(staff);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", staff.CompanyID);
			return View(staff);
		}

		// GET: Staff/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Staff staff = db.Workers.Find(id);
			if (staff == null)
			{
				return HttpNotFound();
			}
			ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", staff.CompanyID);
			return View(staff);
		}

		// POST: Staff/Edit/5
		[HttpPost]
		public ActionResult Edit([Bind(Include = "Id,Name,Surname,Position,CompanyID")] Staff staff)
		{
			if (ModelState.IsValid)
			{
				db.Entry(staff).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", staff.CompanyID);
			return View(staff);
		}

		// GET: Staff/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Staff staff = db.Workers.Find(id);
			if (staff == null)
			{
				return HttpNotFound();
			}
			return View(staff);
		}

		// POST: Staff/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Staff staff = db.Workers.Find(id);
			db.Workers.Remove(staff);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
