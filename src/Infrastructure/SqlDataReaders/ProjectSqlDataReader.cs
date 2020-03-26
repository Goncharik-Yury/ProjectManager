using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.SqlDataReaders
{
    public class ProjectSqlDataReader : SqlDataReader<Project>
    {
        public ProjectSqlDataReader(string connectionString) : base(connectionString) { }

        protected override Project DataParse(SqlDataReader sqlDataReader)
        {
            Project Projects = new Project
            {
                Id = sqlDataReader.GetInt32("Id"),
                Name = sqlDataReader.GetString("Name"),
                ShortName = sqlDataReader.GetString("ShortName"),
                Description = sqlDataReader.GetString("Description"),
            };

            return Projects;
        }
    }
}
