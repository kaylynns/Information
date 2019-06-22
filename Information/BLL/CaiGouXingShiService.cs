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
        public List<CaiGouXingShi> SelectAll()
        {
            return icgxsd.SelectAll();
        }

        public List<CaiGouXingShi> SelectWhere(Expression<Func<CaiGouXingShi, bool>> where)
        {
            return icgxsd.SelectWhere(where);
        }
    }
}
