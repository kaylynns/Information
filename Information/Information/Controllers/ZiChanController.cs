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
    public class ZiChanController : Controller
    {
        IAssetBll iab = IocCreate.CreateAll<AssetService>("AssetTwo", "AssetService");
        IZiChanBeiZhuBll izcbzb = IocCreate.CreateAll<ZiChanBeiZhuService>("ZiChanBeiZhuTwo", "ZiChanBeiZhuService");//资产备注
        // GET: ZiChan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChaXunZiChan(int currentPage) {
            List<ZiChanBeiZhu> list = new List<ZiChanBeiZhu>();
            int rows = 0;
             list = izcbzb.FenYe(e => e.ATypeId, e => e.ATypeId>0, out rows, currentPage, 3);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("list", list);
            dic.Add("rows", rows);
            dic.Add("currentPage", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            dic.Add("pages", (rows - 1) / 3 + 1);
            return Content(JsonConvert.SerializeObject(dic));
        }
        public ActionResult ZiChanShanChu(int id) {
           var panduan=iab.SelectAll();
            foreach (var item in panduan)
            {
                if (item.ATypeId == id)
                {
                    return Content("<script>alert('该数据正在使用中');location.href='/ZiChan/Index'</script>");
                }
            }
                    ZiChanBeiZhu zi = new ZiChanBeiZhu
                    {
                        ATypeId = id
                    };
                    if (izcbzb.Delete(zi) > 0)
                    {
                        return Content("<script>alert('删除成功');location.href='/ZiChan/Index'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('删除失败');location.href='/ZiChan/Index'</script>");
                    }
                }    
        
        public ActionResult ZiChanTianJia(string name, string lei) {
            ZiChanBeiZhu cbs = new ZiChanBeiZhu
            {
                AName = name,
                ABeiZhu = lei
            };
            if (izcbzb.Add(cbs) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/ZiChan/Index'</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');location.href='/ZiChan/Index'</script>");
            }
        }
        [HttpGet]
        public ActionResult JiFangJinChuSelectShiTu(int id) {
         var csshi= izcbzb.SelectWhere(e => e.ATypeId == id).FirstOrDefault();
            return View(csshi);
        }
        [HttpPost]
        public ActionResult JiFangJinChuSelectShiTu(ZiChanBeiZhu zbz) {
            if (izcbzb.Update(zbz)>0)
            {
                return Content("<script>alert('修改成功');location.href='/ZiChan/Index'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');location.href='/ZiChan/Index'</script>");
            }
        }
    }
}