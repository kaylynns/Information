using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
   public interface IZiChanBeiZhuDao
    {
        List<ZiChanBeiZhu> SelectAll();//查询全部
        int Add(ZiChanBeiZhu t);//添加
        List<ZiChanBeiZhu> SelectWhere(Expression<Func<ZiChanBeiZhu, bool>> where);//根据条件查询
    }
}
