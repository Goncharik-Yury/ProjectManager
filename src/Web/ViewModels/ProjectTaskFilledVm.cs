using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManager.Web.ViewModels
{
    public class ProjectTaskFilledVm
    {
        public ProjectTaskVm ProjectTasks { get; set; }
        public SelectList EmployeeSelectList { get; set; }
        public SelectList ProjectSelectList { get; set; }
        public string[] ProjectTaskStatuses { get; set; } // TODO: Delete
        public string Redirect { get; set; } // TODO: Rename?
    }
}
