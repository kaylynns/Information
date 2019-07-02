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
   public class RegistrationService: IRegistrationBll
    {
        IRegistrationDao imd = IocContainer.IocCreate.CreateAll<RegistrationDao>("RegistrationOne", "RegistrationDao");
        public List<info_Registration> SelectAll()
        {
            return imd.SelectAll();
        }

        public int Add(info_Registration t)
        {
            return imd.Add(t);
        }

        public int Update(info_Registration t)
        {
            return imd.Update(t);
        }


        public List<info_Registration> SelectWhere(Expression<Func<info_Registration, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Delete(info_Registration a)
        {
            return imd.Delete(a);
        }

        public List<info_Registration> FenYe(Expression<Func<info_Registration, int>> order, Expression<Func<info_Registration, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }

        public List<v_info_Registration> v_MainAll(Expression<Func<v_info_Registration, int>> order, Expression<Func<v_info_Registration, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.v_MainAll(order, where, out rows, currentPage, pageSize);
        }

    }
}
