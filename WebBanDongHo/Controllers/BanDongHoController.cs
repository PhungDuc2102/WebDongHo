using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanDongHo.Controllers
{
    public class BanDongHoController : Controller
    {
        // GET: BanDongHo
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();

        private List<DongHo> Laydonghomoi(int count)
        {
            //sap xep giam gian theo Ngaycapnhat. lay count dong dau
            return data.DongHos.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            //lay 5 doi giay moi nhat
            int pagesize = 6;
            int pageNum = (page ?? 1);
            var donghomoi = Laydonghomoi(12);
            return View(donghomoi.ToPagedList(pageNum, pagesize));
        }

        public ActionResult Chitiet(string id)
        {
            var dongho = from dh in data.DongHos where dh.MaDongHo == id select dh;
            return View(dongho.Single());
        }

        public ActionResult Nhasanxuat()
        {
            var nhasanxuat = from nsx in data.NhaSanXuats select nsx;
            return PartialView(nhasanxuat);
        }

        public ActionResult Sptheonhasx(int? page, string id, string Stukhoa, string t)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay = from g in data.DongHos
                       where g.MaNhaSanXuat == id
                       select g;
            return View(giay.ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        public ActionResult Sptheonhasx(int? page, string id, string Stukhoa)
        {
            ViewBag.TuKhoa = Stukhoa;
            int pagesize = 12;
            int pageNum = (page ?? 1);
            var giay = from g in data.DongHos
                       where g.MaNhaSanXuat == id
                       select g;
            return View(giay.ToPagedList(pageNum, pagesize));
        }

        public ActionResult Contact()
        {
            return View();
        }


        //[HttpGet]
        public ActionResult donghonam(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pagesize = 6;
            int pageNum = (page ?? 1);
            var All_hoanghoa = from tt in data.DongHos
                               where tt.MaLoai == "1"
                               select tt;
            return View(All_hoanghoa.ToPagedList(pageNum, pagesize));
        }

        public ActionResult donghonu(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pagesize = 6;
            int pageNum = (page ?? 1);
            var All_hoanghoa = from tt in data.DongHos
                               where tt.MaLoai == "2"
                               select tt;
            return View(All_hoanghoa.ToPagedList(pageNum, pagesize));
        }

        //public ActionResult donghonamPart()
        //{
        //    var donghonam = from dhn in data.LoaiDongHos select dhn;
        //    return PartialView(donghonam);
        //}


    }
}