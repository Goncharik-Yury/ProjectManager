using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TrainingTask.Infrastructure.DbOperators
{
    public class DbOperator : IDbOperator
    {
        public string GetConnectionString()
        {
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"C:\SeleSt\Programs\Projects\Database\TestTaskDB.mdf",
                IntegratedSecurity = true,
                ConnectTimeout = 30
            };

            return builder.ToString();
            return null; // TODO: write using appsettings.json
        }

        public bool ExecuteNonQuery(string sqlQueryString, List<SqlParameter> queryParameters = null)
        {
            using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    DBConnection.Open();
                    using SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    if (queryParameters != null)
                    {
                        foreach (var Parameter in queryParameters)
                        {
                            if (Parameter.Value == null)
                            {
                                Parameter.Value = "";
                            }
                            CommandToExecute.Parameters.Add(Parameter);
                        }
                    }
                    CommandToExecute.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return true;
        }

        public DataTable GetData(string sqlQueryString, List<SqlParameter> queryParameters = null)
        {
            using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    DBConnection.Open();
                    SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    if (queryParameters != null)
                    {
                        foreach (var Parameter in queryParameters)
                        {
                            CommandToExecute.Parameters.Add(Parameter);
                        }
                    }
                    DataTable DataTable = new DataTable();

                    new SqlDataAdapter(CommandToExecute).Fill(DataTable);
                    return DataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //protected abstract IEnumerable<T> DataParse(DataTable dataTable);
    }
}
