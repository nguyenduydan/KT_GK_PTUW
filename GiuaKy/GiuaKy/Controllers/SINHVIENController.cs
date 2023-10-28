using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiuaKy.Models;
using static System.Net.WebRequestMethods;

namespace GiuaKy.Controllers
{
    public class SINHVIENController : Controller
    {
        private MyDB db = new MyDB();

        // GET: SINHVIEN
        public ActionResult Index()
        {
            var sinhvien = db.sinhvien.Include(s => s.LOPS);
            ViewBag.malop = new SelectList(db.lop, "malop", "tenlop");
            return View(sinhvien.ToList());
        }

        // GET: SINHVIEN/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIENS sINHVIENS = db.sinhvien.Find(id);
            if (sINHVIENS == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIENS);
        }

        // GET: SINHVIEN/Create
        public ActionResult Create()
        {
            ViewBag.malop = new SelectList(db.lop, "malop", "tenlop");
            return View();
        }

        // POST: SINHVIEN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masv,hosv,tensv,ngaysinh,gioitinh,anhsv,diachi,malop")] SINHVIENS sINHVIENS)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem mã sinh viên đã tồn tại hay chưa
                bool check = db.sinhvien.Any(nv => nv.masv == sINHVIENS.masv);

                if (check)
                {
                    // Mã sinh viên đã tồn tại, hiển thị thông báo lỗi
                    ModelState.AddModelError("masv", "Mã sinh viên đã tồn tại.");

                }
                //Xử lý thông tin cho hình ảnh
                var img = Request.Files["Img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string masv = sINHVIENS.masv;
                        //ten file = imgName + phan mo rong cua tap tin
                        string imgName = masv + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        sINHVIENS.anhsv = imgName;
                        //upload hinh
                        string PathDir = "~/Public/Img/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                db.sinhvien.Add(sINHVIENS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.malop = new SelectList(db.lop, "malop", "tenlop",sINHVIENS.malop);
            return View(sINHVIENS);
        }

        //Tìm kiếm sinh viên
        public ActionResult FindID()
        {
            var sinhvien = db.sinhvien.Include(s => s.LOPS);
            return View(sinhvien.ToList());
        }

        [HttpPost]
        public ActionResult FindID(string filter)
        {
            List<SINHVIENS> listNews = db.sinhvien.Where(m => m.tensv.ToLower().Contains(filter.ToLower()) == true).ToList();
            if (listNews != null && listNews.Count > 0)
            {
                ViewBag.ListNews = listNews; // Lưu danh sách sinh viên vào ViewBag
                return View(listNews);
            }
            else
            {
                ViewBag.Message = "Không có thông tin cần tìm.";
                return View("FindID");
            }
        }

        public ActionResult GioiThieu_SV()
        {
            return View("GioiThieu_SV");
        }
    }
}
