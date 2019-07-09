using BLL;
using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace information.Controllers
{
    public class WhuWuxiuSelectAllController : Controller
    {
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        IComputerRoomVisitBll lcr = IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");//机房进出
        IMaintenanceBll img = IocCreate.CreateAll<MaintenanceService>("MaintenanceTwo", "MaintenanceService");
        ICheckBll icb = IocCreate.CreateAll<CheckService>("CheckTwo", "CheckService");
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        //进入机房维护维修查询
        public ActionResult MaintainSelect()
        {
            return View();
        }
        //机房维护维修查询分页
        public ActionResult MaintainSelectFen(int currentPage, string MDeviceName)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 3;
            int rows;
            List<v_Maintenance> dt;
            if (MDeviceName == "" && MDeviceName == null)
            {
                //普通查询
                dt = icb.v_MainAll(e => e.MIDs, e => e.MIDs > 0, out rows, currentPage, pageSize);

            }
            else
            {
                //带条件查询
                dt = icb.v_MainAll(e => e.MIDs, e => e.MDeviceName.Contains(MDeviceName), out rows, currentPage, pageSize);

            }


            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }


        //查询陪同人员
        public void ShengCheng()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<info_User> ctr = iud.SelectAll();
            foreach (var item in ctr)
            {
                SelectListItem list2 = new SelectListItem()
                {
                    Text = item.UserRealName,
                    Value = item.UserID.ToString()
                };
                list.Add(list2);
            }
            ViewData["p"] = list;
        }
        //查询检测人员
        public List<SelectListItem> FillClass()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Entity.info_ComputerRoomVisit> lists = lcr.SelelctAll();
            foreach (Entity.info_ComputerRoomVisit dr in lists)
            {
                SelectListItem sl = new SelectListItem()
                {
                    Text = dr.CName,
                    Value = dr.CID.ToString()
                };
                list.Add(sl);
            }

            ViewData["s"] = list;
            return list;
        }

        // GET: Maintain/Details/5
        //
        //进入机房维护维修查看页面

        public ActionResult MaintainLook(int id)
        {
            ShengCheng();//查询陪同人员
            FillClass();   //查询检测人员
            info_Maintenance im = img.SelectWhere(e => e.MIDs == id).FirstOrDefault();
            info_Asset ies = iab.SelectWhere(e => e.AID == im.MDeviceName).FirstOrDefault();
            ViewData["a"] = ies.AName;
            return View(im);


        }


     

    
}
}