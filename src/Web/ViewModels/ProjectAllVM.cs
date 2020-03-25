using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectAllVm
    {
        public ProjectVm Projects { get; set; }
        public IList<ProjectTaskVm> ProjectTasks { get; set; }
        //public List<EmployeeVm> Employees { get; set; }
    }
}
