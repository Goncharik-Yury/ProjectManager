using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Infrastructure.Converters
{
    public interface IConvertDb<TIn, TOut>
    {
        public IList<TOut> Convert(TIn item);
    }
}
