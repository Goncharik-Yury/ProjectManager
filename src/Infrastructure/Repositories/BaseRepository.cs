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
        protected readonly ILogger logger;

        protected BaseRepository(ILogger logger)
        {
            this.logger = logger;
        }

        public void ExecuteNonQuery(string sqlQueryString, List<SqlParameter> queryParameters = null)
        {
            logger.LogDebug(GetType() + ".ExecuteNonQuery is called");
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
            {
                try
                {
                    DBConnection.Open();
                    using SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    if (queryParameters != null)
                    {
                        CommandToExecute.Parameters.AddRange(queryParameters.ToArray());
                    }

                    CommandToExecute.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
        }

        public IList<T> GetData(string sqlQueryString, Func<SqlDataReader, IList<T>> converter, List<SqlParameter> queryParameters = null)
        {
            logger.LogDebug(GetType() + ".GetData is called");
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
            {
                DBConnection.Open();
                SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                if (queryParameters != null)
                {
                    CommandToExecute.Parameters.AddRange(queryParameters.ToArray());
                }

                SqlDataReader Reader = CommandToExecute.ExecuteReader();
                IList<T> EmployeesList = new List<T>();
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
