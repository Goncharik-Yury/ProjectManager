using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class ProjectTaskDtoConverter : IConvert<ProjectTaskVm, ProjectTaskDto>
    {
        public ProjectTaskDto Convert(ProjectTaskVm item)
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

        public IList<ProjectTaskDto> ConvertAll(IList<ProjectTaskVm> items)
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
