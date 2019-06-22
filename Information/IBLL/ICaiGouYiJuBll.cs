using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICaiGouYiJuBll
    {
        List<CaiGouYiJu> SelectAll();//查询全部
        List<CaiGouYiJu> SelectWhere(Expression<Func<CaiGouYiJu, bool>> where);//条件查询
    }
}
