using DAO;
using Entity;
using IBLL;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public  class EquipmentService: IEquipmentBll
    {
        IEquipmentDao imd = IocContainer.IocCreate.CreateAll<EquipmentDao>("EquipmentOne", "EquipmentDao");

        public int Add(info_Equipments t)
        {
            return imd.Add(t);
        }

        public int Delete(info_Equipments t)
        {
            return imd.Delete(t);

        }

        public List<info_Equipments> FenYe(Expression<Func<info_Equipments, int>> order, Expression<Func<info_Equipments, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<info_Equipments> SelectAll()
        {
            return imd.SelectAll();
        }

        public List<info_Equipments> SelectWhere(Expression<Func<info_Equipments, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Update(info_Equipments t)
        {
            return imd.Update(t);
        }
    }
}
