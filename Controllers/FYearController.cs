using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FederalTimeTracker.Models;

namespace FederalTimeTracker.Controllers
{
    public class FYearController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /FYear/
        public ActionResult Index()
        {
            return View(db.FYears.ToList());
        }

        // GET: /FYear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FYear fyear = db.FYears.Find(id);
            if (fyear == null)
            {
                return HttpNotFound();
            }
            return View(fyear);
        }

        // GET: /FYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FYear/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FYearId,FYearName,Description,Active,Current,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Note")] FYear fyear)
        {
            if (ModelState.IsValid)
            {
                db.FYears.Add(fyear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fyear);
        }

        // GET: /FYear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FYear fyear = db.FYears.Find(id);
            if (fyear == null)
            {
                return HttpNotFound();
            }
            return View(fyear);
        }

        // POST: /FYear/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FYearId,FYearName,Description,Active,Current,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Note")] FYear fyear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fyear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fyear);
        }

        // GET: /FYear/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FYear fyear = db.FYears.Find(id);
            if (fyear == null)
            {
                return HttpNotFound();
            }
            return View(fyear);
        }

        // POST: /FYear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FYear fyear = db.FYears.Find(id);
            db.FYears.Remove(fyear);
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
