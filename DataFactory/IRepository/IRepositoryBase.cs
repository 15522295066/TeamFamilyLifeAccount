
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace DataFactory
{
    public interface IRepositoryBase : IDisposable
    {
        RepositoryBase BeginTrans();
        int Commit();
        int Add<T>(T entity) where T : class;
        int Update<T>(T entity) where T : class;
        int Delete<T>(T entity) where T : class;
        T FindModel<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> FindAll<T>() where T : class;
        IQueryable<T> FindAll<T>(Expression<Func<T, bool>> predicate) where T : class;
       
         
    }
}
