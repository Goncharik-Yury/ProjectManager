using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.Infrastructure.Repositories
{
    public interface IProjectTaskRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAllByProjectId(int id);
    }
}
