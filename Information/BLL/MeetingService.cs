using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IBLL;
using IDAO;
using DAO;

namespace BLL
{
    public class MeetingService : IMeetingBll
    {
        IMeetingDAO imd = IocContainer.IocCreate.CreateAll<MeetingDao>("MeetingOne", "MeetingDao");
        public List<info_Meeting> SelectAll() {
            return imd.SelectAll();
        }
       
        public List<info_Meeting> FenYe(Expression<Func<info_Meeting, int>> order, Expression<Func<info_Meeting, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }
        public int Add(info_Meeting t) {
            return imd.Add(t);
        }
      
        public int Update(info_Meeting t) {
            return imd.Update(t);
        }
      

        public List<info_Meeting> SelectWhere(Expression<Func<info_Meeting, bool>> where)
        {
            return imd.SelectWhere(where);
        }
        
        public int Delete(info_Meeting a)
        {
            return imd.Delete(a);
        }

    }
}
