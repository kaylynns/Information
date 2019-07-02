using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
   public interface ISectionDao
    {
        List<Section> SelectAll();
        List<Section> SelectWhere(Expression<Func<Section, bool>> where);
        List<Section> FenYe<K>(Expression<Func<Section, K>> order, Expression<Func<Section, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(Section t);
        int Delete(Section t);
        int Update(Section t);
    }
}
