using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectAllVM
    {
        public List<ProjectVM> Projects { get; set; }
        public List<ProjectTaskVM> ProjectTasks { get; set; }
        //public List<EmployeeVM> Employees { get; set; }
    }
}
