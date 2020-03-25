using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.Converters
{
    public class ProjectTaskConverter : IConvert<ProjectTaskDto, ProjectTask>
    {
        public ProjectTask Convert(ProjectTaskDto item)
        {
            return new ProjectTask
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
        public IList<ProjectTask> ConvertAll(IList<ProjectTaskDto> items)
        {
            List<ProjectTask> ProjectTasksDto = new List<ProjectTask>();
            foreach (var item in items)
            {
                ProjectTasksDto.Add(Convert(item));
            }

            return ProjectTasksDto;
        }
    }
}
