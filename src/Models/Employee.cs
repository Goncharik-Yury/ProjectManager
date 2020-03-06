using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_task.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }

        public Employee()
        {
            LastName = "Second name";
            FirstName = "First name";
            MiddleName = "Middle name";
            Position = "Position";
        }

        public Employee(int Id, string LastName, string FirstName, string MiddleName, string Position)
        {
            this.Id = Id;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.Position = Position;
        }

    }
}
