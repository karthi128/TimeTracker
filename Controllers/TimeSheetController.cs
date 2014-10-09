using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FederalTimeTracker.Attributes;
using FederalTimeTracker.Models;



namespace FederalTimeTracker.Controllers
{
    /// <summary>
    /// TimeSheet Controller
    /// </summary>
    public class TimeSheetController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /TimeSheet/
        public ActionResult Index(int? month, int? year, int? employeeId)
        {
            if (month == null)
            {
                month = System.DateTime.Now.Month;
            }

            if (year == null)
            {
                year = System.DateTime.Now.Year;
            }
            if (employeeId == null)
            {
                employeeId = 1;//               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Month = month;
            ViewBag.MonthName = FederalTimeTracker.Utlity.Common.GetMonthName((int)month, false);
            ViewBag.Year = year;
            Session["SelectedYear"] = year;
            ViewBag.MonthList = FederalTimeTracker.Utlity.Common.GetMonthsLookup(true);
            ViewBag.YearList = FederalTimeTracker.Utlity.Common.GetYearLookup();
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.TimesheetStatus = FederalTimeTracker.Utlity.Common.GetTimesheetStatus(true);
           

            var timesheets = db.TimeSheets.Include(t => t.Employee).Include(t => t.FYear).Where(t=>t.Status!=TimeSheetStatus.NotSubmitted);
            return View(timesheets.ToList());
        }

        // GET: /TimeSheet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // GET: /TimeSheet/Create
        public ActionResult Create()
        {
            db.Employees.Load();
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName");
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName");
            return View();
        }

        // POST: /TimeSheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TimeSheetId,Year,Month,SubmittedDate,ReviewedDate,EmployeeComments,ReviewerComments,Signature,FYearFYearId,EmployeeEmployeeId,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TimeSheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.TimeSheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", timesheet.EmployeeEmployeeId);
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", timesheet.FYearFYearId);
            return View(timesheet);
        }

        // GET: /TimeSheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            var myProjects =
                       from p in db.Projects
                       join eo in db.ProjectYearEO on p.ProjectId equals eo.ProjectProjectId into ps
                       from eo in ps.DefaultIfEmpty()
                       select new { Project = p, EOCode = eo == null ? "(No EOCode)" : eo.EOCode };

            var projectList = new List<string>();
            foreach (var p in myProjects)
            {
                projectList.Add(String.Format("{0}-{1}", p.Project.ProjectName, p.EOCode.ToString()));
            }
            ViewData["Projects"] = projectList;

            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", timesheet.EmployeeEmployeeId);
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", timesheet.FYearFYearId);

            ViewBag.Month = timesheet.Month;
            ViewBag.MonthName = FederalTimeTracker.Utlity.Common.GetMonthName((int)timesheet.Month, false);
            ViewBag.Year = timesheet.Year;

            ViewBag.MonthList = FederalTimeTracker.Utlity.Common.GetMonthsLookup(true);
            ViewBag.YearList = FederalTimeTracker.Utlity.Common.GetYearLookup();
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.TimesheetStatus = FederalTimeTracker.Utlity.Common.GetTimesheetStatus(true);
            var startDate = new DateTime((int)timesheet.Year, (int)timesheet.Month, 1);
            ViewData["DayStart"] = startDate;
            ViewData["DayEnd"] = startDate.AddMonths(1);

            return View(timesheet);
        }

        // POST: /TimeSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TimeSheetId,Year,Month,SubmittedDate,ReviewedDate,EmployeeComments,ReviewerComments,Signature,FYearFYearId,EmployeeEmployeeId,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TimeSheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", timesheet.EmployeeEmployeeId);
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", timesheet.FYearFYearId);
            return View(timesheet);
        }

        // GET: /TimeSheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: /TimeSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheet timesheet = db.TimeSheets.Find(id);
            db.TimeSheets.Remove(timesheet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: /TimeSheet/Edit/5
        /// <summary>
        /// Monthly TS .
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MonthlyTS(int? month, int? year, int? employeeId)
        {

           
            if (month == null)
            {
                month = System.DateTime.Now.Month;
            }

            if (year == null)
            {
                year = System.DateTime.Now.Year;
            }
            if (employeeId == null)
            {
                employeeId = 1;//               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var tsEmployee = db.Employees.FirstOrDefault(e => e.SAMAccountName.Equals(User.Identity.Name));
                if (tsEmployee == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {

                    employeeId = tsEmployee.EmployeeId;
                }

            }

            var timesheet = db.TimeSheets.Include(m => m.TimeEntries).FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeEmployeeId == employeeId);

            
            if (timesheet == null)
            {
                timesheet = new TimeSheet()
                {
                    Year = (short)year, Month = (short)month, EmployeeEmployeeId = (int)employeeId ,
                    CreatedBy = User.Identity.Name,CreatedDate = System.DateTime.Now,FYearFYearId = 2
                };
                //TODO: create new Monthly TS & FY yearID
                //return HttpNotFound();
            }

         
             var EmployeeEmployeeId = db.Employees
                    .Where(t => t.SAMAccountName == User.Identity.Name) //TODO: check kogged  in user/DropDown CValue
                    .AsEnumerable();

            if(!EmployeeEmployeeId.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.EmployeeEmployeeId = new SelectList(EmployeeEmployeeId, "EmployeeId", "FullName");
            SetViewData(month, year);

            var startDate = new DateTime((int)year, (int)month, 1);
            var dayEnd = startDate.AddMonths(1);

            for (var sdate = startDate; sdate < dayEnd; sdate = sdate.AddDays(1))
            {
                timesheet.TimeEntries.Add(new TimeEntry()
                {
                    CreatedBy = User.Identity.Name,
                    CreatedDate = System.DateTime.Now,
                    EntryType = EntryType.LSTA,
                    ProjectProjectId = 2,
                    TimeSheetTimeSheetId = timesheet.TimeSheetId,
                    TimeSheetDate = sdate
                });
            }
            return View(timesheet);
        }

        /// <summary>
        /// Sets the view data.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        private void SetViewData(int? month, int? year)
        {
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName");
            //var myProjects = new SelectList(db.Projects, "ProjectId", "ProjectName", timesheet.FYearFYearId);

            var myProjects =
                from m in db.MyProjects
                join p in db.Projects
                    on m.ProjectProjectId equals p.ProjectId
                join eo in db.ProjectYearEO
                    on m.ProjectProjectId equals eo.ProjectProjectId into ps
                from eo in ps.DefaultIfEmpty()
                select new {Project = p, EOCode = eo == null ? "(No EOCode)" : eo.EOCode};

            //var projectList = new List<string>();

            //foreach (var p in myProjects)
            //{
            //    projectList.Add(String.Format("{0}-{1}", p.Project.ProjectName, p.EOCode.ToString()));
            //}

            var projectList = myProjects.AsEnumerable().Select(m => String.Format("{0}-{1}", m.Project.ProjectName, m.EOCode))
                                .ToList();
            
            ViewData["Projects"] = projectList;

            ViewBag.MonthList = FederalTimeTracker.Utlity.Common.GetMonthsLookup(true);
            ViewBag.YearList = FederalTimeTracker.Utlity.Common.GetYearLookup();
            ViewBag.Month = month;
            ViewBag.MonthName = FederalTimeTracker.Utlity.Common.GetMonthName((int) month, false);
            ViewBag.Year = year;
            var startDate = new DateTime((int) year, (int) month, 1);
            ViewData["DayStart"] = startDate;
            var dayEnd = startDate.AddMonths(1);
            ViewData["DayEnd"] = dayEnd;

            ViewBag.MonthNext = dayEnd.Month;
            ViewBag.MonthNextName = FederalTimeTracker.Utlity.Common.GetMonthName(dayEnd.Month, true);
            ViewBag.YearNext = dayEnd.Year;
            var preMonth = startDate.AddMonths(-1);
            ViewBag.MonthPre = preMonth.Month;
            ViewBag.MonthPreName = FederalTimeTracker.Utlity.Common.GetMonthName(preMonth.Month, true);
            ViewBag.YearPre = preMonth.Year;
        }

        // POST: /TimeSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Monthly TS.
        /// </summary>
        /// <param name="timesheet">The timesheet.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("MonthlyTS")]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult MonthlyTS([Bind(Include = "TimeSheetId,Year,Month,SubmittedDate,ReviewedDate,EmployeeComments,ReviewerComments,Signature,FYearFYearId,EmployeeEmployeeId,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TimeSheet timesheet)
        {
            //timesheet.ReviewerComments = String.Format("{0}={1}.", DateTime.Now, timesheet.ReviewerComments);
            //timesheet.Signature = User.Identity.Name;
            timesheet.FYearFYearId = 2;


            if (ModelState.IsValid)
            {
                if (timesheet.TimeSheetId > 0)
                {
                    db.Entry(timesheet).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.TimeSheets.Add(timesheet);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", timesheet.EmployeeEmployeeId);
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", timesheet.FYearFYearId);
            return View(timesheet);
        }


        /// <summary>
        /// Monthly ts submit.
        /// </summary>
        /// <param name="timesheet">The timesheet.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("MonthlyTS")]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "Submit")]
        public ActionResult MonthlyTSSubmit([Bind(Include = "TimeSheetId,Year,Month,SubmittedDate,ReviewedDate,EmployeeComments,ReviewerComments,Signature,FYearFYearId,EmployeeEmployeeId,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] TimeSheet timesheet)
        {
            timesheet.ReviewerComments = String.Format("{0}={1}.", DateTime.Now, timesheet.ReviewerComments);
          
            timesheet.FYearFYearId = 2;
            timesheet.Status = TimeSheetStatus.Submitted;
            timesheet.SubmittedDate = DateTime.Now;
            if (String.IsNullOrEmpty(timesheet.Signature))
            {
                
               ModelState.AddModelError("Signature"," Please enter your password.");
            }

            
            if (ModelState.IsValid)
            {
                if (timesheet.TimeSheetId > 0)
                {
                    db.Entry(timesheet).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.TimeSheets.Add(timesheet);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            SetViewData(timesheet.Month, timesheet.Year);

           
            return View("MonthlyTS",timesheet);
        }



        /// <summary>
        /// Approves the specified timesheet.
        /// </summary>
        /// <param name="timesheet">The timesheet.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Approve([Bind(Include = "TimeSheetId,ReviewedDate,ReviewerComments,Signature,Status,ModifiedBy,ModifiedDate",Exclude="Year,Month,SubmittedDate,EmployeeComments,FYearFYearId,EmployeeEmployeeId,CreatedBy,CreatedDate")] TimeSheet timesheet)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeEmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", timesheet.EmployeeEmployeeId);
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", timesheet.FYearFYearId);
            return View(timesheet);
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
