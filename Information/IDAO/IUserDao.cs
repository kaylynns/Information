using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace IDAO
{
   public interface IUserDao
    {
        List<info_User> DengLu(string username, string pass);
        List<info_User> SelectAll();
        List<info_User> SelectWhere(Expression<Func<info_User, bool>> where);
    }
}
