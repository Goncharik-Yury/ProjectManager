using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Infrastructure.Models;

namespace ApplicationCore.Converters
{
    class ProjectTaskBloConverter : IConvertBll<ProjectTask, ProjectTaskDto>
    {
        public ProjectTask Convert(ProjectTaskDto item)
        {
            return ConvertItem(item);
        }

        public ProjectTaskDto Convert(ProjectTask item)
        {
            return ConvertItem(item);
        }

        public List<ProjectTask> ConvertList(List<ProjectTaskDto> items)
        {
            List<ProjectTask> ProjectTasks = new List<ProjectTask>();
            foreach (var item in items)
            {
                ProjectTasks.Add(ConvertItem(item));
            }

            return ProjectTasks;
        }

        public List<ProjectTaskDto> ConvertList(List<ProjectTask> items)
        {
            List<ProjectTaskDto> ProjectTasksDto = new List<ProjectTaskDto>();
            foreach (var item in items)
            {
                ProjectTasksDto.Add(ConvertItem(item));
            }

            return ProjectTasksDto;
        }
        private ProjectTask ConvertItem(ProjectTaskDto item)
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
        private ProjectTaskDto ConvertItem(ProjectTask item)
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
    }
}
