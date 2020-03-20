using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public static class VMConverter
    {
        public static List<EmployeeVM> DTOtoVM(List<EmployeeDTO> convert)
        {
            List<EmployeeVM> converted = new List<EmployeeVM>();

            foreach (var item in convert)
            {
                EmployeeVM listItem = new EmployeeVM
                {
                    Id = item.Id,
                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    Patronymic = item.Patronymic,
                    Position = item.Position
                };

                converted.Add(listItem);
            }

            return converted;
        }

        public static List<ProjectVM> DTOtoVM(List<ProjectDTO> convert)
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

        public static List<ProjectTaskVM> DTOtoVM(List<ProjectTaskDTO> convert)
        {
            List<ProjectTaskVM> converted = new List<ProjectTaskVM>();

            foreach (var item in convert)
            {
                ProjectTaskVM listItem = new ProjectTaskVM
                {
                    Id = item.Id,
                    ProjectShortName = item.ProjectShortName,
                    Name = item.Name,
                    TimeToComplete = item.TimeToComplete,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                    EmployeeFullName = item.EmloyeeFullName,
                    Status = item.Status,
                    ProjectId = item.ProjectId,
                    EmployeeId = item.EmployeeId
                    
                };

                converted.Add(listItem);
            }

            return converted;
        }

        public static EmployeeDTO VMToDTO(EmployeeVM convert)
        {
            EmployeeDTO converted = new EmployeeDTO
            {
                Id = convert.Id,
                LastName = convert.LastName,
                FirstName = convert.FirstName,
                Patronymic = convert.Patronymic,
                Position = convert.Position,
            };

            return converted;
        }

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

        public static ProjectTaskDTO VMToDTO(ProjectTaskVM convert)
        {
            ProjectTaskDTO converted = new ProjectTaskDTO
            {
                Id = convert.Id,
                ProjectShortName = convert.ProjectShortName,
                Name = convert.Name,
                TimeToComplete = convert.TimeToComplete,
                BeginDate = convert.BeginDate,
                EndDate = convert.EndDate,
                EmloyeeFullName = convert.EmployeeFullName,
                Status = convert.Status,
                ProjectId = convert.ProjectId,
                EmployeeId = convert.EmployeeId
            };

            return converted;
        }
    }
}
