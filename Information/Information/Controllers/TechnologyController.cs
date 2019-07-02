using BLL;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Newtonsoft.Json;

namespace information.Controllers
{
    public class TechnologyController : Controller
    {
        ITechnologyBll itb = IocContainer.IocCreate.CreateAll<TechnologyService>("TechnologyTwo", "TechnologyService");//视图
        ISoftwareBll isb = IocContainer.IocCreate.CreateAll<SoftwareService>("SoftwareTwo", "SoftwareService");//软件
        IQuestionBll iqb = IocContainer.IocCreate.CreateAll<QuestionService>("QuestionTwo", "QuestionService");//问题
        ISectionBll istib=IocContainer.IocCreate.CreateAll<SectionService>("SectionTwo", "SectionService");//部门
        // GET: Technology 技术支持内容登记页面
        public ActionResult Index()
        {
            return View();
        }

        //技术支持内容登记查询
        public ActionResult Index2(int currentpage, string names,string ops)
        {
            int rows;
            List<v_info_Technology> dt;
            if (names == null && ops == "")
            {
                dt = itb.v_Technology(e => e.TID, e => e.TID > 0, out rows, currentpage, 3);
            }
            else
            {
                dt = itb.v_Technology(e => e.TID, e => e.SID.Contains(names) && e.QID.Contains(ops), out rows, currentpage, 3);
            }
             List<info_Question> iq = iqb.SelectAll();

            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            dir.Add("iq",iq);
            return Content(JsonConvert.SerializeObject(dir));
        }
        //查询部门
        public List<SelectListItem> FillSection()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Entity.Section> lists = istib.SelectAll();
            foreach (Entity.Section dr in lists)
            {
                SelectListItem sl = new SelectListItem()
                {
                    Text = dr.SectionName,
                    Value = dr.Sid.ToString()
                };
                list.Add(sl);
            }

            ViewData["b"] = list;
            return list;
        }

        //查询软件名称
        public List<SelectListItem> FillClass()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Entity.info_Software> lists = isb.SelectAll();
            foreach (Entity.info_Software dr in lists)
            {
                SelectListItem sl = new SelectListItem()
                {
                    Text = dr.Sdynacomm,
                    Value = dr.SID.ToString()
                };
                list.Add(sl);
            }

            ViewData["s"] = list;
            return list;
        }

        //查询问题分类
        public List<SelectListItem> FillQuestion()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Entity.info_Question> lists = iqb.SelectAll();
            foreach (Entity.info_Question dr in lists)
            {
                SelectListItem sl = new SelectListItem()
                {
                    Text = dr.QName,
                    Value = dr.QID.ToString()
                };
                list.Add(sl);
            }

            ViewData["q"] = list;
            return list;
        }
       
        // GET: Technology/Create 技术支持内容等级添加
        public ActionResult Create()
        {
            FillClass();//查询软件名称
            FillQuestion();//查询问题分类
            FillSection();//查询部门
            ViewData["urm"] = Session["UserRealName"];//登记人
            return View();
        }

        // POST: Technology/Create技术支持内容等级添加
        [HttpPost]
        public ActionResult Create(info_Technology it)
        {
            
            it.TUserId=(int)Session["UserID"];         
            int add = itb.Add(it);
            if (add > 0)
            {
                return Content("<script>alert('添加成功！');window.location.href='/Technology/Index'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！');window.location.href='Index'</script>");
            }
        }

        // GET: Technology/Delete/5技术支持内容等级删除
        public ActionResult Delete(int id)
        {  
            info_Technology it = new info_Technology();
            it.TID = id;
            int del = itb.Delete(it);
            if (del > 0)
            {
                return Content("<script>alert('删除成功！');window.location.href='/Technology/Index'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='Index'</script>");
            }
        }

        // GET: Technology/Edit/5技术支持内容等级id查询
        public ActionResult Edit(int id)
        {
            FillClass();//查询软件名称
            FillQuestion();//查询问题分类
            FillSection();//查询部门
            ViewData["urm"] = Session["UserRealName"];//登记人
            info_Technology it = itb.SelectWhere(e=>e.TID==id).FirstOrDefault();
            return View(it);
        }

        // POST: Technology/Edit/5
        [HttpPost]
        public ActionResult Edit(info_Technology it)
        {
            int update = itb.Update(it);
            if (update > 0)
            {
                return Content("<script>alert('修改成功！');window.location.href='/Technology/Index'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败！');window.location.href='Index'</script>");
            }
        }

      

        // POST: Technology/Delete/5
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
