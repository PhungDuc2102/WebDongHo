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
    public class GiaoHangController : Controller
    {
        // GET: Admin/
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        public ActionResult Index(int? page)
        {
            //if (Session["taikhoangadmin"] == null || Session["taikhoangadmin"].ToString() == "")
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pagesize = 3;
            int pageNum = (page ?? 1);
            var giaohang = from tt in data.DatHangs
                           where tt.DaGiao == false                          
                          || tt.HTThanhToan == false
                           select tt;
            return View(giaohang.ToPagedList(pageNum, pagesize));
        }
        public ActionResult giaohang(int id, string strUrl)
        {
            var Egiao = data.DatHangs.First(m => m.SoDH == id);
            Egiao.DaGiao = true;
            UpdateModel(Egiao);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult thanhtoan(int id, string strUrl)
        {
            var Etoan = data.DatHangs.First(m => m.SoDH == id);
            Etoan.HTThanhToan = true;
            UpdateModel(Etoan);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
       
        public ActionResult chinhsua(int id, string strUrl)
        {
            var Ehang = data.DatHangs.First(m => m.SoDH == id);
            
            Ehang.DaGiao = false;
            Ehang.HTThanhToan = false;
            UpdateModel(Ehang);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
    }
}