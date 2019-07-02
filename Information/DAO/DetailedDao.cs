using Entity;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
 public class DetailedDao:BaseDao<v_Detailed>,IDetailedDao
    {
        //权限
        public List<v_Detailed> detailed(string rid, string pid) {
            string sql = string.Format(@"select  d.id, text, d.pid, ProAddress, state  from [dbo].[info_Detailed] d
inner join (select * from [dbo].[info_Permission] where [RoleID]='{0}') pr on d.id=pr.id
where d.pid='{1}'", rid, pid);
            return cha(sql);
        }
    }
}
