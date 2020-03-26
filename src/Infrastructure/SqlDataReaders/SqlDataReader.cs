using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TrainingTask.Infrastructure.SqlDataReaders
{
    public abstract class SqlDataReader<T> : ISqlDataReader<T>
    {
        protected SqlDataReader(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
        //protected string ConnectionString
        //{
        //    get { }
        //    set { }
        //}

        public void ExecuteNonQuery(string sqlQueryString, IList<SqlParameter> queryParameters = null)
        {
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
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
                    throw; // TODO: realize appropriate exception handling
                }
            }
        }

        public IList<T> GetData(string sqlQueryString, IList<SqlParameter> queryParameters = null)
        {
            using (SqlConnection DBConnection = new SqlConnection(ConnectionString))
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

                    SqlDataReader Reader = CommandToExecute.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        List<T> DataAll = new List<T>();
                        while (Reader.Read())
                        {
                            DataAll.Add(DataParse(Reader));
                        }
                        Reader.Close();
                        return DataAll;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw; // TODO: realize correct
                }
            }
        }

        protected abstract T DataParse(SqlDataReader sqlDataReader);
    }
}
