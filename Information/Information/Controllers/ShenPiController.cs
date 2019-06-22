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
    public class ShenPiController : Controller
    {
        IComputerRoomVisitBll lcr = IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");
        // GET: ShenPi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChaXun(int id)
        {
            DengJiRen();
            V_ComputerRoomVisit cor = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(cor);
        }
        public void DengJiRen()
        {
            string name = Session["UserRealName"].ToString();
            ViewData["DengJiren"] = name;
        }
        public ActionResult Index2(int currentPage, string id, string ids)
        {
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
        //待审核
        public ActionResult DaiShenHe(int id)
        {
            DengJiRen();
            info_ComputerRoomVisit lfc = lcr.SelectWheres(e => e.CID == id).FirstOrDefault();
            ChaXunPeiTong(id);
            return View(lfc);
        }
        public void ChaXunPeiTong(int id)
        {
            V_ComputerRoomVisit c = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            ViewData["LaiFang"] = c.CEntourage;
        }
        [HttpPost]
        public ActionResult DaiShenHe(info_ComputerRoomVisit ifc)
        {
            info_ComputerRoomVisit cm = lcr.SelectWheres(e => e.CID == ifc.CID).FirstOrDefault();
            if (ifc.CXuanXiang == 1)
            {
                cm.CRelustID = 2;
            }
            else
            {
                cm.CRelustID = 3;
            }
            int cz = lcr.Update(cm);
            if (cz > 0)
            {
                return Content("<script>alert('提交成功');window.location.href='/ShenPi/Index'</script>");
            }
            else
            {
                return Content("<script>alert('提交失败');window.location.href='Index'</script>");
            }

        }
        // GET: ShenPi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShenPi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShenPi/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShenPi/Edit/5
        public ActionResult Edit(int id)
        {
            DengJiRen();
            V_ComputerRoomVisit cor = lcr.SelectWhere(e => e.CID == id).FirstOrDefault();
            return View(cor);
        }

        // POST: ShenPi/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShenPi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShenPi/Delete/5
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
