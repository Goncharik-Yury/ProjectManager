using System;

namespace TrainingTask.ApplicationCore.Dto
{
    public class ProjectTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TimeToComplete { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

    }
}
