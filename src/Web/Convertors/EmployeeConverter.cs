﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.Models;

namespace TrainingTask.Web.Converters
{
    public static class EmployeeConverter
    {
        public static List<EmployeeViewModel> EmployeeDTOtoViewModel(List<EmployeeDTO> convert)
        {
            List<EmployeeViewModel> converted = new List<EmployeeViewModel>();
            foreach (var item in convert)
            {
                EmployeeViewModel listItem = new EmployeeViewModel
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

        public static EmployeeDTO EmployeeViewModelToDTO(EmployeeViewModel convert)
        {
            EmployeeDTO converted = new EmployeeDTO
            {
                Id = convert.Id,
                LastName = convert.LastName,
                FirstName = convert.FirstName,
                Patronymic = convert.Patronymic,
                Position = convert.Position
            };

            return converted;
        }
    }
}
