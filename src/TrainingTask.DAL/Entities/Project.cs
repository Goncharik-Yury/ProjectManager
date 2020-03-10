using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.DAL.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
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
