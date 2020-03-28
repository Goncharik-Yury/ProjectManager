using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingTask.Infrastructure.Converters
{
    public interface IConvertDb<TIn, TOut>
    {
        public IList<TOut> Convert(TIn item);
    }
}
