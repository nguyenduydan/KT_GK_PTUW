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
        public ActionResult FindID(string filterId, string filterName)
        {
            List<SINHVIENS> results;

            if (!string.IsNullOrEmpty(filterId) && !string.IsNullOrEmpty(filterName))
            {
                results = db.sinhvien.Where(m => m.masv.Contains(filterId) && (m.hosv + " " + m.tensv).Contains(filterName)).ToList();
                if (results.Count == 0)
                {
                    ViewBag.Message = "Không tìm thấy mã và tên sinh viên.";
                }
                ViewBag.FilterId = filterId; // Giữ lại giá trị filterId
                ViewBag.FilterName = filterName; // Giữ lại giá trị filterName
                return View(results);
            }
            else if (!string.IsNullOrEmpty(filterId))
            {
                results = db.sinhvien.Where(m => m.masv.Contains(filterId)).ToList();
                if (results.Count == 0)
                {
                    ViewBag.Message = "Không tìm thấy mã sinh viên.";
                }
                ViewBag.FilterId = filterId; // Giữ lại giá trị filterId
                return View(results);
            }
            else if (!string.IsNullOrEmpty(filterName))
            {
                results = db.sinhvien.Where(m => (m.hosv + " " + m.tensv).Contains(filterName)).ToList();
                ViewBag.FilterName = filterName; // Giữ lại giá trị filterName
                if (results.Count == 0)
                {
                    ViewBag.Message = "Không tìm thấy tên sinh viên.";
                }
                return View(results);
            }
            else
            {
                ViewBag.Message = "Không tìm thấy thông tin.";
                return View(db.sinhvien.ToList());
            }
        }


        public ActionResult GioiThieu_SV()
        {
            return View("GioiThieu_SV");
        }
    }
}
