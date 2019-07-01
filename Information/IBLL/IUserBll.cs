using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
    public interface IUserBll
    {
        List<info_User> DengLu(string username, string pass);
        List<info_User> SelectAll();
        //用户管理查询
        List<v_User> SelectUser<K>(Expression<Func<v_User, K>> order, Expression<Func<v_User, bool>> where, out int rows, int currentPage, int pageSize);
        //用户管理修改；
        int Update(info_User t);
        //用户管理删除
        int Delete(info_User t);
        //用户管理添加
        int Add(info_User t);
        //用户管理带条件查询
        List<info_User> SelectWhere(Expression<Func<info_User, bool>> where);
    }
}
