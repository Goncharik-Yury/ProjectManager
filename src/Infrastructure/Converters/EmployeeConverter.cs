using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Common;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Converters
{
    class EmployeeConverter : IConvert<SqlDataReader, Employee>
    {
        public Employee Convert(SqlDataReader item)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> ConvertAll(IList<SqlDataReader> items)
        {
            throw new NotImplementedException();
        }
    }
}
