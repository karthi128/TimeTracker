using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FederalTimeTracker.Models;
using Microsoft.Ajax.Utilities;

namespace FederalTimeTracker.Controllers
{
    public class EmployeeController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /Employee/
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.Managers = db.Employees.ToList();
            var employee = new Employee();

            employee.Active = true;
            return View(employee);
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName,FirstName,LastName,MiddleName,Title,Role,EmployeeType,EmailID,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Note,ReportingTo,SAMAccountName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //Employee e = new Employee();
                //e.FullName = employee.FullName;
                //e.FirstName = employee.FirstName;
                //e.LastName = employee.LastName;
                //e.MiddleName = employee.MiddleName;
                //e.Title = employee.Title;
                //e.Role = employee.Role;
                //e.EmployeeType = employee.EmployeeType;
                //e.EmailID = employee.EmailID;
                //e.Active = employee.Active;
                //e.CreatedBy = User.Identity.Name;
                //e.CreatedDate = DateTime.Now; ;
                //e.Note = employee.Note;
                //e.ReportingTo = employee.ReportingTo;
                //e.SAMAccountName = employee.SAMAccountName;


                employee.CreatedBy = User.Identity.Name;
                employee.CreatedDate = DateTime.Now;
                if (String.IsNullOrEmpty(employee.Note)) employee.Note = " ";

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.Managers = db.Employees.ToList();
            return PartialView(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            ViewBag.Managers = db.Employees.ToList();
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FullName,FirstName,LastName,MiddleName,Title,Role,EmployeeType,EmailID,Active,ModifiedBy,ModifiedDate,Note,SAMAccountName", Exclude = "CreatedBy,CreatedDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.CreatedBy = employee.ModifiedBy = User.Identity.Name;
                employee.CreatedDate = System.DateTime.Now;
                employee.ModifiedDate = System.DateTime.Now;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Managers = db.Employees.ToList();
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Employee/SalaryIndex
        public ActionResult SalaryIndex(int? id)
        {
            Employee employee = db.Employees.Find(id);

            //using (var context = new ProjectContext())
            //{
            //    context.Entry(employee).Reference(p => p.Salaries).Load();

            //}


            return PartialView("PartialSalaryIndex", employee.Salaries);
        }

        // GET: /Employee/GetEmployeeInfo
        [HttpPost, ActionName("GetEmployeeInfo")]
        public ActionResult GetEmployeeInfo([Bind(Include = "SAMAccountName,")] Employee employee)
        {

            using (var context = new PrincipalContext(ContextType.Domain, "net.dos.state.fl.us"))
            {
                var userPrincipal = new UserPrincipal(context) { PasswordNeverExpires = false };
                employee.SAMAccountName = "";

                //if (employee != null &&
                //    employee.SAMAccountName.IndexOf("\\", System.StringComparison.OrdinalIgnoreCase) > 0)
                //{
                //    string[] separators = { "\\" };

                //    userPrincipal.SamAccountName =
                //        employee.SAMAccountName.Split(separators, StringSplitOptions.RemoveEmptyEntries)[1];
                //}
                //else if (employee != null)
                //{
                    userPrincipal.SamAccountName = "KRSwamy"; //TODO: employee.SAMAccountName.Trim().ToLower();
                //}



                using (var searcher = new PrincipalSearcher(userPrincipal))
                {
                    //searcher.QueryFilter = 
                    foreach (var result in searcher.FindAll())
                    {
                        var de = result.GetUnderlyingObject() as DirectoryEntry;



                        if (de != null)
                        {
                            ViewBag.Managers = db.Employees.ToList();
                            //var employee = new Employee();
                            employee.SAMAccountName = String.Format("{0}", de.Properties["samAccountName"].Value);
                            employee.FullName = String.Format("{0}", de.Properties["displayName"].Value);
                            employee.FirstName = String.Format("{0}", de.Properties["givenName"].Value);
                            employee.LastName = String.Format("{0}", de.Properties["sn"].Value);
                            employee.EmailID = String.Format("{0}", de.Properties["Mail"].Value);


                            employee.Active = true;
                            if (ModelState.ContainsKey("{key}"))
                                ModelState["{key}"].Errors.Clear();

                            foreach (var modelValue in ModelState.Values)
                            {
                                modelValue.Errors.Clear();
                            }

                            return PartialView("Create", employee);

                            Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                            Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                            Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                            Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                            Console.WriteLine("Group type: " + de.Properties["groupType"].Value);
                            Console.WriteLine("____________________________");
                            Console.WriteLine();
                        }
                    }
                }
            }


            return PartialView("Create", employee);
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
