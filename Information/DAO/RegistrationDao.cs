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
   public class RegistrationDao : BaseDao<info_Registration>, IRegistrationDao
    {
        BaseDao<v_info_Registration> bd = new BaseDao<v_info_Registration>();
        public List<v_info_Registration> v_MainAll(Expression<Func<v_info_Registration, int>> order, Expression<Func<v_info_Registration, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return bd.FenYe(order, where, out rows, currentPage, pageSize);
        }

      public  List<v_info_Registration> v_RWhere(Expression<Func<v_info_Registration, bool>> where) {
            return bd.SelectWhere(where);
        }
    }
}
