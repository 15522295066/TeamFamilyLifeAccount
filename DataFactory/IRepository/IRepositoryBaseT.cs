using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataFactory
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);

        T FindModel(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindAll();

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, TKey>> keySelector, bool asc = true);


        IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool asc = true);


        IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, TKey>> keySelector, bool asc = true, int pageindex = 0, int pagesize = 10);


        IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool asc = true, int pageindex = 0, int pagesize = 10);


    }
}
