using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAO;
using IocContainer;
using DAO;
using Entity;
using System.Linq.Expressions;

namespace BLL
{
   public class UserService:IUserBll
    {
      
       IUserDao iud = IocCreate.CreateAll<UserDao>("UserOne", "UserDao");

        public int Add(info_User t)
        {
            return iud.Add(t);
        }

        public int Delete(info_User t)
        {
            return iud.Delete(t);
        }

        public List<info_User> DengLu(string username, string pass) {
            return iud.DengLu(username, pass);
        }
        public List<info_User> SelectAll() {
            return iud.SelectAll();
        }

        public List<v_User> SelectUser<K>(Expression<Func<v_User, K>> order, Expression<Func<v_User, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return iud.SelectUser(order, where, out rows, currentPage, pageSize);
        }

        public List<info_User> SelectWhere(Expression<Func<info_User, bool>> where)
        {
            return iud.SelectWhere(where);
        }

        public int Update(info_User t)
        {
            return iud.Update(t);
        }

        public List<info_User> SelectWhere(Expression<Func<info_User, bool>> where) {
            return iud.SelectWhere(where);
        }
    }
}
