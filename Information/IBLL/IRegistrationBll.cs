using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface IRegistrationBll
    {
        List<v_info_Registration> v_MainAll(Expression<Func<v_info_Registration, int>> order, Expression<Func<v_info_Registration, bool>> where, out int rows, int currentPage, int pageSize);
        List<info_Registration> SelectAll();
        List<info_Registration> SelectWhere(Expression<Func<info_Registration, bool>> where);
        List<info_Registration> FenYe(Expression<Func<info_Registration, int>> order, Expression<Func<info_Registration, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Registration t);
        int Delete(info_Registration t);
        int Update(info_Registration t);
        List<v_info_Registration> v_RWhere(Expression<Func<v_info_Registration, bool>> where);
    }
}
