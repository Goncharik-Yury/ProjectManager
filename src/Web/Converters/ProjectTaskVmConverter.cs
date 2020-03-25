﻿using System;
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class ProjectTaskVmConverter : IConvert<ProjectTaskDto, ProjectTaskVm>
    {
        public ProjectTaskVm Convert(ProjectTaskDto item)
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

        public IList<ProjectTaskVm> ConvertAll(IList<ProjectTaskDto> items)
        {
            List<ProjectTaskVm> ProjectTasksDto = new List<ProjectTaskVm>();
            foreach (var item in items)
            {
                ProjectTasksDto.Add(Convert(item));
            }

            return ProjectTasksDto;
        }
    }
}