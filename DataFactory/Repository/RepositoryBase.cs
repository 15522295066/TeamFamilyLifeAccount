using DataFactory.Entity;
using DataFactory.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;


namespace DataFactory
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    public class RepositoryBase : IRepositoryBase, IDisposable
    {
        private FamilyLifeAccountEntities dbcontext = new FamilyLifeAccountEntities();
        private DbTransaction dbTransaction { get; set; }
        public RepositoryBase BeginTrans()
        {
            DbConnection dbConnection = ((IObjectContextAdapter)dbcontext).ObjectContext.Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }
        public int Commit()
        {
            try
            {
                var returnValue = dbcontext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return returnValue;
            }
            catch (Exception)
            {
                if (dbTransaction != null)
                {
                    this.dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void Dispose()
        {
            if (dbTransaction != null)
            {
                this.dbTransaction.Dispose();
            }
            this.dbcontext.Dispose();
        }
        public int Add<T>(T entity) where T : class
        {
            dbcontext.Entry<T>(entity).State = EntityState.Added;
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Add<T>(List<T> entitys) where T : class
        {
            foreach (var entity in entitys)
            {
                dbcontext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Update<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            var enrty = dbcontext.Entry<T>(entity);
            enrty.CurrentValues.SetValues(entity);
            enrty.State = EntityState.Modified;
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Delete<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Entry<T>(entity).State = EntityState.Deleted;
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entitys = dbcontext.Set<T>().Where(predicate).ToList();
            entitys.ForEach(m => dbcontext.Entry<T>(m).State = EntityState.Deleted);
            return dbTransaction == null ? this.Commit() : 0;
        }
       
        public T FindModel<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbcontext.Set<T>().FirstOrDefault(predicate);
        }
        public IQueryable<T> FindAll<T>() where T : class
        {
            return dbcontext.Set<T>();
        }
        public IQueryable<T> FindAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbcontext.Set<T>().Where(predicate);
        }
        public List<T> FindAll<T>(string strSql) where T : class
        {
            return dbcontext.Database.SqlQuery<T>(strSql).ToList<T>();
        }
        public List<T> FindAll<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            return dbcontext.Database.SqlQuery<T>(strSql, dbParameter).ToList<T>();
        }
     
    }
}
