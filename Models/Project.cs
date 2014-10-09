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
    
    public partial class Project
    {
        public Project()
        {
            this.MyProjects = new HashSet<MyProject>();
            this.TimeEntries = new HashSet<TimeEntry>();
            this.ProjectYearEOs = new HashSet<ProjectYearEO>();
        }
    
        public int ProjectId { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public bool Active { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Note { get; set; }
    
        public virtual ICollection<MyProject> MyProjects { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
        public virtual ICollection<ProjectYearEO> ProjectYearEOs { get; set; }
    }
}