using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
    public interface IMaintenanceDao
    {
        List<info_Maintenance> SelectAll();
        List<info_Maintenance> SelectWhere(Expression<Func<info_Maintenance, bool>> where);
        List<info_Maintenance> FenYe<K>(Expression<Func<info_Maintenance, K>> order, Expression<Func<info_Maintenance, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Maintenance t);
        int Delete(info_Maintenance t);
        int Update(info_Maintenance t);
    }
}

