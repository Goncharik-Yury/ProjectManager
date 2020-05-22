using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Infrastructure.Repositories
{
    public interface IProjectTaskRepository<T> : IRepository<T> where T : class // TODO : implenet as separated interace
    {
        IList<T> GetAllByProjectId(int id);
    }
}
