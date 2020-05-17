using System;
using System.Collections.Generic;

namespace ProjectManager.Infrastructure.Models
{
    public class Project
    {
        public Project()
        {
            ProjectTask = new HashSet<ProjectTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProjectTask> ProjectTask { get; set; }
    }
}
