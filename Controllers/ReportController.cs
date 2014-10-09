using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FederalTimeTracker.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonthlyOffset(int? year,int? month)
        {
            if (year == null) year = System.DateTime.Now.Year;
            if (month == null) month = System.DateTime.Now.Month;
            ViewBag.Month = month;
            ViewBag.MonthName = FederalTimeTracker.Utlity.Common.GetMonthName((int)month, false);
            ViewBag.Year = year;
            Session["SelectedYear"] = year;
            ViewBag.MonthList = FederalTimeTracker.Utlity.Common.GetMonthsLookup(false);
            ViewBag.YearList = FederalTimeTracker.Utlity.Common.GetYearLookup();
          
            return PartialView();
        }
	}
}