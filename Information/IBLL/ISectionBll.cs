using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface ISectionBll
    {
        List<Section> SelectAll();
        List<Section> SelectWhere(Expression<Func<Section, bool>> where);
        List<Section> FenYe(Expression<Func<Section, int>> order, Expression<Func<Section, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(Section t);
        int Delete(Section t);
        int Update(Section t);
    }
}
