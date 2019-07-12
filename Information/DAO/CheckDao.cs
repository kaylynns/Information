using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IDAO;
using System.Linq.Expressions;

namespace DAO
{
    public class CheckDao : BaseDao<v_Maintenance>, ICheckDao
    {
        BaseDao<v_Maintenance> bd = new BaseDao<v_Maintenance>();
        BaseDao<v_info_Registration> br = new BaseDao<v_info_Registration>();
        public List<v_Maintenance> v_MainAll(Expression<Func<v_Maintenance, int>> order, Expression<Func<v_Maintenance, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return bd.FenYe(order,where,out rows,currentPage,pageSize);
        }

        public List<v_info_Registration> selectAll() {
            return br.SelectAll();
        }
        public List<v_Maintenance> selectM() {
            return bd.SelectAll();
        }
    }
}
