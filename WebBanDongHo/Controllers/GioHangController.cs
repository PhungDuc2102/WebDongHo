using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
namespace WebBanDongHo.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        public List<GioHang> laygiohang()
        {
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang == null)
            {
                listgiohang = new List<GioHang>();
                Session["GioHang"] = listgiohang;
            }
            return listgiohang;
        }

        public ActionResult ThemGioHang(string imadongho, string strUrl)
        {
            // lay ra session gio hang
            List<GioHang> listgiohang = laygiohang();
            // kt san pham đã tồn tai  trong session["giohang"] chua
            GioHang sanpham = listgiohang.Find(n => n.iMaDongHo == imadongho);
            if (sanpham == null)
            {
                sanpham = new GioHang(imadongho);
                listgiohang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strUrl);
            }
        }

        //Tong so luong
        private int TongSoLuong()
        {
            int iTongsoluong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongsoluong = lstGiohang.Sum(n => n.isoluong);
            }
            return iTongsoluong;
        }

        //Tong tien
        private double TongTien()
        {
            double iTongtien = 0;
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang != null)
            {
                iTongtien = listgiohang.Sum(n => n.thanhtien);
            }
            return iTongtien;
        }

        public ActionResult GioHang()
        {
            List<GioHang> listgiohang = laygiohang();
            if (listgiohang.Count == 0)
            {
                return RedirectToAction("Index", "BanDongHo");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(listgiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGioHang(string iMaSP)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.SingleOrDefault(n => n.iMaDongHo == iMaSP);
            if (sanpham != null)
            {
                listgiohang.RemoveAll(n => n.iMaDongHo == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (listgiohang.Count == 0)
            {
                return RedirectToAction("Index", "BanDongHo");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(String iMaSP, FormCollection f)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.SingleOrDefault(n => n.iMaDongHo == iMaSP);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["ID"] == null || Session["ID"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "BanDongHo");
            }
            // lay gio hang tu session
            List<GioHang> listgiohang = laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(listgiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            DatHang dh = new DatHang();
            KhachHang kh = (KhachHang)Session["ID"];
            List<GioHang> gh = laygiohang();
            dh.NgayDatHang = DateTime.Now;
            dh.DaGiao = false;
            var ngaygiao = DateTime.Parse(collection["NgayGiaoHang"]);
            dh.NgayGiaoHang = ngaygiao;
            dh.TenNguoiNhan = kh.TenKhachHang;
            dh.DiaChiNhan = kh.DiaChi;
            dh.DienThoaiNhan = kh.SDT;
            dh.HTThanhToan = false;
            dh.MaKhachHang = kh.MaKhachHang;
            data.DatHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietDat ctdh = new ChiTietDat();
                ctdh.SoDH = dh.SoDH;
                ctdh.MaDongHo = item.iMaDongHo;
                ctdh.SoLuong = item.isoluong;
                ctdh.DonGia = (decimal)item.idongia;
                data.ChiTietDats.InsertOnSubmit(ctdh);

            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}