using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.Infrastructure.Converters
{
    interface IConvertDal<TEntity, TSource>
    {
        public TEntity Convert(TSource item);
        public IList<TEntity> ConvertAll(TSource items);
    }
}
