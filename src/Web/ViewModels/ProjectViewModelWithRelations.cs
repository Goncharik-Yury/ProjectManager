using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectViewModelWithRelations
    {
        public ProjectViewModel Project { get; set; }
        public List<ProjectTaskViewModelWithRelations> ProjectTasks { get; set; }
    }
}
