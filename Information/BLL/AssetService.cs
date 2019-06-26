using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IocContainer;
using DAO;
using IDAO;
using Entity;
using System.Linq.Expressions;

namespace BLL
{
   public class AssetService:IAssetBll
    {
        IAssetDao iad = IocCreate.CreateAll<AssetDao>("AssetOne", "AssetDao");

        public int Add(info_Asset t)
        {
            return iad.Add(t);
        }

        public int Delete(info_Asset t)
        {
            return iad.Delete(t);
        }
        public List<info_Asset> SelectAll()
        {
            return iad.SelectAll();
        }

        public List<v_Asset> SelectV_Asset<K>(Expression<Func<v_Asset, K>> order, Expression<Func<v_Asset, bool>> where, out int rows, int currentPage, int pageSize)
        {
             return iad.SelectV_Asset(order,where, out rows, currentPage, pageSize);
        }

        public List<info_Asset> SelectWhere(Expression<Func<info_Asset, bool>> where)
        {
            return iad.SelectWhere(where);
        }

        public int Update(info_Asset t)
        {
            return iad.Update(t);
        }
    }
}
