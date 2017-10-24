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
    public class QuanLyDongHoController : Controller
    {
        // GET: Admin/QuanLyDongHo
        public ActionResult Index()
        {
            return View();
        }

        dbQLDongHoDataContext data = new dbQLDongHoDataContext();

        
        public ActionResult donghonam(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pagesize = 5;
            int pageNum = (page ?? 1);          
            var All_hoanghoa = from tt in data.DongHos
                               where tt.MaLoai == "1"
                               select tt;
            return View(All_hoanghoa.ToPagedList(pageNum, pagesize));           
        }

        public ActionResult Createdonghonam()
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
            
            return View();
        }

        [HttpPost]
        public ActionResult Createdonghonam(FormCollection collection, DongHo ldongho)
        {
            var donghonam = 1;
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
                ldongho.MaLoai = CB_MaMau;
                ldongho.MaChatLieu = CB_MaChatLieu;
                ldongho.MaNhaSanXuat = CB_MaNhaSanXuat;
                ldongho.MaLoai = donghonam.ToString();
                ldongho.TenDongHo = CB_Name;
                ldongho.HinhAnh = CB_anh;
                ldongho.MoTa = CB_motamh;
                ldongho.GiaBan = CB_Giaban;
                ldongho.NgayCapNhat = CB_ngaycapnhat;
                data.DongHos.InsertOnSubmit(ldongho);
                data.SubmitChanges();
                return RedirectToAction("donghonam");
            }
            return this.Createdonghonam();
        }

        public ActionResult Editdonghonam(string id)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            var E_dongho = data.DongHos.First(m => m.MaDongHo == id);
            var mamau = from lt in data.LoaiDongHos select lt;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var macl = from lt in data.ChatLieus select lt;
            ViewData["MaChatLieu"] = new SelectList(data.ChatLieus, "MaChatLieu", "TenChatLieu");
            var Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View(E_dongho);
        }
        [HttpPost]
        public ActionResult Editdonghonam(string id, FormCollection collection)
        {
            var donghonam = 1;
            var CB_MaMau = collection["MaMau"];
            var CB_MaChatLieu = collection["MaChatLieu"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];
            var CB_Name = collection["TenDongHo"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["NgayCapNhat"]);
            var Etin = data.DongHos.First(m => m.MaDongHo == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên giày không được để trống";
            }

            else
            {

                Etin.MaLoai = CB_MaMau;
                Etin.MaChatLieu = CB_MaChatLieu;
                Etin.MaNhaSanXuat = CB_MaNhaSanXuat;
                Etin.MaLoai = donghonam.ToString();
                Etin.TenDongHo = CB_Name;
                Etin.HinhAnh = CB_anh;
                Etin.MoTa = CB_motamh;
                Etin.GiaBan = CB_Giaban;
                Etin.NgayCapNhat = CB_ngaycapnhat;
                UpdateModel(Etin);
                data.SubmitChanges();
                return RedirectToAction("donghonam");
            }
            return this.Editdonghonam(id);
        }

        public ActionResult Detailsdonghonam(string id)
        {
           
            //var Details_tin = data.DongHos.Where(m => m.MaDongHo == id).First();
            //return View(Details_tin);
            var dongho = from dh in data.DongHos where dh.MaDongHo == id select dh;
            return View(dongho.Single());
        }

        public ActionResult Deletedonghonam(string id)
        {
            
            var D_tin = data.DongHos.First(m => m.MaDongHo == id);
            return View(D_tin);
        }

        [HttpPost]
        public ActionResult Deletedonghonam(string id, FormCollection collection)
        {

            var D_tin = data.DongHos.Where(m => m.MaDongHo == id).First();
            data.DongHos.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("donghonam");
        }

        //donghonu
        public ActionResult donghonu(int? page)
        {         
            int pagesize = 8;
            int pageNum = (page ?? 1);
            var All_hoanghoa = from tt in data.DongHos
                               where tt.MaLoai == "2"
                               select tt;
            return View(All_hoanghoa.ToPagedList(pageNum, pagesize));
        }

        //them dong ho nu
        public ActionResult Createdonghonu()
        {           
            var CB_MaMau = from mm in data.MauSacs select mm;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var CB_MaCL = from kt in data.ChatLieus select kt;
            ViewData["MaChatLieu"] = new SelectList(data.ChatLieus, "MaChatLieu", "TenChatLieu");
            var CB_Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View();
        }
        [HttpPost]
        public ActionResult Createdonghonu(FormCollection collection, DongHo ldongho)
        {
            var donghonu = 2;
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
                ViewData["Loi"] = " Tên đồng hồ không được để trống ";
            }
            else
            {
                ldongho.MaDongHo = CB_MaDongHo;
                ldongho.MaLoai = CB_MaMau;
                ldongho.MaChatLieu = CB_MaChatLieu;
                ldongho.MaNhaSanXuat = CB_MaNhaSanXuat;
                ldongho.MaLoai = donghonu.ToString();
                ldongho.TenDongHo = CB_Name;
                ldongho.HinhAnh = CB_anh;
                ldongho.MoTa = CB_motamh;
                ldongho.GiaBan = CB_Giaban;
                ldongho.NgayCapNhat = CB_ngaycapnhat;
                data.DongHos.InsertOnSubmit(ldongho);
                data.SubmitChanges();
                return RedirectToAction("donghonu");
            }
            return this.Createdonghonu();
        }

        //chi tiet dong ho nu
        public ActionResult Detailsdonghonu(string id)
        {
            var Details_tin = data.DongHos.Where(m => m.MaDongHo == id).First();
            return View(Details_tin);
        }

        // edit dong ho nu
        public ActionResult Editdonghonu(string id)
        {
            if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_dongho = data.DongHos.First(m => m.MaDongHo == id);
            var mamau = from lt in data.LoaiDongHos select lt;
            ViewData["MaMau"] = new SelectList(data.MauSacs, "MaMau", "TenMau");
            var makt = from lt in data.ChatLieus select lt;
            ViewData["MaChatLieu"] = new SelectList(data.ChatLieus, "MaChatLieu", "TenChatLieu");
            var Manhasx = from sx in data.NhaSanXuats select sx;
            ViewData["MaNhaSanXuat"] = new SelectList(data.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View(E_dongho);
        }
        [HttpPost]
        public ActionResult Editdonghonu(string id, FormCollection collection)
        {
            var donghonu = 2;
            var CB_MaMau = collection["MaMau"];
            var CB_MaChatLieu = collection["MaChatLieu"];
            var CB_MaNhaSanXuat = collection["MaNhaSanXuat"];
            var CB_Name = collection["TenDongHo"];
            var CB_anh = collection["HinhAnh"];
            var CB_motamh = collection["MoTa"];
            var CB_Giaban = decimal.Parse(collection["GiaBan"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["NgayCapNhat"]);
            var Etin = data.DongHos.First(m => m.MaDongHo == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên đồng hồ không được để trống";
            }

            else
            {

                Etin.MaLoai = CB_MaMau;
                Etin.MaChatLieu = CB_MaChatLieu;
                Etin.MaNhaSanXuat = CB_MaNhaSanXuat;
                Etin.MaLoai = donghonu.ToString();
                Etin.TenDongHo = CB_Name;
                Etin.HinhAnh = CB_anh;
                Etin.MoTa = CB_motamh;
                Etin.GiaBan = CB_Giaban;
                Etin.NgayCapNhat = CB_ngaycapnhat;
                UpdateModel(Etin);
                data.SubmitChanges();
                return RedirectToAction("donghonu");
            }
            return this.Editdonghonu(id);
        }

        //xoa dong ho nu
        public ActionResult Deletedonghonu(string id)
        {
            var D_tin = data.DongHos.First(m => m.MaDongHo == id);
            return View(D_tin);
        }
        [HttpPost]
        public ActionResult Deletedonghonu(string id, FormCollection collection)
        {

            var D_tin = data.DongHos.Where(m => m.MaDongHo == id).First();
            data.DongHos.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("donghonu");
        }
    }
}