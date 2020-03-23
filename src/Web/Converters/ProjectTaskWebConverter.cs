using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class ProjectTaskWebConverter : IConvertWeb<ProjectTaskVm, ProjectTaskDto>
    {
        public ProjectTaskVm Convert(ProjectTaskDto item)
        {
            return ConvertItem(item);
        }

        public ProjectTaskDto Convert(ProjectTaskVm item)
        {
            return ConvertItem(item);
        }

        public List<ProjectTaskVm> ConvertAll(List<ProjectTaskDto> items)
        {
            List<ProjectTaskVm> ProjectTasksVm = new List<ProjectTaskVm>();
            foreach (var item in items)
            {
                ProjectTasksVm.Add(ConvertItem(item));
            }

            return ProjectTasksVm;
        }

        public List<ProjectTaskDto> ConvertList(List<ProjectTaskVm> items)
        {
            List<ProjectTaskDto> ProjectTasksDto = new List<ProjectTaskDto>();
            foreach (var item in items)
            {
                ProjectTasksDto.Add(ConvertItem(item));
            }

            return ProjectTasksDto;
        }

        private ProjectTaskDto ConvertItem(ProjectTaskVm item)
        {
            return new ProjectTaskDto
            {
                Id = item.Id,
                ProjectShortName = item.ProjectShortName,
                Name = item.Name,
                TimeToComplete = item.TimeToComplete,
                BeginDate = item.BeginDate,
                EndDate = item.EndDate,
                EmployeeFullName = item.EmployeeFullName,
                Status = item.Status,
                ProjectId = item.ProjectId,
                EmployeeId = item.EmployeeId
            };
        }

        private ProjectTaskVm ConvertItem(ProjectTaskDto item)
        {
            return new ProjectTaskVm
            {
                Id = item.Id,
                ProjectShortName = item.ProjectShortName,
                Name = item.Name,
                TimeToComplete = item.TimeToComplete,
                BeginDate = item.BeginDate,
                EndDate = item.EndDate,
                EmployeeFullName = item.EmployeeFullName,
                Status = item.Status,
                ProjectId = item.ProjectId,
                EmployeeId = item.EmployeeId
            };
        }
    }
}
