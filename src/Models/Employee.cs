using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_task.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Enter the Last name")]
        [MaxLength(50, ErrorMessage = "Last name should not be longer than 50 simbols")]
        public string LastName { get; set; }
        [Display(Name = "First name")]
        [Required(ErrorMessage = "Enter the First name")]
        [MaxLength(50, ErrorMessage = "First name should not be longer than 50 simbols")]
        public string FirstName { get; set; }
        [Display(Name = "Patronymic")]
        [MaxLength(50, ErrorMessage = "Patronymic should not be longer than 50 simbols")]
        public string Patronymic  { get; set; }
        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter the position")]
        [MaxLength(50, ErrorMessage = "Position should not be longer than 50 simbols")]
        public string Position { get; set; }

        public Employee()
        {
            LastName = "Second name";
            FirstName = "First name";
            Patronymic  = "Patronymic ";
            Position = "Position";
        }

        public Employee(int Id, string LastName, string FirstName, string Patronymic , string Position)
        {
            this.Id = Id;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.Patronymic  = Patronymic ;
            this.Position = Position;
        }

    }
}
