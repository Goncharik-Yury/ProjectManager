using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Web.ViewModels
{
    public class ProjectTaskFilledVm
    {
        public ProjectTaskVm ProjectTasks { get; set; }
        public SelectList EmployeeSelectList { get; set; }
        public SelectList ProjectSelectList { get; set; }
        public string[] ProjectTaskStatuses { get; set; }
        
    }
}
