using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Web.ViewModels;

namespace TrainingTask.Web.Converters
{
    public class EmployeeWebConverter : IConvertWeb<EmployeeVm, EmployeeDto>
    {
        public EmployeeVm Convert(EmployeeDto item)
        {
            return ConvertItem(item);
        }

        public EmployeeDto Convert(EmployeeVm item)
        {
            return ConvertItem(item);
        }

        public List<EmployeeVm> ConvertAll(List<EmployeeDto> items)
        {
            List<EmployeeVm> EmployeesVm = new List<EmployeeVm>();
            foreach (var item in items)
            {
                EmployeesVm.Add(ConvertItem(item));
            }

            return EmployeesVm;
        }

        public List<EmployeeDto> ConvertList(List<EmployeeVm> items)
        {
            List<EmployeeDto> EmployeesDto = new List<EmployeeDto>();
            foreach (var item in items)
            {
                EmployeesDto.Add(ConvertItem(item));
            }

            return EmployeesDto;
        }

        private EmployeeDto ConvertItem(EmployeeVm item)
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

        private EmployeeVm ConvertItem(EmployeeDto item)
        {
            return new EmployeeVm
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
