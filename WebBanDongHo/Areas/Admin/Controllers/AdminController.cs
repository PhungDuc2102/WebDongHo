using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using WebBanDongHo.Models;
namespace WebBanDongHo.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        // GET: Areas/Admin
        public ActionResult Index(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            //lay 5 doi giay moi nhat
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var donghomoi = Laydonghomoi(12);
            return View(donghomoi.ToPagedList(pageNum, pagesize));
            
        }

        private List<DongHo> Laydonghomoi(int count)
        {
            //sap xep giam gian theo Ngaycapnhat. lay count dong dau
            return data.DongHos.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        //Thêm đồng hồ
        public ActionResult Createdongho()
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            var CB_MaMau = from mm in data.MauSacs select mm;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var CB_MaKT = from kt in data.ChatLieus select kt;
            ViewData["MaChatLieu"] = new SelectList(data.ChatLieus, "MaChatLieu", "TenChatLieu");
            var CB_Manhasx = from sx in data.NhaSanXuats select sx;

            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            var CB_MaLoai = from ml in data.LoaiDongHos select ml;

            ViewData["MaLoai"] = new SelectList(data.LoaiDongHos, "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        public ActionResult Createdongho(FormCollection collection, DongHo ldongho)
        {
            var CB_Maloai = collection["MaLoai"];
            var CB_MaDongHo = collection["MaDongHo"];
            var CB_MaMau = collection["MaMau"];
            var CB_MaChatLieu = collection["MaChatLieu"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];
            var CB_Name = collection["TenDongHo"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["NgayCapNhat"]);
            if (string.IsNullOrEmpty(CB_MaDongHo))
            {
                ViewData["Loi"] = " Mã đồng hồ không được để trống ";
            }
            else if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi1"] = " Tên đồng hồ không được để trống ";
            }
            else
            {
                ldongho.MaDongHo = CB_MaDongHo;
                ldongho.MaMau = CB_MaMau;
                ldongho.MaChatLieu = CB_MaChatLieu;
                ldongho.MaNhaSanXuat = CB_MaNhaSanXuat;
                ldongho.MaLoai = CB_Maloai;
                ldongho.TenDongHo = CB_Name;
                ldongho.HinhAnh = CB_anh;
                ldongho.MoTa = CB_motamh;
                ldongho.GiaBan = CB_Giaban;
                ldongho.NgayCapNhat = CB_ngaycapnhat;
                data.DongHos.InsertOnSubmit(ldongho);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Createdongho();
        }

        //Login
        //[HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendangnhap = collection["ID"];
            var matkhau = collection["Password"];
            if (String.IsNullOrEmpty(tendangnhap))
            {
                ViewData["Loi1"] = "Phải nhập tài khoản";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                LoginAdmin ad = data.LoginAdmins.SingleOrDefault(n => n.ID == tendangnhap && n.PASWORD == matkhau);
                if (ad != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["taikhoangadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
    }
}