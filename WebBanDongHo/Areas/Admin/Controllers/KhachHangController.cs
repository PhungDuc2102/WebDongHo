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
    public class KhachHangController : Controller
    {
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            //if (Session["taikhoanadmin"] == null || Session["taikhoanadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var kh = from tt in data.KhachHangs select tt;
            return View(kh.ToPagedList(pageNum, pageSize));
        }
    }
}