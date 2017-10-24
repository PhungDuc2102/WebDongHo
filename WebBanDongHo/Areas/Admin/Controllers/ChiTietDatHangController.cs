using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using PagedList;
namespace WebBanDongHo.Areas.Admin.Controllers
{
    public class ChiTietDatHangController : Controller
    {
        // GET: Admin/ChiTietDatHang
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        public ActionResult Index(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pageSize = 3;
            int pageNum = (page ?? 1);
            var chitietdathang = from ctdh in data.ChiTietDats select ctdh;
            return View(chitietdathang.ToPagedList(pageNum, pageSize));
        }
    }
}