using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
namespace WebBanDongHo.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KhachHang kh)
        {
            //var TenKH = collection["TenKhachHang"];
            //var Email = collection["Email"];
            ////var SDT = collection["SDT"];
            //var dienthoai = collection["Dienthoai"];
            //var NgaySinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]); /*DateTime.Parse(collection["NgaySinh"]);*/
            //var DiaChi = collection["DiaChi"];
            //var ID = collection["ID"];
            //var Pasword = collection["Pasword"];
            //var MatKhaunlai = collection["matkhaunhaplai"];
            ////Nếu CB_Loaitin có giá trị == null ( để trống )             
            //if (string.IsNullOrEmpty(TenKH))
            //{
            //    ViewData["Loi1"] = " Tên  khách hàng không được để trống ";
            //}
            //else if (String.IsNullOrEmpty(dienthoai))
            //{
            //    ViewData["Loi3"] = " Số điện thoại không được để trống ";
            //}

            //else if (string.IsNullOrEmpty(DiaChi))
            //{
            //    ViewData["Loi5"] = " Địa chỉ không được để trống ";
            //}
            //else if (string.IsNullOrEmpty(ID))
            //{
            //    ViewData["Loi6"] = " Tên đăng nhập không được để trống ";
            //}
            //else if (string.IsNullOrEmpty(Pasword))
            //{
            //    ViewData["Loi7"] = " Mật Khẩu không được để trống ";
            //}
            //else if (string.IsNullOrEmpty(MatKhaunlai))
            //{
            //    ViewData["Loi8"] = " Mật Khẩu nhập lại không được để trống ";
            //}
            //else if (MatKhaunlai != Pasword)
            //{
            //    ViewData["Loi9"] = " Mật Khẩu nhập lại Không trùng khớp ";
            //}
            //else
            //{
            //    kh.TenKhachHang = TenKH;
            //    kh.Email = Email;
            //    kh.SDT = dienthoai;
            //    kh.NgaySinh = DateTime.Parse(NgaySinh);
            //    kh.DiaChi = DiaChi;
            //    kh.ID = ID;
            //    kh.Pasword = Pasword;
            //    data.KhachHangs.InsertOnSubmit(kh);
            //    data.SubmitChanges();
            //    return RedirectToAction("Dangnhap");
            //}
            //return this.Dangky();
            //Gán các giá trị người dùng nhập liệu cho các biến
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";
            }
            //else if (String.IsNullOrEmpty(diachi))
            //{
            //    ViewData["Loi7"] = "Phải nhập địa chỉ";
            //}
            else
            {
                //Gán giá trị cho đối tượng dc tạo mới
                kh.TenKhachHang = hoten;
                kh.ID = tendn;
                kh.Pasword = matkhau;
                kh.Email = email;
                kh.DiaChi = diachi;
                kh.SDT = dienthoai;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }

        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendangnhap = collection["taikhoan"];
            var matkhau = collection["matkhau"];
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
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.ID == tendangnhap && n.Pasword == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["ID"] = kh;
                    return RedirectToAction("Index", "BanDongHo");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
            return View();
        }
    }
}
