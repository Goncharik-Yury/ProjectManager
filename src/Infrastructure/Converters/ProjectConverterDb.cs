using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Converters
{
    public class ProjectConverterDb : IConvertDb<SqlDataReader, Project>
    {
        public IList<Project> Convert(SqlDataReader sqlDataReader)
        {
            List<Project> Projects = new List<Project>();
            while (sqlDataReader.Read())
            {
                Project project = new Project
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    Name = sqlDataReader.GetString("Name"),
                    ShortName = sqlDataReader.GetString("ShortName"),
                    Description = sqlDataReader["Description"] as string
                };


                Projects.Add(project);
            }
            return Projects;
        }
    }
}
