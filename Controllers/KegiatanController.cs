using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace ira.Models
{
    [Authorize]
    public class KegiatanController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Kegiatan
        //public ActionResult Index()
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    } 
            
        //    RefPeriode period = db.RefPeriode.Find(DateTime.Now.Month);
        //    if (period.Aktif == true)
        //    {
        //        ViewBag.highlightCount = db.TransHighlight.Where(y => y.Period == period.ID && y.Tahun == DateTime.Now.Year && y.RefKegiatan.RefTPU.TPUUnitPJID == currentuser.UnitID).Count();
        //        if (currentuser.RoleID != 1)
        //        {
        //            ViewBag.Periode = true;
        //            var refKegiatan = db.RefKegiatan.Include(r => r.RefPegawai).Include(r => r.RefTPU)
        //.Where(r => r.RefTPU.TransSchedule.Tahun == DateTime.Now.Year&&r.RefTPU.TPUUnitPJID==currentuser.UnitID);
        //            return View(refKegiatan.ToList());
        //        }
        //        ViewBag.Periode = true;
        //        var refKegiatan1a = db.RefKegiatan.Include(r => r.RefPegawai).Include(r => r.RefTPU)
        //        .Where(r => r.RefTPU.TransSchedule.Tahun == DateTime.Now.Year);
        //        return View(refKegiatan1a.ToList());
        //    }
        //    else
        //    {
        //        RefPeriode selectedperiod = db.RefPeriode.Find(period.ID + 1);
        //        ViewBag.highlightCount = db.TransHighlight.Where(y => y.Period == selectedperiod.ID && y.Tahun == DateTime.Now.Year && y.RefKegiatan.RefTPU.TPUUnitPJID == currentuser.UnitID).Count();
        //        if (currentuser.RoleID != 1)
        //        {
        //            ViewBag.Periode = false;
        //            var refKegiatan2 = db.RefKegiatan.Include(r => r.RefPegawai).Include(r => r.RefTPU)
        //                .Where(r => r.RefTPU.TransSchedule.Tahun == DateTime.Now.Year && r.RefTPU.TPUUnitPJID == currentuser.UnitID);
        //            return View(refKegiatan2.ToList());
        //        }
        //        ViewBag.Periode = false;
        //        var refKegiatan2a = db.RefKegiatan.Include(r => r.RefPegawai).Include(r => r.RefTPU)
        //            .Where(r => r.RefTPU.TransSchedule.Tahun == DateTime.Now.Year);
        //        return View(refKegiatan2a.ToList());
        //    }
        //}

        //public ActionResult Highlight(int id)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    } 
            
        //    RefKegiatan kegiatan = db.RefKegiatan.Find(id);
        //    if (kegiatan == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }

        //    if (currentuser.RoleID != 1 && currentuser.UnitID != kegiatan.RefTPU.TPUUnitPJID)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    RefPeriode recentperiod = db.RefPeriode.Find(DateTime.Now.Month);

        //    if (recentperiod.Aktif == true)
        //    {
        //        if (kegiatan.TransHighlight.Where(y => y.Period == recentperiod.ID && y.Tahun == DateTime.Now.Year).Count() != 0 || kegiatan.RefTPU.TransSchedule.Tahun != DateTime.Now.Year)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        var count1 = db.TransHighlight.Where(y => y.Period == recentperiod.ID && y.Tahun == DateTime.Now.Year && y.RefKegiatan.RefTPU.TPUUnitPJID == kegiatan.RefTPU.TPUUnitPJID).Count();

        //        if (count1 >= 5)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        TransHighlight hilite = new TransHighlight
        //        {
        //            Period = recentperiod.ID,
        //            Tahun = DateTime.Now.Year,
        //            KegiatanID = id,
        //            SysTglEntry = DateTime.Now,
        //            SysUsername = User.Identity.GetUserName(),
        //            SysWorkstation = Request.ServerVariables["REMOTE_HOST"]
        //        };
        //        db.TransHighlight.Add(hilite);
        //        db.SaveChanges();
        //        TransNotifikasi notifikasi = new TransNotifikasi
        //        {
        //            RouteID = kegiatan.ID,
        //            body = "Highlight untuk periode " +recentperiod.Ket+" "+hilite.Tahun+" Kegiatan berjudul "+kegiatan.KegName+".",
        //            name = currentuser.FirstName+" "+currentuser.LastName,
        //            Action = "Details",
        //            Controller = "Kegiatan",
        //            RoleID = 1,
        //            Date = DateTime.Now,
        //            NotifType = 6
        //        };
        //        db.TransNotifikasi.Add(notifikasi);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    RefPeriode selectedperiod = db.RefPeriode.Find(DateTime.Now.Month + 1);
        //    if (kegiatan.TransHighlight.Where(y => y.Period == selectedperiod.ID && y.Tahun == DateTime.Now.Year).Count() != 0 || kegiatan.RefTPU.TransSchedule.Tahun != DateTime.Now.Year)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    var count2 = db.TransHighlight.Where(y => y.Period == selectedperiod.ID && y.Tahun == DateTime.Now.Year && y.RefKegiatan.RefTPU.TPUUnitPJID == kegiatan.RefTPU.TPUUnitPJID).Count();

        //    if (count2 >= 5)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    TransHighlight hilite2 = new TransHighlight
        //    {
        //        Period = selectedperiod.ID,
        //        Tahun = DateTime.Now.Year,
        //        KegiatanID = id,
        //        SysTglEntry = DateTime.Now,
        //        SysUsername = User.Identity.GetUserName(),
        //        SysWorkstation = Request.ServerVariables["REMOTE_HOST"]
        //    };
        //    db.TransHighlight.Add(hilite2);
        //    db.SaveChanges();
        //    TransNotifikasi notifikasi2 = new TransNotifikasi
        //    {
        //        RouteID = kegiatan.ID,
        //        body = "Highlight untuk periode " + selectedperiod.Ket + " " + hilite2.Tahun + " Kegiatan berjudul " + kegiatan.KegName + ".",
        //        name = currentuser.FirstName+" "+currentuser.LastName,
        //        Action = "Details",
        //        Controller = "Kegiatan",
        //        RoleID = 1,
        //        Date = DateTime.Now,
        //        NotifType = 6
        //    };
        //    db.TransNotifikasi.Add(notifikasi2);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Unhighlight(int id)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    RefKegiatan kegiatan = db.RefKegiatan.Find(id);
        //    if (kegiatan == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }

        //    if (currentuser.RoleID != 1 && currentuser.UnitID != kegiatan.RefTPU.TPUUnitPJID)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    RefPeriode recentperiod = db.RefPeriode.Find(DateTime.Now.Month);

        //    //return RedirectToAction("Details", recentperiod.Aktif.ToString(), new { id = recentperiod.ID });

        //    if (recentperiod.Aktif == true)
        //    {

        //        if (kegiatan.TransHighlight.Where(y => y.Period == recentperiod.ID && y.Tahun == DateTime.Now.Year).Count() == 0 || kegiatan.RefTPU.TransSchedule.Tahun != DateTime.Now.Year)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        TransHighlight hilite = db.TransHighlight.Where(y => y.Period == recentperiod.ID && y.Tahun == DateTime.Now.Year && y.KegiatanID == kegiatan.ID)
        //            .OrderByDescending(y => y.ID).FirstOrDefault();
        //        db.TransHighlight.Remove(hilite);
        //        db.SaveChanges();

        //        TransNotifikasi notifikasi = new TransNotifikasi
        //        {
        //            RouteID = kegiatan.ID,
        //            body = "Telah dibatalkan Highlight atas Kegiatan berjudul " + kegiatan.KegName + "  untuk periode  " + recentperiod.Ket + " " + hilite.Tahun + ".",
        //            name = currentuser.FirstName+" "+currentuser.LastName,
        //            Action = "Details",
        //            Controller = "Kegiatan",
        //            RoleID = 1,
        //            Date = DateTime.Now,
        //            NotifType = 7
        //        };
        //        db.TransNotifikasi.Add(notifikasi);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    RefPeriode selectedperiod = db.RefPeriode.Find(DateTime.Now.Month + 1);
        //    if (kegiatan.TransHighlight.Where(y => y.Period == selectedperiod.ID && y.Tahun == DateTime.Now.Year).Count() == 0 || kegiatan.RefTPU.TransSchedule.Tahun != DateTime.Now.Year)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    TransHighlight hilite2 = db.TransHighlight.Where(y => y.Period == selectedperiod.ID && y.Tahun == DateTime.Now.Year && y.KegiatanID == kegiatan.ID)
        //        .OrderByDescending(y => y.ID).FirstOrDefault();
            
        //    //return RedirectToAction("Index", new { id = hilite2.Period });
        //    db.TransHighlight.Remove(hilite2);
        //    db.SaveChanges();
        //    TransNotifikasi notifikasi2 = new TransNotifikasi
        //    {
        //        RouteID = kegiatan.ID,
        //        body = "Telah dibatalkan Highlight atas Kegiatan berjudul " + kegiatan.KegName + "  untuk periode  " + recentperiod.Ket + " " + hilite2.Tahun + ".",
        //        name = currentuser.FirstName+" "+currentuser.LastName,
        //        Action = "Details",
        //        Controller = "Kegiatan",
        //        RoleID = 1,
        //        Date = DateTime.Now,
        //        NotifType = 7
        //    };
        //    db.TransNotifikasi.Add(notifikasi2);
        //    db.SaveChanges();
        //    //TransHighlight hilite = db.TransHighlight.Find(id);
        //    //db.TransHighlight.Remove(hilite);
        //    //db.SaveChanges();
        //    //TransNotifikasi notifikasi = new TransNotifikasi
        //    //{
        //    //    RouteID = kegiatan.ID,
        //    //    body = "Highlight untuk periode " + recentperiod.Ket + " " + hilite.Tahun + " Kegiatan berjudul " + kegiatan.KegName + ".",
        //    //    name = currentuser.FirstName+" "+currentuser.LastName,
        //    //    Action = "Details",
        //    //    Controller = "Kegiatan",
        //    //    RoleID = 1,
        //    //    Date = DateTime.Now,
        //    //    NotifType = 6
        //    //};
        //    //db.TransNotifikasi.Add(notifikasi);
        //    //db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // GET: Kegiatan/Details/5
        public ActionResult Details(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.user = User.Identity.GetUserName();
            RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
            
            if (refKegiatan == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            
            if (currentuser.RoleID != 1 && currentuser.UnitID != refKegiatan.RefTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            TransKegiatanProgress LastRecord = db.TransProgressKegiatan.Where(y => y.KegiatanID == id).OrderByDescending(y => y.Period).FirstOrDefault();
            RefPeriode latest = db.RefPeriode.OrderByDescending(y => y.ID).FirstOrDefault();
            if (LastRecord != null&&LastRecord.Period == latest.ID)
            {
                ViewBag.limit = true;
                ViewBag.OutputCount = refKegiatan.TransKegiatanOutput.Count();
                ViewBag.SysUsername = refKegiatan.SysUsername;
                ViewBag.SysTglEntry = refKegiatan.SysTglEntry;
                ViewBag.SysWorkStation = refKegiatan.SysWorkstation;
                ViewBag.KegName = refKegiatan.KegName;
                ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
                ViewBag.KegTarget = refKegiatan.KegTarget;
                ViewBag.KegMjrID = refKegiatan.KegMjrID;
                ViewBag.Keterangan = refKegiatan.Keterangan;
                return View(refKegiatan);
            }

            ViewBag.OutputCount = refKegiatan.TransKegiatanOutput.Count();
            ViewBag.SysUsername = refKegiatan.SysUsername;
            ViewBag.SysTglEntry = refKegiatan.SysTglEntry;
            ViewBag.SysWorkStation = refKegiatan.SysWorkstation;
            ViewBag.KegName = refKegiatan.KegName;
            ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            ViewBag.KegTarget = refKegiatan.KegTarget;
            ViewBag.KegMjrID = refKegiatan.KegMjrID;
            ViewBag.Keterangan = refKegiatan.Keterangan;
            return View(refKegiatan);
        }

        // GET: Kegiatan/Create
        public ActionResult Create(int id)
        {
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
            //    return RedirectToAction("NotFound", "ErrorPage", null);
            //}

                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            RefTPU tpu = db.RefTPU.Find(id);
            
            if (tpu == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.RoleID != 1 && currentuser.UnitID != tpu.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            if (tpu.Finalize == 1)
            {
                return RedirectToAction("Details", "TPU", new { id=tpu.ID });
            }

            ViewBag.tpu = tpu.TPUName;
            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket");
            ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y=>y.Aktif==true&&y.PegUnitID==tpu.TPUUnitPJID), "ID", "PegName");
            ViewBag.KegiatanTPUID =  id;
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
        public ActionResult Create([Bind(Include = "ID,KegName,KegiatanTPUID,KegMjrID,KegOutput,KegTarget,Output,Keterangan,SysUsername,SysTglEntry,SysWorkstation,PeriodeID")] RefKegiatan refKegiatan)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                RefTPU tpu = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
                if (tpu == null)
                {
                    return View(refKegiatan);
                }
                if (tpu.RefKegiatan.Count() == 0)
                {
                    db.RefKegiatan.Add(refKegiatan);
                    db.SaveChanges();
                    var Elevate = db.RefTPU.FirstOrDefault(y=>y.ID==refKegiatan.KegiatanTPUID);
                    Elevate.TPUStatusID = 2;
                    db.SaveChanges();
                    RefTPU tema = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
                    var judul = tema.TPUName;
                    RefTPUStatus statusTPU = db.RefStatusTPU.Find(2);
                    var status = statusTPU.Ket;
                    TransNotifikasi notifikasi = new TransNotifikasi
                    {
                        RouteID = refKegiatan.KegiatanTPUID,
                        body = "Perubahan status Tema Pengawasan menjadi " + status + " untuk Tema Pengawasan berjudul " + judul + "...",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Details",
                        Controller = "TPU",
                        RoleID = 1,
                        Date = DateTime.Now,
                        NotifType = 3
                    };
                    db.TransNotifikasi.Add(notifikasi);
                    db.SaveChanges();

                    if (db.TransNDPermintaan.Where(y => y.PKPTID == tema.PKPTID && y.Locked == false).Count() != 0)
                    {
                        TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PKPTID == tema.PKPTID && y.Locked == false).OrderByDescending(y => y.PeriodeID).FirstOrDefault();
                        TransKegiatanProgress progress = new TransKegiatanProgress
                        {
                            Period = nd.PeriodeID,
                            KegiatanID = refKegiatan.ID,
                            KegStatusID = 2,
                            SysUsername = User.Identity.GetUserName(),
                            SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                            SysTglEntry = DateTime.Now
                        };
                        db.TransProgressKegiatan.Add(progress);
                        db.SaveChanges();
                        return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });
                    }

                    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });
                }
                db.RefKegiatan.Add(refKegiatan);
                db.SaveChanges();
                RefTPU temapengawasan = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
                if (db.TransNDPermintaan.Where(y => y.PKPTID == temapengawasan.PKPTID && y.Locked == false).Count() != 0)
                {
                    TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PKPTID == temapengawasan.PKPTID && y.Locked == false).OrderByDescending(y => y.PeriodeID).FirstOrDefault();
                    TransKegiatanProgress progress = new TransKegiatanProgress
                    {
                        Period = nd.PeriodeID,
                        KegiatanID = refKegiatan.ID,
                        KegStatusID = 2,
                        SysUsername = User.Identity.GetUserName(),
                        SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                        SysTglEntry = DateTime.Now
                    };
                    db.TransProgressKegiatan.Add(progress);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });
                }

                return RedirectToAction("Details", "Kegiatan", new { id=refKegiatan.ID});
            }
            RefTPU Tpu = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
            ViewBag.tpu = Tpu.TPUName;
            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
            ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID), "ID", "PegName", refKegiatan.KegMjrID);
            ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(refKegiatan);
        }

        // GET: Kegiatan/Edit/5
        public ActionResult Edit(int? id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
            if (refKegiatan == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.RoleID != 1 && currentuser.UnitID != refKegiatan.RefTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            if (refKegiatan.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
            ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => (y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID) || y.ID == refKegiatan.KegMjrID), "ID", "PegName", refKegiatan.KegMjrID);
            ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(refKegiatan);
        }

        // POST: Kegiatan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KegName,KegiatanTPUID,KegMjrID,KegTarget,Keterangan,SysUsername,SysTglEntry,SysWorkstation,Finalize,PeriodeID")] RefKegiatan refKegiatan)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                //db.Entry(refKegiatan).State = EntityState.Modified;
                //db.SaveChanges();
                var dbKeg = db.RefKegiatan.FirstOrDefault(p => p.ID == refKegiatan.ID);
                if (dbKeg == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                dbKeg.KegMjrID = refKegiatan.KegMjrID;
                dbKeg.KegName = refKegiatan.KegName;
                dbKeg.KegTarget = refKegiatan.KegTarget;
                dbKeg.PeriodeID = refKegiatan.PeriodeID;
                dbKeg.Keterangan = refKegiatan.Keterangan;
                dbKeg.SysUsername = User.Identity.GetUserName();
                dbKeg.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbKeg.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", new { id=refKegiatan.ID });
            }

            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
            ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => (y.Aktif == true && y.PegUnitID == refKegiatan.RefTPU.TPUUnitPJID) || y.ID == refKegiatan.KegMjrID), "ID", "PegName", refKegiatan.KegMjrID);
            ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(refKegiatan);
        }

                // GET: Komentar/Create
        public ActionResult Komentar(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            RefKegiatan kegiatan = db.RefKegiatan.Find(id);
            
            if (currentuser.RoleID != 1 && currentuser.UnitID != kegiatan.RefTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

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
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

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
                TransNotifikasi notifikasi = new TransNotifikasi
                {
                    RouteID = transKegiatanKomentar.KomenKegID,
                    body = "Memberikan komentar pada kegiatan berjudul " + judul + "...",
                    name = currentuser.FirstName+" "+currentuser.LastName,
                    Action = "Details",
                    Controller = "Kegiatan",
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
                return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanKomentar.KomenKegID });
                //                return PartialView("_KomentarCreatePartial", komentar);
            }
            return View(transKegiatanKomentar);
        }
        //public ActionResult Finalize(int id)
        //{
            
        //    RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
        //    if (refKegiatan == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
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
        public ActionResult Finalize([Bind(Include = "ID,KegName,KegiatanTPUID,KegMjrID,KegTarget,Keterangan,SysUsername,SysTglEntry,SysWorkstation,Finalize")] RefKegiatan refKegiatan)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if (refKegiatan.Finalize == 1)
                {
                    //db.Entry(refKegiatan).State = EntityState.Modified;
                    var dbKeg = db.RefKegiatan.FirstOrDefault(p => p.ID == refKegiatan.ID);
                    if (dbKeg == null)
                    {
                        return RedirectToAction("NotFound", "ErrorPage", null);
                    }
                    if (currentuser.RoleID != 1 && currentuser.UnitID != dbKeg.RefTPU.TPUUnitPJID)
                    {
                        return RedirectToAction("Index");
                    }
                    dbKeg.Finalize = refKegiatan.Finalize;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Kegiatan", new { refKegiatan.ID });
                }

                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });
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

        // GET: Kegiatan/Delete/5
        public ActionResult Delete(int? id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
            if (refKegiatan == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.RoleID != 1 && currentuser.UnitID != refKegiatan.RefTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            if (refKegiatan.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(refKegiatan);
        }

        // POST: Kegiatan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
            if (currentuser.RoleID != 1 && currentuser.UnitID != refKegiatan.RefTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }
            db.RefKegiatan.Remove(refKegiatan);
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
