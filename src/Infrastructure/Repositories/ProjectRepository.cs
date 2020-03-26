using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Converters;
using TrainingTask.Infrastructure.SqlDataReaders;
using TrainingTask.Infrastructure.Models;
using System.Linq;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly ISqlDataReader<Project> ProjectSqlDataReader;
        private readonly IConvertDal<Project, DataTable> Converter;

        public ProjectRepository(ISqlDataReader<Project> projectSqlDataReader)
        {
            ProjectSqlDataReader = projectSqlDataReader;
            Converter = new ProjectDalConverter();
        }

        public void Create(Project item)
        {
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ShortName", item.ShortName),
                new SqlParameter("@Description", item.Description)
            };

            string SqlQueryString = $"INSERT INTO Project (Name, ShortName, Description) VALUES (@Name, @ShortName, @Description)";

            ProjectSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Project WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            ProjectSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public IList<Project> GetAll()
        {
            string SqlQueryString = "SELECT * FROM Project";
            return ProjectSqlDataReader.GetData(SqlQueryString);
        }

        public Project Get(int id)
        {
            string SqlQueryString = $"SELECT * FROM Project where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            Project Project = ProjectSqlDataReader.GetData(SqlQueryString, QueryParameters).FirstOrDefault();
            return Project;
        }

        public void Update(Project item)
        {
            string SqlQueryString = $"UPDATE Project SET Name = @Name, ShortName = @ShortName, Description = @Description WHERE Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ShortName", item.ShortName),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@Id", item.Id)
            };

            ProjectSqlDataReader.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }
    }
}
