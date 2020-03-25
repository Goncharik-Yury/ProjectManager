using System;
using System.Collections.Generic;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
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

        public IList<ProjectDto> ConvertAll(IList<ProjectVm> items)
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
