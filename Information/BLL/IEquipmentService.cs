using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IBLL;
using IocContainer;
using DAO;
using IDAO;

namespace BLL
{
    public class IEquipmentService : lIEquipmentBll
    {
        IEquipmentDao iud = IocCreate.CreateAll<EquipmentDao>("JiFangSheBeiOne", "EquipmentDao");
        public List<V_info_Equipment> FenYes<K>(Expression<Func<V_info_Equipment, K>> order, Expression<Func<V_info_Equipment, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return iud.FenYes(order, where, out rows, currentPage, pageSize);
        }
        public int Add(info_Equipment t) {
            return iud.Add(t);
        }
        public List<info_Equipment> SelectWhere(Expression<Func<info_Equipment, bool>> where)
        {
            return iud.SelectWhere(where);
        }
        public int Update(info_Equipment cz) {
            return iud.Update(cz);
        }
        public List<info_Equipment> SelectAll() {
            return iud.SelectAll();
        }
    }
}
