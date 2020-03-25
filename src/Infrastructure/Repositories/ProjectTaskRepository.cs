using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Converters;
using TrainingTask.Infrastructure.SqlDataReaders;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository<ProjectTask>
    {
        ISqlDataReader<ProjectTask> ProjectTaskSqlDataReader;
        IConvertDal<ProjectTask, DataTable> Converter;

        public ProjectTaskRepository(ISqlDataReader<ProjectTask> projectTaskSqlDataReader)
        {
            ProjectTaskSqlDataReader = projectTaskSqlDataReader;
            Converter = new ProjectTaskDalConverter();
        }

        public void Create(ProjectTask item)
        {
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ProjectId, EmployeeId) VALUES (@Name, @TimeToComplete, @BeginDate, @EndDate, @Status, @ProjectId, @EmployeeId)";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TimeToComplete", item.TimeToComplete),
                new SqlParameter("@BeginDate", item.BeginDate),
                new SqlParameter("@EndDate", item.EndDate),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ProjectId", item.ProjectId),
                new SqlParameter("@EmployeeId", item.EmployeeId)
            };
            ProjectTaskSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            ProjectTaskSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public IList<ProjectTask> GetAll()
        {
            string SqlQueryString = "SELECT * FROM ProjectTask";
            IList<ProjectTask> ProjectTasksList = ProjectTaskSqlDataReader.GetData(SqlQueryString);

            return ProjectTasksList;
        }

        public IList<ProjectTask> GetAllByProjectId(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ProjectId", id)
            };

            IList<ProjectTask> Projects = ProjectTaskSqlDataReader.GetData(SqlQueryString, QueryParameters);

            return Projects;
        }

        public ProjectTask GetSingle(int id)
        {
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            ProjectTask ProjectTask = ProjectTaskSqlDataReader.GetData(SqlQueryString, QueryParameters)[0];

            return ProjectTask;
        }

        public void Update(ProjectTask item)
        {
            string SqlQueryString = $"UPDATE ProjectTask SET Name = @Name, TimeToComplete = @TimeToComplete, BeginDate = @BeginDate, EndDate = @EndDate, Status = @Status, EmployeeId = @EmployeeId, ProjectId = @ProjectId WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TimeToComplete", item.TimeToComplete),
                new SqlParameter("@BeginDate", item.BeginDate),
                new SqlParameter("@EndDate", item.EndDate),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ProjectId", item.ProjectId),
                new SqlParameter("@EmployeeId", item.EmployeeId)
            };
            ProjectTaskSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        private ProjectTask ConvertToProjectTask(DataTable dataTable)
        {
            return Converter.ConvertAll(dataTable)[0];
        }
        private IList<ProjectTask> ConvertToProjectTasksList(DataTable dataTable)
        {
            return Converter.ConvertAll(dataTable);
        }
    }
}
