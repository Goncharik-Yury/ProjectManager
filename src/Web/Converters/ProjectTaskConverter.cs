using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Functional
{
    public static class ProjectTaskConverter
    {
        public static List<ProjectTaskVM> DTOtoVM(List<ProjectTaskDTO> convert)
        {
            List<ProjectTaskVM> converted = new List<ProjectTaskVM>();

            foreach (var item in convert)
            {
                ProjectTaskVM listItem = new ProjectTaskVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    TimeToComplete = item.TimeToComplete,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                    Status = item.Status,
                    ProjectId = item.ProjectId
                };

                converted.Add(listItem);
            }

            return converted;
        }

        public static ProjectTaskDTO VMToDTO(ProjectTaskVM convert)
        {
            ProjectTaskDTO converted = new ProjectTaskDTO
            {
                Id = convert.Id,
                Name = convert.Name,
                TimeToComplete = convert.TimeToComplete,
                BeginDate = convert.BeginDate,
                EndDate = convert.EndDate,
                Status = convert.Status,
                ProjectId = convert.ProjectId
            };

            return converted;
        }
    }
}
