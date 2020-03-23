using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectTaskVm
    {
        public int Id { get; set; }
        public string ProjectShortName { get; set; }
        [Display(Name = "Project task name")]
        [Required(ErrorMessage = "Enter Project task name")]
        [MaxLength(50, ErrorMessage = "Project Task name should not be longer than 50 simbols")]
        public string Name { get; set; }
        public int? TimeToComplete { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [MaxLength(50, ErrorMessage = "Status should not be longer than 50 simbols")]
        public string EmployeeFullName { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Choose ProjectId")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Choose EmployeeId")]
        public int? EmployeeId { get; set; } // TODO: why it cant write null in null allowed field in DB?
    }
}
