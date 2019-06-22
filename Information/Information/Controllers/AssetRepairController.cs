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
    public class AssetRepairController : Controller
    {
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        ICaiGouXingShiBll icgxsb = IocCreate.CreateAll<CaiGouXingShiService>("CaiGouXingShiTwo", "CaiGouXingShiService");//采购形式
        IZiChanBeiZhuBll izcbzb = IocCreate.CreateAll<ZiChanBeiZhuService>("ZiChanBeiZhuTwo", "ZiChanBeiZhuService");//资产备注
        ICaiGouYiJuBll icgyjb = IocCreate.CreateAll<CaiGouYiJuService>("CaiGouYiJuTwo", "CaiGouYiJuService");//采购依据
        IPiShiJieGuoBll ipsjgb = IocCreate.CreateAll<PiShiJieGuoService>("PiShiJieGuoTwo", "PiShiJieGuoService");//批示结果
        // GET: AssetRepair
        public ActionResult AssetRepairSelect()
        {
            return View();
        }
        public ActionResult index2(int currentPage)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 2;
            int rows;
            var dt = iab.SelectV_Asset(e => e.AID, e => e.AID > 0, out rows, currentPage, pageSize);
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            //dir.Add("currentPage",rows%2>0?(rows/2)+1 : (rows/2) );
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }


        // GET: AssetRepair/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetRepair/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetRepair/Create
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

        // GET: AssetRepair/Edit/5
        public ActionResult AssetRepairEdit(int id)
        {
            var dt=iab.SelectWhere(e => e.AID == id).FirstOrDefault();
            var ATypeId = dt.ATypeId;//资产类别
            var JID = dt.JID;//采购依据
            var SID = dt.SID;//采购形式
            var CRelustID = dt.CRelustID;//批示结果

            ViewBag.ATypeId = izcbzb.SelectWhere(e => e.ATypeId == ATypeId);
             ViewBag.JID= icgyjb.SelectWhere(e => e.JID == JID);
             ViewBag.SID= icgxsb.SelectWhere(e => e.SID == SID);
            // ViewBag.CRelustID = ipsjgb.SelectAll();
            return View(dt);
        }

        // POST: AssetRepair/Edit/5
        [HttpPost]
        public ActionResult Edit(info_Asset asset)
        {
            try
            {
                asset.CRelustID = 1;
                // TODO: Add update logic here
                if (iab.Update(asset) > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='/AssetRepair/AssetRepairSelect'</script>");
                }
                else {
                    return Content("<script>alert('修改成功');window.location.href='/AssetRepair/AssetRepairSelect'</script>");
                }
              
            }
            catch
            {
                return RedirectToAction("AssetRepairSelect");
            }
        }

        // GET: AssetRepair/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssetRepair/Delete/5
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
