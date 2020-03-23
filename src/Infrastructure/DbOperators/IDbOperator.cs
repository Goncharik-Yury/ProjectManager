using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TrainingTask.Infrastructure.DbOperators
{
    public interface IDbOperator
    {
        string GetConnectionString();
        bool ExecuteNonQuery(string sqlQueryString, List<SqlParameter> queryParameters = null);
        DataTable GetData(string sqlQueryString, List<SqlParameter> queryParameters = null);
    }
}
