using BLL;
using Entity;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace information.Controllers
{
    public class RegistrationController : Controller
    {
        ITechnologyBll itb = IocContainer.IocCreate.CreateAll<TechnologyService>("TechnologyTwo", "TechnologyService");//等级
        IRegistrationBll ird = IocContainer.IocCreate.CreateAll<RegistrationService>("RegistrationTwo", "RegistrationService");//业务系统
        ISectionBll istib = IocContainer.IocCreate.CreateAll<SectionService>("SectionTwo", "SectionService");//部门
        ISoftwareBll isb = IocContainer.IocCreate.CreateAll<SoftwareService>("SoftwareTwo", "SoftwareService");//软件
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
                                               
        //业务系统使用申请                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        public ActionResult Index2(int currentpage, string names,string rname)
        {
            int rows;
            List<v_info_Registration> dt;
            if (names == null  && rname == null )
            {
                dt = ird.v_MainAll(e => e.RID, e => e.RState=="审核成功", out rows, currentpage, 3);
            }
            else
            {

                //  dt = imb.FenYe(e => e.MID, e => e.MName.Contains(names), out rows, currentpage, 3);
                dt = ird.v_MainAll(e => e.RID, e => e.SIDS.Contains(names)&&e.RName.Contains(rname), out rows, currentpage, 3);
            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("dt", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }


        //业务系统使用审核
        public ActionResult IndexR()
        {
            return View();
        }

        //业务系统使用审核
        public ActionResult IndexRs(int currentpage, string names, string rname)
        {
            int rows;
            List<v_info_Registration> dt;
            if (names == null && rname == null)
            {
                dt = ird.v_MainAll(e => e.RID, e =>e.RID>0, out rows, currentpage, 3);
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

        //业务系统查询
        public ActionResult IndexSelect() {

            return View();
        }

        // GET: Registration/Details/5审核
        public ActionResult ShenHe(int id)
        {
            info_Registration ir = ird.SelectWhere(e => e.RID == id).FirstOrDefault();
            int roffice =Convert.ToInt32(ir.ROffice);

            //查询部门
            Section ss = istib.SelectWhere(e =>e.Sid== roffice).FirstOrDefault();
            ViewData["bb"] = ss.SectionName;

            //查询软件名称
            info_Software iss= isb.SelectWhere(e =>e.SID==ir.SID).FirstOrDefault();
            ViewData["ss"] = iss.Sdynacomm;
            return View(ir);
        }
        
        public ActionResult ChanKan(int id) {
            info_Registration ir = ird.SelectWhere(e => e.RID == id).FirstOrDefault();
            int roffice = Convert.ToInt32(ir.ROffice);

            //查询部门
            Section ss = istib.SelectWhere(e => e.Sid == roffice).FirstOrDefault();
            ViewData["bb"] = ss.SectionName;

            //查询软件名称
            info_Software iss = isb.SelectWhere(e => e.SID == ir.SID).FirstOrDefault();
            ViewData["ss"] = iss.Sdynacomm;
            return View(ir);
        }
        public ActionResult SheHeEdit(info_Registration ir) {
            
            if (ir.RInformationCenter == "同意") {
                ir.RState = "审核成功";
            } else if (ir.RInformationCenter == "不同意") {
                ir.RState = "驳回";
            }
            int updateSH = ird.Update(ir);
            if (updateSH > 0)
            {
                return Content("<script>alert('审核完毕！');window.location.href='/Registration/IndexR'</script>");
            }
            else
            {
                return Content("<script>alert('审核完毕！');window.location.href='Index'</script>");
            }
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

        // GET: Registration/Create添加
        public ActionResult Create()
        {
            FillSection(); //查询部门
            FillClass(); //查询软件名称
            return View();
        }

        // POST: Registration/Create
        [HttpPost]
        public ActionResult Create(info_Registration ir)
        {
            List<info_Technology> lt = itb.SelectAll();
            foreach (var item in lt) {
                if (item.TConsultant == ir.RName)
                {
                    List<info_Registration> lir = ird.SelectAll();
                    foreach (var items in lir) {
                        if (ir.RName == items.RName)
                        {
                            return Content("<script>alert('此申请人已经申请，如需第二次申请，请重新登记！');window.location.href='/Technology/Index'</script>");
                        }

                    }

                      
                        var ift = itb.SelectWhere(e => e.TConsultant == ir.RName).FirstOrDefault();
                        int cs = Convert.ToInt32(ir.ROffice);
                        if (ift.SID == ir.SID && ift.TDepartment == cs)
                        {

                            ir.RState = "待审核";
                            int add = ird.Add(ir);
                            if (add > 0)
                            {
                            return Content("<script>alert('申请成功！');window.location.href='/Registration/Index'</script>");
                        }
                            else
                            {

                            }
                        }
                        else if (ift.TDepartment != cs)
                        {
                            return Content("<script>alert('部门名称选择不正确，请重新填写申请表！');window.location.href='/Registration/Create'</script>");
                        }
                        else
                        {
                            return Content("<script>alert('软件名称选择不正确，请重新填写申请表！');window.location.href='/Registration/Create'</script>");
                        }
                        
                }
            }
            return Content("<script>alert('此申请人还未登记，请先登记！');window.location.href='/Technology/Index'</script>");




        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int id)
        {
            FillSection(); //查询部门
            FillClass(); //查询软件名称
            info_Registration ir = ird.SelectWhere(e=>e.RID==id).FirstOrDefault();
            return View(ir);
        }

        // POST: Registration/Edit/5
        [HttpPost]
        public ActionResult Edit(info_Registration ir)
        {
            List<info_Technology> lt = itb.SelectAll();
            foreach (var item in lt)
            {
                if (item.TConsultant == ir.RName)
                {
                    var ift = itb.SelectWhere(e => e.TConsultant == ir.RName).FirstOrDefault();
                    int cs = Convert.ToInt32(ir.ROffice);
                    if (ift.SID == ir.SID && ift.TDepartment == cs)
                    {

                        ir.RState = "待审核";
                       int update = ird.Update(ir);
                       if (update > 0)
                         {
                            return Content("<script>alert('修改申请成功！');window.location.href='/Registration/Index'</script>");
                         }
                     else
                       {
                            return Content("<script>alert('添加失败！');window.location.href='Index'</script>");
                       }
                     }
                         else if (ift.TDepartment != cs)
                    {
                         return Content("<script>alert('部门名称选择不正确，请重新填写申请表！');window.location.href='/Registration/Edit/"+ir.RID+"'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('软件名称选择不正确，请重新填写申请表！');window.location.href='/Registration/Edit/" + ir.RID+"'</script>");
                    }


                }

            }
            return Content("<script>alert('此申请人还未登记，请先登记！');window.location.href='/Technology/Index'</script>");

        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registration/Delete/5
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
