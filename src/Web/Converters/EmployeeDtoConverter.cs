using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class EmployeeDtoConverter : IConvert<EmployeeVm, EmployeeDto>
    {
        public EmployeeDto Convert(EmployeeVm item)
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

        public IList<EmployeeDto> Convert(IList<EmployeeVm> items)
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
