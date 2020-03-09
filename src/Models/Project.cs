using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training_task.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "Project name")]
        [Required(ErrorMessage = "Enter the Project name")]
        [MaxLength(50, ErrorMessage = "Project name should not be longer than 50 simbols")]
        public string Name { get; set; }
        [Display(Name = "Short name")]
        [MaxLength(50, ErrorMessage = "Short name should not be longer than 50 simbols")]
        public string ShortName { get; set; }
        [Display(Name = "Description")]
        [MaxLength(3999, ErrorMessage = "Description should not be longer than 3999 simbols")]
        public string Description { get; set; }

        public Project()
        {
            Id = 0;
            Name = "Name";
            ShortName = "ShortName";
            Description = "Description";
        }

        public Project(int id, string name, string shortName, string description)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
            Description = description;
        }
    }
}
