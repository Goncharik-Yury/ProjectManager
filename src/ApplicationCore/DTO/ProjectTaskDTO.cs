using System;

namespace TrainingTask.ApplicationCore.DTO
{
    public class ProjectTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeToComplete { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int ExecutorId { get; set; }
    }
}
