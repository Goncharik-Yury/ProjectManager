using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ProjectManager.Infrastructure.Models;
using System.Linq;
using ProjectManager.Common;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Converters;

namespace ProjectManager.Infrastructure.Repositories.Ado
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IRepository<ProjectTask>, IRepositoryExtention<ProjectTask>
    {
        protected override string ConnectionString { get; }
        private readonly Func<SqlDataReader, IList<ProjectTask>> projectTaskConverterDelegate;

        public ProjectTaskRepository(string connectionString, IConvertDb<SqlDataReader, ProjectTask> projectTaskConverter, ILogger logger) : base(logger)
        {
            ConnectionString = connectionString;
            projectTaskConverterDelegate = projectTaskConverter.Convert;
        }

        public void Create(ProjectTask item)
        {
            logger.LogDebug(GetType() + ".Create is called");
            string SqlQueryString = $"INSERT INTO ProjectTask (Name, TimeToComplete, BeginDate, EndDate, Status, ProjectId, EmployeeId) VALUES (@Name, @TimeToComplete, @BeginDate, @EndDate, @Status, @ProjectId, @EmployeeId)";
            ExecuteNonQuery(SqlQueryString, GetCreateParameters(item));
        }

        public void Delete(int id)
        {
            logger.LogDebug(GetType() + ".Delete is called");
            string SqlQueryString = $"DELETE FROM ProjectTask WHERE id = @Id";
            ExecuteNonQuery(SqlQueryString, GetIdParameter(id));
        }

        public IList<ProjectTask> GetAll()
        {
            logger.LogDebug(GetType() + ".GetAll is called");
            string SqlQueryString = "SELECT * FROM ProjectTask";
            IList<ProjectTask> ProjectTasksList = GetData(SqlQueryString, projectTaskConverterDelegate);
            return ProjectTasksList;
        }

        public IList<ProjectTask> GetAllByProjectId(int id)
        {
            logger.LogDebug(GetType() + ".GetAllByProjectId is called");
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            IList<ProjectTask> Projects = GetData(SqlQueryString, projectTaskConverterDelegate, GetProjectIdParameter(id));
            return Projects;
        }

        public ProjectTask GetSingle(int id)
        {
            logger.LogDebug(GetType() + ".Get is called");
            string SqlQueryString = $"SELECT * FROM ProjectTask WHERE Id = @Id";
            ProjectTask ProjectTask = GetData(SqlQueryString, projectTaskConverterDelegate, GetIdParameter(id)).FirstOrDefault();
            return ProjectTask;
        }

        public void Update(ProjectTask item)
        {
            logger.LogDebug(GetType() + ".Update is called");
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
                new SqlParameter("@EmployeeId", item.EmployeeId)
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
