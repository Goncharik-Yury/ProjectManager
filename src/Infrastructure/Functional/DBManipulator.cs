using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TrainingTask.Infrastructure.Functional
{
    public abstract class DBManipulator
    {
        public object CustomQuery(string query)
        {
            return DBGetData(query);
        }
        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"C:\SeleSt\Programs\Projects\Database\TestTaskDB.mdf",
                //AttachDBFilename = @"C:\_Programs\CSharp\TrainingTask\Database\TestTaskDB.mdf",
                IntegratedSecurity = true,
                ConnectTimeout = 30
            };

            return builder.ToString();
        }

        protected static bool DBDoAction(string sqlQueryString, List<SqlParameter> queryParameters = null)
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

        //protected object DBGetData(string sqlQueryString, List<SqlParameter> queryParameters = null)
        //{
        //    using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
        //    {
        //        try
        //        {
        //            DBConnection.Open();
        //            SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
        //            if (queryParameters != null)
        //            {
        //                foreach (var Parameter in queryParameters)
        //                {
        //                    CommandToExecute.Parameters.Add(Parameter);
        //                }
        //            }

        //            DataTable DataReader = CommandToExecute.ExecuteReader();
        //            return DataParse(DataReader);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        protected object DBGetData(string sqlQueryString, List<SqlParameter> queryParameters = null)
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
                    return DataParse(DataTable);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //protected abstract object DataParse(SqlDataReader dataReader);
        protected abstract object DataParse(DataTable dataTable);
    }
}
