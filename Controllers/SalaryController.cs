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
    public class JsonResponseFactory
    {
        public static object ErrorResponse(string error)
        {
            return new { Success = false, ErrorMessage = error };
        }

        public static object SuccessResponse()
        {
            return new { Success = true };
        }

        public static object SuccessResponse(object referenceObject)
        {
            return new { Success = true, Object = referenceObject };
        }

    }

    public class SalaryController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /Salary/
        public  ActionResult Index(int id)
        {
            var salaries = db.Salaries.Where(s => s.EmployeeEmployeeId==id);
            return PartialView(salaries.ToList().OrderByDescending(s=>s.StartDate));
        }

        // GET: /Salary/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: /Salary/Create
        public ActionResult Create(int id)
        {
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName");
            Salary salary = new Salary();
            salary.EmployeeEmployeeId = id;
            salary.StartDate = System.DateTime.Now.Date;
            ViewBag.EmployeeEmployeeId = id;
            return PartialView("Create",salary);
        }

        // POST: /Salary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include = "SalaryId,EmployeeEmployeeId,StartDate,TotalSalary,Rate,PositionNumber,EO,IsITStaff,Note", Exclude = "CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Salary salary)
        {
            salary.CreatedBy = User.Identity.Name;
            salary.CreatedDate= DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Salaries.Add(salary);
                db.SaveChanges();
                var id = salary.EmployeeEmployeeId;
                salary = new Salary();
                salary.EmployeeEmployeeId = id;
                salary.StartDate = System.DateTime.Now.Date;
                ViewBag.EmployeeEmployeeId = id;
                //return PartialView("Create", salary); //Content("Sucessfully Added.");//return PartialView("Create");

                return PartialView("Create", salary);

            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList(); 

                ModelState.AddModelError("Error", errors.ToString());
                //HttpContext.Response.StatusCode = 500;
               // HttpContext.Response.Clear();
                ViewBag.EmployeeEmployeeId = salary.EmployeeEmployeeId;
                //return Json(salary);
                return PartialView("Create", salary);
                //return Json(JsonResponseFactory.ErrorResponse(errors.ToString()), JsonRequestBehavior.DenyGet);
            }
        }

        // GET: /Salary/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary =  db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", salary.EmployeeEmployeeId);
            return View(salary);
        }

        // POST: /Salary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit([Bind(Include="SalaryId,EmployeeEmployeeId,StartDate,TotalSalary,Rate,PositionNumber,EO,IsITStaff,Note,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", salary.EmployeeEmployeeId);
            return View(salary);
        }

        // GET: /Salary/Delete/5
        public  ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary =  db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: /Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            Salary salary =  db.Salaries.Find(id);
            db.Salaries.Remove(salary);
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
