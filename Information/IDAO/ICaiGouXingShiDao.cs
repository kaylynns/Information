using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
    public interface ICaiGouXingShiDao
    {
        List<CaiGouXingShi> SelectAll();//查询全部
        List<CaiGouXingShi> SelectWhere(Expression<Func<CaiGouXingShi, bool>> where);//根据条件查询
        List<CaiGouXingShi> FenYe<K>(Expression<Func<CaiGouXingShi, K>> order, Expression<Func<CaiGouXingShi, bool>> where, out int rows, int currentPage, int pageSize);//分页查询
        int Add(CaiGouXingShi t);//添加
        int Delete(CaiGouXingShi t);//删除
        int Update(CaiGouXingShi t);//修改

    }
}
