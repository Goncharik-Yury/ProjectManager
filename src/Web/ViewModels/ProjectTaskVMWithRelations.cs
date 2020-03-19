using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectTaskVMWithRelations
    {
        public ProjectTaskVM ProjectTask { get; set; }
        public EmployeeVM Employee { get; set; }
    }
}
