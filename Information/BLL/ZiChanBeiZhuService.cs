using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IDAO;
using DAO;
using IocContainer;
using System.Linq.Expressions;

namespace BLL
{
    public class ZiChanBeiZhuService : IZiChanBeiZhuBll
    {
        IZiChanBeiZhuDao izcbd = IocCreate.CreateAll<ZiChanBeiZhuDao>("ZiChanBeiZhuOne", "ZiChanBeiZhuDao");
        public int Add(ZiChanBeiZhu t)
        {
            return izcbd.Add(t);
        }

        public List<ZiChanBeiZhu> SelectAll()
        {
            return izcbd.SelectAll();
        }

        public List<ZiChanBeiZhu> SelectWhere(Expression<Func<ZiChanBeiZhu, bool>> where)
        {
            return izcbd.SelectWhere(where);
        }
    }
}
