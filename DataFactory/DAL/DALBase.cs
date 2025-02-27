using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataFactory.MODEL;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Transactions;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataFactory.DAL
{
    public class DALBase
    {
        private familylifeaccountEntities _dbcontext;

        public familylifeaccountEntities dbcontext
        {
            get
            {
                if (_dbcontext == null)
                {
                    _dbcontext = new familylifeaccountEntities();
                }
                return _dbcontext;
            }
        }
        /// <summary>
        /// 添加对象实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public int Add<T>(T entity) where T : class
        {
            int count = 0;

            dbcontext.Entry<T>(entity).State = EntityState.Added;
            return dbcontext.SaveChanges();
        }

        /// <summary>
        /// 添加对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public int AddList<T>(List<T> entitys) where T : class
        {
            int count = 0;
            foreach (var entity in entitys)
            {
                dbcontext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbcontext.SaveChanges();
        }


        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public int Update<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Entry<T>(entity).State = EntityState.Modified;
            return dbcontext.SaveChanges();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public int Delete<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Entry<T>(entity).State = EntityState.Deleted;
            return dbcontext.SaveChanges();
        }



        public T GetOneModel<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbcontext.Set<T>().Where(predicate).FirstOrDefault();
        }



        /// <summary>
        /// 所有数据的查询列表
        /// </summary>
        /// <returns></returns>
        public IQueryable FindAll<T>(T t) where T : class
        {
            return dbcontext.Set<T>().AsQueryable();
        }

       


        /// <summary>
        /// 根据指定条件表达式得到数据查询列表
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public IQueryable FindAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbcontext.Set<T>().Where(predicate);
        }

        public IEnumerable<T> Queryable<T>(string sql, params object[] predicate)
        {
            return dbcontext.Database.SqlQuery<T>(sql, predicate);
        }


        public List<T> GetList<T>() where T : class
        {
            return dbcontext.Set<T>().AsQueryable().ToList();
        }

        /// <summary>
        /// 获取指定条件表达式数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbcontext.Set<T>().Where<T>(predicate).ToList();
        }

        /// <summary>
        /// 多字段倒排序
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="Tkey">排序字段类型</typeparam>
        /// <param name="sort">排序字段</param>
        /// <returns></returns>
        public List<T> GetList<T, Tkey>(Expression<Func<T, Tkey>> sort) where T : class
        {
            return dbcontext.Set<T>().OrderBy(sort).ToList();
        }

        /// <summary>
        /// 条件+排序=数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="exp"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<T> GetList<T, Tkey>(Expression<Func<T, bool>> predicate, Func<T, Tkey> sort) where T : class
        {
            return dbcontext.Set<T>().Where(predicate).OrderBy(sort).ToList();
        }


    }
}
