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
  
    public class MainController : Controller
    {
        //权限
        IDetailedBll idb = IocCreate.CreateAll<DetailedService>("DetailedTwo", "DetailedService");


        // GET: Main
        //主页
        public ActionResult Index()
        {
            return View();
        }

        //整体主页
        public ActionResult Main()
        {
            return View();
        }

        //左边
        public ActionResult left()
        {
           
            return View();
        }
        //头部
        public ActionResult top()
        {
           
            return View();
        }


        //测试：权限
        public ActionResult Mains()
        {
            ViewData["user"] = Session["UserName"];
            return View();
        }

        public ActionResult MainsTree()
        {

            List<v_Detailed> dt;
           var id= HttpContext.Request["id"];
            string rid = Session["RoleID"].ToString();
            if (id == null)
            {

                 dt = idb.detailed(rid, "0");
            }
            else {
               dt = idb.detailed(rid,id);

            }
            //
            //DataTable dt;
            //if (context.Request["id"] == null)
            //{
            //    dt = ipdb.Tree(context.Application["RoleID"].ToString(), "0");
            //}
            //else
            //{
            //    dt = ipdb.Tree(context.Application["RoleID"].ToString(), context.Request["id"]);
            //}
            //context.Response.Write(JsonConvert.SerializeObject(dt));

            return Content(JsonConvert.SerializeObject(dt));
        }








        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Main/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Main/Create
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

        // GET: Main/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Main/Edit/5
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

        // GET: Main/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Main/Delete/5
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
