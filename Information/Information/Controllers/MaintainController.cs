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
    public class MaintainController : Controller
    {
        IComputerRoomVisitBll lcr = IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");
        // GET: Maintain
        //进入机房维护维修查询
        public ActionResult MaintainSelect()
        {
            return View();
        }
        //机房维护维修查询分页
        //public ActionResult MaintainSelectFen(int currentPage, string MDeviceName)
        //{
        //    //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
        //    var pageSize = 2;
        //    int rows;
        //    List<info_Maintenance> dt;
        //    if (MDeviceName == "" && MDeviceName == null )
        //    {
        //        //普通查询
        //         dt = iab.SelectV_Asset(e => e.MID, e => e.MID > 0, out rows, currentPage, pageSize);
        //    }
        //    else
        //    {
        //        //带条件查询
        //          dt = iab.SelectV_Asset(e => e.MID, e => e.MDeviceName.Contains(MDeviceName), out rows, currentPage, pageSize);
        //    }


        //    Dictionary<string, object> dir = new Dictionary<string, object>();
        //    dir.Add("data", dt);
        //    dir.Add("rows", rows);
        //    dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
        //    dir.Add("pages", (rows - 1) / pageSize + 1);
        //    return Content(JsonConvert.SerializeObject(dir));

        //}

        // GET: Maintain/Details/5
        //
        //进入机房维护维修查看页面
           
        public ActionResult MaintainLook(int id)
        {
            // var ml = iab.SelectWhere(e => e.MID == id).FirstOrDefault(); //根据id查询
            // return View(ml);
            return View();
        }

        //设备维护查询页面的检测查询

        public ActionResult DetectionSelect()
        {
            //var Yi = icgyjb.SelectAll();
            //return Content(JsonConvert.SerializeObject(Yi));
            return View();
        }
        //进入机房进出查询
        public ActionResult JiFangJinChuSelelctAll() {

            return View();
        }
        //机房进出查询分页
        public ActionResult JiFangJinChuSelelctAllFen(int currentPage, string lai, string ren)
        {
            List<V_ComputerRoomVisit> list;
            int rows = 0;
                list = lcr.FenYes(e => e.CID, e => e.Cause.Contains(lai) && e.CName.Contains(ren), out rows, currentPage, 2);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list", list);
            dic.Add("rows", rows);
            dic.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dic.Add("pages", (rows - 1) / 2 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }


        //获取登记人显示到页面
        public void DengJiRen()
        {
            string name = Session["UserRealName"].ToString();
            ViewData["DengJiren"] = name;
        }

        //机房进出查询查看
        public ActionResult JiFangJinChuSelectLook(int id) {
            DengJiRen();
          var dt=  lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(dt);
        }


    }
}
