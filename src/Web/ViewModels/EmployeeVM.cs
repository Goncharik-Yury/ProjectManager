﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace TrainingTask.Web.ViewModels
{
    public class EmployeeVM
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

        public string Patronymic { get; set; }
        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter the position")]
        [MaxLength(50, ErrorMessage = "Position should not be longer than 50 simbols")]

        public string Position { get; set; }
    }
}
