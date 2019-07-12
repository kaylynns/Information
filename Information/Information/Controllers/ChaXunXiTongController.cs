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
    public class ChaXunXiTongController : Controller
    {
        //检测
        ICheckBll icb = IocContainer.IocCreate.CreateAll<CheckService>("CheckTwo", "CheckService");
        //视频会议
        IMeetingBll imb = IocContainer.IocCreate.CreateAll<MeetingService>("MeetingTwo", "MeetingService");
        IComputerRoomVisitBll lcr = IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");
        IMaintenanceBll img = IocContainer.IocCreate.CreateAll<MaintenanceService>("MaintenanceTwo", "MaintenanceService");
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        //机房设备管理
        lIEquipmentBll shebei = IocCreate.CreateAll<IEquipmentService>("JiFangSheBeiTwo", "IEquipmentService");
        //用户表
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        IRegistrationBll ird = IocContainer.IocCreate.CreateAll<RegistrationService>("RegistrationTwo", "RegistrationService");//业务系统
        ISoftwareBll isb = IocContainer.IocCreate.CreateAll<SoftwareService>("SoftwareTwo", "SoftwareService");//软件
        // GET: ChaXunXiTong
        //查询系统机房进出查询视图
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelelctAllJinChu(int currentPage, string lai, string pei) {
            List<V_ComputerRoomVisit> list;
            int rows = 0;
            if (lai == "" && lai == null && pei == "" && pei == null)
            {
                list = lcr.FenYes(e => e.CID, e => e.CID > 0, out rows, currentPage, 3);
            }
            else
            {
                list = lcr.FenYes(e => e.CID, e => e.Cause.Contains(lai) && e.CEntourage.Contains(pei), out rows, currentPage, 3);
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list", list);
            dic.Add("rows", rows);
            dic.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            //反序列
            return Content(JsonConvert.SerializeObject(dic));
        }
        //查询系统机房进出审核通过视图
        public ActionResult SelelctAllShenHe(int id) {
            V_ComputerRoomVisit cop = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            //info_ComputerRoomVisit cop = lcr.SelectWheres(e => e.CID == id).FirstOrDefault();
            ChaXunPeiTong(id);
            return View(cop);
        }
        //查询系统机房进出驳回视图
        public ActionResult SelelctAllBoHui(int id)
        {
            V_ComputerRoomVisit cop = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();

            ChaXunPeiTong(id);
            return View(cop);
        }
        //查询陪同人员的方法
        public void ChaXunPeiTong(int id)
        {
            V_ComputerRoomVisit c = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            ViewData["PeiTong"] = c.CEntourage;
        }
        //视频会议查询视图
        public ActionResult HuiYiChaXun() {
            return View();
        }
        //视频会议测试及开会记录查询
        public ActionResult SelectHuiYiChaXun(int currentpage, string names) {
            int rows;
            List<info_Meeting> dt;
            if (names == null || names.Equals("") || names.Equals("undefined"))
            {
                dt = imb.FenYe(e => e.MID, e => e.MID >0, out rows, currentpage, 3);
            }
            else
            {
                dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names), out rows, currentpage, 3);
            }

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }
        //查看视频会议测试及开会记录查询
        public ActionResult SelelctHuiYiChaKan(int id) {
            info_Meeting me = imb.SelectWhere(e => e.MID == id).FirstOrDefault();
            return View(me);
        }
        //机房维护维修查询视图
        public ActionResult SelectTongJiWeiHuWeiXiu() {
            return View();
        }
        //机房维护维修查询
        public ActionResult SelelctTongWeiHuWeiXiuAll(int currentpage, string names) {
            int rows;
            List<v_Maintenance> dt;
            if (names == null || names.Equals("") || names.Equals("undefined"))
            {
                dt = icb.v_MainAll(e => e.MIDs, e => e.MIDs > 0, out rows, currentpage, 3);
            }
            else
            {
                dt = icb.v_MainAll(e => e.MIDs, e => e.MDeviceName.Contains(names), out rows, currentpage, 3);
            }

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }
        //点击机房维护维修查看
        public ActionResult SelectTongJiWeiHuWeiXiuChaKan(int id) {

            info_Maintenance im = img.SelectWhere(e => e.MIDs == id).FirstOrDefault();


           info_Software iss= isb.SelectWhere(e => e.SID == im.MDeviceName).FirstOrDefault();
           
            ViewData["a"] = iss.Sdynacomm;

           
           
            //查询陪同人员
            int mm = Convert.ToInt32(im.MAccompanyingOfficials);
            info_User iu = iud.SelectWhere(e => e.UserID == mm).FirstOrDefault();
            ViewData["p"] = iu.UserRealName;

            //查询检测人员
            int rr = Convert.ToInt32(im.MTestingPersonnel);
            info_ComputerRoomVisit ir = lcr.SelectWheres(e => e.CID == rr).FirstOrDefault();
            ViewData["s"] = ir.CName;
            return View(im);
        }
        
        //机房设备查询视图
        public ActionResult JiFangSheBeiSelelctAll() {
            return View();
        }
        //机房设备查询
        public ActionResult JiFangSheBeiCha(int currentPage, string name, string lei) {
            List<V_info_Equipment> list;
            int rows = 0;
            if (name == "" || name == null && lei == "" || lei == null)
            {
                list = shebei.FenYes(e => e.EID, e => e.EID > 0, out rows, currentPage, 3);
            }
            else
            {
                list = shebei.FenYes(e => e.EID, e => e.EAID.Contains(name) && e.EquipmentModel.Contains(lei), out rows, currentPage, 3);
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list", list);
            dic.Add("rows", rows);
            dic.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }
        //机房设备查询查看编辑
        public ActionResult JiFangSheBeiChaKan(int id) {
            info_Equipment ts = shebei.SelectWhere(e => e.EID == id).FirstOrDefault();
            var eaid = ts.EAID;
            ViewBag.leibiao = isb.SelectWhere(e => e.SID == eaid);
            return View(ts);
        }

        //业务系统使用查询视图
        public ActionResult IndexR() {
            return View();
        }

        //业务系统使用查询
        public ActionResult IndexRs(int currentpage, string names, string rname)
        {
            int rows;
            List<v_info_Registration> dt;
            if (names == null && rname == null)
            {
                dt = ird.v_MainAll(e => e.RID, e => e.RID > 0, out rows, currentpage, 3);
            }
            else
            {

                //  dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names), out rows, currentpage, 3);
                dt = ird.v_MainAll(e => e.RID, e => e.SIDS.Contains(names) && e.RName.Contains(rname), out rows, currentpage, 3);
            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }
    }
}