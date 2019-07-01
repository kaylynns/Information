using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
   public interface ICheckDao
    {
        List<v_Maintenance> v_MainAll(Expression<Func<v_Maintenance, int>> order, Expression<Func<v_Maintenance, bool>> where, out int rows, int currentPage, int pageSize);
        List<info_Check> SelectAll();
        List<info_Check> SelectWhere(Expression<Func<info_Check, bool>> where);
        List<info_Check> FenYe<K>(Expression<Func<info_Check, K>> order, Expression<Func<info_Check, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Check t);
        int Delete(info_Check t);
        int Update(info_Check t);
    }
}
