using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrainingTask.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public string Position { get; set; }

        public Employee()
        {
            LastName = "Second name";
            FirstName = "First name";
            Patronymic = "Patronymic ";
            Position = "Position";
        }

        public Employee(int Id, string LastName, string FirstName, string Patronymic, string Position)
        {
            this.Id = Id;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.Patronymic = Patronymic;
            this.Position = Position;
        }
    }
}
