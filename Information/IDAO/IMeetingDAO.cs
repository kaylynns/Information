using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace IDAO
{
   public interface IMeetingDAO
    {
        List<info_Meeting> SelectAll();
        List<info_Meeting> SelectWhere(Expression<Func<info_Meeting, bool>> where);
        List<info_Meeting> FenYe<K>(Expression<Func<info_Meeting, K>> order, Expression<Func<info_Meeting, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Meeting t);
        int Delete(info_Meeting t);
        int Update(info_Meeting t);
    }
}
