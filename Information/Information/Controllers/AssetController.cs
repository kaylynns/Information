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

        #region 资产登记


        // GET: Asset
        //进入资产登记页面
        public ActionResult AssetSelect()
        {
            selectType(); //资产类别
            return View();
        }
        //资产登记页面分页查询
        public ActionResult AssetSelectFen(int currentPage, string AType, string AName)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 2;
            int rows;
            List<v_Asset> dt;
            if (AType == "" && AType == null || AName == "" && AName == null)
            {
                //普通查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.AID > 0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.ATypeId.Contains(AType) && e.AName.Contains(AName), out rows, currentPage, pageSize);
            }


            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }

        //采购形式（查询）
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
        public ActionResult selectZCType()
        {
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
        //采购依据（查询）
        public ActionResult selectYiJu()
        {
            var Yi = icgyjb.SelectAll();
            return Content(JsonConvert.SerializeObject(Yi));
        }
        //资产状态（查询）

        // GET: Asset/Create
        //资产登记页面添加（进入添加页面）
        public ActionResult AssetCreate()
        {
            selectXinShi(); //采购形式查询
            selectType();   //资产类别
            return View();
        }

        // POST: Asset/Create
        //资产登记页面添加（进行添加操作）
        [HttpPost]
        public ActionResult AssetCreates(info_Asset asset)
        {
            try
            {
                // TODO: Add insert logic here
                asset.RealName = Session["UserRealName"].ToString();
                asset.TID = 1;
                if (iab.Add(asset) > 0)
                {
                    return Content("<script>alert('添加成功');window.location.href='/Asset/AssetSelect'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');window.location.href='/Asset/AssetSelect'</script>");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Asset/Edit/5
        //资产登记页面修改（进入修改页面）
        public ActionResult AssetEdit(int id)
        {
            selectXinShi(); //采购形式查询
            selectType();   //资产类别
            var asset = iab.SelectWhere(e => e.AID == id).FirstOrDefault(); //根据id查询
            var JID = asset.JID;  //获取采购依据
            var jname = icgyjb.SelectWhere(f => f.JID == JID); //根据采购依据进行查询
            ViewBag.jname = jname; //将查询结果存入viewBag
            return View(asset);
        }

        // POST: Asset/Edit/5
        //资产登记页面修改（进行修改操作）
        [HttpPost]
        public ActionResult AssetEdits(info_Asset asset)
        {
            try
            {
                // TODO: Add insert logic here
                asset.RealName = Session["UserRealName"].ToString();
                asset.TID = 1;
                if (iab.Update(asset) > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='/Asset/AssetSelect'</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败');window.location.href='/Asset/AssetSelect'</script>");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        //资产登记页面删除（进行删除操作）
        // GET: Asset/Delete/5
        public ActionResult AssetDelete(int id)
        {
            info_Asset asset = new info_Asset
            {
                AID = id
            };
            if (iab.Delete(asset) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Asset/AssetSelect'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/Asset/AssetSelect'</script>");
            }


        }
        //资产类别添加
        [HttpPost]
        public ActionResult AssetCreateType(ZiChanBeiZhu Type)
        {
            try
            {
                // TODO: Add insert logic here

                if (izcbzb.Add(Type) > 0)
                {
                    return Content("<script>alert('添加成功');window.location.href='/Asset/AssetSelect'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');window.location.href='/Asset/AssetSelect'</script>");
                }
            }
            catch
            {
                return RedirectToAction("AssetSelect");
            }

        }
        #endregion
        #region 资产维修报废申请
        //进入资产维修申请页面
        public ActionResult AssetRepairSelect()
        {
            return View();
        }
        //进入资产维修申请页面分页查询
        public ActionResult AssetRepairSelectFen(int currentPage, string AType, string AName, string TName)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 2;
            int rows;

            List<v_Asset> dt;

            if (AType == "" && AType == null || AName == "" && AName == null || TName == "" && TName == null)
            {
                //普通查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.AID > 0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.ATypeId.Contains(AType) && e.AName.Contains(AName) && e.TID.Contains(TName), out rows, currentPage, pageSize);
            }

            // var dt = iab.SelectV_Asset(e => e.AID, e => e.AID > 0, out rows, currentPage, pageSize);
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            //dir.Add("currentPage",rows%2>0?(rows/2)+1 : (rows/2) );
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }

        // GET: AssetRepair/Edit/5
        //资产维修申请页面修改（进入修改页面）
        public ActionResult AssetRepairEdit(int id)
        {
            var dt = iab.SelectWhere(e => e.AID == id).FirstOrDefault();
            var ATypeId = dt.ATypeId;//资产类别
            var JID = dt.JID;//采购依据
            var SID = dt.SID;//采购形式
            var CRelustID = dt.CRelustID;//批示结果

            ViewBag.ATypeId = izcbzb.SelectWhere(e => e.ATypeId == ATypeId);
            ViewBag.JID = icgyjb.SelectWhere(e => e.JID == JID);
            ViewBag.SID = icgxsb.SelectWhere(e => e.SID == SID);
            // ViewBag.CRelustID = ipsjgb.SelectAll();
            return View(dt);
        }

        // POST: AssetRepair/Edit/5
        //资产维修申请页面修改（进行修改操作）
        [HttpPost]
        public ActionResult AssetRepairEdits(info_Asset asset)
        {
            try
            {
                asset.CRelustID = 1;
                // TODO: Add update logic here
                if (iab.Update(asset) > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='/Asset/AssetRepairSelect'</script>");
                }
                else
                {
                    return Content("<script>alert('修改成功');window.location.href='/Asset/AssetRepairSelect'</script>");
                }

            }
            catch
            {
                return RedirectToAction("AssetRepairSelect");
            }
        }
        #endregion
        #region 资产报废停用维修审核
        //进入资产审核申请页面
        public ActionResult AssetReviewSelect()
        {
            return View();
        }
        //进入资产审核申请页面分页查询
        public ActionResult AssetReviewFen(int currentPage, string AType, string AName, string TName)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 2;
            int rows;

            List<v_Asset> dt;

            if (AType == "" && AType == null || AName == "" && AName == null || TName == "" && TName == null)
            {
                //普通查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.CRelustID=="待审核", out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = iab.SelectV_Asset(e => e.AID,e => e.ATypeId.Contains(AType) && e.AName.Contains(AName) && e.TID.Contains(TName)&&e.CRelustID.Contains("待审核"), out rows, currentPage, pageSize);
            }

            // var dt = iab.SelectV_Asset(e => e.AID, e => e.AID > 0, out rows, currentPage, pageSize);
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            //dir.Add("currentPage",rows%2>0?(rows/2)+1 : (rows/2) );
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }

        //进入资产审核申请操作页面（进入修改页面）
        public ActionResult AssetReviewEdit(int id)
        {

            var dt = iab.SelectWhere(e => e.AID == id).FirstOrDefault();
            var ATypeId = dt.ATypeId;//资产类别
            var JID = dt.JID;//采购依据
            var SID = dt.SID;//采购形式
            ViewBag.ATypeId = izcbzb.SelectWhere(e => e.ATypeId == ATypeId);
            ViewBag.JID = icgyjb.SelectWhere(e => e.JID == JID);
            ViewBag.SID = icgxsb.SelectWhere(e => e.SID == SID);
            return View(dt);
        }

        //进入资产审核申请操作页面（进行修改操作）
        public ActionResult AssetReviewEdits(info_Asset asset)
        {
           asset.AShenHeRan = Session["UserRealName"].ToString();
            asset.AShenHeRiQi = DateTime.Now.ToString();
            if (asset.AShenHeJieGuo == "同意")
            {
                asset.CRelustID = 2;
            }
            else if (asset.AShenHeJieGuo == "不同意")
            {
                asset.CRelustID = 3;
            }
            if (iab.Update(asset) > 0)
            {
                return Content("<script>alert('修改成功');window.location.href='/Asset/AssetReviewSelect'</script>");
            }
            else
            {
                return Content("<script>alert('修改成功');window.location.href='/Asset/AssetReviewSelect'</script>");
            }
        }
        #endregion

        #region 资产报废停用维修查询
        //进入资产报废停用维修查询页面
        public ActionResult AssetStatusSelect() {
            return View();
        }
        //进入资产报废停用维修查询页面分页查询
        public ActionResult AssetStatusSelectFen(int currentPage, string AType, string AName, string TName)
        {
            //currentPage：当前页 pageSize:显示几页,rows:总记录数,pages总页数
            var pageSize = 2;
            int rows;

            List<v_Asset> dt;

            if (AType == "" && AType == null || AName == "" && AName == null || TName == "" && TName == null)
            {
                //普通查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.AID>0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = iab.SelectV_Asset(e => e.AID, e => e.ATypeId.Contains(AType) && e.AName.Contains(AName) && e.TID.Contains(TName) , out rows, currentPage, pageSize);
            }

            // var dt = iab.SelectV_Asset(e => e.AID, e => e.AID > 0, out rows, currentPage, pageSize);
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            //dir.Add("currentPage",rows%2>0?(rows/2)+1 : (rows/2) );
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }

        //进入资产报废停用维修查询申请操作页面（进入待审核的修改页面）
        public ActionResult AssetStatusWaitSelect(int id) {
            var dt = iab.SelectWhere(e => e.AID == id).FirstOrDefault();
            var ATypeId = dt.ATypeId;//资产类别
            var JID = dt.JID;//采购依据
            var SID = dt.SID;//采购形式
            ViewBag.ATypeId = izcbzb.SelectWhere(e => e.ATypeId == ATypeId);
            ViewBag.JID = icgyjb.SelectWhere(e => e.JID == JID);
            ViewBag.SID = icgxsb.SelectWhere(e => e.SID == SID);
            return View(dt);
        }

        //进入资产报废停用维修查询申请操作页面（进入不是待审核的修改页面）
        public ActionResult AssetStatusSelectEdit(int id)
        {
            var dt = iab.SelectWhere(e => e.AID == id).FirstOrDefault();
            var ATypeId = dt.ATypeId;//资产类别
            var JID = dt.JID;//采购依据
            var SID = dt.SID;//采购形式
            ViewBag.ATypeId = izcbzb.SelectWhere(e => e.ATypeId == ATypeId);
            ViewBag.JID = icgyjb.SelectWhere(e => e.JID == JID);
            ViewBag.SID = icgxsb.SelectWhere(e => e.SID == SID);
            return View(dt);
        }
        #endregion

    }
}

