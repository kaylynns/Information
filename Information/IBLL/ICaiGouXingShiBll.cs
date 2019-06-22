using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICaiGouXingShiBll
    {
        List<CaiGouXingShi> SelectAll();//查询全部
        List<CaiGouXingShi> SelectWhere(Expression<Func<CaiGouXingShi, bool>> where);//根据条件查询
    }
}
