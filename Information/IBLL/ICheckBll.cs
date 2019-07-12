using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
  public  interface ICheckBll
    {
        List<v_Maintenance> v_MainAll(Expression<Func<v_Maintenance, int>> order, Expression<Func<v_Maintenance, bool>> where, out int rows, int currentPage, int pageSize);
        List<v_info_Registration> selectAll();
        List<v_Maintenance> selectM();
    }
}
