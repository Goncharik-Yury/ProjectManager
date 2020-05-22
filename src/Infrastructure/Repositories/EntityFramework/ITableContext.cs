using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Infrastructure.Repositories.EntityFramework
{
    interface ITableContext<T> : IDisposable where T : class
    {
        public DbSet<T> Table { get; set; }
    }
}
