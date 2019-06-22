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
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        IComputerRoomVisitBll lcr =IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");
        // GET: JiFang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChaXun(int id) {
            DengJiRen();
         V_ComputerRoomVisit cor=lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(cor);
        }
        public ActionResult Index2(int currentPage ,string lai,string ren)
        {      
            List<V_ComputerRoomVisit> list;
            int rows = 0;
            if (lai==""|| lai == null && ren==""|| ren == null)
            {
               list = lcr.FenYes(e => e.CID,e=>e.CRelustID=="待审核", out rows, currentPage, 3);   
            }
            else
            {
             list = lcr.FenYes(e => e.CID, e => e.Cause.Contains(lai) && e.CName.Contains(ren), out rows, currentPage, 3);
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list",list);
            dic.Add("rows",rows);
            dic.Add("currentPage",rows%2>0?(rows/2)+1:(rows/2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }
        // GET: JiFang/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JiFang/Create
        public ActionResult Create()
        {
            ShengCheng();
            DengJiRen();
            return View();
        }
        public void DengJiRen() {
            string name=Session["UserRealName"].ToString();
            ViewData["DengJiren"] = name;
        }
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
        [HttpPost]
        public ActionResult Create(info_ComputerRoomVisit crv)
        {
            try
            {
                crv.CRelustID = 1;
                crv.CEntryTime=DateTime.Now;
                crv.CLeaveTime = DateTime.Now;
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

        // GET: JiFang/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
            
        // POST: JiFang/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
