using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
namespace IBLL
{
    public interface IUserBll
    {
        List<info_User> DengLu(string username, string pass);
        List<info_User> SelectAll();
    }
}
