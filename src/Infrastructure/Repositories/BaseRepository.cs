using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TrainingTask.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected abstract string ConnectionString { get; }

        public void ExecuteNonQuery(string sqlQueryString, List<SqlParameter> queryParameters = null)
        {
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    DBConnection.Open();
                    using SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    if (queryParameters != null)
                    {
                        queryParameters.ForEach(x => { if (x.Value == null) { x.Value = DBNull.Value; } });
                        CommandToExecute.Parameters.AddRange(queryParameters.ToArray());
                    }
                    CommandToExecute.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw; // TODO: realize appropriate exception handling
                }
            }
        }

        public IList<T> GetData(string sqlQueryString, Func<SqlDataReader, List<T>> converter, List<SqlParameter> queryParameters = null)
        {
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
            {
                DBConnection.Open();
                SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                if (queryParameters != null)
                {
                    queryParameters.ForEach(x => { if (x.Value == null) { x.Value = DBNull.Value; } });
                    CommandToExecute.Parameters.AddRange(queryParameters.ToArray());
                }

                SqlDataReader Reader = CommandToExecute.ExecuteReader();
                List<T> EmployeesList = new List<T>();

                if (Reader.HasRows)
                {
                    EmployeesList = converter(Reader);
                    Reader.Close();
                }

                return EmployeesList;
            }
        }
    }
}
