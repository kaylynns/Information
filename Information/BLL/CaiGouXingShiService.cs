using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IocContainer;
using IDAO;
using System.Linq.Expressions;

namespace BLL
{
    public class CaiGouXingShiService : ICaiGouXingShiBll
    {
        ICaiGouXingShiDao icgxsd = IocCreate.CreateAll<ICaiGouXingShiDao>("CaiGouXingShiOne", "CaiGouXingShiDao");

        public int Add(CaiGouXingShi t)
        {
            return icgxsd.Add(t);
        }

        public int Delete(CaiGouXingShi t)
        {
            return icgxsd.Delete(t);
        }

        public List<CaiGouXingShi> FenYe<K>(Expression<Func<CaiGouXingShi, K>> order, Expression<Func<CaiGouXingShi, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return icgxsd.FenYe(order, where,  out rows, currentPage, pageSize);
        }

        public List<CaiGouXingShi> SelectAll()
        {
            return icgxsd.SelectAll();
        }

        public List<CaiGouXingShi> SelectWhere(Expression<Func<CaiGouXingShi, bool>> where)
        {
            return icgxsd.SelectWhere(where);
        }

        public int Update(CaiGouXingShi t)
        {
            return icgxsd.Update(t);
        }
    }
}
