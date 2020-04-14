using System;
using System.Collections.Generic;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Web.Common;
using ProjectManager.Web.ViewModels;

namespace ProjectManager.Web.Converters
{
    public class ProjectTaskDtoConverter : IConvert<ProjectTaskVm, ProjectTaskDto>
    {
        public ProjectTaskDto Convert(ProjectTaskVm item)
        {
            return new ProjectTaskDto
            {
                Id = item.Id,
                Name = item.Name,
                TimeToComplete = item.TimeToComplete,
                BeginDate = item.BeginDate,
                EndDate = item.EndDate,
                Status = item.Status.ToString(),
                ProjectId = item.ProjectId,
                EmployeeId = item.EmployeeId
            };
        }

        public IList<ProjectTaskDto> Convert(IList<ProjectTaskVm> items)
        {
            List<ProjectTaskDto> ProjectTasksDto = new List<ProjectTaskDto>();
            foreach (var item in items)
            {
                ProjectTasksDto.Add(Convert(item));
            }

            return ProjectTasksDto;
        }
    }
}
