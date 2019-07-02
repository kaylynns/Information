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
    public class DetailedService : IDetailedBll
    {
        IDetailedDao idd = IocCreate.CreateAll<DetailedDao>("DetailedOne", "DetailedDao");

        public List<v_Detailed> detailed(string rid, string pid)
        {
            return idd.detailed(rid, pid);
        }
    }
}
