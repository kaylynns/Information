using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace information.Controllers
{
    public class JiFangSheBeiController : Controller
    {
        // GET: JiFangSheBei
        public ActionResult Index()
        {
            return View();
        }

        // GET: JiFangSheBei/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JiFangSheBei/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JiFangSheBei/Create
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

        // GET: JiFangSheBei/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JiFangSheBei/Edit/5
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

        // GET: JiFangSheBei/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JiFangSheBei/Delete/5
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
