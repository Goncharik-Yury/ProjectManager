using System;
using System.Collections.Generic;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Web.ViewModels;

namespace ProjectManager.Web.Converters
{
    public class ProjectDtoConverter : IConvert<ProjectVm, ProjectDto>
    {
        public ProjectDto Convert(ProjectVm item)
        {
            return new ProjectDto
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }

        public IList<ProjectDto> Convert(IList<ProjectVm> items)
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
