using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
   public interface IDetailedDao
    {
        //权限
        List<v_Detailed> detailed(string rid, string pid);
        //查询出所有权限
        List<v_DetailedSelect> DetailedSelect(string rid, string pid);
    }
}
