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
    public class CaiGouYiJuService :ICaiGouYiJuBll
    {
        ICaiGouYiJuDao icgyjd = IocCreate.CreateAll<CaiGouYiJuDao>("CaiGouYiJuOne", "CaiGouYiJuDao");
        public List<CaiGouYiJu> SelectAll()
        {
            return icgyjd.SelectAll();
        }

        public List<CaiGouYiJu> SelectWhere(Expression<Func<CaiGouYiJu, bool>> where)
        {
            return icgyjd.SelectWhere(where);
        }
    }
}
