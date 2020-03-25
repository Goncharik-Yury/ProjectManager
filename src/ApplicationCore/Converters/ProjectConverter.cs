﻿using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.Converters
{
    public class ProjectConverter : IConvert<ProjectDto, Project>
    {
        public Project Convert(ProjectDto item)
        {
            return new Project
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
        public IList<Project> ConvertAll(IList<ProjectDto> items)
        {
            List<Project> ProjectsDto = new List<Project>();
            foreach (var item in items)
            {
                ProjectsDto.Add(Convert(item));
            }

            return ProjectsDto;
        }
    }
}