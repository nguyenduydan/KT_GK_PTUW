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

namespace GiuaKy.Controllers
{
    public class SINHVIENController : Controller
    {
        private MyDB db = new MyDB();

        // GET: SINHVIEN
        public ActionResult Index()
        {
            return View(db.sinhvien.ToList());
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
                //Xử lý thông tin cho hình ảnh
                var img = Request.Files["Img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string masv = sINHVIENS.masv;
                        //ten file = Slug + phan mo rong cua tap tin
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

            return View(sINHVIENS);
        }

        // GET: SINHVIEN/Edit/5
        public ActionResult Edit(string id)
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

        // POST: SINHVIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "masv,hosv,tensv,ngaysinh,gioitinh,anhsv,diachi,malop")] SINHVIENS sINHVIENS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIENS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sINHVIENS);
        }

        // GET: SINHVIEN/Delete/5
        public ActionResult GioiThieu_SV()
        {
            return View("GioiThieu_SV");
        }
    }
}
