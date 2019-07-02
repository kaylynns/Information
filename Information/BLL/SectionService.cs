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
   public class SectionService: ISectionBll
    {
        ISectionDao imd = IocContainer.IocCreate.CreateAll<SectionDao>("SoftwareOne", "SoftwareDao");
        public List<Section> SelectAll()
        {
            return imd.SelectAll();
        }

        public List<Section> FenYe(Expression<Func<Section, int>> order, Expression<Func<Section, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }
        public int Add(Section t)
        {
            return imd.Add(t);
        }

        public int Update(Section t)
        {
            return imd.Update(t);
        }


        public List<Section> SelectWhere(Expression<Func<Section, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Delete(Section a)
        {
            return imd.Delete(a);
        }
    }
}
