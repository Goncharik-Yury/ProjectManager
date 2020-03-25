using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.ApplicationCore.Repository
{
    public interface IRepositoryService<TDto> where TDto : class
    {
        IList<TDto> GetAll();
        TDto GetSingle(int id);
        void Create(TDto item);
        void Update(TDto item);
        void Delete(int id);
    }
}
