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
  public  class TechnologyService: ITechnologyBll
    {
        ITechnologyDao imd = IocContainer.IocCreate.CreateAll<TechnologyDao>("TechnologyOne", "TechnologyDao");
        public List<info_Technology> SelectAll()
        {
            return imd.SelectAll();
        }

        public int Add(info_Technology t)
        {
            return imd.Add(t);
        }

        public int Update(info_Technology t)
        {
            return imd.Update(t);
        }


        public List<info_Technology> SelectWhere(Expression<Func<info_Technology, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Delete(info_Technology a)
        {
            return imd.Delete(a);
        }

        public List<info_Technology> FenYe(Expression<Func<info_Technology, int>> order, Expression<Func<info_Technology, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<v_info_Technology> v_Technology(Expression<Func<v_info_Technology, int>> order, Expression<Func<v_info_Technology, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.v_Technology(order, where, out rows, currentPage, pageSize);
        }

    }
}
