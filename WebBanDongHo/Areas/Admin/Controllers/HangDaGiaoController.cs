using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebBanDongHo.Models;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    public class HangDaGiaoController : Controller
    {
        // GET: Admin/HangDaGiao
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        
        public ActionResult Index(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pagesize = 3;
            int pageNum = (page ?? 1);
            var nhanhang = from tt in data.DatHangs
                           where tt.DaGiao == true
                         //&& tt.HTGiaoHang == true
                       && tt.HTThanhToan == true
                           select tt;
            return View(nhanhang.ToPagedList(pageNum, pagesize));
        }
    }
}