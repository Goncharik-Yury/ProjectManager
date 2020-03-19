using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectAndTaskViewModel
    {
        public List<ProjectViewModel> Projects { get; set; }
        public List<ProjectTaskViewModel> ProjectTasks { get; set; }
        //public ProjectTaskViewModelWithRelations ProjectTasks { get; set; }
        //public List<EmployeeViewModel> Employee { get; set; }
    }
}
