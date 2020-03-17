namespace TrainingTask.Infrastructure.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public int ProjectTaskId { get; set; }
    }
}
