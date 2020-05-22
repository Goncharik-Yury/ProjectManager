using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using ProjectManager.Infrastructure.Repositories;
using ProjectManager.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ProjectManager.Infrastructure.EntityFramework
{
    public class ProjectTaskRepositoryEf : BaseDbContextEf<ProjectTask>, IProjectTaskRepository<ProjectTask> // TODO: separate iterface to two different interfaces
    {
        protected override string ConnectionString { get; }
        private DbSet<ProjectTask> entityContext { get; set; }
        public ProjectTaskRepositoryEf(string connectionString, ILogger logger) : base(logger)
        {
            ConnectionString = connectionString;
        }

        public void Create(ProjectTask item)
        {
            Logger.LogDebug(GetType() + ".Create is called");
            entityContext.Add(item);
            SaveChanges();
        }

        public void Delete(int id)
        {
            Logger.LogDebug(GetType() + ".Delete is called");
            ProjectTask projectTaskToDelete = new ProjectTask() { Id = id };
            Entity.Remove(projectTaskToDelete);
            SaveChanges();
        }

        public ProjectTask GetSingle(int id)
        {
            Logger.LogDebug(GetType() + ".Get is called");
            ProjectTask projectTask = Entity.FirstOrDefault(item => item.Id == id);
            return projectTask;
        }

        public IList<ProjectTask> GetAll()
        {
            Logger.LogDebug(GetType() + ".GetAll is called");
            List<ProjectTask> projectTasksList = Entity.ToList();
            return projectTasksList;
        }

        public void Update(ProjectTask item)
        {
            Logger.LogDebug(GetType() + ".Update is called");
            Entry(Entity.FirstOrDefault(itemq => itemq.Id == item.Id)).CurrentValues.SetValues(item);
            SaveChanges();
        }

        public IList<ProjectTask> GetAllByProjectId(int id)
        {
            Logger.LogDebug(GetType() + ".GetAllByProjectId is called");
            List<ProjectTask> projectTasksList = Entity.Where(item => item.ProjectId == id).ToList();
            return projectTasksList;
            //string SqlQueryString = $"SELECT * FROM ProjectTask WHERE ProjectId = @ProjectId";
            //IList<ProjectTask> Projects = GetData(SqlQueryString, projectTaskConverterDelegate, GetProjectIdParameter(id));
            //return Projects;
        }
    }
}
