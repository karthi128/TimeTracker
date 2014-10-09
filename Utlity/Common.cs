using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FederalTimeTracker.Utlity
{
    public  static class Common

    {
        public static string GetMonthName(int month, bool abbrev) { DateTime date = new DateTime(2000, month, 1); if (abbrev) return date.ToString("MMM"); return date.ToString("MMMM"); }

        public static Dictionary<int, String> GetMonthsLookup(bool abbrev)
        {
           var monthList = new Dictionary<int, String>();
            for (int i = 1; i < 13; i++)
            {
                monthList.Add(i, FederalTimeTracker.Utlity.Common.GetMonthName(i, abbrev));
            }
            return monthList;
       }

       public static List<int> GetYearLookup()
       {
          List<int> years= new List<int>();
           for(int i=2013; i<= System.DateTime.Now.Year;i++) 
           {

               years.Add(i);
           }
           return years;
       }

      

       public static Dictionary<int, String> GetTimesheetStatus(bool addDefault)
       {
           var TimesheetStatus = new Dictionary<int, String>();
           if (addDefault) TimesheetStatus.Add(0, "--- Status----");
           TimesheetStatus.Add(1, "Submitted");
           TimesheetStatus.Add(2, "Approved");
           TimesheetStatus.Add(3, "Request Update");
           
           return TimesheetStatus;
       }

    }
}