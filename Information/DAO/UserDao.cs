using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAO;
using Entity;
using System.Linq.Expressions;

namespace DAO
{
   public class UserDao:BaseDao<info_User>,IUserDao
    {
        BaseDao<v_User> bd = new BaseDao<v_User>();//用户管理
        public List<info_User> DengLu(string username, string pass) {
            return SelectWhere(e=>e.UserName==username&&e.UserPass==pass);
        }
        //用户管理查询
        public List<v_User> SelectUser<K>(Expression<Func<v_User, K>> order, Expression<Func<v_User, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return bd.FenYe(order, where, out rows, currentPage, pageSize);
        }

      
    }
}
