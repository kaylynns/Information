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
    public  class CheckService: ICheckBll
    {
        ICheckDao imd = IocContainer.IocCreate.CreateAll<CheckDao>("CheckOne", "CheckDao");
        public List<info_Check> SelectAll()
        {
            return imd.SelectAll();
        }
        
        public int Add(info_Check t)
        {
            return imd.Add(t);
        }

        public int Update(info_Check t)
        {
            return imd.Update(t);
        }


        public List<info_Check> SelectWhere(Expression<Func<info_Check, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Delete(info_Check a)
        {
            return imd.Delete(a);
        }

        public List<info_Check> FenYe(Expression<Func<info_Check, int>> order, Expression<Func<info_Check, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order,where, out rows,currentPage,pageSize);
        }

        public List<v_Maintenance> v_MainAll(Expression<Func<v_Maintenance, int>> order, Expression<Func<v_Maintenance, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.v_MainAll(order, where, out rows, currentPage, pageSize);
        }


    }
}
