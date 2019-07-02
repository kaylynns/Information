using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface IDetailedBll
    {
        //权限
        List<v_Detailed> detailed(string rid, string pid);
    }
}
