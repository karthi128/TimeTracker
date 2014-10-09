using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Model Validaion and attributes
/// </summary>
namespace FederalTimeTracker.Models
{

    #region Project

    /// <summary>
    /// Project Model
    /// </summary>
    [MetadataType(typeof(ProjectMetadata))]
    public partial class Project
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.


    }

    /// <summary>
    /// Project Model Metadata
    /// </summary>
    public class ProjectMetadata
    {

        [Required]
        [Display(Name = "Project Name")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        [UniqueProjectAttribute("ProjectId", ErrorMessage = "This Project is already exists.")]
        public string ProjectName;

        [DataType(DataType.Text)]
        [MaxLength(2000)]
        public string Description;

        [Required]
        [Display(Name = "Effective Date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public string EffectiveDate;
        [Display(Name = "Closed Date")]
        [CompareDate("EffectiveDate", ErrorMessage = "Project Close Date should be greater than Project Effective Date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ClosedDate { get; set; }

    }

    #endregion


    #region Employee

    /// <summary>
    /// Employee Model
    /// </summary>
    [MetadataType(typeof(EmployeeMetadata))]

    public partial class Employee
    {
       

    }

    /// <summary>
    /// Employee Model Metadata
    /// </summary>
    public class EmployeeMetadata
    {

       
 
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Full Display Name")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string LastName { get; set; }
        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        [MaxLength(200)]
        public string MiddleName { get; set; }
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string Title { get; set; }
      
        [Display(Name = "User Role")]
        [Required]
        public Role Role { get; set; }
        [Display(Name = "Employee Type")]
        [Required]
        public EmployeeType EmployeeType { get; set; }
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailID { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        
        [DataType(DataType.Text)]
        [MaxLength(2000)]
        public string Note { get; set; }
        [Required]
        public Nullable<int> ReportingTo { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(2000)]
        [Display(Name = "LDAP Account Name")]
        [Required]
        public string SAMAccountName { get; set; }

        // Note this class has nothing in it.  It's just here to add the class-level attribute.
        [ForeignKey("ReportingTo")]
        [Display(Name = "Reporting To")]
        public Employee ReportingManager { get; set; }
    }

    #endregion

    #region Salary

    /// <summary>
    /// Project Model
    /// </summary>
    [MetadataType(typeof(SalaryMetadata))]
    public partial class Salary
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.


    }

    /// <summary>
    /// Project Model Metadata
    /// </summary>
    public class SalaryMetadata
    {

        [Required]
        public int EmployeeEmployeeId { get; set; }

        [Required]
        [Display(Name = "Effective Date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true,  DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime StartDate { get; set; }
        [Display(Name = "Salary")]
        [Required]
        [DataType(DataType.Currency)]
        public double TotalSalary { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Rate { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Display(Name = "Position #")]
        public string PositionNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string EO { get; set; }
        [Required]
        [DefaultValue(true)]
        [Display(Name = "IT Staff ?")]
        public bool IsITStaff { get; set; }
        [Required]
        public string Note { get; set; }

    }

    #endregion


    #region Salary

    /// <summary>
    /// Project Model 
    /// </summary>
    [MetadataType(typeof(MyProjectMetadata))]
    public partial class MyProject
    {
        // Note this class has nothing in it.  It's just here to add the class-level attribute.


    }

    /// <summary>
    /// MyProject Model Metadata
    /// </summary>
    public class MyProjectMetadata
    {

         [Required]
        public int EmployeeEmployeeId { get; set; }
         [Required]
         public int ProjectProjectId { get; set; }

    }

    #endregion

    #region Validation Attributes
    /// <summary>
    /// Unique Project Validation Attribute
    /// </summary>
    public class UniqueProjectAttribute : ValidationAttribute
    {
        private readonly string _other;
        public UniqueProjectAttribute(string other)
        {
            _other = other;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            if (otherValue == null)// in-Case of New Project
            {
                TimeTrackerContainer db = new TimeTrackerContainer();
                var projectWithTheSameName = db.Projects.SingleOrDefault(
                    u => u.ProjectName == (string)value);
                if (projectWithTheSameName == null)
                {
                    return null;
                }
                else return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            else// Edit Project Project
            {
                var projectId = Convert.ToInt16(otherValue);

                TimeTrackerContainer db = new TimeTrackerContainer();
                var projectWithTheSameName = db.Projects.SingleOrDefault(
                    u => u.ProjectName == (string)value && u.ProjectId != projectId);
                if (projectWithTheSameName == null)
                {
                    return null;
                }
                else return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

            }

            return null;
        }
    }

    /// <summary>
    /// Date Compare Validation Attribute
    /// </summary>
    public class CompareDate : ValidationAttribute
    {
        private readonly string _other;
        public CompareDate(string other)
        {
            _other = other;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            // at this stage you have "value" and "otherValue" pointing
            // to the value of the property on which this attribute
            // is applied and the value of the other property respectively
            // => you could do some checks
            if (value != null && otherValue != null && Convert.ToDateTime(value) < Convert.ToDateTime(otherValue))
            {
                // here we are verifying whether the 2 values are equal
                // but you could do any custom validation you like
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
    #endregion

}