using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectAndTaskVM
    {
        public List<ProjectVM> Projects { get; set; }
        public List<ProjectTaskVM> ProjectTasks { get; set; }
        //public ProjectTaskVMWithRelations ProjectTasks { get; set; }
        //public List<EmployeeVM> Employee { get; set; }
    }
}
