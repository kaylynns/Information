using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IDAO;
using IocContainer;
using DAO;

namespace BLL
{
    public class PermissionService : IPermissionBll
    {
        IPermissionDao ipsd = IocCreate.CreateAll<PermissionDao>("PermissionOne", "PermissionDao");

        public int Add(string rid, string pid)
        {
            return ipsd.Add(rid, pid);
        }

        public int delete(int rid)
        {
            return ipsd.delete(rid);
        }
    }
}
