using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TrainingTask.Infrastructure.SqlDataReaders
{
    public interface ISqlDataReader<T>
    {
        //string GetConnectionString();
        void ExecuteNonQuery(string sqlQueryString, IList<SqlParameter> queryParameters = null);
        IList<T> GetData(string sqlQueryString, IList<SqlParameter> queryParameters = null);
    }
}
