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
            /* @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SeleSt\Programs\Projects\Database\TestTaskDB.mdf;Integrated Security=True;Connect Timeout=30"*//*connectionString*/
            ;
        }
        public string ConnectionString { get; set; }
        //protected string ConnectionString
        //{
        //    get { return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SeleSt\Programs\Projects\Database\TestTaskDB.mdf;Integrated Security=True;Connect Timeout=30"; }
        //    set { }
        //}

        //public string GetConnectionString() // TODO: rewrite using appsettings.json
        //{

        //    SqlConnectionStringBuilder builder =
        //    new SqlConnectionStringBuilder
        //    {
        //        DataSource = @"(LocalDB)\MSSQLLocalDB",
        //        AttachDBFilename = @"C:\SeleSt\Programs\Projects\Database\TestTaskDB.mdf",
        //        IntegratedSecurity = true,
        //        ConnectTimeout = 30
        //    };

        //    return builder.ToString();
        //    //return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SeleSt\\Programs\\Projects\\Database\\TestTaskDB.mdf;Integrated Security=True;Connect Timeout=30";
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
