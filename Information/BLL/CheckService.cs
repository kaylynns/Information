using DAO;
using Entity;
using IBLL;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class CheckService: ICheckBll
    {
        ICheckDao imd = IocContainer.IocCreate.CreateAll<CheckDao>("CheckOne", "CheckDao");
      

        public List<v_Maintenance> v_MainAll(Expression<Func<v_Maintenance, int>> order, Expression<Func<v_Maintenance, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.v_MainAll(order, where, out rows, currentPage, pageSize);
        }


    }
}
