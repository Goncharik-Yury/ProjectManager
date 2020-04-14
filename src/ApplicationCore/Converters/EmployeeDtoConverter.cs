using System;
using System.Collections.Generic;
using System.Text;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Infrastructure.Models;

namespace ProjectManager.ApplicationCore.Converters
{
    public class EmployeeDtoConverter : IConvert<Employee, EmployeeDto>
    {
        public EmployeeDto Convert(Employee item)
        {
            return new EmployeeDto
            {
                Id = item.Id,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Patronymic = item.Patronymic,
                Position = item.Position
            };
        }
        public IList<EmployeeDto> Convert(IList<Employee> items)
        {
            List<EmployeeDto> EmployeesDto = new List<EmployeeDto>();
            foreach (var item in items)
            {
                EmployeesDto.Add(Convert(item));
            }

            return EmployeesDto;
        }
    }
}
