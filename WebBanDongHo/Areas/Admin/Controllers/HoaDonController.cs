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
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        public ActionResult Index(int ? page)
        {
            int pagesize = 4;
            int pageNum = (page ?? 1);
            var hd = from tt in data.ChiTietDats
                     select tt;
            return View(hd.ToPagedList(pageNum, pagesize));
        }
    }
}