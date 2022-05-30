using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ira.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Novacode;
using System.Diagnostics;
using Microsoft.Owin.Security;

namespace ira.Controllers
{
    public class KomentarController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //// GET: Komentar
        //public ActionResult Index()
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var transKomentar = db.TransKomentar.Include(t => t.RefKegiatan);
        //    return View(transKomentar.ToList());
        //}

        //// GET: Komentar/Details/5
        //public ActionResult Details(int? id)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    if (id == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    TransKegiatanKomentar transKegiatanKomentar = db.TransKomentar.Find(id);
        //    if (transKegiatanKomentar == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transKegiatanKomentar);
        //}

        // GET: Komentar/Create
        public ActionResult Create(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefKegiatan kegiatan = db.RefKegiatan.Find(id);
            ViewBag.KomenKegID = kegiatan.ID;
            ViewBag.KomenTgl = DateTime.Now;
            ViewBag.KomenUserID = User.Identity.GetUserName();
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            //var transKomentar = db.TransKomentar.Where(y=>y.KomenKegID==id).Include(t => t.RefKegiatan).ToList();
            //return View();
            return PartialView("_KomentarCreatePartial");
            //ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName");
            //return View();
        }

        // POST: Komentar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 //       [ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KomenIsi,KomenKegID,SysUsername,KomenID,SysTglEntry,SysWorkstation,KomenTgl,KomenUserID")] TransKegiatanKomentar transKegiatanKomentar)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                transKegiatanKomentar.KomenTgl = DateTime.Now;
                transKegiatanKomentar.KomenUserID = User.Identity.GetUserName();
                transKegiatanKomentar.SysUsername = User.Identity.GetUserName();
                transKegiatanKomentar.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                transKegiatanKomentar.SysTglEntry = DateTime.Now;
                db.TransKomentar.Add(transKegiatanKomentar);
                db.SaveChanges();
                RefKegiatan act = db.RefKegiatan.Find(transKegiatanKomentar.KomenKegID);
                var judul = act.KegName;
                if (currentuser.RoleID == 1)
                {
                    TransNotifikasi notifikasi = new TransNotifikasi
                    {
                        RouteID = transKegiatanKomentar.KomenKegID,
                        body = "Memberikan komentar pada kegiatan berjudul " + judul + "...",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Details",
                        Controller = "Kegiatan",
                        RoleID = 2,
                        Date = DateTime.Now,
                        NotifType = 2,
                        UnitID = act.RefTPU.TPUUnitPJID
                    };
                    db.TransNotifikasi.Add(notifikasi);
                    db.SaveChanges();
                }
                else
                {
                    TransNotifikasi notifikasi = new TransNotifikasi
                    {
                        RouteID = transKegiatanKomentar.KomenKegID,
                        body = "Memberikan komentar pada kegiatan berjudul " + judul + "...",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Details",
                        Controller = "Kegiatan",
                        RoleID = 1,
                        Date = DateTime.Now,
                        NotifType = 2,
                        UnitID = 9
                    };
                    db.TransNotifikasi.Add(notifikasi);
                    db.SaveChanges();
                }

                //TransKegiatanKomentar komentar = new TransKegiatanKomentar
                //{
                //    KomenIsi = "",
                //    KomenKegID = transKegiatanKomentar.KomenKegID,
                //    KomenTgl = DateTime.Now,
                //    KomenUserID = User.Identity.GetUserName(),
                //    SysUsername = User.Identity.GetUserName(),
                //    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                //    SysTglEntry = DateTime.Now
                //};
                return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanKomentar.KomenKegID });
//                return PartialView("_KomentarCreatePartial", komentar);
            }

            //ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanKomentar.KomenKegID);
            //return View(transKegiatanKomentar);
            return PartialView("_KomentarCreatePartial", transKegiatanKomentar);
        }

        // GET: Komentar/Create
        public ActionResult Flash(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatan kegiatan = db.TransFlashKegiatan.Find(id);
            ViewBag.KegiatanID = kegiatan.ID;
            ViewBag.KomenTgl = DateTime.Now;
            ViewBag.KomenUserID = User.Identity.GetUserName();
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            //var transKomentar = db.TransKomentar.Where(y=>y.KomenKegID==id).Include(t => t.RefKegiatan).ToList();
            //return View();
            return PartialView("_KomentarFlashCreatePartial");
            //ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName");
            //return View();
        }

        // POST: Komentar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //       [ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Flash([Bind(Include = "KomenIsi,KegiatanID,SysUsername,KomenID,SysTglEntry,SysWorkstation,KomenTgl,KomenUserID")] TransFlashKegiatanKomentar flashKomentar)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                flashKomentar.KomenTgl = DateTime.Now;
                flashKomentar.KomenUserID = User.Identity.GetUserName();
                flashKomentar.SysUsername = User.Identity.GetUserName();
                flashKomentar.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                flashKomentar.SysTglEntry = DateTime.Now;
                db.TransFlashKomentar.Add(flashKomentar);
                db.SaveChanges();
                TransFlashKegiatan act = db.TransFlashKegiatan.Find(flashKomentar.KegiatanID);
                var judul = act.Judul;
                if (currentuser.RoleID == 1)
                {
                    TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                    {
                        RouteID = flashKomentar.KegiatanID,
                        body = "Memberikan komentar pada kegiatan berjudul " + judul + "...",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Details",
                        Controller = "Flash",
                        RoleID = 3,
                        Date = DateTime.Now,
                        NotifType = 5,
                        UnitID = act.UnitID
                    };
                    db.TransFlashNotifikasi.Add(notifikasi);
                    db.SaveChanges();
                }

                else
                {
                    TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                    {
                        RouteID = flashKomentar.KegiatanID,
                        body = "Memberikan komentar pada kegiatan berjudul " + judul + "...",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Details",
                        Controller = "Flash",
                        RoleID = 1,
                        Date = DateTime.Now,
                        NotifType = 5,
                        UnitID = 9
                    };
                    db.TransFlashNotifikasi.Add(notifikasi);
                    db.SaveChanges();
                }
                //TransKegiatanKomentar komentar = new TransKegiatanKomentar
                //{
                //    KomenIsi = "",
                //    KomenKegID = transKegiatanKomentar.KomenKegID,
                //    KomenTgl = DateTime.Now,
                //    KomenUserID = User.Identity.GetUserName(),
                //    SysUsername = User.Identity.GetUserName(),
                //    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                //    SysTglEntry = DateTime.Now
                //};
                return RedirectToAction("Details", "Flash", new { id = flashKomentar.KegiatanID });
                //                return PartialView("_KomentarCreatePartial", komentar);
            }

            //ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanKomentar.KomenKegID);
            //return View(transKegiatanKomentar);
            return PartialView("_KomentarFlashCreatePartial", flashKomentar);
        }

        //public void CreateSampleDocument()
        //{
        //    string fileName = AppDomain.CurrentDomain.BaseDirectory + "Template/template_cetak1.docx";
        //    string headlineText = "Constitution of the United States";
        //    string paraOne = ""
        //        + "We the People of the United States, in Order to form a more perfect Union, "
        //        + "establish Justice, insure domestic Tranquility, provide for the common defence, "
        //        + "promote the general Welfare, and secure the Blessings of Liberty to ourselves "
        //        + "and our Posterity, do ordain and establish this Constitution for the United "
        //        + "States of America.";

        //    // A formatting object for our headline:
        //    var headLineFormat = new Formatting();
        //    headLineFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
        //    headLineFormat.Size = 18D;
        //    headLineFormat.Position = 12;

        //    // A formatting object for our normal paragraph text:
        //    var paraFormat = new Formatting();
        //    paraFormat.FontFamily = new System.Drawing.FontFamily("Calibri");
        //    paraFormat.Size = 10D;

        //    // Create the document in memory:
        //    var doc = DocX.Create(fileName);

        //    // Insert the now text obejcts;
        //    doc.InsertParagraph(headlineText, false, headLineFormat);
        //    doc.InsertParagraph(paraOne, false, paraFormat);

        //    // Save to the output directory:
        //    doc.Save();

        //    // Open in Word:
        //    Process.Start("WINWORD.EXE", fileName);
        //}

        //// GET: Komentar/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    TransKegiatanKomentar transKegiatanKomentar = db.TransKomentar.Find(id);
        //    if (transKegiatanKomentar == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanKomentar.KomenKegID);
        //    return View(transKegiatanKomentar);
        //}

        //// POST: Komentar/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "KomenID,KomenUserID,KomenIsi,KomenKegID,KomenTgl,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanKomentar transKegiatanKomentar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(transKegiatanKomentar).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName", transKegiatanKomentar.KomenKegID);
        //    return View(transKegiatanKomentar);
        //}

        // GET: Komentar/Delete/5
        public ActionResult Delete(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            TransKegiatanKomentar transKegiatanKomentar = db.TransKomentar.Find(id);
            if (transKegiatanKomentar == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transKegiatanKomentar);
        }

        // POST: Komentar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransKegiatanKomentar transKegiatanKomentar = db.TransKomentar.Find(id);
            db.TransKomentar.Remove(transKegiatanKomentar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
