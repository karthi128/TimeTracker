using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FederalTimeTracker.Models;

namespace FederalTimeTracker.Controllers
{
    public class MyProjectController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /MyProject/
        public ActionResult Index()
        {

            var myprojects = db.MyProjects.Include(m => m.Project).Where(m => m.EmployeeEmployeeId == EmployeeId);
            return PartialView(myprojects.ToList().OrderByDescending(s => s.Project.ProjectName));
        }

        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        private int EmployeeId
        {
            get
            {
                int id;
                var emp = (db.Employees.FirstOrDefault(m => m.SAMAccountName == User.Identity.Name));
                if (emp == null) id = -1; //TOOD: throw Exception
                else
                {
                    id = emp.EmployeeId;
                }
                return id;
            }
        }

        private int YearId
        {
            get
            {
                int id;
                int selectedyear;
                if (Session["SelectedYear"] == null)
                {
                    selectedyear = System.DateTime.Now.Year;

                }
                else
                {
                    selectedyear = Convert.ToInt16(Session["SelectedYear"].ToString());
                }

                id = selectedyear; //TODO: Change to retrive FY Year

                return id;
            }
        }


        // GET: /MyProject/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyProject myproject = await db.MyProjects.FindAsync(id);
            if (myproject == null)
            {
                return HttpNotFound();
            }
            return View(myproject);
        }

        // GET: /MyProject/Create
        public ActionResult Create()
        {
            LoadAvaliableProjects(new MyProject());
            return PartialView("Create");
        }

        // POST: /MyProject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectProjectId", Exclude = "MyProjectId,EmployeeEmployeeId")] MyProject myproject)
        {
            myproject.EmployeeEmployeeId = EmployeeId;

            var myprojects = db.MyProjects.Include(m => m.Project).Where(m => m.EmployeeEmployeeId == EmployeeId && m.ProjectProjectId == myproject.ProjectProjectId);
            if (ModelState.IsValid)
            {
                if (!myprojects.Any())
                {
                    db.MyProjects.Add(myproject);
                    db.SaveChanges();
                }
                //return Json(new { value = "Sucesse" }, JsonRequestBehavior.AllowGet);
                LoadAvaliableProjects(myproject);
                return PartialView("Create", new MyProject());
            }
            var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
            ModelState.AddModelError("Error", errors.ToString());
            LoadAvaliableProjects(myproject);
            return PartialView("Create", myproject);
        }

        /// <summary>
        /// Loads the avaliable projects.
        /// </summary>
        /// <param name="myproject">The myproject.</param>
        private void LoadAvaliableProjects(MyProject myproject)
        {
            List<int> myprojects = db.MyProjects.Select(q => q.ProjectProjectId).ToList();
            var avaliableProjects = db.Projects.Where(q => !myprojects.Contains(q.ProjectId));

            ViewBag.ProjectProjectId = (myproject == null) ?
                (new SelectList(avaliableProjects, "ProjectId", "ProjectName")) :
                 new SelectList(avaliableProjects, "ProjectId", "ProjectName", myproject.ProjectProjectId);



        }


        // GET: /MyProject/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyProject myproject = db.MyProjects.Find(id);


            if (myproject != null)
            {
                try
                {
                    db.MyProjects.Remove(myproject);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    LoadAvaliableProjects(myproject);
                    return PartialView("Create", new MyProject());
                }

            }

            LoadAvaliableProjects(myproject);
            return PartialView("Create", new MyProject());


        }

        // POST: /MyProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MyProject myproject = await db.MyProjects.FindAsync(id);
            db.MyProjects.Remove(myproject);
            await db.SaveChangesAsync();
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
