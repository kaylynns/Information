using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
  public  interface IRoleDao
    {
        //查询全部
        List<info_Role> SelectAll();
        //根据添加查询
        List<info_Role> SelectWhere(Expression<Func<info_Role, bool>> where);
        //修改
        int Update(info_Role t);
        //删除
        int Delete(info_Role t);
        //添加
        int Add(info_Role t);
    }
}
