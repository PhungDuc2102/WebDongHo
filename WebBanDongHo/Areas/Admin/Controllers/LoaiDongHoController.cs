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
    public class LoaiDongHoController : Controller
    {
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        // GET: /Admin/LoaiDongHo/
        public ActionResult Index(int? page)
        {           
            int pagesize = 2;
            int pageNum = (page ?? 1);
            var loai = from tt in data.LoaiDongHos
                       select tt;
            return View(loai.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Createloai()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult Createloai(FormCollection collection, LoaiDongHo ldh)
        {

            var CB_MaLoai = collection["MaLoai"];
            var CB_TenLoai = collection["TenLoai"];
            if (string.IsNullOrEmpty(CB_MaLoai))
            {
                ViewData["Loi"] = "Mã loại không được để trống";
            }
            else if (string.IsNullOrEmpty(CB_TenLoai))
            {
                ViewData["Loi1"] = " Tên loại không được để trống ";
            }
            else
            {
                ldh.MaLoai = CB_MaLoai;
                ldh.TenLoai = CB_TenLoai;
                data.LoaiDongHos.InsertOnSubmit(ldh);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Createloai();
        }
        public ActionResult Deleteloai(string id)
        {
            var D_loai = data.LoaiDongHos.First(m => m.MaLoai == id);
            return View(D_loai);
        }
        [HttpPost]
        public ActionResult Deleteloai(string id, FormCollection collection)
        {

            var D_loai = data.LoaiDongHos.First(m => m.MaLoai == id);
            data.LoaiDongHos.DeleteOnSubmit(D_loai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Edit
        public ActionResult Editloai(string id)
        {
            var E_loai = data.LoaiDongHos.First(m => m.MaLoai == id);
            return View(E_loai);
        }
        [HttpPost]
        public ActionResult Editloai(string id, FormCollection collection)
        {
            var CB_TenLoai = collection["TenLoai"];
            var E_loai = data.LoaiDongHos.First(m => m.MaLoai == id);
            if (String.IsNullOrEmpty(CB_TenLoai))
            {
                ViewData["Loi1"] = "Tên loại không được để trống";
            }

            else
            {

                E_loai.TenLoai = CB_TenLoai;
                UpdateModel(E_loai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editloai(id);
        }
    }
}