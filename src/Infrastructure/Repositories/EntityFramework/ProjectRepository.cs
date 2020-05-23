using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Repositories;
using ProjectManager.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Infrastructure.EntityFramework
{
    public class ProjectRepository : BaseDbContext<Project>, IRepository<Project>
    {
        protected override string ConnectionString { get; }
        private DbSet<Employee> entityContext { get; set; }
        public ProjectRepository(string connectionString, ILogger logger) : base(logger)
        {
            ConnectionString = connectionString;
        }

        public void Create(Project item)
        {
            Logger.LogDebug(GetType() + ".Create is called");
            Table.Add(item);
            SaveChanges();
        }

        public void Delete(int id)
        {
            Logger.LogDebug(GetType() + ".Delete is called");
            Project projectToDelete = new Project() { Id = id };
            Table.Remove(projectToDelete);
            SaveChanges();
        }

        public Project GetSingle(int id)
        {
            Logger.LogDebug(GetType() + ".Get is called");
            Project project = Table.FirstOrDefault(item => item.Id == id);
            return project;
        }

        public IList<Project> GetAll()
        {
            Logger.LogDebug(GetType() + ".GetAll is called");
            List<Project> projectsList = Table.ToList();
            return projectsList;
        }

        public void Update(Project item)
        {
            Logger.LogDebug(GetType() + ".Update is called");
            Entry(Table.FirstOrDefault(itemq => itemq.Id == item.Id)).CurrentValues.SetValues(item);
            SaveChanges();
        }
    }
}
