using TrainingTask.ApplicationCore.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;
using TrainingTask.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace TrainingTask.ApplicationCore.Repository
{
    public class EmployeeService : IService<EmployeeDto>
    {
        private readonly ILogger logger;

        private readonly IRepository<Employee> EmployeeRepository;
        private readonly IConvert<Employee, EmployeeDto> EmployeeDtoConverter;
        private readonly IConvert<EmployeeDto, Employee> EmployeeConverter;

        public EmployeeService(ILogger logger, IRepository<Employee> employeeRepository, IConvert<Employee, EmployeeDto> employeeDtoConverter, IConvert<EmployeeDto, Employee> employeeConverter)
        {
            this.logger = logger;
            EmployeeRepository = employeeRepository;
            EmployeeDtoConverter = employeeDtoConverter;
            EmployeeConverter = employeeConverter;
        }

        public void Create(EmployeeDto item)
        {
            logger.LogDebug(this.GetType() + ".Create is called");
            Employee Employee = EmployeeConverter.Convert(item);
            EmployeeRepository.Create(Employee);
        }

        public void Delete(int id)
        {
            logger.LogDebug(this.GetType() + ".Delete is called");
            EmployeeRepository.Delete(id);
        }

        public EmployeeDto Get(int id)
        {
            logger.LogDebug(this.GetType() + ".Get is called");
            Employee Employee = EmployeeRepository.Get(id);
            EmployeeDto EmployeeDto = EmployeeDtoConverter.Convert(Employee);
            return EmployeeDto;
        }

        public IList<EmployeeDto> GetAll()
        {
            logger.LogDebug(this.GetType() + ".GetAll is called");
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
            logger.LogDebug(this.GetType() + ".Update is called");
            Employee Employee = EmployeeConverter.Convert(item);
            EmployeeRepository.Update(Employee);
        }
    }
}
