using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface ITechnologyBll
    {
        List<v_info_Technology> v_Technology(Expression<Func<v_info_Technology, int>> order, Expression<Func<v_info_Technology, bool>> where, out int rows, int currentPage, int pageSize);
        List<info_Technology> SelectAll();
        List<info_Technology> SelectWhere(Expression<Func<info_Technology, bool>> where);
        List<info_Technology> FenYe(Expression<Func<info_Technology, int>> order, Expression<Func<info_Technology, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Technology t);
        int Delete(info_Technology t);
        int Update(info_Technology t);
    }
}
