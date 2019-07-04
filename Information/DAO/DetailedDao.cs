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
        //根据登陆进来的人查询出它的权限
        public List<v_Detailed> detailed(string rid, string pid) {
            string sql = string.Format(@"select  d.id, text, d.pid, ProAddress, state  from [dbo].[info_Detailed] d
inner join (select * from [dbo].[info_Permission] where [RoleID]='{0}') pr on d.id=pr.id
where d.pid='{1}'", rid, pid);
            return cha(sql);
        }
        BaseDao<v_DetailedSelect> bd = new BaseDao<v_DetailedSelect>();
        //查询出所有权限
        public List<v_DetailedSelect> DetailedSelect(string rid, string pid)
        {
            string sql = string.Format(@"select d.id, text, ProAddress,d.pid, state,
                case when p.id  is null then 0
                else 1
                end as checked
                from  [dbo].[info_Detailed] d
                left join (select [id] from [dbo].[info_Permission] where [RoleID]='{0}') p on d.id=p.id
                where d.pid='{1}'", rid, pid);
            return bd.cha(sql);
        }

    }
}
