using DataFactory.Entity;
using DataFactory.MODEL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataFactory
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private FamilyLifeAccountEntities _dbcontext;

        public FamilyLifeAccountEntities dbcontext
        {
            get
            {
                if (_dbcontext == null)
                {
                    _dbcontext = new FamilyLifeAccountEntities();
                }
                return _dbcontext;
            }
        }

       // private FreightEntity dbcontext = new FreightEntity();
        public int Add(T entity)
        {
            dbcontext.Entry<T>(entity).State = EntityState.Added;
            return dbcontext.SaveChanges();
        }
        public int Add(List<T> entitys)
        {
            foreach (var entity in entitys)
            {
                dbcontext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbcontext.SaveChanges();
        }
        public int Update(T entity)
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Entry<T>(entity).State = EntityState.Modified;
            return dbcontext.SaveChanges();
        }
        public int Delete(T entity)
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Entry<T>(entity).State = EntityState.Deleted;
            return dbcontext.SaveChanges();
        }
        public int Delete(Expression<Func<T, bool>> predicate)
        {
            var entitys = dbcontext.Set<T>().Where(predicate).ToList();
            entitys.ForEach(m => dbcontext.Entry<T>(m).State = EntityState.Deleted);
            return dbcontext.SaveChanges();
        }

        public T FindModel(Expression<Func<T, bool>> predicate)
        {
            return dbcontext.Set<T>().Where(predicate).FirstOrDefault();
        }
        public IQueryable<T> FindAll()
        {
            return dbcontext.Set<T>().AsQueryable();
        }
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return dbcontext.Set<T>().Where(predicate);
        }

        public IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, TKey>> keySelector, bool asc = true)
        {
            return asc ? dbcontext.Set<T>().OrderBy(keySelector) : dbcontext.Set<T>().OrderByDescending(keySelector);
        }

        public IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool asc = true)
        {
            return asc ? dbcontext.Set<T>().Where(predicate).OrderBy(keySelector) : dbcontext.Set<T>().Where(predicate).OrderByDescending(keySelector);
        }

        public IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, TKey>> keySelector, bool asc = true, int pageindex = 0, int pagesize = 10)
        {
            return asc ? dbcontext.Set<T>().OrderBy(keySelector).Skip(pageindex).Take(pagesize) : dbcontext.Set<T>().OrderByDescending(keySelector).Skip(pageindex).Take(pagesize);
        }

        public IQueryable<T> FindAll<TSource, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool asc = true, int pageindex = 0, int pagesize = 10)
        {
            return asc ? dbcontext.Set<T>().Where(predicate).OrderBy(keySelector).Skip(pageindex).Take(pagesize) :
                dbcontext.Set<T>().Where(predicate).OrderByDescending(keySelector).Skip(pageindex).Take(pagesize);
        }
    }
}
