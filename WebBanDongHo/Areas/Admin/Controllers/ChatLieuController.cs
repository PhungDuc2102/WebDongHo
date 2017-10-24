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
    public class ChatLieuController : Controller
    {
        // GET: Admin/ChatLieu
        
        dbQLDongHoDataContext data = new dbQLDongHoDataContext();
        
        public ActionResult Index(int? page)
        {          
            int pagesize = 10;
            int pageNum = (page ?? 1);
            var All_chatlieu = from tt in data.ChatLieus
                                select tt;
            return View(All_chatlieu.ToPagedList(pageNum, pagesize));
        }
        public ActionResult Createcl()
        {          
            return View();
        }
        [HttpPost]
        public ActionResult Createcl(FormCollection collection, ChatLieu cl)
        {

            var CB_MaChatLieu = collection["MaChatLieu"];
            var CB_TenChatLieu = collection["TenChatLieu"];
            if (string.IsNullOrEmpty(CB_MaChatLieu))
            {
                ViewData["Loi"] = "Mã chất liệu không được để trống";
            }
            else if (string.IsNullOrEmpty(CB_TenChatLieu))
            {
                ViewData["Loi1"] = " Tên chất liệu không được để trống ";
            }
            else
            {
                cl.MaChatLieu = CB_MaChatLieu;
                cl.TenChatLieu = CB_TenChatLieu;
                data.ChatLieus.InsertOnSubmit(cl);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Createcl();
        }
        public ActionResult Deletecl(string id)
        {
            var D_cl = data.ChatLieus.First(m => m.MaChatLieu == id);
            return View(D_cl);
        }
        [HttpPost]
        public ActionResult Deletecl(string id, FormCollection collection)
        {

            var D_cl = data.ChatLieus.First(m => m.MaChatLieu == id);
            data.ChatLieus.DeleteOnSubmit(D_cl);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Editcl(string id)
        {
            var E_cl = data.ChatLieus.First(m => m.MaChatLieu == id);
            return View(E_cl);
        }
        [HttpPost]
        public ActionResult Editcl(string id, FormCollection collection)
        {
            var CB_TenCL = collection["TenChatLieu"];
            var E_cl = data.ChatLieus.First(m => m.MaChatLieu == id);
            if (String.IsNullOrEmpty(CB_TenCL))
            {
                ViewData["Loi1"] = "Tên chất liệu không được để trống";
            }

            else
            {

                E_cl.TenChatLieu = CB_TenCL;
                UpdateModel(E_cl);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editcl(id);
        }
    }
}