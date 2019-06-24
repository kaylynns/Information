﻿using BLL;
using Entity;
using IBLL;
using IocContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace information.Controllers
{
    public class LoginController : Controller
    {
        IUserBll iud = IocCreate.CreateAll<UserService>("UserTwo", "UserService");
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        //post:Login
        [HttpPost]
        public ActionResult Login(string UserName, string Pass)
        {
            string UserRealName;
            if (UserName == null || UserName == "")
            {
                return Content("<script>alert('用户名不能为空');window.location.href='Login'</script>");
            }
            else if (Pass == null || Pass == "")
            {
                return Content("<script>alert('密码不能为空');window.location.href='/Login'</script>");
            }
            else
            {
                List<info_User> result = iud.DengLu(UserName, Pass);
                foreach (info_User item in result)
                {
                    UserName = item.UserName;
                    Pass = item.UserPass;
                    UserRealName = item.UserRealName;

                if (UserName != null)
                {
                    //登录成功
                    Session["UserName"] = UserName;
                    Session["UserRealName"] = UserRealName;
                    return Content("<script>alert('登录成功');window.location.href='/Main/Main'</script>");
                }
                }
            }
            return Content("<script>alert('登录失败');window.location.href='/Login'</script>");
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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