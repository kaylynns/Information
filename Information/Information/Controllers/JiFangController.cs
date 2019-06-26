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
    public class JiFangController : Controller
    {
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        //机房设备管理
        lIEquipmentBll shebei = IocCreate.CreateAll<IEquipmentService>("JiFangSheBeiTwo", "IEquipmentService");
        //用户表
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        //机房进出
        IComputerRoomVisitBll lcr =IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");
        // GET: JiFang
        //进入机房创建视图
        public ActionResult Index()
        {
            return View();
        }
        //机房进出申请的视图
        public ActionResult ChaXun(int id) {
            DengJiRen();
         V_ComputerRoomVisit cor=lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(cor);
        }
        //机房进出查询申请
        public ActionResult Index2(int currentPage ,string lai)
        {      
            List<V_ComputerRoomVisit> list;
            int rows = 0;
            if (lai=="" || lai == null)
            {
               list = lcr.FenYes(e => e.CID,e=>e.CRelustID=="待审核", out rows, currentPage, 3);   
            }
            else
            {
             list = lcr.FenYes(e => e.CID, e => e.Cause.Contains(lai), out rows, currentPage, 3);
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list",list);
            dic.Add("rows",rows);
            dic.Add("currentPage",rows%2>0?(rows/2)+1:(rows/2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }
        // GET: JiFang/Create
        //机房进出申请添加申请：视图
        public ActionResult Create()
        {
            ShengCheng();
            DengJiRen();
            return View();
        }
        //用户登陆进来的名字
        public void DengJiRen() {
            string name=Session["UserRealName"].ToString();
            ViewData["DengJiren"] = name;
        }
        //下拉框来自于用户表的
        public void ShengCheng() {
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
            ViewData["PeiTong"] = list;
        }
        // POST: JiFang/Create
        [HttpPost]//机房出入申请的添加操作
        public ActionResult Create(info_ComputerRoomVisit crv)
        {
            try
            {
                crv.CRelustID = 1;
                crv.CEntryTime=DateTime.Now;
                crv.CLeaveTime = DateTime.Now;
                crv.CRegistar = Session["UserRealName"].ToString();
                int tianjia = lcr.Add(crv);
                if (tianjia > 0)
                {
                    return Content("<script>alert('保存成功');window.location.href='Index'</script>");
                }
                else
                {
                    return Content("<script>alert('保存失败');window.location.href='Create'</script>");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: JiFang/Edit/5
        public ActionResult Edit(int id)
        {
            DengJiRen();
             V_ComputerRoomVisit dtc=lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(dtc);
        }

        // POST: JiFang/Edit/5
        [HttpPost]
        public ActionResult Edit(info_ComputerRoomVisit cr)
        {
            try
            {
               int cmr=lcr.Update(cr);
                if (cmr>0)
                {
                    return Content("<script>alert('修改成功');window.location.href='Index'</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败');window.location.href='Edit'</script>");
                }
            }
            catch
            {
                return View();
            }
        }
        //进入机房审批创建视图
        public  ActionResult ShenPiShiTu() {
            return View();
        }
        //审批查询
         public ActionResult ShenPiSelect(int currentPage, string id, string ids) {
            List<V_ComputerRoomVisit> list;
            int rows = 0;
            list = lcr.FenYes(e => e.CID, e => e.CRelustID == id || e.CRelustID == ids, out rows, currentPage, 3);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list", list);
            dic.Add("rows", rows);
            dic.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }
        //审核通过
        public ActionResult ShenPiTongGuo(int id) {
            //DengJiRen();
            V_ComputerRoomVisit cor = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(cor);
        }
        //驳回
        public ActionResult ShenPiBoHui(int id) {
            //DengJiRen();
            V_ComputerRoomVisit cor = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(cor);
        }
        //待审核
        public ActionResult DaiShenHe(int id) {
            //DengJiRen();
            info_ComputerRoomVisit lfc = lcr.SelectWheres(e => e.CID == id).FirstOrDefault();
            ChaXunPeiTong(id);
            return View(lfc);
        }
        [HttpPost]
        //待审核保存
        public ActionResult DaiShenHe(info_ComputerRoomVisit ifc) {
            info_ComputerRoomVisit cm = lcr.SelectWheres(e => e.CID == ifc.CID).FirstOrDefault();
            if (ifc.CXuanXiang == 1)
            {
                cm.CRelustID = 2;
                cm.CBringOut = ifc.CBringOut;
            }
            else
            {
                cm.CRelustID = 3;
            }
            cm.CInstructor= ifc.CRegistar;
            cm.CInstructorTime = DateTime.Now;
            cm.COpinion = ifc.COpinion;
            int cz = lcr.Update(cm);
            if (cz > 0)
            {
                return Content("<script>alert('提交成功');window.location.href='/JiFang/ShenPiShiTu'</script>");
            }
            else
            {
                return Content("<script>alert('提交失败');window.location.href='JiFang/ShenPiShiTu'</script>");
            }
        }
        //查询陪同人员的方法
        public void ChaXunPeiTong(int id)
        {
            V_ComputerRoomVisit c = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            ViewData["LaiFang"] = c.CEntourage;
        }
        //机房维护维修创建视图
        public ActionResult WeiHuWeiXiuShiTu()
        {
            return View();
        }
        public ActionResult WeiHuSelect(int currentPage) {
            return View();
        }
        //机房设备管理表
        public ActionResult SheBeiGuanLiShiTu() {
            return View();
        }
        public ActionResult SheBeiSelelct(int currentPage, string name, string lei) {
            List<V_info_Equipment> list;
            int rows = 0;
            if (name == ""|| name == null && lei == "" || lei == null)
            {
                list = shebei.FenYes(e => e.EID, e => e.EID>0, out rows, currentPage, 3);
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
        //机房设备管理编辑
        public ActionResult SheBeiBianJi(int id) {
          info_Equipment ts=shebei.SelectWhere(e => e.EID == id).FirstOrDefault();
          var eaid=ts.EAID;
          ViewBag.leibiao=iab.SelectWhere(e => e.AID == eaid);
            return View(ts);
        }
        [HttpPost]
        public ActionResult SheBeiBianJiXiu(info_Equipment zhi) {
            try
            {
                int SheBeiUpdate = shebei.Update(zhi);
                if (SheBeiUpdate > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='SheBeiGuanLiShiTu'</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败');window.location.href='SheBeiGuanLiShiTu'</script>");
                }
            }
            catch
            {
                return View("SheBeiGuanLiShiTu");
            }
        }
        //机房设备添加视图
        public ActionResult SheBeiAddShiTu() {
           
            return View();
        }
        [HttpPost]
        //添加机房设备信息
        public ActionResult SheBeiAddShiTu(info_Equipment len) {
            try
            {
            //    len.EAID = AID;
                int SheBeiAdd = shebei.Add(len);
                if (SheBeiAdd > 0)
                {
                    return Content("<script>alert('添加成功');window.location.href='SheBeiGuanLiShiTu'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');window.location.href='SheBeiGuanLiShiTu'</script>");
                }
            }
            catch
            {
                return View();
            }
        }
        //机房设备查询设备名称
        public ActionResult SheBeiChaLeiBie() {
            List<info_Asset> Yi = iab.SelectAll();
            return Content(JsonConvert.SerializeObject(Yi));
        }

        //进入机房进出查询
        public ActionResult JiFangJinChuSelelctAll()
        {

            return View();
        }
        //机房进出查询分页
        public ActionResult JiFangJinChuSelelctAllFen(int currentPage, string lai, string ren)
        {
            List<V_ComputerRoomVisit> list;
            int rows = 0;
            if (lai == "" && lai == null || ren == "" && ren == null)
            {
                list = lcr.FenYes(e => e.CID, e => e.CID > 0, out rows, currentPage, 3);
            }
            else
            {
                list = lcr.FenYes(e => e.CID, e => e.Cause.Contains(lai) && e.CName.Contains(ren), out rows, currentPage, 3);
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list", list);
            dic.Add("rows", rows);
            dic.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }

        //机房进出查询查看
        public ActionResult JiFangJinChuSelectLook(int id)
        {
            DengJiRen();
            var dt = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(dt);
        }

    }
}
