using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface ISoftwareBll
    {
        List<info_Software> SelectAll();
        List<info_Software> SelectWhere(Expression<Func<info_Software, bool>> where);
        List<info_Software> FenYe(Expression<Func<info_Software, int>> order, Expression<Func<info_Software, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Software t);
        int Delete(info_Software t);
        int Update(info_Software t);
    }
}
