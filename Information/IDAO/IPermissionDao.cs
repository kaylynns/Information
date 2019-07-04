using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
   public interface IPermissionDao
    {
        //先删除
        int delete(int rid);
        //再添加
        int Add(string rid, string pid);
        

    }
}
