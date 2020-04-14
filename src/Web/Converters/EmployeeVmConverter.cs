using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.ApplicationCore.Dto;
using ProjectManager.Common;
using ProjectManager.Web.Common;
using ProjectManager.Web.ViewModels;

namespace ProjectManager.Web.Converters
{
    public class EmployeeVmConverter : IConvert<EmployeeDto, EmployeeVm>
    {
        public EmployeeVm Convert(EmployeeDto item)
        {
            return new EmployeeVm
            {
                Id = item.Id,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Patronymic = item.Patronymic,
                Position = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), item.Position)
            };
        }

        public IList<EmployeeVm> Convert(IList<EmployeeDto> items)
        {
            List<EmployeeVm> EmployeesVm = new List<EmployeeVm>();
            foreach (var item in items)
            {
                EmployeesVm.Add(Convert(item));
            }

            return EmployeesVm;
        }
    }
}
