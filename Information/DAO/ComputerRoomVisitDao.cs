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
    public class ComputerRoomVisitDao : BaseDao<info_ComputerRoomVisit>, IComputerRoomVisitDao
    {
        BaseDao<V_ComputerRoomVisit> bd = new BaseDao<V_ComputerRoomVisit>();
        public List<V_ComputerRoomVisit> FenYes<K>(Expression<Func<V_ComputerRoomVisit, K>> order, Expression<Func<V_ComputerRoomVisit, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return bd.FenYe(order, where, out rows, currentPage, pageSize);
        }
        public List<info_ComputerRoomVisit> SelectWheres(Expression<Func<info_ComputerRoomVisit, bool>> where)
        {
            return SelectWhere(where);
        }
        public List<V_ComputerRoomVisit> SelectWhere(Expression<Func<V_ComputerRoomVisit, bool>> where)
        {
            return bd.SelectWhere(where);
        }
    }
}
