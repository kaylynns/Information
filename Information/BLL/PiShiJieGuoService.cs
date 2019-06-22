using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IDAO;
using IocContainer;
using DAO;

namespace BLL
{
    public class PiShiJieGuoService : IPiShiJieGuoBll
    {
        IPiShiJieGuoDao ipsjgd = IocCreate.CreateAll<PiShiJieGuoDao>("PiShiJieGuoOne", "PiShiJieGuoDao"); //批示结果
        public List<PiShiJieGuo> SelectAll()
        {
            return ipsjgd.SelectAll();
        }
    }
}
