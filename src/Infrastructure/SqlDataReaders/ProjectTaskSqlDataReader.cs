using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.SqlDataReaders
{
    public class ProjectTaskSqlDataReader : SqlDataReader<ProjectTask>
    {
        public ProjectTaskSqlDataReader(string connectionString) : base(connectionString) { }
        protected override ProjectTask DataParse(SqlDataReader sqlDataReader)
        {
            ProjectTask ProjectTasks = new ProjectTask
            {
                Id = sqlDataReader.GetInt32("Id"),
                Name = sqlDataReader.GetString("Name"),
                TimeToComplete = sqlDataReader.GetInt32("TimeToComplete"),
                BeginDate = sqlDataReader.GetDateTime("BeginDate"),
                EndDate = sqlDataReader.GetDateTime("EndDate"),
                Status = sqlDataReader.GetString("Status"),
                ProjectId = sqlDataReader.GetInt32("ProjectId"),
                EmployeeId = sqlDataReader.GetInt32("EmployeeId")
            };

            return ProjectTasks;
        }
    }
}
