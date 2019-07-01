using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;
using IocContainer;
using IDAO;
using DAO;
namespace BLL
{
  public  class RoleService : IRoleBll
    {
        IRoleDao ird = IocCreate.CreateAll<RoleDao>("RoleOne", "RoleDao");

        public int Add(info_Role t)
        {
            return ird.Add(t);
        }

        public int Delete(info_Role t)
        {
            return ird.Delete(t);
        }

        public List<info_Role> SelectAll()
        {
            return ird.SelectAll();
        }

        public List<info_Role> SelectWhere(Expression<Func<info_Role, bool>> where)
        {
            return ird.SelectWhere(where);
        }

        public int Update(info_Role t)
        {
            return ird.Update(t);
        }
    }
}
