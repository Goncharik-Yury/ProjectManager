using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Infrastructure.Functional;
using TrainingTask.Infrastructure.Models;

namespace TrainingTask.ApplicationCore.DBManipulators
{
    public class DBProjectManipulator : DBManipulator
    {
        public List<ProjectDTO> GetProjectsList()
        {
            string SqlQueryString = "SELECT * FROM Project";
            var ProjectsList = (List<Project>)DBGetData(SqlQueryString);
            return DTOConverter.ProjectToDTO(ProjectsList);
        }

        public List<ProjectDTO> GetProjectById(int id)
        {
            string SqlQueryString = $"SELECT * FROM Project where Id = {id}";
            var ProjectsList = (List<Project>)DBGetData(SqlQueryString);
            return DTOConverter.ProjectToDTO(ProjectsList);
        }

        public bool CreateProject(ProjectDTO project)
        {
            string SqlQueryString = $"INSERT INTO Project (Name, ShortName, Description) VALUES ('{project.Name}', '{project.ShortName}', '{project.Description}')";

            DBDoAction(SqlQueryString);
            return true;
        }

        public bool DeleteProject(int id)
        {
            string SqlQueryString = $"DELETE FROM Project WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        public bool EditProject(int id, ProjectDTO project)
        {
            string SqlQueryString = $"UPDATE Project SET Name = '{project.Name}', ShortName = '{project.ShortName}', Description = '{project.Description}' WHERE id = {id}";

            DBDoAction(SqlQueryString);
            return true;
        }

        protected override object DataParse(SqlDataReader dataReader)
        {
            List<Project> projectsList = new List<Project>();
            while (dataReader.Read())
            {
                projectsList.Add(new Project
                {
                    Id = (int)dataReader.GetValue(0),
                    Name = (string)dataReader.GetValue(1),
                    ShortName = (string)dataReader.GetValue(2),
                    Description = (string)dataReader.GetValue(3)
                });
            }
            return projectsList;
        }
    }

}
