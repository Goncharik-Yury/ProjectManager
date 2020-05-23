using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Infrastructure.Repositories
{
    public interface IRepositoryExtention<T> where T : class
    {
        IList<T> GetAllByProjectId(int id);
    }
}
