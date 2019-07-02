using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using IBLL;
using Newtonsoft.Json;
using Entity;
using IocContainer;

namespace information.Controllers
{
    //系统设置模块
    public class SystemsController : Controller  
    {
        IRoleBll irb = IocCreate.CreateAll<RoleService>("RoleTwo", "RoleService");//角色表
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");//用户表
        ICaiGouXingShiBll icgxsb = IocCreate.CreateAll<CaiGouXingShiService>("CaiGouXingShiTwo", "CaiGouXingShiService");//采购形式
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");//资产表
        #region 系统管理

        //进入系统管理页面
        // GET: Systems
        public ActionResult PowerSelect()
        {
            return View();
        }
        //进入系统管理页面查询操作
        public ActionResult PowerSelects()
        {
          var dt=irb.SelectAll();
            return Content(JsonConvert.SerializeObject(dt));
        }
        //信息管理页面添加（进行添加操作）
        // POST: Systems/Create
        [HttpPost]
        public ActionResult PowerCreate(info_Role role)
        {
            try
            {
                // TODO: Add insert logic here

                if (irb.Add(role) > 0)
                {
                    return Content("<script>alert('添加成功');window.location.href='/Systems/PowerSelect'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');window.location.href='/Systems/PowerSelect'</script>");
                }
            }
            catch
            {
                return View("PowerSelect");
            }
         
        }
        //信息管理页面删除（进行删除操作）
        // GET: Systems/Delete/5
        public ActionResult PowerDelete(int id)
        {
            info_Role role = new info_Role
            {
                RoleID = id
            };
            if (irb.Delete(role) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Systems/PowerSelect'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/Systems/PowerSelect'</script>");
            }
        }
        //信息管理页面修改(进入修改操作页面)
        // GET: Systems/Edit/5
        public ActionResult PowerEdit(int id)
        {
            var dt = irb.SelectWhere(e=>e.RoleID==id).FirstOrDefault();
            return View(dt);
        }

        //信息管理页面修改(进行修改操作)
        // POST: Systems/Edit/5
        [HttpPost]
        public ActionResult PowerEdits(info_Role role)
        {
        
                if (irb.Update(role) > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='/Systems/PowerSelect'</script>");
                }
                else {
                    return Content("<script>alert('修改失败');window.location.href='/Systems/PowerSelect'</script>");

                }
           
        }
        #endregion
        #region 用户管理

        //用户管理页面查询
        public ActionResult UserManagementSelect() {
            return View();
        }

        //用户管理页面查询(分页查询)
        public ActionResult UserManagementSelectFen(int currentPage,string name)
        {
            var pageSize = 2;
            int rows;

            List<v_User> dt;

            if (name == "" && name == null)
            {
                //普通查询
                dt = iud.SelectUser(e => e.UserID, e => e.UserID > 0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = iud.SelectUser(e => e.UserID, e => e.UserRealName.Contains(name), out rows, currentPage, pageSize);
            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));
        }


        //用户管理页面删除
        public ActionResult UserManagementDelete(int id) {
            info_User user = new info_User
            {
                UserID = id
            };
            if (iud.Delete(user) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Systems/UserManagementSelect'</script>");
            }
            else {
                return Content("<script>alert('删除失败');window.location.href='/Systems/UserManagementSelect'</script>");
            }

        }

        //用户管理页面修改（进入修改页面）
        public ActionResult UserManagementEdit(int id) {

            var dt = iud.SelectWhere(e => e.UserID == id).FirstOrDefault();
            selectRole();
            return View(dt);
        }
        //角色类别（下拉框）
        private void selectRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var dt = irb.SelectAll();
            foreach (var dr in dt)
            {

                SelectListItem cl = new SelectListItem()
                {
                    Value = dr.RoleID.ToString(),
                    Text = dr.RoleName.ToString()
                };
                list.Add(cl);
            }
            ViewData["role"] = list;
        }

        //用户管理页面修改（进行修改操作）
        [HttpPost]
        public ActionResult UserManagementEdits(info_User u)
        {
            if (iud.Update(u) > 0)
            {
                return Content("<script>alert('修改成功');window.location.href='/Systems/UserManagementSelect'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');window.location.href='/Systems/UserManagementEdit/" + u.UserID+"'</script>");
            }
           // return View(dt);
        }
        //用户管理页面添加（进入添加页面）
        // GET: Systems/Create
        public ActionResult UserManagementCreate()
        {
            selectRole();
            return View();
        }

        //用户管理页面添加（进行添加操作）
        [HttpPost]
        public ActionResult UserManagementCreates(info_User u)
        {
            if (iud.Add(u) > 0)
            {
                return Content("<script>alert('添加成功');window.location.href='/Systems/UserManagementSelect'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');window.location.href='/Systems/UserManagementEdit/" + u.UserID + "'</script>");
            }
            // return View(dt);
        }

        [HttpGet]
        //修改密码[进入修改密码页面]
        public ActionResult UserManagementPassEdit(int id) {
           var dt= iud.SelectWhere(e=>e.UserID==id).FirstOrDefault();
           return  View(dt);
        }
        //修改密码[进行修改密码操作]
        public ActionResult UserManagementPassEdits(int id,string pass) {
          
            info_User u = iud.SelectWhere(e => e.UserID == id).FirstOrDefault();
            u.UserPass = pass;
          
            if (iud.Update(u) > 0)
            {
                return Content("ok");
            }
            else {
                return Content("no");
            }
        }


        #endregion

        #region 问题分类维护
        //进入问题分类维护页面
        public ActionResult QuestionSelect() {
            return View();
        }
        //问题分类维护页面分页
        // public ActionResult QuestionSelectFen(int currentPage, string name)
        //  {
        //    var pageSize = 2;
        //    int rows;

        //    List<v_User> dt;

        //    if (name == "" && name == null)
        //    {
        //        //普通查询
        //          dt = iud.SelectUser(e => e.QID, e => e.QID > 0, out rows, currentPage, pageSize);
        //    }
        //    else
        //    {
        //        //带条件查询
        //         dt = iud.SelectUser(e => e.QID, e => e.QName.Contains(name), out rows, currentPage, pageSize);
        //    }
        //    Dictionary<string, object> dir = new Dictionary<string, object>();
        //    dir.Add("data", dt);
        //    dir.Add("rows", rows);
        //    dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
        //    dir.Add("pages", (rows - 1) / pageSize + 1);
        //    return Content(JsonConvert.SerializeObject(dir));

        //}

        //问题分类维护页面添加
        // GET: Systems/Create
        public ActionResult QuestionCreate(info_Question quest)
        {
        //    if (iub.Add(quest) > 0)
        //    {
        //        return Content("<script>alert('添加成功');window.location.href='/Systems/QuestionSelect'</script>");
        //    }
        //    else
        //    {
        //        return Content("<script>alert('添加失败');window.location.href='/Systems/QuestionSelect'</script>");
        //    }
            return View();
        }


        //问题分类维护页面修改（进入修改页面）
        // GET: Systems/Create
        public ActionResult QuestioEdit(int id)
        {
           //var dt= iud.SelectWhere(e => e.QID == id).FirstOrDefault();
           // return View(dt);
            return View();
        }

        //问题分类维护页面修改（进行修改操作）
        // GET: Systems/Create
        public ActionResult QuestioEdits(info_Question quest)
        {
            //  if (iub.Add(quest) > 0)
            //    {
            //        return Content("<script>alert('修改成功');window.location.href='/Systems/QuestionSelect'</script>");
            //    }
            //    else
            //    {
            //        return Content("<script>alert('修改失败');window.location.href='/Systems/QuestionSelect'</script>");
            //    }
            return View();
        }

        //问题分类维护页面删除（进入修改页面）
        // GET: Systems/Create
        public ActionResult QuestioDelete(int id)
        {
            //info_Question q = new info_Question()
            //{
            //    QID = id
            //};
            //if (iud.Delete(q) > 0)
            //{ 
            //        return Content("<script>alert('修改成功');window.location.href='/Systems/QuestionSelect'</script>");
            //    }
            //    else
            //    {
            //        return Content("<script>alert('修改失败');window.location.href='/Systems/QuestionSelect'</script>");
            //    }
            return View();
        }

        #endregion

        #region 软件名称维护（info_Software）
        //进入软件名称维护页面
        public ActionResult SoftwareSelect()
        {
            return View();
        }
        //软件名称维护页面分页
        // public ActionResult SoftwareSelectFen(int currentPage, string name)
        //  {
        //    var pageSize = 2;
        //    int rows;

        //    List<v_User> dt;

        //    if (name == "" && name == null)
        //    {
        //        //普通查询
        //          dt = iud.SelectUser(e => e.SID, e => e.SID > 0, out rows, currentPage, pageSize);
        //    }
        //    else
        //    {
        //        //带条件查询
        //         dt = iud.SelectUser(e => e.SID, e => e.Sdynacomm.Contains(name), out rows, currentPage, pageSize);
        //    }
        //    Dictionary<string, object> dir = new Dictionary<string, object>();
        //    dir.Add("data", dt);
        //    dir.Add("rows", rows);
        //    dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
        //    dir.Add("pages", (rows - 1) / pageSize + 1);
        //    return Content(JsonConvert.SerializeObject(dir));

        //}

        //软件名称维护页面添加
        // GET: Systems/Create
        public ActionResult SoftwareCreate(info_Software soft)
        {
            //    if (iub.Add(soft) > 0)
            //    {
            //        return Content("<script>alert('添加成功');window.location.href='/Systems/SoftwareSelect'</script>");
            //    }
            //    else
            //    {
            //        return Content("<script>alert('添加失败');window.location.href='/Systems/SoftwareSelect'</script>");
            //    }
            return View();
        }


        //软件名称维护页面修改（进入修改页面）
        // GET: Systems/Create
        public ActionResult SoftwareEdit(int id)
        {
            //var dt= iud.SelectWhere(e => e.SID == id).FirstOrDefault();
            // return View(dt);
            return View();
        }

        //软件名称维护修改（进行修改操作）
        // GET: Systems/Create
        public ActionResult SoftwareEdits(info_Software soft)
        {
            //  if (iub.Add(soft) > 0)
            //    {
            //        return Content("<script>alert('修改成功');window.location.href='/Systems/SoftwareSelect'</script>");
            //    }
            //    else
            //    {
            //        return Content("<script>alert('修改失败');window.location.href='/Systems/SoftwareSelect'</script>");
            //    }
            return View();
        }

        //软件名称维护删除
        // GET: Systems/Create
        public ActionResult SoftwareDelete(int id)
        {
            //info_Software soft = new info_Software()
            //{
            //    SID = id
            //};
            //if (iud.Delete(soft) > 0)
            //{ 
            //        return Content("<script>alert('修改成功');window.location.href='/Systems/SoftwareSelect'</script>");
            //    }
            //    else
            //    {
            //        return Content("<script>alert('修改失败');window.location.href='/Systems/SoftwareSelect'</script>");
            //    }
            return View();
        }

        #endregion

        #region 采购形式维护（CaiGouXingShi）
        //进入采购形式维护页面
        public ActionResult ShoppingSelect()
        {
            return View();
        }
        //采购形式维护页面分页
        public ActionResult ShoppingSelectFen(int currentPage, string name)
        {
            var pageSize = 2;
            int rows;

            List<CaiGouXingShi> dt;

            if (name == "" && name == null)
            {
                //普通查询
                dt = icgxsb.FenYe(e => e.SID, e => e.SID > 0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = icgxsb.FenYe(e => e.SID, e => e.SName.Contains(name), out rows, currentPage, pageSize);
            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }
        //采购形式维护页面添加
        public ActionResult ShoppingCreate(CaiGouXingShi cgxs)
        {
            if (icgxsb.Add(cgxs) > 0)
            {
                return Content("<script>alert('添加成功');window.location.href='/Systems/ShoppingSelect'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');window.location.href='/Systems/ShoppingSelect'</script>");
            }
            //return View();
        }
        //采购形式维护页面修改（进入修改页面）
        // GET: Systems/Create
        public ActionResult ShoppingEdit(int id)
        {
            var dt= icgxsb.SelectWhere(e => e.SID == id).FirstOrDefault();
            return View(dt);
        }

        //采购形式维护修改（进行修改操作）
        // GET: Systems/Create
        public ActionResult ShoppingEdits(CaiGouXingShi cgxs)
        {
            if (icgxsb.Update(cgxs) > 0)
            {
               // return Content("ok");
              return Content("<script>alert('修改成功');window.location.href='/Systems/ShoppingSelect'</script>");
            }
            else
            {
               // return Content("no");
                return Content("<script>alert('修改失败');window.location.href='/Systems/ShoppingSelect'</script>");
            }
          //  return View();
        }

        //采购形式维护删除

        public ActionResult ShoppingDelete(int id) {

             var aid = iab.SelectAll();
            foreach (var item in aid)
            {
                if (item.AID == id)
                {
                    return Content("<script>alert('正在使用中，不能删除');window.location.href='/systems/shoppingselect'</script>");

                }
            }

            CaiGouXingShi cgxs = new CaiGouXingShi()
            {
                SID = id
            };

            if (icgxsb.Delete(cgxs) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/systems/shoppingselect'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/systems/shoppingselect'</script>");
            }

           
        }

        #endregion


        // GET: Systems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    
        // GET: Systems/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Systems/Delete/5
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
