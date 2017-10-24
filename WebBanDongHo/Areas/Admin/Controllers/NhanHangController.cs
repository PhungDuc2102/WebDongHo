using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    public class NhanHangController : Controller
    {
       
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        // GET: /Admin/NhanHang/
        //
        public ActionResult Index(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pagesize = 3;
            int pageNum = (page ?? 1);
            var nhanhang = from tt in data.DatHangs
                           where tt.DaGiao == false                            
                           select tt;
            return View(nhanhang.ToPagedList(pageNum, pagesize));
        }
       
    }
}