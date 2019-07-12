using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
    public interface IEquipmentDao
    {
        //分页查询
       List<V_info_Equipment> FenYes<K>(Expression<Func<V_info_Equipment, K>> order, Expression<Func<V_info_Equipment, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Equipment t);
        List<info_Equipment> SelectWhere(Expression<Func<info_Equipment, bool>> where);
        int Update(info_Equipment t);
        List<info_Equipment> SelectAll();
    }
}
