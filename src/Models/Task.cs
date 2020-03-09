using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_task.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Display(Name = "Task name")]
        [Required(ErrorMessage = "Enter task name")]
        [MaxLength(50, ErrorMessage = "Task name should not be longer than 50 simbols")]
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

        public Task()
        {
            Id = 66;
            Name = "name";
            TimeToComplete = 1;
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
            Status = "0";
            ExecutorId = 0;
        }

        public Task(int id, string name, int timeToComplete, DateTime beginDate, DateTime endDate, string status, int executorId = 0)
        {
            Id = id;
            Name = name;
            TimeToComplete = timeToComplete;
            BeginDate = beginDate;
            EndDate = endDate;
            Status = status;
            ExecutorId = executorId;
        }
    }
}
