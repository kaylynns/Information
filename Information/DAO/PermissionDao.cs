using Entity;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class PermissionDao:BaseDao<info_Permission>, IPermissionDao
    {
        public int delete(int rid)
        {
            var sql = string.Format(@"DELETE FROM info_Permission where [RoleID]='{0}'", rid);
            return ExecuteSql(sql);
        }
        public int Add(string rid,string pid)
        {
            var sql = string.Format(@"insert into info_Permission(RoleID, id) values('{0}','{1}')", rid, pid);
            return ExecuteSql(sql);
        }
    }
}
