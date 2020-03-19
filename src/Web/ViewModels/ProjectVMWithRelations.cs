using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectVMWithRelations
    {
        public ProjectVM Project { get; set; }
        public List<ProjectTaskVMWithRelations> ProjectTasks { get; set; }
    }
}
