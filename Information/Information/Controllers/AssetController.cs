using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using Entity;

namespace information.Controllers
{
    public class AssetController : Controller
    {
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        ICaiGouXingShiBll icgxsb = IocCreate.CreateAll<CaiGouXingShiService>("CaiGouXingShiTwo", "CaiGouXingShiService");//采购形式
        IZiChanBeiZhuBll izcbzb = IocCreate.CreateAll<ZiChanBeiZhuService>("ZiChanBeiZhuTwo", "ZiChanBeiZhuService");//资产备注
        ICaiGouYiJuBll icgyjb = IocCreate.CreateAll<CaiGouYiJuService>("CaiGouYiJuTwo", "CaiGouYiJuService");//采购依据
        // GET: Asset
        public ActionResult Index()
        {
            selectType();

            return View();
        }
        public ActionResult index2(int currentPage)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 2;
            int rows;
            var dt = iab.SelectV_Asset(e => e.AID, e => e.AID>0, out rows, currentPage, pageSize);
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            //dir.Add("currentPage",rows%2>0?(rows/2)+1 : (rows/2) );
            dir.Add("pages", (rows - 1) /pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir)) ;
         
        }
        // GET: Asset/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //采购形式
        private void selectXinShi()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var dt = icgxsb.SelectAll();
            foreach (var dr in dt)
            {

                SelectListItem cl = new SelectListItem()
                {
                    Value = dr.SID.ToString(),
                    Text = dr.SName.ToString()
                };
                 list.Add(cl);
            }
         ViewData["xs"] = list;  
        }
        //资产类别（查询）
        public ActionResult selectZCType() {
            var dt = izcbzb.SelectAll();
            return Content(JsonConvert.SerializeObject(dt));
        }
        //资产类别（下拉框）
        private void selectType()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var dt = izcbzb.SelectAll();
            foreach (var dr in dt)
            {

                SelectListItem cl = new SelectListItem()
                {
                    Value = dr.ATypeId.ToString(),
                    Text = dr.AName.ToString()
                };
                list.Add(cl);
            }
            ViewData["type"] = list;
        }

        //采购依据查询
        public ActionResult selectYiJu() {
            var Yi=icgyjb.SelectAll();
            return Content(JsonConvert.SerializeObject(Yi));
        }
        // GET: Asset/Create
        public ActionResult Create()
        {
            selectXinShi();
            selectType();
            return View();
        }

        // POST: Asset/Create
        [HttpPost]
        public ActionResult Create(info_Asset asset)
        {
            try
            {
                // TODO: Add insert logic here
                // asset.RealName = Session["UserRealName"].ToString();
                asset.TID = 1;
                if (iab.Add(asset) > 0)
                {
                    return Content("<script>alert('添加成功');window.location.href='/Asset/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');window.location.href='/Asset/Index'</script>");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(int id)
        {
            selectXinShi();
            selectType();
            var asset=iab.SelectWhere(e => e.AID == id).FirstOrDefault();
              var JID= asset.JID;
            var jname=icgyjb.SelectWhere(f => f.JID == JID);
            ViewBag.jname = jname;
            return View(asset);
        }

        // POST: Asset/Edit/5
        [HttpPost]
        public ActionResult Edit(info_Asset asset)
        {
            try
            {
                // TODO: Add insert logic here
                // asset.RealName = Session["UserRealName"].ToString();
                if (iab.Update(asset) > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='/Asset/Index'</script>");
                }
                else {
                    return Content("<script>alert('修改失败');window.location.href='/Asset/Index'</script>");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int id)
        {
            info_Asset asset = new info_Asset
            {
                AID = id
            };
            if (iab.Delete(asset) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Asset/Index'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/Asset/Index'</script>");
            }

           
        }

        // POST: Asset/Delete/5
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
