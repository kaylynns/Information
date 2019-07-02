using Entity;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class TechnologyDao: BaseDao<info_Technology>, ITechnologyDao
    {
        BaseDao<v_info_Technology> bd = new BaseDao<v_info_Technology>();
        public List<v_info_Technology> v_Technology(Expression<Func<v_info_Technology, int>> order, Expression<Func<v_info_Technology, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return bd.FenYe(order, where, out rows, currentPage, pageSize);
        }
    }
}
