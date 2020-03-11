using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.Models;

namespace TrainingTask.Web.Functional
{
    public static class ProjectTaskConverter
    {
        public static List<ProjectTaskViewModel> ProjectTaskDTOtoViewModel(List<ProjectTaskDTO> convert)
        {
            List<ProjectTaskViewModel> converted = new List<ProjectTaskViewModel>();
            foreach (var item in convert)
            {
                ProjectTaskViewModel listItem = new ProjectTaskViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    TimeToComplete = item.TimeToComplete,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                    Status = item.Status,
                    ExecutorId = item.ExecutorId
                };

                converted.Add(listItem);
            }

            return converted;
        }

        public static ProjectTaskDTO ProjectTaskViewModelToDTO(ProjectTaskViewModel convert)
        {
            ProjectTaskDTO converted = new ProjectTaskDTO
            {
                Id = convert.Id,
                Name = convert.Name,
                TimeToComplete = convert.TimeToComplete,
                BeginDate = convert.BeginDate,
                EndDate = convert.EndDate,
                Status = convert.Status,
                ExecutorId = convert.ExecutorId
            };

            return converted;
        }
    }
}
