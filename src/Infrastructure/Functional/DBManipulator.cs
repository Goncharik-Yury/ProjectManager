using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Infrastructure.Functional
{
    public abstract class DBManipulator
    {
        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"C:\SeleSt\Programs\Projects\Database\TestTaskDatabase.mdf",
                IntegratedSecurity = true,
                ConnectTimeout = 30
            };

            return builder.ToString();
        }

        protected static bool DBDoAction(string sqlQueryString)
        {
            using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    DBConnection.Open();
                    using SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);
                    CommandToExecute.ExecuteNonQuery();
                }
                catch { throw; }
            }
            return true;
        }

        protected object DBGetData(string sqlQueryString)
        {
            using (SqlConnection DBConnection = new SqlConnection(GetConnectionString()))
            {
                DBConnection.Open();
                SqlCommand CommandToExecute = new SqlCommand(sqlQueryString, DBConnection);

                SqlDataReader DataReader = CommandToExecute.ExecuteReader();
                return DataParse(DataReader);

            }
        }

        protected abstract object DataParse(SqlDataReader dataReader);
    }
}
