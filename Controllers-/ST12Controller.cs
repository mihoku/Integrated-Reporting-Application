//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ira.Models;

//namespace ira.Controllers
//{
//    [Authorize]
//    public class ST12Controller : Controller
//    {
//        private IRADbContext db = new IRADbContext();

//        // GET: ST12
//        public ActionResult Index()
//        {
//            var transKegiatanST = db.TransKegiatanST.Include(t => t.RefKegiatan);
//            return View(transKegiatanST.ToList());
//        }

//        // GET: ST12/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            TransKegiatanST transKegiatanST = db.TransKegiatanST.Find(id);
//            if (transKegiatanST == null)
//            {
//                return HttpNotFound();
//            }
//            return View(transKegiatanST);
//        }

//        // GET: ST12/Create
//        public ActionResult Create()
//        {
//            ViewBag.KegiatanID = new SelectList(db.RefKegiatan, "ID", "KegName");
//            return View();
//        }

//        // POST: ST12/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,NoST,JudulST,Tahun,TanggalST,KegiatanID,TglAwal,TglAkhir,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanST transKegiatanST)
//        {
//            if (ModelState.IsValid)
//            {
//                db.TransKegiatanST.Add(transKegiatanST);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.KegiatanID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanST.KegiatanID);
//            return View(transKegiatanST);
//        }

//        // GET: ST12/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            TransKegiatanST transKegiatanST = db.TransKegiatanST.Find(id);
//            if (transKegiatanST == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.KegiatanID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanST.KegiatanID);
//            return View(transKegiatanST);
//        }

//        // POST: ST12/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,NoST,JudulST,Tahun,TanggalST,KegiatanID,TglAwal,TglAkhir,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanST transKegiatanST)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(transKegiatanST).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.KegiatanID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanST.KegiatanID);
//            return View(transKegiatanST);
//        }

//        // GET: ST12/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            TransKegiatanST transKegiatanST = db.TransKegiatanST.Find(id);
//            if (transKegiatanST == null)
//            {
//                return HttpNotFound();
//            }
//            return View(transKegiatanST);
//        }

//        // POST: ST12/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            TransKegiatanST transKegiatanST = db.TransKegiatanST.Find(id);
//            db.TransKegiatanST.Remove(transKegiatanST);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
