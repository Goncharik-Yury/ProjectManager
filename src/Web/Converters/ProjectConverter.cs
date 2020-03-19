using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public static class ProjectConverter
    {
        public static List<ProjectVM> DTOtoVMList(List<ProjectDTO> convert)
        {
            List<ProjectVM> converted = new List<ProjectVM>();
            foreach (var item in convert)
            {
                ProjectVM listItem = new ProjectVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    ShortName = item.ShortName,
                    Description = item.Description
                };
                converted.Add(listItem);
            }

            return converted;
        }
        //public static ProjectVM DTOtoVM(ProjectDTO convert)
        //{
        //    ProjectVM converted = new ProjectVM
        //    {
        //        Id = convert.Id,
        //        Name = convert.Name,
        //        ShortName = convert.ShortName,
        //        Description = convert.Description
        //    };

        //    return converted;
        //}

        public static ProjectDTO VMToDTO(ProjectVM convert)
    {
        ProjectDTO Project = new ProjectDTO
        {
            Id = convert.Id,
            Name = convert.Name,
            ShortName = convert.ShortName,
            Description = convert.Description
        };

        return Project;
    }
}
}
