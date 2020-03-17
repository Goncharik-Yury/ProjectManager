using System;
using System.ComponentModel.DataAnnotations;


namespace TrainingTask.Infrastructure.Models
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
        public string Status { get; set; }
        public int ProjectId { get; set; }
    }
}
