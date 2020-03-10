using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.DAL.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeToComplete { get; set; }
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // TODO: change to TaskStatus type
        public int ExecutorId { get; set; }

        public ProjectTask()
        {
            Id = 66;
            Name = "name";
            TimeToComplete = 1;
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
            Status = "0";
            ExecutorId = 0;
        }

        public ProjectTask(int id, string name, int timeToComplete, DateTime beginDate, DateTime endDate, string status, int executorId = 0)
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
