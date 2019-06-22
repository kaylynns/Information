using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IDAO;
namespace DAO
{

    public class AssetDao : BaseDao<info_Asset>,IAssetDao
    {
        BaseDao<v_Asset> bd = new BaseDao<v_Asset>();


        public List<v_Asset> SelectV_Asset<K>(Expression<Func<v_Asset, K>> order, Expression<Func<v_Asset, bool>> where, out int rows, int currentPage, int pageSize) {
            return bd.FenYe(order, where, out rows, currentPage, pageSize);
        }

    }
}
