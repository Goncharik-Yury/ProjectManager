using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectTaskViewModelWithRelations
    {
        public ProjectTaskViewModel ProjectTask { get; set; }
        public EmployeeViewModel Employee { get; set; }
    }
}
