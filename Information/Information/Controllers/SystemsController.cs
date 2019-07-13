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
using StackExchange.Redis;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace information.Controllers
{
    //系统设置模块
    public class SystemsController : Controller  
    {
        IRoleBll irb = IocCreate.CreateAll<RoleService>("RoleTwo", "RoleService");//角色表
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");//用户表
        ICaiGouXingShiBll icgxsb = IocCreate.CreateAll<CaiGouXingShiService>("CaiGouXingShiTwo", "CaiGouXingShiService");//采购形式
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");//资产表
        IQuestionBll iqb = IocContainer.IocCreate.CreateAll<QuestionService>("QuestionTwo", "QuestionService");//问题
        ISoftwareBll isb = IocContainer.IocCreate.CreateAll<SoftwareService>("SoftwareTwo", "SoftwareService");//软件
       IDetailedBll idb = IocCreate.CreateAll<DetailedService>("DetailedTwo", "DetailedService"); //权限详细
        IPermissionBll ipsb = IocCreate.CreateAll<PermissionService>("PermissionTwo", "PermissionService");//权限分配
        #region 系统管理

        //进入权限管理页面
        // GET: Systems
        public ActionResult PowerSelect()
        {
            return View();
        }
        //进入权限角色管理页面查询操作
        public ActionResult PowerSelects()
        {
          var dt=irb.SelectAll();
            return Content(JsonConvert.SerializeObject(dt));
        }
        //权限角色页面添加（进行添加操作）
        // POST: Systems/Create
        [HttpPost]
        public ActionResult PowerCreate(info_Role role)
        {


            // TODO: Add insert logic here

            if (irb.Add(role) > 0)
            {
                return Content("<script>alert('添加成功');window.location.href='/Systems/PowerSelect'</script>");

            } else {
                return Content("<script>alert('添加失败');window.location.href='/Systems/PowerSelect'</script>");
            }
                       
            
                 
        }
        //权限角色页面删除（进行删除操作）
        // GET: Systems/Delete/5
        public ActionResult PowerDelete(int id)
        {
            //事务
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {
                var dd = iud.SelectAll();
                foreach (var item in dd)
                {   //判断角色是否被分配权限
                    if (item.UserJueSe == id)
                    {
                        return Content("<script>alert('不能删除,这个角色正在被使用');window.location.href='/Systems/PowerSelect'</script>");
                    }
                }

                info_Role role = new info_Role
                {
                    RoleID = id
                };

                if (ipsb.delete(id) > 0)
                {
                    if (irb.Delete(role) > 0)
                    {
                        ts.Complete();
                        return Content("<script>alert('删除成功');window.location.href='/Systems/PowerSelect'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('删除失败');window.location.href='/Systems/PowerSelect'</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('删除失败');window.location.href='/Systems/PowerSelect'</script>");
                }
            }
        }
        //权限角色页面修改(进入修改操作页面)
        // GET: Systems/Edit/5
        public ActionResult PowerEdit(int id)
        {
            var dt = irb.SelectWhere(e=>e.RoleID==id).FirstOrDefault();
            return View(dt);
        }

        //权限角色页面修改(进行修改操作)
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
      //权限添加（进入权限页面）
        public ActionResult PowerAdd(string id) {
            ViewBag.Rid = id;
            return View();
        }
        //权限添加（查询所有权限）
        public ActionResult PowerAdds(string rid){
           
           var idd = HttpContext.Request["id"];
            
            List<v_DetailedSelect> dt;
            if (idd == null && rid != null)
            {
                dt = idb.DetailedSelect(rid, "0");
            }
            else {
                dt = idb.DetailedSelect(rid, idd);
            }
            //var dt = idb.DetailedSelect(id, "0");
        
        
           

            return Content(JsonConvert.SerializeObject(dt));

        }
        //权限添加（进行添加操作）
        public ActionResult PowerAddss(string rid,string qid) {
            //事务
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope()) {
                string[] qidd = qid.Split(',');
            
                int num =ipsb.delete(Convert.ToInt16(rid));
            if (num >=0)
            {
                int num1 = 0;
                foreach (var item in qidd)
                {
                    num1 = ipsb.Add(rid, item.ToString());
                }
                if (num1 > 0)
                {
                     ts.Complete();
                 return Content("ok");
                }

                   
            }
            return Content("ok");
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


            //查询出要删除用户的真实姓名是否和登陆进来的姓名相同  AsNoTracking
            var list = iud.SelectWhere(e => e.UserID == id);

            foreach (var item in list)
            {
                if (item.UserRealName == Session["UserRealName"].ToString())
                {
                    return Content("<script>alert('用户正在使用中不能被删除');window.location.href='/Systems/UserManagementSelect'</script>");
                }
            }
            info_User user = new info_User
            {
                UserID = id
            };

            //不相同，则删除
            if (iud.Delete(user) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Systems/UserManagementSelect'</script>");
            }
            else
            {
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
        public async Task<ActionResult> UserManagementCreates(info_User u)
        {
            //事务
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {
                if (iud.Add(u) > 0)
                {
                    await AddUser(u);
                    ts.Complete();
                    return Content("<script>alert('添加成功');window.location.href='/Systems/UserManagementSelect'</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');window.location.href='/Systems/UserManagementEdit/" + u.UserID + "'</script>");
                }
            }

            // return View(dt);
        }
        

        public async Task AddUser(info_User u) {
            using (ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("127.0.0.1:6379"))
            {
                IDatabase db = redis.GetDatabase();
                await db.ListLeftPushAsync("d", JsonConvert.SerializeObject(u));
                string zhi = db.ListRightPop("d");
                //把json字符串转换为对象
                info_User p = JsonConvert.DeserializeObject<info_User>(zhi);
                //使用MailMessage发送电子邮件
                MailMessage mail = new MailMessage();
                //主题
                mail.Subject = "欢迎您成为信息系统的一员";
                //发件人
                mail.From = new MailAddress("2214097825@qq.com");
                //收件人
                mail.To.Add(new MailAddress(u.Email));
                //主体 
                //HtmlHelper h=new HtmlHelper(Login, Login);
                //    string login = "< a href = 'http://localhost:41502/Login/Login' > 登陆 </ a >";
              
                mail.Body = "欢迎"+u.UserRealName+ "，恭喜您成为信息系统的一员(*^_^*)，您的密码是：" + u.UserPass+ "!快去登陆一下吧";
                //允许程序发邮件
                SmtpClient sc = new SmtpClient("smtp.qq.com");
                //发信的凭证
                sc.Credentials = new NetworkCredential("2214097825@qq.com", "onfwcyynkhtjeadg");
                sc.Send(mail);

            }

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
        public ActionResult QuestionSelectFen(int currentPage, string name)
        {
            var pageSize = 2;
            int rows;

            List<info_Question> dt;

            if (name == "" && name == null)
            {
                //普通查询
                dt = iqb.FenYe(e => e.QID, e => e.QID > 0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = iqb.FenYe(e => e.QID, e => e.QName.Contains(name), out rows, currentPage, pageSize);
            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }

        //问题分类维护页面添加
        // GET: Systems/Create
        public ActionResult QuestionCreate(info_Question quest)
        {
            if (iqb.Add(quest) > 0)
            {
                return Content("<script>alert('添加成功');window.location.href='/Systems/QuestionSelect'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');window.location.href='/Systems/QuestionSelect'</script>");
            }
            //return View();
        }

        [HttpGet]
        //问题分类维护页面修改（进入修改页面）
        // GET: Systems/Create
        public ActionResult QuestioEdit(int id)
        {
            var dt = iqb.SelectWhere(e => e.QID == id).FirstOrDefault();
            return View(dt);
          //  return View();
        }
        [HttpPost]
        //问题分类维护页面修改（进行修改操作）
        // GET: Systems/Create
        public ActionResult QuestioEdit(info_Question quest)
        {
            if (iqb.Update(quest) > 0)
            {
                return Content("<script>alert('修改成功');window.location.href='/Systems/QuestionSelect'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');window.location.href='/Systems/QuestionSelect'</script>");
            }
          //  return View();
        }

        //问题分类维护页面删除（进入修改页面）
        // GET: Systems/Create
        public ActionResult QuestioDelete(int id)
        {
            info_Question q = new info_Question()
            {
                QID = id
            };
            if (iqb.Delete(q) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Systems/QuestionSelect'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/Systems/QuestionSelect'</script>");
            }
            //return View();
        }

        #endregion

        #region 软件名称维护（info_Software）
        //进入软件名称维护页面
        public ActionResult SoftwareSelect()
        {
            return View();
        }
        //软件名称维护页面分页
        public ActionResult SoftwareSelectFen(int currentPage, string name)
        {
            var pageSize = 2;
            int rows;

            List<info_Software> dt;

            if (name == "" && name == null)
            {
                //普通查询
                dt = isb.FenYe(e => e.SID, e => e.SID > 0, out rows, currentPage, pageSize);
            }
            else
            {
                //带条件查询
                dt = isb.FenYe(e => e.SID, e => e.Sdynacomm.Contains(name), out rows, currentPage, pageSize);
            }
            Dictionary<string, object> dir = new Dictionary<string, object>();
            dir.Add("data", dt);
            dir.Add("rows", rows);
            dir.Add("currentpage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dir.Add("pages", (rows - 1) / pageSize + 1);
            return Content(JsonConvert.SerializeObject(dir));

        }

        //软件名称维护页面添加
        // GET: Systems/Create
        public ActionResult SoftwareCreate(info_Software soft)
        {
            if (isb.Add(soft) > 0)
            {
                return Content("<script>alert('添加成功');window.location.href='/Systems/SoftwareSelect'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');window.location.href='/Systems/SoftwareSelect'</script>");
            }
            //return View();
        }


        //软件名称维护页面修改（进入修改页面）
        // GET: Systems/Create
        public ActionResult SoftwareEdit(int id)
        {
            var dt = isb.SelectWhere(e => e.SID == id).FirstOrDefault();
            return View(dt);
        }

        //软件名称维护修改（进行修改操作）
        // GET: Systems/Create
        public ActionResult SoftwareEdits(info_Software soft)
        {
            if (isb.Update(soft) > 0)
            {
                return Content("<script>alert('修改成功');window.location.href='/Systems/SoftwareSelect'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');window.location.href='/Systems/SoftwareSelect'</script>");
            }
            //return View();
        }

        //软件名称维护删除
        // GET: Systems/Create
        public ActionResult SoftwareDelete(int id)
        {
            info_Software soft = new info_Software()
            {
                SID = id
            };
            if (isb.Delete(soft) > 0)
            {
                return Content("<script>alert('删除成功');window.location.href='/Systems/SoftwareSelect'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');window.location.href='/Systems/SoftwareSelect'</script>");
            }
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
                if (item.SID == id)
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



    }
}
