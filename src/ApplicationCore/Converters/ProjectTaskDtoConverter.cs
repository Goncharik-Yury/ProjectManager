using System.Collections.Generic;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Infrastructure.Models;

namespace ProjectManager.ApplicationCore.Converters
{
    public class ProjectTaskDtoConverter : IConvert<ProjectTask, ProjectTaskDto>
    {
        public ProjectTaskDto Convert(ProjectTask item)
        {
            return new ProjectTaskDto
            {
                Id = item.Id,
                Name = item.Name,
                TimeToComplete = item.TimeToComplete,
                BeginDate = item.BeginDate,
                EndDate = item.EndDate,
                Status = item.Status,
                ProjectId = item.ProjectId,
                EmployeeId = item.EmployeeId
            };
        }
        public IList<ProjectTaskDto> Convert(IList<ProjectTask> items)
        {
            List<ProjectTaskDto> ProjectTasksDto = new List<ProjectTaskDto>();
            if (items == null)
                return null;
            foreach (var item in items)
            {
                ProjectTasksDto.Add(Convert(item));
            }

            return ProjectTasksDto;
        }
    }
}
