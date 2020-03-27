using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Models;
using System.Linq;
using TrainingTask.Common;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository<ProjectTask>
    {
        protected override string ConnectionString { get; }

        Func<SqlDataReader, List<ProjectTask>> converter = ConvertToProjectTask;
        public ProjectTaskRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        static List<ProjectTask> ConvertToProjectTask(SqlDataReader sqlDataReader)
        {
            List<ProjectTask> ProjectTasks = new List<ProjectTask>();
            while (sqlDataReader.Read())
            {
                ProjectTask ProjectTask = new ProjectTask
                {
                    Id = sqlDataReader.GetInt32("Id"),
                    Name = sqlDataReader.GetString("Name"),
                    TimeToComplete = sqlDataReader["TimeToComplete"] as int?,
                    BeginDate = sqlDataReader["BeginDate"] as DateTime?,
                    EndDate = sqlDataReader["EndDate"] as DateTime?,
                    Status = sqlDataReader.GetString("Status"),
                    ProjectId = sqlDataReader.GetInt32("ProjectId"),
                    EmployeeId = sqlDataReader["EmployeeId"] as int?
                };

                ProjectTasks.Add(ProjectTask);
            }
            return ProjectTasks;
        }

        public void Create(ProjectTask item)
        {
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ProjectId, EmployeeId) VALUES (@Name, @TimeToComplete, @BeginDate, @EndDate, @Status, @ProjectId, @EmployeeId)";
            ExecuteNonQuery(SqlQueryString, GetCreateParameters(item));
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = @Id";
            ExecuteNonQuery(SqlQueryString, GetIdParameter(id));
        }

        public IList<ProjectTask> GetAll()
        {
            string SqlQueryString = "SELECT * FROM ProjectTask";
            IList<ProjectTask> ProjectTasksList = GetData(SqlQueryString, converter);
            return ProjectTasksList;
        }

        public IList<ProjectTask> GetAllByProjectId(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            IList<ProjectTask> Projects = GetData(SqlQueryString, converter, GetProjectIdParameter(id));
            return Projects;
        }

        public ProjectTask Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = @Id";
            ProjectTask ProjectTask = GetData(SqlQueryString, converter, GetIdParameter(id)).FirstOrDefault();
            return ProjectTask;
        }

        public void Update(ProjectTask item)
        {
            string SqlQueryString = $"UPDATE ProjectTask SET Name = @Name, TimeToComplete = @TimeToComplete, BeginDate = @BeginDate, EndDate = @EndDate, Status = @Status, EmployeeId = @EmployeeId, ProjectId = @ProjectId WHERE Id = @Id";
            ExecuteNonQuery(SqlQueryString, GetUpdateParameters(item));
        }

        private List<SqlParameter> GetCreateParameters(ProjectTask item)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TimeToComplete", SqlDbType.Int){Value = item.TimeToComplete ?? (object)DBNull.Value},
                new SqlParameter("@BeginDate", SqlDbType.Date){Value = item.BeginDate ?? (object)DBNull.Value},
                new SqlParameter("@EndDate", SqlDbType.Date){Value = item.EndDate ?? (object)DBNull.Value},
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ProjectId", item.ProjectId),
                new SqlParameter("@EmployeeId", SqlDbType.Int){Value = item.EmployeeId ?? (object)DBNull.Value},
            };
        }

        private List<SqlParameter> GetUpdateParameters(ProjectTask item)
        {
            List<SqlParameter> SqlParameters = GetCreateParameters(item);
            SqlParameters.AddRange(GetIdParameter(item.Id));
            return SqlParameters;
        }

        private List<SqlParameter> GetIdParameter(int id)
        {
            List<SqlParameter> QueryParameters = new List<SqlParameter>();
            QueryParameters.Add(new SqlParameter("@Id", id));
            return QueryParameters;
        }

        private List<SqlParameter> GetProjectIdParameter(int id)
        {
            List<SqlParameter> QueryParameters = new List<SqlParameter>();
            QueryParameters.Add(new SqlParameter("@ProjectId", id));
            return QueryParameters;
        }
    }
}
