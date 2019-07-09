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

namespace information.Views.JiFang
{
    public class WhuWxiuController : Controller
    {
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        IComputerRoomVisitBll lcr = IocCreate.CreateAll<ComputerRoomVisitService>("ComputerRoomVisitTwo", "ComputerRoomVisitService");//机房进出查询
        IMaintenanceBll img = IocContainer.IocCreate.CreateAll<MaintenanceService>("MaintenanceTwo", "MaintenanceService");
        ICheckBll icb = IocContainer.IocCreate.CreateAll<CheckService>("CheckTwo", "CheckService");
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        // GET: WhuWxiu 设备维护
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Index2(int currentpage, string names)
        {
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

        public ActionResult selectCB()
        {
           List<info_Asset> list= iab.SelectWhere(e=>e.TID==4);
           // List<info_Equipments> list = ieb.SelectAll();
            return Content(JsonConvert.SerializeObject(list));
        }

        // GET: WhuWxiu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //查询检测人员
        public List<SelectListItem> FillClass()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Entity.info_ComputerRoomVisit> lists = lcr.SelectWheres(e => e.CRelustID == 2);
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
   
        // GET: WhuWxiu/Create
        public ActionResult Create()
        {
            ShengCheng();//查询陪同人员
            FillClass();   //查询检测人员
            return View();
        }
     
     
      

        // POST: WhuWxiu/Create
        [HttpPost]
        public ActionResult Create(info_Maintenance imc)
        {
               int add=img.Add(imc);
            
                if (add > 0)
                {
                    return Content("<script>alert('添加成功！');window.location.href='/WhuWxiu/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败！');window.location.href='Index'</script>");
                }
            
               
        }


        // GET: WhuWxiu/Edit/5
        public ActionResult Edit(int id)
        {
            ShengCheng();//查询陪同人员
            FillClass();   //查询检测人员
            info_Maintenance im = img.SelectWhere(e=>e.MIDs==id).FirstOrDefault();
            info_Asset ies = iab.SelectWhere(e => e.AID == im.MDeviceName).FirstOrDefault();
            ViewData["a"] =ies.AName;
            return View(im);
        }

        // POST: WhuWxiu/Edit/5
        [HttpPost]
        public ActionResult Edit(info_Maintenance im)
        {
            //info_Maintenance ims = img.SelectWhere(e => e.MIDs == im.MIDs).FirstOrDefault();
            int update = img.Update(im);
            if (update > 0)
            {
                return Content("<script>alert('修改成功！');window.location.href='/WhuWxiu/Index'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败！');window.location.href='Index'</script>");
            }
        }
        
              

        // POST: WhuWxiu/Delete/5
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
