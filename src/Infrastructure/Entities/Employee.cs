using System;
using System.Collections.Generic;

namespace ProjectManager.Infrastructure.Models
{
    public class Employee
    {
        public Employee()
        {
            ProjectTask = new HashSet<ProjectTask>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }

        public virtual ICollection<ProjectTask> ProjectTask { get; set; }
    }
}
