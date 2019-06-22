using DAO;
using Entity;
using IBLL;
using IDAO;
using IocContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BLL
{
   public  class ComputerRoomVisitService:IComputerRoomVisitBll
    {
        IComputerRoomVisitDao iud = IocCreate.CreateAll<ComputerRoomVisitDao>("ComputerRoomVisitOne", "ComputerRoomVisitDao");
        //添加机房记录
        public int Add(info_ComputerRoomVisit cr)
        {
            return iud.Add(cr);
        }
        //删除机房记录
        public int Delete(info_ComputerRoomVisit cr)
        {
            return iud.Delete(cr);
        }
        //按照条件机房记录
        public List<V_ComputerRoomVisit> SelectWhere(Expression<Func<V_ComputerRoomVisit, bool>> where)
        {
            return iud.SelectWhere(where);
        }
        //查询所有机房记录
        public List<info_ComputerRoomVisit> SelelctAll() {
            return iud.SelectAll();
        }
        //修改机房记录
        public int Update(info_ComputerRoomVisit cr)
        {
            return iud.Update(cr);
        }
        //分页
        public List<info_ComputerRoomVisit> SelectWheres(Expression<Func<info_ComputerRoomVisit, bool>> where) {
            return iud.SelectWheres(where);
        }
        public List<V_ComputerRoomVisit> FenYes<K>(Expression<Func<V_ComputerRoomVisit, K>> order, Expression<Func<V_ComputerRoomVisit, bool>> where, out int rows, int currentPage, int pageSize) {
                return iud.FenYes(order, where ,out rows, currentPage, pageSize);        
        }
    }
}
