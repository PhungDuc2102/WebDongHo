using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanDongHo.Models;
namespace WebBanDongHo.Models
{
    public class GioHang
    {
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();

        public string iSoDDH { get; set; }
        public string iMaDongHo { get; set; }
        public string iTenDongHo { get; set; }
        public string Anh { get; set; }
        public int isoluong { get; set; }
        public double idongia { get; set; }

        public double thanhtien
        {
            get
            {
                return isoluong * idongia;
            }
        }

        //Khoi tao gio hang theo MaDongHo duoc truyen vao so luong mac dinh la 1
        public GioHang(string MaDongHo)
        {
            iMaDongHo = MaDongHo;
            DongHo dongho = data.DongHos.Single(n => n.MaDongHo == iMaDongHo);
            iTenDongHo = dongho.TenDongHo;
            Anh = dongho.HinhAnh;
            idongia = double.Parse(dongho.GiaBan.ToString());
            isoluong = 1;
        }
    }
}