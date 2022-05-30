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
using System.Web.Security;

namespace ira.Controllers
{
    [Authorize]
    public class ProgressController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Progress
        public ActionResult Matriks()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentuser.RoleID == 1)
            {
                var refTPU = db.RefTPU.Include(r => r.RefEselon1).Include(r => r.RefPegawai).Include(r => r.RefQuarter).Include(r => r.RefTPUJenis).Include(r => r.RefTPUStatus).Include(r => r.RefUnitPJ)
                .Where(r => r.TransSchedule.Tahun == DateTime.Now.Year)
                .OrderBy(r => r.No);
                return View(refTPU.ToList());
            }

            else
            {
                var refTPU = db.RefTPU.Include(r => r.RefEselon1).Include(r => r.RefPegawai).Include(r => r.RefQuarter).Include(r => r.RefTPUJenis).Include(r => r.RefTPUStatus).Include(r => r.RefUnitPJ)
                .Where(r => r.TransSchedule.Tahun == DateTime.Now.Year&&r.TPUUnitPJID==currentuser.UnitID)
                .OrderBy(r => r.No);
                return View(refTPU.ToList());
            }
        }

        public ActionResult loadIkhtisar(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            return PartialView("_loadIkhtisar",ikhtisar);
        }

        public ActionResult loadIkhtisarHistory(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == ikhtisar.PeriodeID && y.PKPTID == ikhtisar.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            ViewBag.nd = nd.ID;

            return PartialView("_loadIkhtisarHistory", ikhtisar);
        }

        public ActionResult loadSubmit(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == ikhtisar.PeriodeID && y.PKPTID == ikhtisar.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd.Locked == true)
            {
                return PartialView("_NotFound");
            }

            return PartialView("_loadSubmit", ikhtisar);
        }

        public ActionResult loadReview(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == ikhtisar.PeriodeID && y.PKPTID == ikhtisar.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd.Locked==true)
            {
                return PartialView("_NotFound");
            }

            return PartialView("_loadReview", ikhtisar);
        }

        public ActionResult Lock()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var nd in db.TransNDPermintaan.Where(y => y.Locked == false))
            {
                nd.Locked = true;
            }
            db.SaveChanges();
            return RedirectToAction("Pending");
        }

        public ActionResult AddKegiatan(int id)
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
                return RedirectToAction("Details", "TPU", new { id = tpu.ID });
            }

            ViewBag.tpu = tpu.TPUName;

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();
            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PKPTID == master.ID && y.Locked == false).OrderByDescending(y => y.PeriodeID).FirstOrDefault();
            //TransNDPermintaan nd = db.TransNDPermintaan.Find(id);

            if (nd == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (nd.Locked == true)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            ViewBag.ND = nd.ID;
            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket");
            ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == currentuser.UnitID), "ID", "PegName");
            ViewBag.KegiatanTPUID = tpu.ID;
            //ViewBag.KegiatanTPUID = new SelectList(db.RefTPU.Where(y=>y.TransSchedule.Locked == 0 && y.Finalize != 1&&y.TPUUnitPJID==currentuser.UnitID), "ID", "TPUName");
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
        public ActionResult AddKegiatan([Bind(Include = "ID,KegName,KegiatanTPUID,KegMjrID,KegOutput,KegTarget,Output,Keterangan,SysUsername,SysTglEntry,SysWorkstation,PeriodeID,ND")] AddKegiatan refKegiatan)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                TransNDPermintaan nd = db.TransNDPermintaan.Find(refKegiatan.ND);

                if (nd == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }

                if (nd.Locked == true)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }

                RefTPU tpu = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
                if (tpu == null)
                {
                    return View(refKegiatan);
                }

                if (tpu.TPUUnitPJID != currentuser.UnitID)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }

                if (tpu.RefKegiatan.Count() == 0)
                {
                    RefKegiatan dataKegiatan = new RefKegiatan
                    {
                        KegName = refKegiatan.KegName,
                        KegiatanTPUID = refKegiatan.KegiatanTPUID,
                        KegMjrID = refKegiatan.KegMjrID,
                        PeriodeID = refKegiatan.PeriodeID,
                        KegTarget = refKegiatan.KegTarget,
                        Keterangan = refKegiatan.Keterangan,
                        SysUsername = User.Identity.GetUserName(),
                        SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                        SysTglEntry = DateTime.Now
                    };
                    db.RefKegiatan.Add(dataKegiatan);
                    db.SaveChanges();
                    var Elevate = db.RefTPU.FirstOrDefault(y => y.ID == refKegiatan.KegiatanTPUID);
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
                    TransKegiatanProgress progress = new TransKegiatanProgress
                    {
                        Period = nd.PeriodeID,
                        KegiatanID = dataKegiatan.ID,
                        KegStatusID = 2,
                        SysUsername = User.Identity.GetUserName(),
                        SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                        SysTglEntry = DateTime.Now
                    };
                    db.TransProgressKegiatan.Add(progress);
                    db.SaveChanges();
                    return RedirectToAction("Pending", "Progress", null);
                }
                RefKegiatan kegiatan = new RefKegiatan
                {
                    KegName = refKegiatan.KegName,
                    KegiatanTPUID = refKegiatan.KegiatanTPUID,
                    KegMjrID = refKegiatan.KegMjrID,
                    PeriodeID = refKegiatan.PeriodeID,
                    KegTarget = refKegiatan.KegTarget,
                    Keterangan = refKegiatan.Keterangan,
                    SysUsername = User.Identity.GetUserName(),
                    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                    SysTglEntry = DateTime.Now
                };
                db.RefKegiatan.Add(kegiatan);
                db.SaveChanges();
                TransKegiatanProgress dataProgress = new TransKegiatanProgress
                {
                    Period = nd.PeriodeID,
                    KegiatanID = kegiatan.ID,
                    KegStatusID = 2,
                    SysUsername = User.Identity.GetUserName(),
                    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                    SysTglEntry = DateTime.Now
                };
                db.TransProgressKegiatan.Add(dataProgress);
                db.SaveChanges();
                return RedirectToAction("Pending", "Progress", null);
            }
            RefTPU Tpu = db.RefTPU.Find(refKegiatan.KegiatanTPUID);
            ViewBag.tpu = Tpu.TPUName;
            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", refKegiatan.PeriodeID);
            ViewBag.KegMjrID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == currentuser.UnitID), "ID", "PegName", refKegiatan.KegMjrID);
            ViewBag.KegiatanTPUID = refKegiatan.KegiatanTPUID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(refKegiatan);
        }

        public ActionResult ProgressDetails(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransKegiatanProgress progress = db.TransProgressKegiatan.Find(id);

            if (currentuser.RoleID != 1 && currentuser.UnitID != progress.RefKegiatan.RefTPU.TPUUnitPJID)
            {
                return PartialView("_NotAuthorized");
            }

            return PartialView("_progressDetails", progress);
        }

        public ActionResult ProgressDetailsHistory(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransKegiatanProgress progress = db.TransProgressKegiatan.Find(id);

            if (currentuser.RoleID != 1 && currentuser.UnitID != progress.RefKegiatan.RefTPU.TPUUnitPJID)
            {
                return PartialView("_NotAuthorized");
            }

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == progress.Period && y.PKPTID == progress.RefKegiatan.RefTPU.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            ViewBag.nd = nd.ID;

            return PartialView("_progressDetailsHistory", progress);
        }

        public ActionResult ListProgress(int id, int nd, int tema)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            TransNDPermintaan ndProgress = db.TransNDPermintaan.Find(nd);

            RefTPU tpu = db.RefTPU.Find(tema);
            
            //if (tpu.TPUUnitPJID != currentuser.UnitID)
            //{
            //    return RedirectToAction("NotFound", "ErrorPage", null);
            //}

            ViewBag.nd = nd;

            ViewBag.tema = tema;

            if (currentuser.RoleID == 1)
            {
                ViewBag.hide = true;
                return PartialView("_ListProgress", db.TransProgressKegiatan.Where(y => y.RefKegiatan.RefTPU.TPUUnitPJID == unit.ID && y.Period == ndProgress.PeriodeID && y.RefKegiatan.KegiatanTPUID == tpu.ID).ToList());
            }

            ViewBag.hide = false;
            return PartialView("_ListProgress", db.TransProgressKegiatan.Where(y=>y.RefKegiatan.RefTPU.TPUUnitPJID==unit.ID&&y.Period==ndProgress.PeriodeID&&y.RefKegiatan.KegiatanTPUID==tpu.ID).ToList());
        }

        public ActionResult ListProgressHistory(int id, int nd, int tema)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            TransNDPermintaan ndProgress = db.TransNDPermintaan.Find(nd);

            RefTPU tpu = db.RefTPU.Find(tema);

            //if (tpu.TPUUnitPJID != currentuser.UnitID)
            //{
            //    return RedirectToAction("NotFound", "ErrorPage", null);
            //}

            ViewBag.nd = nd;

            ViewBag.tema = tema;

            if (currentuser.RoleID == 1)
            {
                ViewBag.hide = true;
                return PartialView("_ListProgressHistory", db.TransProgressKegiatan.Where(y => y.RefKegiatan.RefTPU.TPUUnitPJID == unit.ID && y.Period == ndProgress.PeriodeID && y.RefKegiatan.KegiatanTPUID == tpu.ID).ToList());
            }

            ViewBag.hide = false;
            return PartialView("_ListProgressHistory", db.TransProgressKegiatan.Where(y => y.RefKegiatan.RefTPU.TPUUnitPJID == unit.ID && y.Period == ndProgress.PeriodeID && y.RefKegiatan.KegiatanTPUID == tpu.ID).ToList());
        }

        public ActionResult Summarize(int id, int nd)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            //if (unit.ID != currentuser.UnitID)
            //{
            //    return RedirectToAction("NotFound", "ErrorPage", null);
            //}

            TransNDPermintaan ndProgress = db.TransNDPermintaan.Find(nd);

            var kegiatan = db.TransProgressKegiatan.Where(y => y.RefKegiatan.RefTPU.TPUUnitPJID == unit.ID && y.Period == ndProgress.PeriodeID);

            var objekPengawasan = db.TransIkhtisarProgresses.Where(y => y.PeriodeID == ndProgress.PeriodeID && y.RefUniverseAudit.UnitID == unit.ID);

            ViewBag.isPrimeMover = unit.isPrimeMover;
            ViewBag.jumlahKegiatan = kegiatan.Count();
            ViewBag.jumlahObjekPengawasan = objekPengawasan.Count();
            ViewBag.progressKegiatanInputted = kegiatan.Where(y => y.Detail != null).Count();
            ViewBag.ikhtisarInputted = objekPengawasan.Where(y => y.Accepted == true).Count();
            ViewBag.kegiatanIsGreen = ViewBag.jumlahKegiatan == ViewBag.progressKegiatanInputted && ViewBag.jumlahKegiatan != 0;
            ViewBag.objekPengawasanIsGreen = ViewBag.jumlahObjekPengawasan == ViewBag.ikhtisarInputted && ViewBag.jumlahObjekPengawasan != 0;

            return PartialView("_Summarize");
        }

        public ActionResult ListIkhtisar(int id, int nd)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            TransNDPermintaan ndProgress = db.TransNDPermintaan.Find(nd);

            ViewBag.unitID = unit.ID;

            ViewBag.hide = currentuser.RoleID;

            return PartialView("_ListIkhtisar", db.TransIkhtisarProgresses.Where(y=>y.PeriodeID==ndProgress.PeriodeID&&y.RefUniverseAudit.UnitID==unit.ID).ToList());
        }

        public ActionResult ListIkhtisarHistory(int id, int nd)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            TransNDPermintaan ndProgress = db.TransNDPermintaan.Find(nd);

            ViewBag.unitID = unit.ID;

            ViewBag.hide = currentuser.RoleID;

            return PartialView("_ListIkhtisarHistory", db.TransIkhtisarProgresses.Where(y => y.PeriodeID == ndProgress.PeriodeID && y.RefUniverseAudit.UnitID == unit.ID).ToList());
        }

        public ActionResult ND()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            //TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

            //var NDList = db.TransNDPermintaan.Where(y => y.PKPTID == master.ID && y.Locked == true).OrderByDescending(y => y.PeriodeID).ToList();

            var NDList = db.TransNDPermintaan.Where(y =>y.Locked==true).OrderByDescending(y => y.PeriodeID).ToList();

            return View(NDList);
        }

        public ActionResult loadND(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == 0)
            //{
            //    return RedirectToAction("Pending", null);
            //}

            TransNDPermintaan nd = db.TransNDPermintaan.Find(id);

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

            ViewBag.masterID = master.ID;

            RefPeriode periode = db.RefPeriode.Find(nd.PeriodeID);

            ViewBag.empty = false;
            ViewBag.nd = nd.ID;
            ViewBag.ndno = nd.NomorND;

            if (currentuser.RoleID == 1)
            {
                //RefUnitPJ example = db.RefUnitPJ.Where(y => y.RefTPU.Where(r => r.PKPTID == master.ID && r.RefKegiatan.Where(y1 => y1.TransKegiatanProgress.Where(r1 => r1.Period == nd.PeriodeID).Count() != 0).Count() != 0).Count() != 0 || y.RefUniverseAudit.Where(r => r.TransIkhtisarProgress.Where(y1 => y1.PeriodeID == nd.PeriodeID && y1.PKPTID == master.ID).Count() != 0).Count() != 0).OrderBy(y => y.ID).FirstOrDefault();
                //ViewBag.example = example.ID;
                ViewBag.hide = false;
                return PartialView("_loadND", db.RefUnitPJ.Where(y => y.RefTPU.Where(r => r.PKPTID == master.ID && r.RefKegiatan.Where(y1 => y1.TransKegiatanProgress.Where(r1 => r1.Period == nd.PeriodeID).Count() != 0).Count() != 0).Count() != 0 || y.RefUniverseAudit.Where(r => r.TransIkhtisarProgress.Where(y1 => y1.PeriodeID == nd.PeriodeID && y1.PKPTID == master.ID).Count() != 0).Count() != 0).OrderBy(y => y.ID).ToList());
            }

            //RefUnitPJ example2 = db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID).OrderBy(y => y.ID).FirstOrDefault();
            //ViewBag.example = example2.ID;
            ViewBag.hide = true;
            return PartialView("_loadND", db.RefUnitPJ.Where(y => y.ID == currentuser.UnitID && (y.RefTPU.Where(r => r.PKPTID == master.ID && r.RefKegiatan.Where(y1 => y1.TransKegiatanProgress.Where(r1 => r1.Period == nd.PeriodeID).Count() != 0).Count() != 0).Count() != 0 || y.RefUniverseAudit.Where(r => r.TransIkhtisarProgress.Where(y1 => y1.PeriodeID == nd.PeriodeID && y1.PKPTID == master.ID).Count() != 0).Count() != 0)).ToList());
        }

        public ActionResult Pending(int?id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == 0)
            //{
            //    return RedirectToAction("Pending", null);
            //}

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

            if (master == null)
            {
                ViewBag.Title = "Permintaan Data Progress Report";
                ViewBag.empty = true;
                ViewBag.kirimNDPermintaanEnabled = false;

                if (currentuser.RoleID == 1)
                {
                    ViewBag.hide = false;
                    ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang dikirim.";
                    return View();
                }

                ViewBag.hide = true;
                ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang diterima.";
                return View();
            }

            ViewBag.kirimNDPermintaanEnabled = true;

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PKPTID == master.ID && y.Locked == false).OrderByDescending(y => y.PeriodeID).FirstOrDefault();

            ViewBag.masterID = master.ID;

            if (nd == null)
            {
                ViewBag.Title = "Permintaan Data Progress Report";
                ViewBag.empty = true;

                if (currentuser.RoleID == 1)
                {
                    ViewBag.hide = false;
                    ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang dikirim.";
                    return View();
                }

                ViewBag.hide = true;
                ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang diterima.";
                return View();
            }

            RefPeriode periode = db.RefPeriode.Find(nd.PeriodeID);

            ViewBag.empty = false;
            ViewBag.nd = nd.ID;
            ViewBag.ndno = nd.NomorND;

            if (currentuser.RoleID == 1)
            {
                RefUnitPJ example = db.RefUnitPJ.Where(y => y.Aktif == true).OrderBy(y => y.ID).FirstOrDefault();
                ViewBag.example = example.ID;
                ViewBag.hide = false;
                ViewBag.Title = "Rekap Hasil Permintaan Data Progress Report Periode " + periode.Ket + " " + nd.TransSchedule.Tahun;
                return View(db.RefUnitPJ.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList());
            }

            RefUnitPJ example2 = db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID).OrderBy(y => y.ID).FirstOrDefault();
            ViewBag.example = example2.ID;
            ViewBag.hide = true;
            ViewBag.Title = "Permintaan Data Progress Report Periode " + periode.Ket + " " + nd.TransSchedule.Tahun;
            return View(db.RefUnitPJ.Where(y => y.Aktif == true&&y.ID==currentuser.UnitID).ToList());
        }

        public ActionResult CallforReport()
        {
            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (db.TransNDPermintaan.Where(y => y.Locked == false).Count() != 0)
            {
                return RedirectToAction("Pending");
            }

            ViewBag.PeriodeID = new SelectList(db.RefPeriode.Where(y=>y.TransNDPermintaan.Where(y1=>y1.PKPTID==master.ID).Count()==0), "ID", "Ket");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CallforReport([Bind(Include = "ID,NomorND,TanggalND,PeriodeID")] CallforReport callforReport)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

                RefPeriode period = db.RefPeriode.Find(callforReport.PeriodeID);

                TransNDPermintaan nd = new TransNDPermintaan
                {
                    TanggalND = callforReport.TanggalND,
                    NomorND = callforReport.NomorND,
                    PKPTID = master.ID,
                    PeriodeID = callforReport.PeriodeID,
                    Locked = false,
                    SysUsername = User.Identity.GetUserName(),
                    SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                    SysTglEntry = DateTime.Now
                };

                db.TransNDPermintaan.Add(nd);
                db.SaveChanges();
                foreach (var universe in db.RefUniverseAudit.Where(y => y.Aktif == true))
                {
                    TransIkhtisarProgress feed = new TransIkhtisarProgress
                    {
                        UniverseID = universe.ID,
                        PKPTID = master.ID,
                        PeriodeID = callforReport.PeriodeID,
                        SysTglEntry = DateTime.Now
                    };
                    db.TransIkhtisarProgresses.Add(feed);
                };
                db.SaveChanges();
                foreach (var activity in db.RefKegiatan.Where(y => y.RefTPU.PKPTID == master.ID))
                {
                    TransKegiatanProgress latestprogress = db.TransProgressKegiatan.Where(y => y.KegiatanID == activity.ID).OrderByDescending(y=>y.Period).FirstOrDefault();

                    if (latestprogress == null)
                    {
                        TransKegiatanProgress progress = new TransKegiatanProgress
                        {
                            Period = callforReport.PeriodeID,
                            KegiatanID = activity.ID,
                            KegStatusID = 2,
                            SysTglEntry = DateTime.Now
                        };
                        db.TransProgressKegiatan.Add(progress);
                        //db.SaveChanges();
                    }

                    else if (activity.Finalize == 1)
                    {
                        TransKegiatanProgress progress = new TransKegiatanProgress
                        {
                            Period = callforReport.PeriodeID,
                            KegiatanID = activity.ID,
                            KegStatusID = latestprogress.KegStatusID,
                            SysTglEntry = DateTime.Now,
                            Detail = "Kegiatan telah selesai dilaksanakan."
                        };
                        db.TransProgressKegiatan.Add(progress);
                        //db.SaveChanges();
                    }

                    else
                    {
                        TransKegiatanProgress progress = new TransKegiatanProgress
                        {
                            Period = callforReport.PeriodeID,
                            KegiatanID = activity.ID,
                            KegStatusID = latestprogress.KegStatusID,
                            SysTglEntry = DateTime.Now
                        };
                        db.TransProgressKegiatan.Add(progress);
                        //db.SaveChanges();
                    }
                    //db.TransProgressKegiatan.Add(progress);
                }
                db.SaveChanges();
                foreach (var unit in db.RefUnitPJ.Where(y => y.Aktif == true))
                {
                    TransNotifikasi notifikasi = new TransNotifikasi
                    {
                        RouteID = null,
                        //body="test",
                        body = "Penyampaian Progress Report untuk periode " + period.Ket + " " + master.Tahun + " telah dapat dilaksanakan sesuai Nota Dinas nomor " + callforReport.NomorND + ". Harap segera melakukan pengisian Progress Report.",
                        name = currentuser.FirstName+" "+currentuser.LastName,
                        Action = "Pending",
                        Controller = "Progress",
                        RoleID = 2,
                        Date = DateTime.Now,
                        NotifType = 8,
                        UnitID = unit.ID
                    };
                    db.TransNotifikasi.Add(notifikasi);
                };
                db.SaveChanges();

                MailSender.ProgressNotify(nd);

                ViewBag.PeriodeID = new SelectList(db.RefPeriode.Where(y => y.TransNDPermintaan.Where(y1 => y1.PKPTID == master.ID).Count() == 0), "ID", "Ket");
                return RedirectToAction("Pending");
            }

            ViewBag.PeriodeID = new SelectList(db.RefPeriode, "ID", "Ket", callforReport.PeriodeID);
            return View(callforReport);
        }

        public ActionResult TPU(int id, int period)
        {
            //var tpu = db.RefTPU.Find(id);

            //var progress = db.TransProgressKegiatan
            //    .Where(y => y.RefKegiatan.RefTPU.ID == id && y.Period <= period)
            //    .OrderByDescending(y => y.Period).Take(2)
            //    .Select(r => new {
            //        progress = r.Detail,
            //        tahun = r.RefKegiatan.RefTPU.TransSchedule.Tahun,
            //        bulan = r.RefPeriode.Ket,
            //        kegiatan = r.RefKegiatan.KegName
            //    });


            //return Json(progress,JsonRequestBehavior.AllowGet);

                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 

            RefTPU refTPU = db.RefTPU.Find(id);
            if (refTPU == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.RoleID != 1)
            {
                if (currentuser.UnitID != refTPU.TPUUnitPJID)
                {
                    return RedirectToAction("Login", "Account", null);
                }

                if (refTPU.RefKegiatan.Count() == 0)
                {
                    ViewBag.msg = "Belum ada kegiatan.";
                    return View(refTPU);
                }
                RefPeriode earliest = db.RefPeriode.Where(y => y.Aktif == true).OrderBy(y => y.ID).FirstOrDefault();
                if (period == earliest.ID)
                {
                    RefPeriode pickedperiod = db.RefPeriode.Find(period);
                    ViewBag.periodID = pickedperiod.ID;
                    ViewBag.thisperiod = pickedperiod.Ket;
                    ViewBag.ReportCount = db.TransKegiatanOutput.Where(r => r.OutputJenisID == 1 && r.RefKegiatan.KegiatanTPUID == refTPU.ID).Count();
                    return View(refTPU);
                }
                RefPeriode selected = db.RefPeriode.Find(period);
                RefPeriode previous = db.RefPeriode.Find(period - 2);
                ViewBag.periodID = selected.ID;
                ViewBag.thisperiod = selected.Ket;
                ViewBag.prevperiod = previous.Ket;
                ViewBag.ReportCount = db.TransKegiatanOutput.Where(r => r.OutputJenisID == 1 && r.RefKegiatan.KegiatanTPUID == refTPU.ID).Count();
                return View(refTPU);
            }

            else
            {
                if (refTPU.RefKegiatan.Count() == 0)
                {
                    ViewBag.msg = "Belum ada kegiatan.";
                    return View(refTPU);
                }
                RefPeriode earliest = db.RefPeriode.Where(y => y.Aktif == true).OrderBy(y => y.ID).FirstOrDefault();
                if (period == earliest.ID)
                {
                    RefPeriode pickedperiod = db.RefPeriode.Find(period);
                    ViewBag.periodID = pickedperiod.ID;
                    ViewBag.thisperiod = pickedperiod.Ket;
                    ViewBag.ReportCount = db.TransKegiatanOutput.Where(r => r.OutputJenisID == 1 && r.RefKegiatan.KegiatanTPUID == refTPU.ID).Count();
                    return View(refTPU);
                }
                RefPeriode selected = db.RefPeriode.Find(period);
                RefPeriode previous = db.RefPeriode.Find(period - 2);
                ViewBag.periodID = selected.ID;
                ViewBag.thisperiod = selected.Ket;
                ViewBag.prevperiod = previous.Ket;
                ViewBag.ReportCount = db.TransKegiatanOutput.Where(r => r.OutputJenisID == 1 && r.RefKegiatan.KegiatanTPUID == refTPU.ID).Count();
                return View(refTPU);
            }
        }

        //public ActionResult Index()
        //{
        //    var transProgressKegiatan = db.TransProgressKegiatan.Include(t => t.RefKegiatan).Include(t => t.RefKegiatanStatus).Include(t => t.RefPeriode);
        //    return View(transProgressKegiatan.ToList());
        //}

        //// GET: Progress/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransKegiatanProgress transKegiatanProgress = db.TransProgressKegiatan.Find(id);
        //    if (transKegiatanProgress == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transKegiatanProgress);
        //}

        public ActionResult Entri(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransKegiatanProgress transKegiatanProgress = db.TransProgressKegiatan.Find(id);
            if (transKegiatanProgress == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PKPTID == transKegiatanProgress.RefKegiatan.RefTPU.PKPTID && y.PeriodeID == transKegiatanProgress.Period).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd.Locked == true)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (transKegiatanProgress.RefKegiatan.RefTPU.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.keg = transKegiatanProgress.RefKegiatan.KegName;
            ViewBag.month = transKegiatanProgress.RefPeriode.Ket;
            ViewBag.year = transKegiatanProgress.RefKegiatan.RefTPU.TransSchedule.Tahun;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            ViewBag.KegiatanID = transKegiatanProgress.KegiatanID;
            ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket", transKegiatanProgress.KegStatusID);
            ViewBag.Period = transKegiatanProgress.Period;
            return View(transKegiatanProgress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entri([Bind(Include = "ID,Detail,Period,KegiatanID,KegStatusID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanProgress transKegiatanProgress)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(transKegiatanProgress).State = EntityState.Modified;
                var dbTarget = db.TransProgressKegiatan.Find(transKegiatanProgress.ID);
                dbTarget.KegStatusID = transKegiatanProgress.KegStatusID;
                dbTarget.Detail = transKegiatanProgress.Detail;
                dbTarget.SysUsername = User.Identity.GetUserName();
                dbTarget.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbTarget.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Pending", "Progress", null);
            }
            ViewBag.keg = transKegiatanProgress.RefKegiatan.KegName;
            ViewBag.month = transKegiatanProgress.RefPeriode.Ket;
            ViewBag.year = transKegiatanProgress.RefKegiatan.RefTPU.TransSchedule.Tahun;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            ViewBag.KegiatanID = transKegiatanProgress.KegiatanID;
            ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket", transKegiatanProgress.KegStatusID);
            ViewBag.Period = transKegiatanProgress.Period;
            return View(transKegiatanProgress);
        }

        //// GET: Progress/Create
        //public ActionResult Create(int id)
        //{
        //    //if (!id.HasValue)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}

        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    } 
            
        //    RefKegiatan refKegiatan = db.RefKegiatan.Find(id);
        //    if (refKegiatan == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    if (refKegiatan.RefTPU.Finalize == 1|| refKegiatan.Finalize==1)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    TransKegiatanProgress LastRecord = db.TransProgressKegiatan.Where(y => y.KegiatanID == id).OrderByDescending(y => y.Period).FirstOrDefault();

        //    if (LastRecord != null)
        //    {
        //        RefPeriode latest = db.RefPeriode.OrderByDescending(y => y.ID).FirstOrDefault();
        //        if (LastRecord.Period == latest.ID)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        RefPeriode selection = db.RefPeriode.OrderBy(y => y.ID).Where(y => y.TransKegiatanProgress.Where(r=>r.KegiatanID==refKegiatan.ID).Count()==0).FirstOrDefault();
        //        ViewBag.keg = refKegiatan.KegName;
        //        //ViewBag.KegiatanID = new SelectList(db.RefKegiatan, "ID", "KegName");
        //        ViewBag.KegiatanID = id;
        //        ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket");
        //        //ViewBag.Period = LastRecord.RefPeriode.ID + 2;
        //        ViewBag.Period = selection.ID;
        //        RefPeriode selected = db.RefPeriode.Find(ViewBag.Period);
        //        ViewBag.year = refKegiatan.RefTPU.TransSchedule.Tahun;
        //        ViewBag.monthnow = DateTime.Now.Month;
        //        ViewBag.month = selected.Ket;
        //        ViewBag.SysUsername = User.Identity.GetUserName();
        //        ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //        ViewBag.SysTglEntry = DateTime.Now;
        //        return View();
        //    }

        //    RefPeriode earliest = db.RefPeriode.OrderBy(y => y.ID).FirstOrDefault();
        //    ViewBag.monthnow = DateTime.Now.Month;
        //    ViewBag.keg = refKegiatan.KegName;
        //    ViewBag.KegiatanID = id;
        //    ViewBag.Period = earliest.ID;
        //    ViewBag.month = earliest.Ket;
        //    ViewBag.year = refKegiatan.RefTPU.TransSchedule.Tahun;
        //    ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket");
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    return View();
        //}

        //// POST: Progress/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Detail,Period,KegiatanID,KegStatusID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanProgress transKegiatanProgress)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    } 
            
        //    if (ModelState.IsValid)
        //    {
        //        TransKegiatanProgress LastRecord = db.TransProgressKegiatan.Where(y => y.KegiatanID == transKegiatanProgress.KegiatanID).OrderByDescending(y => y.Period).FirstOrDefault();
        //        if (LastRecord != null)
        //        {
        //            RefPeriode selection = db.RefPeriode.OrderBy(y => y.ID).Where(y => y.TransKegiatanProgress.Where(r => r.KegiatanID == transKegiatanProgress.KegiatanID).Count() == 0).FirstOrDefault();
        //            //transKegiatanProgress.Period = LastRecord.RefPeriode.ID + 2;
        //            transKegiatanProgress.Period = selection.ID;
        //            transKegiatanProgress.SysUsername = User.Identity.GetUserName();
        //            transKegiatanProgress.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //            transKegiatanProgress.SysTglEntry = DateTime.Now;
        //            db.TransProgressKegiatan.Add(transKegiatanProgress);
        //            db.SaveChanges();
        //            //if (LastRecord.KegStatusID != transKegiatanProgress.KegStatusID)
        //            //{
        //            //    RefKegiatan act = db.RefKegiatan.Find(transKegiatanProgress.KegiatanID);
        //            //    var judul = act.KegName;
        //            //    RefKegiatanStatus stat = db.RefStatusKegiatan.Find(transKegiatanProgress.KegStatusID);
        //            //    var status = stat.Ket;
        //            //    TransNotifikasi notifikasi = new TransNotifikasi
        //            //    {
        //            //        RouteID = transKegiatanProgress.KegiatanID,
        //            //        body = "Perubahan status kegiatan menjadi " + status + " untuk kegiatan berjudul " + judul + "...",
        //            //        name = currentuser.FirstName+" "+currentuser.LastName,
        //            //        Action = "Details",
        //            //        Controller = "Kegiatan",
        //            //        RoleID = 1,
        //            //        Date = DateTime.Now,
        //            //        NotifType = 1
        //            //    };
        //            //    db.TransNotifikasi.Add(notifikasi);
        //            //    db.SaveChanges();
        //            //};
        //            return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanProgress.KegiatanID });
        //        }
        //        RefPeriode earliest = db.RefPeriode.OrderBy(y => y.ID).Where(y => y.ID>transKegiatanProgress.RefKegiatan.PeriodeID).FirstOrDefault();
        //        transKegiatanProgress.Period = earliest.ID;
        //        transKegiatanProgress.SysUsername = User.Identity.GetUserName();
        //        transKegiatanProgress.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //        transKegiatanProgress.SysTglEntry = DateTime.Now;
        //        db.TransProgressKegiatan.Add(transKegiatanProgress);
        //        db.SaveChanges();
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
        //        return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanProgress.KegiatanID });
        //    }

        //    ViewBag.KegiatanID = transKegiatanProgress.KegiatanID;
        //    ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket", transKegiatanProgress.KegStatusID);
        //    ViewBag.Period = transKegiatanProgress.Period;
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    return View(transKegiatanProgress);
        //}

        //// GET: Progress/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransKegiatanProgress transKegiatanProgress = db.TransProgressKegiatan.Find(id);
        //    if (transKegiatanProgress == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    if (transKegiatanProgress.RefKegiatan.RefTPU.Finalize == 1)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    ViewBag.keg = transKegiatanProgress.RefKegiatan.KegName;
        //    ViewBag.month = transKegiatanProgress.RefPeriode.Ket;
        //    ViewBag.year = transKegiatanProgress.RefKegiatan.RefTPU.TransSchedule.Tahun;
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    ViewBag.KegiatanID = transKegiatanProgress.KegiatanID;
        //    ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket", transKegiatanProgress.KegStatusID);
        //    ViewBag.Period = transKegiatanProgress.Period;
        //    return View(transKegiatanProgress);
        //}

        //// POST: Progress/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Detail,Period,KegiatanID,KegStatusID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanProgress transKegiatanProgress)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        //db.Entry(transKegiatanProgress).State = EntityState.Modified;
        //        var dbTarget = db.TransProgressKegiatan.Find(transKegiatanProgress.ID);
        //        dbTarget.KegStatusID = transKegiatanProgress.KegStatusID;
        //        dbTarget.Detail = transKegiatanProgress.Detail;
        //        dbTarget.SysUsername = User.Identity.GetUserName();
        //        dbTarget.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //        dbTarget.SysTglEntry = DateTime.Now;
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "Kegiatan", new { id=transKegiatanProgress.KegiatanID});
        //    }
        //    ViewBag.keg = transKegiatanProgress.RefKegiatan.KegName;
        //    ViewBag.month = transKegiatanProgress.RefPeriode.Ket;
        //    ViewBag.year = transKegiatanProgress.RefKegiatan.RefTPU.TransSchedule.Tahun;
        //    ViewBag.SysUsername = User.Identity.GetUserName();
        //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
        //    ViewBag.SysTglEntry = DateTime.Now;
        //    ViewBag.KegiatanID = transKegiatanProgress.KegiatanID;
        //    ViewBag.KegStatusID = new SelectList(db.RefStatusKegiatan, "ID", "Ket", transKegiatanProgress.KegStatusID);
        //    ViewBag.Period = transKegiatanProgress.Period;
        //    return View(transKegiatanProgress);
        //}

        //// GET: Progress/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransKegiatanProgress transKegiatanProgress = db.TransProgressKegiatan.Find(id);
        //    if (transKegiatanProgress == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transKegiatanProgress);
        //}

        //// POST: Progress/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TransKegiatanProgress transKegiatanProgress = db.TransProgressKegiatan.Find(id);
        //    db.TransProgressKegiatan.Remove(transKegiatanProgress);
        //    db.SaveChanges();
        //    return RedirectToAction("Details", "Kegiatan", new { id=transKegiatanProgress.KegiatanID});
        //}

        public ActionResult Notifications()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransNotifClick click = new TransNotifClick
            {
                Date = DateTime.Now,
                UserName = User.Identity.GetUserName()
            };
            db.TransNotifClick.Add(click);
            db.SaveChanges();

            if (currentuser.RoleID == 1)
            {
                var notifications = db.TransNotifikasi.Where(y => y.RoleID == 1).OrderByDescending(y => y.Date).ToList();

                return View(notifications);
            }

            else
            {
                var notifications = db.TransNotifikasi.Where(y => y.RoleID == 2 && y.UnitID == currentuser.UnitID).OrderByDescending(y => y.Date).ToList();

                return View(notifications);
            }
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
