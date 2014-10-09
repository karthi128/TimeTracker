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
    public class ProjectYearEOController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: /ProjectYearEO/
        public async Task<ActionResult> Index(int id)
        {
            var projectyeareo = db.ProjectYearEO.Include(p => p.FYear).Include(p => p.Project).Where(p=>p.ProjectProjectId==id);
            return PartialView(await projectyeareo.ToListAsync());
        }

        // GET: /ProjectYearEO/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectYearEO projectyeareo = await db.ProjectYearEO.FindAsync(id);
            if (projectyeareo == null)
            {
                return HttpNotFound();
            }
            return View(projectyeareo);
        }

        // GET: /ProjectYearEO/Create
        public ActionResult Create()
        {
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName");
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: /ProjectYearEO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ProjectYearEOId,EOCode,ProjectName,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Note,ProjectProjectId,FYearFYearId")] ProjectYearEO projectyeareo)
        {
            if (ModelState.IsValid)
            {
                db.ProjectYearEO.Add(projectyeareo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", projectyeareo.FYearFYearId);
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectyeareo.ProjectProjectId);
            return View(projectyeareo);
        }

        // GET: /ProjectYearEO/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectYearEO projectyeareo = await db.ProjectYearEO.FindAsync(id);
            if (projectyeareo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", projectyeareo.FYearFYearId);
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectyeareo.ProjectProjectId);
            return View(projectyeareo);
        }

        // POST: /ProjectYearEO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ProjectYearEOId,EOCode,ProjectName,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Note,ProjectProjectId,FYearFYearId")] ProjectYearEO projectyeareo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectyeareo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FYearFYearId = new SelectList(db.FYears, "FYearId", "FYearName", projectyeareo.FYearFYearId);
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectyeareo.ProjectProjectId);
            return View(projectyeareo);
        }

        // GET: /ProjectYearEO/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectYearEO projectyeareo = await db.ProjectYearEO.FindAsync(id);
            if (projectyeareo == null)
            {
                return HttpNotFound();
            }
            return View(projectyeareo);
        }

        // POST: /ProjectYearEO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProjectYearEO projectyeareo = await db.ProjectYearEO.FindAsync(id);
            db.ProjectYearEO.Remove(projectyeareo);
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
