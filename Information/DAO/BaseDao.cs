using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using System.Data.Entity;
using Entity;
using System.Data;

namespace DAO
{
   
     public class BaseDao<T> where T : class
    {
        InformationEntities ce = new InformationEntities();
        public List<T> SelectAll()
        {
            
            return ce.Set<T>().Select(e => e).ToList();
        }
        public List<T> SelectWhere(Expression<Func<T, bool>> where)
        {
            return ce.Set<T>().Where(where)
                  .Select(e => e)
                  .ToList();
        }
        
        public List<T> FenYe<K>(Expression<Func<T, K>> order, Expression<Func<T, bool>> where, out int rows, int currentPage, int pageSize)
        {
            var data = ce.Set<T>().OrderBy(order).Where(where);
            rows = data.Count();
            return data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public int Add(T t)
        {
            ce.Entry<T>(t).State = EntityState.Added;
            return ce.SaveChanges();
        }

        public int Update(T t)
        {
            ce.Entry<T>(t).State = EntityState.Modified;
            return ce.SaveChanges();
        }

        public int Delete(T t)
        {
            ce.Entry<T>(t).State = EntityState.Deleted;
            return ce.SaveChanges();
        }
        //sql语句查询
        public List<T> cha(string sql){
            return ce.Database.SqlQuery<T>(sql).ToList();

        }
        //sql语句增删改
        public int ExecuteSql(string sql)
        {
            return ce.Database.ExecuteSqlCommand(sql);
        }
    }
}
