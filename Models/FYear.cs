//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FederalTimeTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FYear
    {
        public FYear()
        {
            this.TimeSheets = new HashSet<TimeSheet>();
            this.ProjectYearEOs = new HashSet<ProjectYearEO>();
        }
    
        public int FYearId { get; set; }
        public string FYearName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Current { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
        public virtual ICollection<ProjectYearEO> ProjectYearEOs { get; set; }
    }
}
