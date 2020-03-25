using TrainingTask.ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;

namespace TrainingTask.ApplicationCore.Repository
{
    public class EmployeeRepositoryService : IRepositoryService<EmployeeDto>
    {
        IRepository<Employee> EmployeeRepository;
        IConvert<Employee, EmployeeDto> EmployeeDtoConverter;
        IConvert<EmployeeDto, Employee> EmployeeConverter;
        public EmployeeRepositoryService(
            IRepository<Employee> employeeRepository,
        IConvert<Employee, EmployeeDto> employeeDtoConverter,
        IConvert<EmployeeDto, Employee> employeeConverter
            )
        {
            EmployeeRepository = employeeRepository;
            EmployeeDtoConverter = employeeDtoConverter;
            EmployeeConverter = employeeConverter;
        }

        public void Create(EmployeeDto item)
        {
            Employee Employee = EmployeeConverter.Convert(item);
            EmployeeRepository.Create(Employee);
        }

        public void Delete(int id)
        {
            EmployeeRepository.Delete(id);
        }

        public EmployeeDto GetSingle(int id)
        {
            Employee Employee = EmployeeRepository.GetSingle(id);
            EmployeeDto EmployeeDto = EmployeeDtoConverter.Convert(Employee);
            return EmployeeDto;
        }

        public IList<EmployeeDto> GetAll()
        {
            IList<Employee> Employees = EmployeeRepository.GetAll();
            List<EmployeeDto> EmployeesDto = new List<EmployeeDto>();
            foreach (var item in Employees)
            {
                EmployeesDto.Add(EmployeeDtoConverter.Convert(item));
            }
            return EmployeesDto;
        }

        public void Update(EmployeeDto item)
        {
            Employee Employee = EmployeeConverter.Convert(item);
            EmployeeRepository.Update(Employee);
        }
    }
}
