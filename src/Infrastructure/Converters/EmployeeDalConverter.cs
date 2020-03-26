using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Converters
{
    class EmployeeDalConverter : IConvertDal<Employee, DataTable>
    {
        public Employee Convert(DataTable item)
        {
            Employee Employee = GetRowItem(item.Rows[0]);
            item.Dispose();

            return Employee;
        }

        public IList<Employee> ConvertAll(DataTable items)
        {
            List<Employee> employees = new List<Employee>();
            foreach (DataRow DataRow in items.Rows)
            {
                employees.Add(GetRowItem(DataRow));
            }

            items.Dispose();

            return employees;
        }

        private Employee GetRowItem(DataRow DataRow)
        {
            Employee Employee = new Employee
            {
                Id = DataRow.Field<int>("Id"),
                LastName = DataRow.Field<string>("LastName"),
                FirstName = DataRow.Field<string>("FirstName"),
                Patronymic = DataRow.Field<string>("Patronymic"),
                Position = DataRow.Field<string>("Position")
            };

            return Employee;
        }
    }
}
