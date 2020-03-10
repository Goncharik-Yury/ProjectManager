using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrainingTask.WEB.Models
{
    public class ProjectTaskViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Project task name")]
        [Required(ErrorMessage = "Enter Project task name")]
        [MaxLength(50, ErrorMessage = "Project Task name should not be longer than 50 simbols")]
        public string Name { get; set; }
        public int TimeToComplete { get; set; }
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Choose the status of project")]
        [MaxLength(50, ErrorMessage = "Status should not be longer than 50 simbols")]
        public string Status { get; set; } // TODO: change to TaskStatus type
        public int ExecutorId { get; set; }
    }
}
