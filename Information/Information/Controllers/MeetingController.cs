using BLL;
using Entity;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace information.Controllers
{
    public class MeetingController : Controller
    {
        IMeetingBll imb = IocContainer.IocCreate.CreateAll<MeetingService>("MeetingTwo", "MeetingService");
        IUserBll iub = IocContainer.IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        IMaintenanceBll img = IocContainer.IocCreate.CreateAll<MaintenanceService>("MaintenanceTwo", "MaintenanceService");
        ISoftwareBll isb = IocContainer.IocCreate.CreateAll<SoftwareService>("SoftwareTwo", "SoftwareService");//软件     
        ICheckBll icb = IocContainer.IocCreate.CreateAll<CheckService>("CheckTwo", "CheckService");
        // GET: Meeting视频会议测试及开会记录
        public ActionResult Index()
        {

            return View();
        }

        //视频会议测试及开会记录查询
        public ActionResult Index2(int currentpage, string names)
        {
            int rows;
            List<info_Meeting> dt;
            if (names == null || names.Equals("") || names.Equals("undefined"))
            {
                dt = imb.FenYe(e => e.MID, e => e.MID > 0, out rows, currentpage, 3);
            }
            else
            {

                //  dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names), out rows, currentpage, 3);
                dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names), out rows, currentpage, 3);
            }

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }

        //查询出无故障的签字
        public ActionResult IndexQZ2(int currentpage, string names)
        {
            int rows;
            List<info_Meeting> dt;
            if (names == null || names.Equals("") || names.Equals("undefined"))
            {
                dt = imb.FenYe(e => e.MID,e=>e.MSolve=="无"||e.MState=="审核成功", out rows, currentpage, 3);
            }
            else
            {
                dt = imb.FenYe(e => e.MID, e => e.MSolve == "无" ||( e.MSolve=="有"&& e.MState == "审核成功" && e.MName.Contains(names)), out rows, currentpage, 3);
            }

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }

        public ActionResult selectCB() {
            var im = icb.selectM();

            return Content(JsonConvert.SerializeObject(im));
        }

        // GET: Meeting/Details/5
        public ActionResult CXIndex(int currentpage, string names)
        {
            int rows;
            List<info_Meeting> dt;
            if (names == null || names.Equals("") || names.Equals("undefined"))
            {
                dt = imb.FenYe(e => e.MID,e=>e.QZ==2, out rows, currentpage, 3);
            }
            else
            {
                dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names)&&e.QZ==2, out rows, currentpage, 3);
            }

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }


        [HttpGet]
        // GET: Meeting/Create 视频会议测试及开会记录添加显示
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meeting/Create视频会议测试及开会记录统计
        [HttpPost]
        public ActionResult Create(info_Meeting im)
        {
            if (im.MSolve == "有")
            {
                im.QZ = 1;
                im.MState = "待审核";
            }
            else {
                im.QZ = 1;
            }
                // TODO: Add insert logic here
               
                int add = imb.Add(im);
                if (add > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("<script>alert('添加失败！');window.location.href='Index'</script>");
                }
        }

        // GET: Meeting/Edit/5视频会议测试及开会记录id查询
        public ActionResult Edit(int id)
        {
            info_Meeting me = imb.SelectWhere(e => e.MID == id).FirstOrDefault();

            return View(me);
        }

        // POST: Meeting/Edit/5视频会议测试及开会记录修改
        [HttpPost]
        public ActionResult Edit(info_Meeting im)
        {

            if (im.MSolve == "有")
            {
                im.QZ = 1;
                im.MState = "待审核";
            }
            else
            {
                im.QZ = 1;
                im.MState = "";
            }
            int update = imb.Update(im);
            if (update > 0)
            {
                return Content("<script>alert('修改成功！');window.location.href='/Meeting/Index'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！');window.location.href='Index'</script>");
            }

        }


        // GET: Meeting/Delete/5视频会议测试及开会记录删除
        public ActionResult Delete(int id)
        {
            info_Meeting im = new info_Meeting();
            im.MID = id;
            if (imb.Delete(im) > 0)
            {
                return Content("<script>alert('删除成功！');window.location.href='/Meeting/Index'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='Index'</script>");
            }

        }

        //会议测试及开会签字
        public ActionResult IndexQZ()
        {
            return View();
        }

        //会议测试及开会签字 查询真实姓名
        [HttpPost]
        public ActionResult selectUser()
        {
            List<info_User> list = iub.SelectAll();
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", list);
            return Content(JsonConvert.SerializeObject(list));
        }

        //会议测试及开会签字 修改签字
        public ActionResult MeetingUpdate(int mid, string urm)
        {
            //info_Meeting im = new info_Meeting();
            //im.MID = mid;
            info_Meeting im = imb.SelectWhere(e => e.MID == mid).FirstOrDefault();
            im.Msignature = urm;
            im.QZ = 2;
            //im.Msignature = urm;
            //im.MName = im.MName;
            int update = imb.Update(im);
            if (update > 0)
            {
                return Content("<script>window.location.href='/Meeting/Index'</script>");
            }
            else
            {
                return Content("<script>window.location.href='Index'</script>");
            }
        }

        //视频会议测试及开会记录查询
        public ActionResult selectCX()
        {
            return View();
        }
        //id查询
        public ActionResult selectId(int id)
        {
            info_Meeting me = imb.SelectWhere(e => e.MID == id).FirstOrDefault();
            return View(me);
        }

        //会议设备维修申请查询视图
        public ActionResult selectSQ() {
            return View();
        }

        //会议设备维修申请查询 有故障
        public ActionResult selectSQ2(int currentpage,string names)
        {
            int rows;
            List<info_Meeting> dt;
            if (names == null || names.Equals("") || names.Equals("undefined"))
            {
                dt = imb.FenYe(e => e.MID, e => e.MSolve == "有", out rows, currentpage, 3);
            }
            else
            {

                //  dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names), out rows, currentpage, 3);
                dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names) && e.MSolve == "有", out rows, currentpage, 3);
            }

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }

        //会议设备维修审核查询视图
        public ActionResult selectSH()
        {
            return View();
        }
        //会议设备维修审核
        public ActionResult EditSH(int id)
        {
            info_Meeting me = imb.SelectWhere(e => e.MID == id).FirstOrDefault();

            return View(me);
        }

        //会议设备维修审核
        public ActionResult EditSH2(info_Meeting im)
        {
            if (im.MOpinion == "同意") {
                im.MState = "审核成功";
            } else if (im.MOpinion == "不同意") {
                im.MState = "驳回";
            }
            im.QZ = 1;
            int update = imb.Update(im);
            if (update > 0)
            {
                return Content("<script>alert('审批完毕！');window.location.href='/Meeting/selectSH'</script>");
            }
            else {
                return Content("<script>alert('审批失败！');window.location.href='/Meeting/selectSH'</script>");
            }
         
        }
    }
}
