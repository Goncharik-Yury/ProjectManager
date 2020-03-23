using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Infrastructure.Models;

namespace ApplicationCore.Converters
{
    public class EmployeeBloConverter : IConvertBll<Employee, EmployeeDto>
    {
        public Employee Convert(EmployeeDto item)
        {
            return ConvertItem(item);
        }

        public EmployeeDto Convert(Employee item)
        {
            return ConvertItem(item);
        }

        public List<Employee> ConvertList(List<EmployeeDto> items)
        {
            List<Employee> Employees = new List<Employee>();
            foreach (var item in items)
            {
                Employees.Add(ConvertItem(item));
            }

            return Employees;
        }

        public List<EmployeeDto> ConvertList(List<Employee> items)
        {
            List<EmployeeDto> EmployeesDto = new List<EmployeeDto>();
            foreach (var item in items)
            {
                EmployeesDto.Add(ConvertItem(item));
            }

            return EmployeesDto;
        }
        private Employee ConvertItem(EmployeeDto item)
        {
            return new Employee
            {
                Id = item.Id,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Patronymic = item.Patronymic,
                Position = item.Position
            };
        }
        private EmployeeDto ConvertItem(Employee item)
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
    }
}
