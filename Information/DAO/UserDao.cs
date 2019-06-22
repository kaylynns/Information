using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAO;
using Entity;
namespace DAO
{
   public class UserDao:BaseDao<info_User>,IUserDao
    {
        public List<info_User> DengLu(string username, string pass) {
            return SelectWhere(e=>e.UserName==username&&e.UserPass==pass);
        }
        
    }
}
