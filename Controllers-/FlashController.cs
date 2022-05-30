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
using ira.Web.Utilities;
using Microsoft.Owin.Security;

namespace ira.Controllers
{
    [Authorize]
    public class FlashController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Kegiatan
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            //var flashKegiatan = db.TransFlashKegiatan.Include(r => r.RefPegawai).Include(r => r.RefUnitPJ);
            //return View(flashKegiatan.ToList());
            var statusKegiatan = db.RefFlashStatusKegiatan.Where(y => y.Aktif == true);
            return View(statusKegiatan.ToList());
        }

        public ActionResult ListKegiatan(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefFlashKegiatanStatus status = db.RefFlashStatusKegiatan.Find(id);

            if (status == null)
            {
                return PartialView("_NotFound");
            }

            var flashKegiatan = db.TransFlashKegiatan.Where(y=>y.Finalize==status.ID).Include(r => r.RefPegawai).Include(r => r.RefUnitPJ);
            return PartialView("_listKegiatan", flashKegiatan.ToList());
        }

        // GET: Kegiatan/Details/5
        public ActionResult Details(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ViewBag.user = User.Identity.GetUserName();
            TransFlashKegiatan flashKegiatan = db.TransFlashKegiatan.Find(id);
            if (flashKegiatan == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            //TransKegiatanProgress LastRecord = db.TransProgressKegiatan.Where(y => y.KegiatanID == id).OrderByDescending(y => y.Period).FirstOrDefault();
            //RefPeriode latest = db.RefPeriode.OrderByDescending(y => y.ID).FirstOrDefault();
            //if (LastRecord != null&&LastRecord.Period == latest.ID)
            //{
            //    ViewBag.limit = true;
            //    ViewBag.OutputCount = refKegiatan.TransKegiatanOutput.Count();
            //    ViewBag.SysUsername = refKegiatan.SysUsername;
            //    ViewBag.SysTglEntry = refKegiatan.SysTglEntry;
            //    ViewBag.SysWorkStation = refKegiatan.SysWorkstation;
            //    ViewBag.KegName = refKegiatan.KegName;
            //    ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            //    ViewBag.KegTarget = refKegiatan.KegTarget;
            //    ViewBag.KegMjrID = refKegiatan.KegMjrID;
            //    ViewBag.Keterangan = refKegiatan.Keterangan;
            //    return View(refKegiatan);
            //}

            //ViewBag.OutputCount = refKegiatan.TransKegiatanOutput.Count();
            //ViewBag.SysUsername = refKegiatan.SysUsername;
            //ViewBag.SysTglEntry = refKegiatan.SysTglEntry;
            //ViewBag.SysWorkStation = refKegiatan.SysWorkstation;
            //ViewBag.KegName = refKegiatan.KegName;
            //ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            //ViewBag.KegTarget = refKegiatan.KegTarget;
            //ViewBag.KegMjrID = refKegiatan.KegMjrID;
            //ViewBag.Keterangan = refKegiatan.Keterangan;
            RefFlashKegiatanStatus status = db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize+1);
            if (status == null)
            {
                return View(flashKegiatan);
            }
            if (status.ID == 2)
            {
                ViewBag.Finalize = flashKegiatan.Finalize + 1;
                ViewBag.Elevate = "Hukuman Disiplin";
                return View(flashKegiatan);
            }
            ViewBag.Finalize = flashKegiatan.Finalize + 1;
            ViewBag.Elevate = status.Ket;
            return View(flashKegiatan);
        }

         //GET: Kegiatan/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }
            //if (!id.HasValue)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //if (id == null)
            //{
            //    //ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket");
            //    //ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y=>y.Aktif==true), "ID", "PegName");
            //    //ViewBag.KegiatanTPUID = new SelectList(db.RefTPU.Where(y=>y.TransSchedule.Locked==0), "ID", "TPUName");
            //    //ViewBag.SysUsername = User.Identity.GetUserName();
            //    //ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            //    //ViewBag.SysTglEntry = DateTime.Now;
            //    //return View();
            //    return HttpNotFound();
            //}
            
            //RefTPU tpu = db.RefTPU.Find(id);
            
            //if (tpu == null)
            //{
            //    return HttpNotFound();
            //}

            if (currentuser.RoleID == 1)
            {
                ViewBag.Finalize = 1;
                ViewBag.ManajerID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true), "ID", "PegName");
                ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false), "ID", "Detail");
                ViewBag.SysUsername = User.Identity.GetUserName();
                ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                ViewBag.SysTglEntry = DateTime.Now;
                return View();
            }

            ViewBag.Finalize = 1;
            ViewBag.ManajerID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true&&y.PegUnitID==currentuser.UnitID), "ID", "PegName");
            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true&&y.isPrimeMover==false&&y.ID==currentuser.UnitID), "ID", "Detail");
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: Kegiatan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UnitID,Judul,ManajerID,TanggalKasus,SysUsername,SysTglEntry,SysWorkstation,FInalize")] TransFlashKegiatan flashKegiatan)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                    flashKegiatan.Finalize = 1;
                    db.TransFlashKegiatan.Add(flashKegiatan);
                    db.SaveChanges();
                    //var Elevate = db.RefTPU.FirstOrDefault(y=>y.ID==refKegiatan.KegiatanTPUID);
                    //Elevate.TPUStatusID = 2;
                    //db.SaveChanges();
                    var judul = flashKegiatan.Judul;
                    RefFlashKegiatanStatus statusFlash = db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize);
                    var status = statusFlash.Ket;
                    TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                    {
                        RouteID = flashKegiatan.ID,
                        body = "Telah masuk pada tahap "+status+" satu kegiatan baru yang berjudul  " + judul + ".",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Details",
                        Controller = "Flash",
                        RoleID = 1,
                        Date = DateTime.Now,
                        NotifType = 4
                    };
                    db.TransFlashNotifikasi.Add(notifikasi);
                    db.SaveChanges();

                    if (db.TransNDPermintaanFlash.Where(y => y.Locked == false).OrderByDescending(y => y.ID).Count() != 0)
                    {
                        TransNDPermintaanFlash nd = db.TransNDPermintaanFlash.Where(y => y.Locked == false).OrderByDescending(y => y.ID).FirstOrDefault();
                        TransFlashKegiatanProgress progress = new TransFlashKegiatanProgress
                        {
                            Tahun = nd.Tahun,
                            KegiatanID = flashKegiatan.ID,
                            KegStatusID = 1,
                            Period = nd.PeriodeID,
                            SysUsername = User.Identity.GetUserName(),
                            SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                            SysTglEntry = DateTime.Now
                        };
                        db.TransFlashProgress.Add(progress);
                        db.SaveChanges();
                        return RedirectToAction("Details", "Flash", new { id = flashKegiatan.ID });
                    }

                    return RedirectToAction("Details", "Flash", new { id = flashKegiatan.ID });
            }
            if (currentuser.RoleID == 1)
            {
                ViewBag.Finalize = 1;
                ViewBag.ManajerID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true), "ID", "PegName", flashKegiatan.ManajerID);
                ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false), "ID", "Detail", flashKegiatan.UnitID);
                ViewBag.SysUsername = User.Identity.GetUserName();
                ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                ViewBag.SysTglEntry = DateTime.Now;
                return View(flashKegiatan);
            }
            ViewBag.Finalize = 1;
            ViewBag.ManajerID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == currentuser.UnitID), "ID", "PegName", flashKegiatan.ManajerID);
            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false && y.ID == currentuser.UnitID), "ID", "Detail", flashKegiatan.UnitID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            //RefTPU Tpu = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
            //ViewBag.tpu = Tpu.TPUName;
            //ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
            //ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID), "ID", "PegName", refKegiatan.KegMjrID);
            //ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            //ViewBag.SysUsername = User.Identity.GetUserName();
            //ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            //ViewBag.SysTglEntry = DateTime.Now;
            return View(flashKegiatan);
        }

        // GET: Kegiatan/Create
        public ActionResult AddKegiatan( int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentuser.RoleID == 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (unit == null)
            {
                return HttpNotFound();
            }

            ViewBag.Finalize = 1;
            ViewBag.ManajerID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == currentuser.UnitID), "ID", "PegName");
            ViewBag.UnitID = unit.ID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: Kegiatan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddKegiatan([Bind(Include = "ID,UnitID,Judul,ManajerID,TanggalKasus,SysUsername,SysTglEntry,SysWorkstation,FInalize")] TransFlashKegiatan flashKegiatan)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                flashKegiatan.Finalize = 1;
                db.TransFlashKegiatan.Add(flashKegiatan);
                db.SaveChanges();
                //var Elevate = db.RefTPU.FirstOrDefault(y=>y.ID==refKegiatan.KegiatanTPUID);
                //Elevate.TPUStatusID = 2;
                //db.SaveChanges();
                
                TransNDPermintaanFlash nd = db.TransNDPermintaanFlash.Where(y => y.Locked == false).OrderByDescending(y => y.ID).FirstOrDefault();

                TransFlashKegiatanProgress progress = new TransFlashKegiatanProgress
                {
                    Tahun = nd.Tahun,
                    KegiatanID = flashKegiatan.ID,
                    KegStatusID = 1,
                    Period = nd.PeriodeID,
                    SysUsername = User.Identity.GetUserName(),
                    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                    SysTglEntry = DateTime.Now
                };

                db.TransFlashProgress.Add(progress);
                db.SaveChanges();

                var judul = flashKegiatan.Judul;
                RefFlashKegiatanStatus statusFlash = db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize);
                var status = statusFlash.Ket;
                TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                {
                    RouteID = flashKegiatan.ID,
                    body = "Telah masuk pada tahap " + status + " satu kegiatan baru yang berjudul  " + judul + ".",
                    name = currentuser.FirstName + " " + currentuser.LastName,
                    Action = "Details",
                    Controller = "Flash",
                    RoleID = 1,
                    Date = DateTime.Now,
                    NotifType = 4
                };
                db.TransFlashNotifikasi.Add(notifikasi);
                db.SaveChanges();
                return RedirectToAction("Pending");
            }
            if (currentuser.RoleID == 1)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Finalize = 1;
            ViewBag.ManajerID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == currentuser.UnitID), "ID", "PegName", flashKegiatan.ManajerID);
            ViewBag.UnitID = flashKegiatan.UnitID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            //RefTPU Tpu = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
            //ViewBag.tpu = Tpu.TPUName;
            //ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
            //ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID), "ID", "PegName", refKegiatan.KegMjrID);
            //ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            //ViewBag.SysUsername = User.Identity.GetUserName();
            //ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            //ViewBag.SysTglEntry = DateTime.Now;
            return View(flashKegiatan);
        }

        // GET: Kegiatan/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
        //    if (refKegiatan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (refKegiatan.Finalize == 1)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        //    }

        //    ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
        //    ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => (y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID) || y.ID == refKegiatan.KegMjrID), "ID", "PegName", refKegiatan.KegMjrID);
        //    ViewBag.KegiatanTPUID = new SelectList(db.RefTPU.Where(y=>y.TransSchedule.ID==refKegiatan.RefTPU.TransSchedule.ID), "ID", "TPUName", refKegiatan.KegiatanTPUID);
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    return View(refKegiatan);
        //}

        //// POST: Kegiatan/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,KegName,KegiatanTPUID,KegMjrID,KegOutput,KegTarget,Output,Keterangan,SysUsername,SysTglEntry,SysWorkstation")] RefKegiatan refKegiatan)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(refKegiatan).State = EntityState.Modified;
        //        db.SaveChanges();
        //        var dbKeg = db.RefKegiatan.FirstOrDefault(p => p.ID == refKegiatan.ID);
        //        if (dbKeg == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        dbKeg.SysUsername = User.Identity.GetUserName();
        //        dbKeg.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //        dbKeg.SysTglEntry = DateTime.Now;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
        //    ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => (y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID) || y.ID == refKegiatan.KegMjrID), "ID", "PegName", refKegiatan.KegMjrID);
        //    ViewBag.KegiatanTPUID = new SelectList(db.RefTPU.Where(y => y.TransSchedule.ID == refKegiatan.RefTPU.TransSchedule.ID), "ID", "TPUName", refKegiatan.KegiatanTPUID);
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    return View(refKegiatan);
        //}

        // GET: Komentar/Create
        public ActionResult Komentar(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
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
            return View();
            //ViewBag.KomenKegID = new SelectList(db.RefKegiatan, "ID", "KegName");
            //return View();
        }

        // POST: Komentar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[ActionName("Details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Komentar([Bind(Include = "KomenIsi,KomenKegID,SysUsername,KomenID,SysTglEntry,SysWorkstation,KomenTgl,KomenUserID")] TransKegiatanKomentar transKegiatanKomentar)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
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
                TransNotifikasi notifikasi = new TransNotifikasi
                {
                    RouteID = transKegiatanKomentar.KomenKegID,
                    body = "Memberikan komentar pada kegiatan berjudul " + judul + ".",
                    name = currentuser.FirstName+" "+currentuser.LastName,
                    Action = "Details",
                    Controller = "Flash",
                    RoleID = 1,
                    Date = DateTime.Now,
                    NotifType = 2
                };
                db.TransNotifikasi.Add(notifikasi);
                db.SaveChanges();
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
                return RedirectToAction("Details", "Flash", new { id = transKegiatanKomentar.KomenKegID });
                //                return PartialView("_KomentarCreatePartial", komentar);
            }
            return View(transKegiatanKomentar);
        }

        public ActionResult PopUp()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }
            var refPopup = db.RefPopUpText.Where(y => y.ModulID == 2).FirstOrDefault();

            return View(refPopup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PopUp([Bind(Include = "ID,Message,Airing")] RefPopupText refPopup)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Entry(refPopup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refPopup);
        }
        //public ActionResult Finalize(int id)
        //{
            
        //    RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
        //    if (refKegiatan == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (refKegiatan.TransKegiatanOutput.Count() < 1)
        //    {
        //        return RedirectToAction("Dashboard", "Home", null);
        //    }

        //    ViewBag.SysUsername = refKegiatan.SysUsername;
        //    ViewBag.SysTglEntry = refKegiatan.SysTglEntry;
        //    ViewBag.SysWorkStation = refKegiatan.SysWorkstation;
        //    ViewBag.KegName = refKegiatan.KegName;
        //    ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
        //    ViewBag.KegTarget = refKegiatan.KegTarget;
        //    ViewBag.KegMjrID = refKegiatan.KegMjrID;
        //    ViewBag.Keterangan = refKegiatan.Keterangan;
        //    return View(refKegiatan);
        //}

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult Finalize([Bind(Include = "ID,Judul,ManajerID,TanggalKasus,SysUsername,SysTglEntry,SysWorkstation,Finalize")] TransFlashKegiatan flashKegiatan)
        {
            //if (refKegiatan.TransKegiatanOutput.Count > 0)
            //{
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }    
            if (ModelState.IsValid)
                {
                    //status yng dimasukkan bener2 ada di tabel referensi
                    if (db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize) != null)
                    {
                        TransFlashKegiatan flash = db.TransFlashKegiatan.Find(flashKegiatan.ID);
                        if (flash.TransFlashKegiatanProgress.Where(y => y.KegStatusID == flashKegiatan.Finalize).Count() != 0)
                        {
                            //db.Entry(refKegiatan).State = EntityState.Modified;
                            var dbKeg = db.TransFlashKegiatan.Find(flashKegiatan.ID);
                            if (dbKeg == null)
                            {
                                return HttpNotFound();
                            }
                            dbKeg.Finalize = flashKegiatan.Finalize;
                            dbKeg.DateFinalized = DateTime.Now;
                            db.SaveChanges();
                            TransFlashKegiatan act2 = db.TransFlashKegiatan.Find(flashKegiatan.ID);
                            var judul2 = act2.Judul;
                            RefFlashKegiatanStatus stat2 = db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize);
                            var status2 = stat2.Ket;
                            var notif = new TransFlashNotifikasi
                            {
                                name = currentuser.FirstName+" "+currentuser.LastName,
                                Action = "Details",
                                Controller = "Flash",
                                RouteID = flashKegiatan.ID,
                                body = "Perubahan status kegiatan menjadi " + status2 + " untuk kegiatan berjudul " + judul2 + "...",
                                RoleID = 1,
                                Date = DateTime.Now,
                                NotifType = 4
                            };
                            db.TransFlashNotifikasi.Add(notif);
                            db.SaveChanges();
                            return RedirectToAction("Details", "Flash", new { flashKegiatan.ID });
                        }

                        return HttpNotFound();
                    }

                    return HttpNotFound();
                }
                return RedirectToAction("Details", "Flash", new { id = flashKegiatan.ID });
        //    }
        //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });            
        }

        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Finalize2([Bind(Include = "ID,Finalize")] KegiatanFinalizeModel keg)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(keg).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "Kegiatan", new { keg.ID });
        //    }
        //    return RedirectToAction("Details", "Kegiatan", new { id = keg.ID });
        //}

        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Unfinalize([Bind(Include = "ID,KegName,KegiatanTPUID,KegMjrID,KegTarget,Keterangan,SysUsername,SysTglEntry,SysWorkstation,Finalize")] RefKegiatan refKegiatan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(refKegiatan).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "Kegiatan", new { refKegiatan.ID });
        //    }
        //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });
        //}

        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Finalize([Bind(Include = "ID,Finalize")] RefKegiatan refKegiatan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(refKegiatan).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "Kegiatan", new { refKegiatan.ID });
        //    }
        //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });
        //}

        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Finalize(int id)
        //{
        //    RefKegiatan refKegiatan = db.RefKegiatan.Find(id);

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(refKegiatan).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Kegiatan");
        //    }
        //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID + 2 });
        //}
        //public ActionResult Progress(int id)
        //{
        //    //if (!id.HasValue)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}

        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}

        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    TransFlashKegiatan flash = db.TransFlashKegiatan.Find(id);
        //    if (flash == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //if (refKegiatan.RefTPU.Finalize == 1 || refKegiatan.Finalize == 1)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    TransFlashKegiatanProgress LastRecord = db.TransFlashProgress.Where(y => y.KegiatanID == id).OrderByDescending(y => y.ID).FirstOrDefault();

        //    if (LastRecord != null)
        //    {
        //        RefPeriode latest = db.RefPeriode.OrderByDescending(y => y.ID).FirstOrDefault();
        //        if (LastRecord.Period == latest.ID)
        //        {
        //            RefPeriode selection1 = db.RefPeriode.OrderBy(y=>y.ID).FirstOrDefault();
        //            ViewBag.keg = flash.Judul;
        //            ViewBag.KegiatanID = id;
        //            ViewBag.KegStatusID = new SelectList(db.RefFlashStatusKegiatan, "ID", "Ket");
        //            ViewBag.Period = selection1.ID;
        //            ViewBag.Tahun = LastRecord.Tahun+1;
        //            ViewBag.month = selection1.Ket;
        //            ViewBag.SysUsername = User.Identity.GetUserName();
        //            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //            ViewBag.SysTglEntry = DateTime.Now;
        //            return View();
        //        }
        //        RefPeriode selection = db.RefPeriode.Find(LastRecord.Period+1);
        //        ViewBag.keg = flash.Judul;
        //        ViewBag.KegiatanID = id;
        //        ViewBag.KegStatusID = new SelectList(db.RefFlashStatusKegiatan, "ID", "Ket");
        //        ViewBag.Period = selection.ID;
        //        ViewBag.Tahun = LastRecord.Tahun;
        //        //ViewBag.monthnow = DateTime.Now.Month;
        //        ViewBag.month = selection.Ket;
        //        ViewBag.SysUsername = User.Identity.GetUserName();
        //        ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //        ViewBag.SysTglEntry = DateTime.Now;
        //        return View();
        //    }

        //    RefPeriode earliest = db.RefPeriode.OrderBy(y => y.ID).FirstOrDefault();
        //    ViewBag.keg = flash.Judul;
        //    ViewBag.KegiatanID = id;
        //    ViewBag.Tahun = DateTime.Now.Year;
        //    ViewBag.month = earliest.Ket;
        //    ViewBag.period = earliest.ID;
        //    ViewBag.year = flash.TanggalKasus.Year;
        //    ViewBag.KegStatusID = new SelectList(db.RefFlashStatusKegiatan, "ID", "Ket");
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    return View();
        //}

        public ActionResult Progress(int id)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatanProgress flash = db.TransFlashProgress.Find(id);

            if (flash == null)
            {
                return HttpNotFound();
            }

            if (currentuser.RoleID!=1&& currentuser.UnitID != flash.TransFlashKegiatan.UnitID)
            {
                return RedirectToAction("Pending");
            }

            if (flash.TransFlashKegiatan.Finalize == 3)
            {
                return RedirectToAction("Pending");
            }

            ViewBag.keg = flash.TransFlashKegiatan.Judul;
            ViewBag.KegiatanID = flash.KegiatanID;
            ViewBag.Tahun = DateTime.Now.Year;
            ViewBag.month = flash.RefPeriode.Ket;
            ViewBag.period = flash.Period;
            ViewBag.year = flash.TransFlashKegiatan.TanggalKasus.Year;
            ViewBag.KegStatusID = new SelectList(db.RefFlashStatusKegiatan, "ID", "Ket", flash.KegStatusID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(flash);
        }

        // POST: Progress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Progress([Bind(Include = "ID,Detail,Period,Tahun,KegiatanID,KegStatusID,SysUsername,SysTglEntry,SysWorkstation")] TransFlashKegiatanProgress flash)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatanProgress flashTargeted = db.TransFlashProgress.Find(flash.ID);

            if (currentuser.RoleID != 1 && currentuser.UnitID != flashTargeted.TransFlashKegiatan.UnitID)
            {
                return RedirectToAction("Pending");
            }

            if (flashTargeted.TransFlashKegiatan.Finalize == 3)
            {
                return RedirectToAction("Pending");
            }

            if (ModelState.IsValid)
            {
                TransFlashKegiatanProgress LastRecord = db.TransFlashProgress.Where(y => y.KegiatanID == flash.KegiatanID&&y.ID!=flash.ID).OrderByDescending(y => y.ID).FirstOrDefault();
                TransFlashKegiatanProgress flashProgress = db.TransFlashProgress.Find(flash.ID);
                if (LastRecord != null){
                    TransFlashKegiatan act = db.TransFlashKegiatan.Find(flashProgress.KegiatanID);
                    flashProgress.Detail = flash.Detail;
                    flashProgress.KegStatusID = flash.KegStatusID;
                    flashProgress.SysUsername = User.Identity.GetUserName();
                    flashProgress.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                    flashProgress.SysTglEntry = DateTime.Now;
                    db.SaveChanges();
                    if (LastRecord.KegStatusID != flashProgress.KegStatusID)
                    {
                        //TransFlashKegiatan act = db.TransFlashKegiatan.Find(flash.KegiatanID);
                        var judul = act.Judul;
                        RefFlashKegiatanStatus stat = db.RefFlashStatusKegiatan.Find(flashProgress.KegStatusID);
                        var status = stat.Ket;
                        TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                        {
                            RouteID = act.ID,
                            body = "Usulan perubahan status kegiatan menjadi " + status + " untuk kegiatan berjudul " + judul + ".",
                            name = currentuser.FirstName + " " + currentuser.LastName,
                            Action = "Details",
                            Controller = "Flash",
                            RoleID = 1,
                            Date = DateTime.Now,
                            NotifType = 4
                        };
                        db.TransFlashNotifikasi.Add(notifikasi);
                        db.SaveChanges();
                    };
                    return RedirectToAction("Pending");                
                }
          
                TransFlashKegiatan kegiatan = db.TransFlashKegiatan.Find(flashProgress.KegiatanID);
                flashProgress.Detail = flash.Detail;
                flashProgress.KegStatusID = flash.KegStatusID;
                flashProgress.SysUsername = User.Identity.GetUserName();
                flashProgress.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                flashProgress.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                if (flashProgress.KegStatusID!=kegiatan.Finalize)
                {
                    //TransFlashKegiatan act = db.TransFlashKegiatan.Find(flash.KegiatanID);
                    var judul = kegiatan.Judul;
                    RefFlashKegiatanStatus stat = db.RefFlashStatusKegiatan.Find(flashProgress.KegStatusID);
                    var status = stat.Ket;
                    TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                    {
                        RouteID = kegiatan.ID,
                        body = "Usulan perubahan status kegiatan menjadi " + status + " untuk kegiatan berjudul " + judul + ".",
                        name = currentuser.FirstName + " " + currentuser.LastName,
                        Action = "Details",
                        Controller = "Flash",
                        RoleID = 1,
                        Date = DateTime.Now,
                        NotifType = 4
                    };
                    db.TransFlashNotifikasi.Add(notifikasi);
                    db.SaveChanges();
                };
                return RedirectToAction("Pending");
            }

            ViewBag.KegiatanID = flash.KegiatanID;
            ViewBag.KegStatusID = new SelectList(db.RefFlashStatusKegiatan, "ID", "Ket", flash.KegStatusID);
            ViewBag.Period = flash.Period;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(flash);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Progress([Bind(Include = "ID,Detail,Period,Tahun,KegiatanID,KegStatusID,SysUsername,SysTglEntry,SysWorkstation")] TransFlashKegiatanProgress flash)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        TransFlashKegiatanProgress LastRecord = db.TransFlashProgress.Where(y => y.KegiatanID == flash.KegiatanID).OrderByDescending(y => y.ID).FirstOrDefault();
        //        if (LastRecord != null)
        //        {
        //            RefPeriode latest = db.RefPeriode.OrderByDescending(y => y.ID).FirstOrDefault();
        //            if (latest.ID == LastRecord.Period)
        //            {
        //                RefPeriode selection1 = db.RefPeriode.OrderBy(y => y.ID).FirstOrDefault();
        //                flash.Tahun = LastRecord.Tahun + 1;
        //                flash.Period = selection1.ID;
        //                flash.SysUsername = User.Identity.GetUserName();
        //                flash.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //                flash.SysTglEntry = DateTime.Now;
        //                db.TransFlashProgress.Add(flash);
        //                db.SaveChanges();
        //                if (LastRecord.KegStatusID != flash.KegStatusID)
        //                {
        //                    TransFlashKegiatan act = db.TransFlashKegiatan.Find(flash.KegiatanID);
        //                    var judul = act.Judul;
        //                    RefFlashKegiatanStatus stat = db.RefFlashStatusKegiatan.Find(flash.KegStatusID);
        //                    var status = stat.Ket;
        //                    TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
        //                    {
        //                        RouteID = flash.KegiatanID,
        //                        body = "Usulan perubahan status kegiatan menjadi " + status + " untuk kegiatan berjudul " + judul + ".",
        //                        name = currentuser.FirstName+" "+currentuser.LastName,
        //                        Action = "Details",
        //                        Controller = "Flash",
        //                        RoleID = 1,
        //                        Date = DateTime.Now,
        //                        NotifType = 4
        //                    };
        //                    db.TransFlashNotifikasi.Add(notifikasi);
        //                    db.SaveChanges();
        //                };
        //                return RedirectToAction("Details", "Flash", new { id = flash.KegiatanID });

        //            }
        //            RefPeriode selection = db.RefPeriode.Find(LastRecord.Period + 1);
        //            flash.Tahun = LastRecord.Tahun;
        //            flash.Period = selection.ID;
        //            flash.SysUsername = User.Identity.GetUserName();
        //            flash.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //            flash.SysTglEntry = DateTime.Now;
        //            db.TransFlashProgress.Add(flash);
        //            db.SaveChanges();
        //            if (LastRecord.KegStatusID != flash.KegStatusID)
        //            {
        //                TransFlashKegiatan act = db.TransFlashKegiatan.Find(flash.KegiatanID);
        //                var judul = act.Judul;
        //                RefFlashKegiatanStatus stat = db.RefFlashStatusKegiatan.Find(flash.KegStatusID);
        //                var status = stat.Ket;
        //                TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
        //                {
        //                    RouteID = flash.KegiatanID,
        //                    body = "Usulan perubahan status kegiatan menjadi " + status + " untuk kegiatan berjudul " + judul + ".",
        //                    name = currentuser.FirstName+" "+currentuser.LastName,
        //                    Action = "Details",
        //                    Controller = "Flash",
        //                    RoleID = 1,
        //                    Date = DateTime.Now,
        //                    NotifType = 4
        //                };
        //                db.TransFlashNotifikasi.Add(notifikasi);
        //                db.SaveChanges();
        //            };
        //            return RedirectToAction("Details", "Flash", new { id = flash.KegiatanID });
        //        }
        //        TransFlashKegiatan kegiatan = db.TransFlashKegiatan.Find(flash.ID);
        //        RefPeriode periode = db.RefPeriode.OrderBy(y => y.ID).Where(y => y.ID > kegiatan.TanggalKasus.Month).FirstOrDefault();
        //        flash.Period = periode.ID;
        //        flash.SysUsername = User.Identity.GetUserName();
        //        flash.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //        flash.SysTglEntry = DateTime.Now;
        //        db.TransFlashProgress.Add(flash);
        //        db.SaveChanges();
        //        if (flash.TransFlashKegiatan.Finalize != flash.KegStatusID)
        //        {
        //            TransFlashKegiatan act = db.TransFlashKegiatan.Find(flash.KegiatanID);
        //            var judul = act.Judul;
        //            RefFlashKegiatanStatus stat = db.RefFlashStatusKegiatan.Find(flash.KegStatusID);
        //            var status = stat.Ket;
        //            TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
        //            {
        //                RouteID = flash.KegiatanID,
        //                body = "Usulan perubahan status kegiatan menjadi " + status + " untuk kegiatan berjudul " + judul + ".",
        //                name = currentuser.FirstName+" "+currentuser.LastName,
        //                Action = "Details",
        //                Controller = "Flash",
        //                RoleID = 1,
        //                Date = DateTime.Now,
        //                NotifType = 4
        //            };
        //            db.TransFlashNotifikasi.Add(notifikasi);
        //            db.SaveChanges();
        //        };
        //        return RedirectToAction("Details", "Flash", new { id = flash.KegiatanID });
        //        //RefKegiatan act2 = db.RefKegiatan.Find(transKegiatanProgress.KegiatanID);
        //        //var judul2 = act2.KegName;
        //        //RefKegiatanStatus stat2 = db.RefStatusKegiatan.Find(transKegiatanProgress.KegStatusID);
        //        //var status2 = stat2.Ket;
        //        //var notif = new TransNotifikasi
        //        //{
        //        //    name = currentuser.FirstName+" "+currentuser.LastName,
        //        //    Action = "Details",
        //        //    Controller = "Kegiatan",
        //        //    RouteID = transKegiatanProgress.KegiatanID,
        //        //    body = "Perubahan status kegiatan menjadi " + status2 + " untuk kegiatan berjudul " + judul2 + "...",
        //        //    RoleID = 1,
        //        //    Date = DateTime.Now,
        //        //    NotifType = 1
        //        //};
        //        //db.TransNotifikasi.Add(notif);
        //        //db.SaveChanges();
        //    }

        //    ViewBag.KegiatanID = flash.KegiatanID;
        //    ViewBag.KegStatusID = new SelectList(db.RefFlashStatusKegiatan, "ID", "Ket", flash.KegStatusID);
        //    ViewBag.Period = flash.Period;
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    return View(flash);
        //}

        //[Authorize(Roles="Admin")]
        // GET: Kegiatan/Delete/5
        public ActionResult Delete(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransFlashKegiatan flashKegiatan = db.TransFlashKegiatan.Find(id);
            if (flashKegiatan == null)
            {
                return HttpNotFound();
            }
            if (flashKegiatan.Finalize == 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(flashKegiatan);
        }

        public ActionResult Notifications()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account", null);
            }

            TransFlashNotifClick click = new TransFlashNotifClick
            {
                Date = DateTime.Now,
                UserName = User.Identity.GetUserName()
            };
            db.TransFlashNotifClick.Add(click);
            db.SaveChanges();

            if (currentuser.RoleID == 1)
            {
                var notifications = db.TransFlashNotifikasi.Where(y=>y.RoleID==1).OrderByDescending(y => y.Date).ToList();

                return View(notifications);
            }

            else
            {
                var notifications = db.TransFlashNotifikasi.Where(y => y.RoleID == 3&&y.UnitID==currentuser.UnitID).OrderByDescending(y => y.Date).ToList();

                return View(notifications);
            }
        }

        // POST: Kegiatan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
            db.RefKegiatan.Remove(refKegiatan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Pending()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == 0)
            //{
            //    return RedirectToAction("Pending", null);
            //}

            TransNDPermintaanFlash nd = db.TransNDPermintaanFlash.Where(y => y.Locked == false).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd == null)
            {
                ViewBag.Title = "Permintaan Data Flash Report";
                ViewBag.empty = true;

                if (currentuser.RoleID == 1)
                {
                    ViewBag.hide = false;
                    ViewBag.message = "Belum ada ND Permintaan Flash Report baru yang dikirim.";
                    return View();
                }

                ViewBag.hide = true;
                ViewBag.message = "Belum ada ND Permintaan Flash Report baru yang diterima.";
                return View();
            }

            ViewBag.YearND = nd.Tahun;
            ViewBag.PeriodND = nd.PeriodeID;

            RefPeriode periode = db.RefPeriode.Find(nd.PeriodeID);

            ViewBag.empty = false;
            ViewBag.nd = nd.ID;
            ViewBag.ndno = nd.NomorND;
            ViewBag.unitID = currentuser.UnitID;

            if (currentuser.RoleID == 1)
            {
                RefUnitPJ example = db.RefUnitPJ.Where(y => y.Aktif == true&&y.isPrimeMover==false).OrderBy(y => y.ID).FirstOrDefault();
                ViewBag.example = example.ID;
                ViewBag.hide = false;
                ViewBag.Title = "Rekap Hasil Permintaan Data Flash Report Periode " + periode.Ket + " " + nd.Tahun;
                return View(db.RefUnitPJ.Where(y => y.Aktif == true&&y.isPrimeMover == false).OrderBy(y => y.ID).ToList());
            }

            RefUnitPJ example2 = db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID&&y.isPrimeMover == false).OrderBy(y => y.ID).FirstOrDefault();
            ViewBag.example = example2.ID;
            ViewBag.hide = true;
            ViewBag.Title = "Permintaan Data Flash Report Periode " + periode.Ket + " " + nd.Tahun;
            return View(db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID && y.isPrimeMover == false).ToList());
        }

        public ActionResult loadND(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == 0)
            //{
            //    return RedirectToAction("Pending", null);
            //}

            TransNDPermintaanFlash nd = db.TransNDPermintaanFlash.Find(id);

            if (nd == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.YearND = nd.Tahun;
            ViewBag.PeriodND = nd.PeriodeID;

            RefPeriode periode = db.RefPeriode.Find(nd.PeriodeID);

            ViewBag.empty = false;
            ViewBag.nd = nd.ID;
            ViewBag.ndno = nd.NomorND;
            ViewBag.unitID = currentuser.UnitID;

            if (currentuser.RoleID == 1)
            {
                //RefUnitPJ example = db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false).OrderBy(y => y.ID).FirstOrDefault();
                //ViewBag.example = example.ID;
                ViewBag.hide = false;
                ViewBag.Title = "Rekap Hasil Permintaan Data Flash Report Periode " + periode.Ket + " " + nd.Tahun;
                return PartialView("_loadND", db.RefUnitPJ.Where(y => y.TransFlashKegiatan.Where(r=>r.TransFlashKegiatanProgress.Where(y1=>y1.Period==nd.PeriodeID&&y1.Tahun==nd.Tahun).Count()!=0).Count()!=0 && y.isPrimeMover == false).OrderBy(y => y.ID).ToList());
            }

            //RefUnitPJ example2 = db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID && y.isPrimeMover == false).OrderBy(y => y.ID).FirstOrDefault();
            //ViewBag.example = example2.ID;
            ViewBag.hide = true;
            ViewBag.Title = "Permintaan Data Flash Report Periode " + periode.Ket + " " + nd.Tahun;
            return PartialView("_loadND", db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID && y.isPrimeMover == false).ToList());
        }

        public ActionResult ListProgress(int id, int nd)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            TransNDPermintaanFlash ndFlash = db.TransNDPermintaanFlash.Find(nd);

            //if (tpu.TPUUnitPJID != currentuser.UnitID)
            //{
            //    return HttpNotFound();
            //}

            ViewBag.nd = nd;
            ViewBag.unitID = currentuser.UnitID;

            if (currentuser.RoleID == 1)
            {
                ViewBag.hide = true;
                return PartialView("_ListProgress", db.TransFlashProgress.Where(y => y.TransFlashKegiatan.UnitID == unit.ID && y.Period == ndFlash.PeriodeID && y.Tahun==ndFlash.Tahun).ToList());
            }

            ViewBag.hide = false;
            return PartialView("_ListProgress", db.TransFlashProgress.Where(y => y.TransFlashKegiatan.UnitID == unit.ID && y.Period == ndFlash.PeriodeID && y.Tahun == ndFlash.Tahun).ToList());
        }

        public ActionResult ListProgressHistory(int id, int nd)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            TransNDPermintaanFlash ndFlash = db.TransNDPermintaanFlash.Find(nd);

            //if (tpu.TPUUnitPJID != currentuser.UnitID)
            //{
            //    return HttpNotFound();
            //}

            ViewBag.nd = nd;
            ViewBag.unitID = currentuser.UnitID;

            if (currentuser.RoleID == 1)
            {
                ViewBag.hide = true;
                return PartialView("_ListProgressHistory", db.TransFlashProgress.Where(y => y.TransFlashKegiatan.UnitID == unit.ID && y.Period == ndFlash.PeriodeID && y.Tahun == ndFlash.Tahun).ToList());
            }

            ViewBag.hide = false;
            return PartialView("_ListProgressHistory", db.TransFlashProgress.Where(y => y.TransFlashKegiatan.UnitID == unit.ID && y.Period == ndFlash.PeriodeID && y.Tahun == ndFlash.Tahun).ToList());
        }

        public ActionResult Lock()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var nd in db.TransNDPermintaanFlash.Where(y => y.Locked == false))
            {
                nd.Locked = true;
            }
            db.SaveChanges();
            return RedirectToAction("Pending");
        }

        public ActionResult ProgressDetails(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatanProgress progress = db.TransFlashProgress.Find(id);

            if (currentuser.RoleID != 1 && currentuser.UnitID != progress.TransFlashKegiatan.UnitID)
            {
                return PartialView("_NotAuthorized");
            }

            return PartialView("_progressDetails", progress);
        }

        public ActionResult ProgressDetailsHistory(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatanProgress progress = db.TransFlashProgress.Find(id);

            if (currentuser.RoleID != 1 && currentuser.UnitID != progress.TransFlashKegiatan.UnitID)
            {
                return PartialView("_NotAuthorized");
            }

            TransNDPermintaanFlash nd = db.TransNDPermintaanFlash.Where(y => y.Tahun == progress.Tahun && y.PeriodeID == progress.Period).OrderByDescending(y => y.ID).FirstOrDefault();

            ViewBag.nd = nd.ID;

            return PartialView("_progressDetailsHistory", progress);
        }

        public ActionResult CallforReport()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (db.TransNDPermintaanFlash.Where(y => y.Locked == false).Count() != 0)
            {
                return RedirectToAction("Pending");
            }

            ViewBag.PeriodeID = new SelectList(db.RefPeriode.Where(y => y.TransNDPermintaanFlash.Where(y1 => y1.Tahun == DateTime.Now.Year).Count() == 0), "ID", "Ket");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CallforReport([Bind(Include = "ID,NomorND,TanggalND,PeriodeID")] CallforReport callforReport)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {

                RefPeriode period = db.RefPeriode.Find(callforReport.PeriodeID);

                TransNDPermintaanFlash nd = new TransNDPermintaanFlash
                {
                    TanggalND = callforReport.TanggalND,
                    NomorND = callforReport.NomorND,
                    Tahun = DateTime.Now.Year,
                    PeriodeID = callforReport.PeriodeID,
                    Locked = false,
                    SysUsername = User.Identity.GetUserName(),
                    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                    SysTglEntry = DateTime.Now
                };

                db.TransNDPermintaanFlash.Add(nd);
                db.SaveChanges();
                //foreach (var universe in db.RefUniverseAudit.Where(y => y.Aktif == true))
                //{
                //    TransIkhtisarProgress feed = new TransIkhtisarProgress
                //    {
                //        UniverseID = universe.ID,
                //        PKPTID = master.ID,
                //        PeriodeID = callforReport.PeriodeID,
                //        SysTglEntry = DateTime.Now
                //    };
                //    db.TransIkhtisarProgresses.Add(feed);
                //};
                //db.SaveChanges();
                foreach (var activity in db.TransFlashKegiatan.Where(y => y.Finalize!=3))
                {
                    TransFlashKegiatanProgress latestprogress = db.TransFlashProgress.Where(y => y.KegiatanID == activity.ID).OrderByDescending(y => y.Period).FirstOrDefault();

                    if (latestprogress == null)
                    {
                        TransFlashKegiatanProgress progress = new TransFlashKegiatanProgress
                        {
                            Period = callforReport.PeriodeID,
                            Tahun = DateTime.Now.Year,
                            KegiatanID = activity.ID,
                            KegStatusID = 1,
                            SysTglEntry = DateTime.Now
                        };
                        db.TransFlashProgress.Add(progress);
                        //db.SaveChanges();
                    }

                    //else if (activity.Finalize == 1)
                    //{
                    //    TransKegiatanProgress progress = new TransKegiatanProgress
                    //    {
                    //        Period = callforReport.PeriodeID,
                    //        KegiatanID = activity.ID,
                    //        KegStatusID = latestprogress.KegStatusID,
                    //        SysTglEntry = DateTime.Now,
                    //        Detail = "Kegiatan telah selesai dilaksanakan."
                    //    };
                    //    db.TransProgressKegiatan.Add(progress);
                    //    //db.SaveChanges();
                    //}

                    else
                    {
                        TransFlashKegiatanProgress progress = new TransFlashKegiatanProgress
                        {
                            Period = callforReport.PeriodeID,
                            Tahun = DateTime.Now.Year,
                            KegiatanID = activity.ID,
                            KegStatusID = latestprogress.KegStatusID,
                            SysTglEntry = DateTime.Now
                        };
                        db.TransFlashProgress.Add(progress);
                        //db.SaveChanges();
                    }
                    //db.TransProgressKegiatan.Add(progress);
                }
                db.SaveChanges();
                foreach (var unit in db.RefUnitPJ.Where(y => y.Aktif == true&&y.isPrimeMover==false))
                {
                    TransFlashNotifikasi notifikasi = new TransFlashNotifikasi
                    {
                        RouteID = null,
                        //body="test",
                        body = "Penyampaian Flash Report untuk periode " + period.Ket + " " + nd.Tahun + " telah dapat dilaksanakan sesuai Nota Dinas nomor " + callforReport.NomorND + ". Harap segera melakukan pengisian Flash Report.",
                        name = currentuser.FirstName + " " + currentuser.LastName,
                        Action = "Pending",
                        Controller = "Flash",
                        RoleID = 3,
                        Date = DateTime.Now,
                        NotifType = 8,
                        UnitID = unit.ID
                    };
                    db.TransFlashNotifikasi.Add(notifikasi);
                };
                db.SaveChanges();

                MailSender.FlashNotify(nd);

                ViewBag.PeriodeID = new SelectList(db.RefPeriode.Where(y => y.TransNDPermintaanFlash.Where(y1 => y1.Tahun==DateTime.Now.Year).Count() == 0), "ID", "Ket");
                return RedirectToAction("Pending");
            }

            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", callforReport.PeriodeID);
            return View(callforReport);
        }

        public ActionResult ND()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            //TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

            //var NDList = db.TransNDPermintaan.Where(y => y.PKPTID == master.ID && y.Locked == true).OrderByDescending(y => y.PeriodeID).ToList();

            var NDList = db.TransNDPermintaanFlash.Where(y => y.Locked == true).OrderByDescending(y => y.Tahun).ToList();

            return View(NDList);
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
