using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
    public interface IEquipmentsDao
    {
        List<info_Equipments> SelectAll();
        List<info_Equipments> SelectWhere(Expression<Func<info_Equipments, bool>> where);
        List<info_Equipments> FenYe<K>(Expression<Func<info_Equipments, K>> order, Expression<Func<info_Equipments, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Equipments t);
        int Delete(info_Equipments t);
        int Update(info_Equipments t);
    }

}

