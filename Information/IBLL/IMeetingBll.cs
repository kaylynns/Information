using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IBLL
{
   public interface IMeetingBll
    {
        List<info_Meeting> FenYe(Expression<Func<info_Meeting, int>> order, Expression<Func<info_Meeting, bool>> where, out int rows, int currentPage, int pageSize);
        List<info_Meeting> SelectAll();
        List<info_Meeting> SelectWhere(Expression<Func<info_Meeting, bool>> where);
        int Delete(info_Meeting a);
        int Update(info_Meeting a);
        int Add(info_Meeting a);
    }
}
