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
    public class ProjectController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /Project/
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProjectId,EffectiveDate,ClosedDate,Active,ProjectName,Description,Note")] Project project)
        {
            if (!string.IsNullOrEmpty(project.ProjectName))
                
            // UniqueProjectValidation(project);

            if (ModelState.IsValid)
            {
                project.CreatedBy = User.Identity.Name;
                project.CreatedDate = System.DateTime.Now;

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

      

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProjectId,EffectiveDate,ClosedDate,Active,ProjectName,Description,ModifiedBy,ModifiedDate,Note",Exclude="CreatedBy,CreatedDate")] Project project)
        {

            ModelState.Remove("CreatedBy");
            ModelState.Remove("CreatedDate");
            //UniqueProjectValidation(project);
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                project.ModifiedDate = DateTime.Now;
                project.ModifiedBy = User.Identity.Name;
                db.Entry(project).Property(x => x.CreatedBy).IsModified = false;
                db.Entry(project).Property(x => x.CreatedDate).IsModified = false;
               // project.CreatedDate = DateTime.Now;
                //project.CreatedBy = User.Identity.Name;

              


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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


        /// <summary>
        /// Unique project validation.
        /// </summary>
        /// <param name="project">The project.</param>
        private void UniqueProjectValidation(Project project)
        {
            TimeTrackerContainer db = new TimeTrackerContainer();
            var projectWithTheSameName = db.Projects.SingleOrDefault(
                u => u.ProjectName == project.ProjectName.Trim() && u.ProjectId != project.ProjectId);


            if (projectWithTheSameName != null)
            {
                ModelState.AddModelError("ProjectName", "This Project is already exists.");
            }
        }
    }
}
