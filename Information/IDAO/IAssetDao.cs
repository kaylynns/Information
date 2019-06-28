using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace IDAO
{
   public interface IAssetDao
    {
        //分页

        List<v_Asset> SelectV_Asset<K>(Expression<Func<v_Asset, K>> order, Expression<Func<v_Asset, bool>> where, out int rows, int currentPage, int pageSize);
        List<info_Asset> SelectWhere(Expression<Func<info_Asset, bool>> where);//根据条件查询
        int Add(info_Asset t);//添加
        int Delete(info_Asset t);//删除
        int Update(info_Asset t);//修改
        List<info_Asset> SelectAll();

    }
}
