using System;
using System.Collections.Generic;
using System.Text;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Infrastructure.Models;

namespace ProjectManager.ApplicationCore.Converters
{
    public class ProjectDtoConverter : IConvert<Project, ProjectDto>
    {
        public ProjectDto Convert(Project item)
        {
            return new ProjectDto
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
        public IList<ProjectDto> Convert(IList<Project> items)
        {
            List<ProjectDto> ProjectsDto = new List<ProjectDto>();
            foreach (var item in items)
            {
                ProjectsDto.Add(Convert(item));
            }

            return ProjectsDto;
        }
    }
}
