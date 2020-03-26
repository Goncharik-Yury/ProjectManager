using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.Converters
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
        public IList<ProjectTaskDto> ConvertAll(IList<ProjectTask> items)
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
