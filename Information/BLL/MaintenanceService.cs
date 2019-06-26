using DAO;
using Entity;
using IBLL;
using IDAO;
using IocContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class MaintenanceService: IMaintenanceBll
    {
        IMaintenanceDao iad = IocCreate.CreateAll<MaintenanceDao>("MaintenanceOne", "MaintenanceDao");

        public List<info_Maintenance> SelectAll()
        {
            return iad.SelectAll();
        }
        public int Add(info_Maintenance t)
        {
            return iad.Add(t);
        }

        public int Delete(info_Maintenance t)
        {
            return iad.Delete(t);
        }

        public List<info_Maintenance> FenYe(Expression<Func<info_Maintenance, int>> order, Expression<Func<info_Maintenance, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return iad.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<info_Maintenance> SelectWhere(Expression<Func<info_Maintenance, bool>> where)
        {
            return iad.SelectWhere(where);
        }

        public int Update(info_Maintenance t)
        {
            return iad.Update(t);
        }

    }
}

