using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetSingle(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
