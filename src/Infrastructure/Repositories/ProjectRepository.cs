using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingTask.Infrastructure.Converters;
using TrainingTask.Infrastructure.DbOperators;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.Infrastructure.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        IDbOperator DbOperator;
        IConvertDal<Project, DataTable> Converter;

        public ProjectRepository()
        {
            DbOperator = new DbOperator();
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

            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public void Delete(int id)
        {
            string SqlQueryString = $"DELETE FROM Project WHERE Id = @Id";
            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }

        public List<Project> GetAll()
        {
            string SqlQueryString = "SELECT * FROM Project";
            return ConvertToProjectsList(DbOperator.GetData(SqlQueryString));
        }

        public Project GetSingle(int id)
        {
            string SqlQueryString = $"SELECT * FROM Project where Id = @Id";

            List<SqlParameter> QueryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            Project Project = ConvertToProject(DbOperator.GetData(SqlQueryString, QueryParameters));
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
            DbOperator.ExecuteNonQuery(SqlQueryString, QueryParameters);
        }
        private Project ConvertToProject(DataTable dataTable)
        {
            return Converter.ConvertAll(dataTable)[0];
        }
        private List<Project> ConvertToProjectsList(DataTable dataTable)
        {
            return Converter.ConvertAll(dataTable);
        }
    }
}
