using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using PagedList;


namespace WebBanDongHo.Areas.Admin.Controllers
{
    public class KiemHangController : Controller
    {
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        // GET: Admin/KiemHang
        public ActionResult Index(int ? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pageSize = 3;
            int pageNum = (page ?? 1);
            var nhanhang = from tt in data.DatHangs select tt;
            return View(nhanhang.ToPagedList(pageNum, pageSize));
        }

    }
}