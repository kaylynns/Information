using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
   public interface IComputerRoomVisitDao
    {
        //查询全部机房记录
        List<info_ComputerRoomVisit> SelectAll();
        //添加机房记录
         int Add(info_ComputerRoomVisit cr);
        //删除机房记录
        int Delete(info_ComputerRoomVisit cr);
        //修改机房记录
        int Update(info_ComputerRoomVisit cr);
        //条件查询
        List<info_ComputerRoomVisit> SelectWheres(Expression<Func<info_ComputerRoomVisit, bool>> where);
        List<V_ComputerRoomVisit> SelectWhere(Expression<Func<V_ComputerRoomVisit, bool>> where);
        List<V_ComputerRoomVisit> FenYes<K>(Expression<Func<V_ComputerRoomVisit, K>> order, Expression<Func<V_ComputerRoomVisit, bool>> where, out int rows, int currentPage, int pageSize);
      //  List<T> FenYe<K>(Expression<Func<T, K>> order, Expression<Func<T, bool>> where, out int rows, int currentPage, int pageSize);
    }
}
