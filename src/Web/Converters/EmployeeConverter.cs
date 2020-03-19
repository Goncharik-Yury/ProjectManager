using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public static class EmployeeConverter
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
                    Position = item.Position,
                    ProjectTaskId = item.ProjectTaskId
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
                ProjectTaskId = convert.ProjectTaskId
            };

            return converted;
        }
    }
}
