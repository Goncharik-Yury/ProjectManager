using System;
using System.Collections.Generic;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Controllers;
using ProjectManager.Web.Common;
using ProjectManager.Web.ViewModels;

namespace ProjectManager.Web.Converters
{
    public class ProjectTaskVmConverter : IConvert<ProjectTaskDto, ProjectTaskVm>
    {
        public ProjectTaskVm Convert(ProjectTaskDto item)
        {
            return new ProjectTaskVm
            {
                Id = item.Id,
                Name = item.Name,
                TimeToComplete = item.TimeToComplete,
                BeginDate = item.BeginDate,
                EndDate = item.EndDate,
                Status = (ProjectTaskStatus)Enum.Parse(typeof(ProjectTaskStatus), item.Status),
                ProjectId = item.ProjectId,
                EmployeeId = item.EmployeeId
            };
        }

        public IList<ProjectTaskVm> Convert(IList<ProjectTaskDto> items)
        {
            if (items == null)
                return null;
            List<ProjectTaskVm> ProjectTasksDto = new List<ProjectTaskVm>();
            foreach (var item in items)
            {
                ProjectTasksDto.Add(Convert(item));
            }

            return ProjectTasksDto;
        }
    }
}
