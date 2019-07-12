using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface lIEquipmentBll
    {
        int Add(info_Equipment t);
        int Update(info_Equipment t);
        List<info_Equipment> SelectWhere(Expression<Func<info_Equipment, bool>> where);
        List<info_Equipment> SelectAll();
        List<V_info_Equipment> FenYes<K>(Expression<Func<V_info_Equipment, K>> order, Expression<Func<V_info_Equipment, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
