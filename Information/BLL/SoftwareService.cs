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
   public class SoftwareService : ISoftwareBll
    {
        ISoftwareDao imd = IocContainer.IocCreate.CreateAll<SoftwareDao>("SoftwareOne", "SoftwareDao");
        public List<info_Software> SelectAll()
        {
            return imd.SelectAll();
        }

        public List<info_Software> FenYe(Expression<Func<info_Software, int>> order, Expression<Func<info_Software, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }
        public int Add(info_Software t)
        {
            return imd.Add(t);
        }

        public int Update(info_Software t)
        {
            return imd.Update(t);
        }


        public List<info_Software> SelectWhere(Expression<Func<info_Software, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Delete(info_Software a)
        {
            return imd.Delete(a);
        }

    }
}
