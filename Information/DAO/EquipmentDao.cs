using Entity;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DAO
{
    public class EquipmentDao:BaseDao<info_Equipment>, IEquipmentDao
    {
        BaseDao<V_info_Equipment> bd = new BaseDao<V_info_Equipment>();

        public List<V_info_Equipment> FenYes<K>(Expression<Func<V_info_Equipment, K>> order, Expression<Func<V_info_Equipment, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return bd.FenYe(order, where, out rows, currentPage, pageSize);
        }
    }
}
