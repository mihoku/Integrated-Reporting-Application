using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ira.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ira.Controllers
{
    [Authorize]
    public class TPUController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ViewTPUEntities db1 = new ViewTPUEntities();

         //GET: TPU

        //public ActionResult Index()
        //{
        //    return RedirectToAction("Tema",)
        //}

        public ActionResult loadFinalizeTema(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefTPU tema = db.RefTPU.Find(id);

            if (tema.RefKegiatan.Where(y => y.Finalize == 0).Count() != 0||tema.RefKegiatan.Count()==0)
            {
                ViewBag.show = tema.RefKegiatan.Count();
                var kegiatan = tema.RefKegiatan.Where(y => y.Finalize == 0).ToList();
                return PartialView("_loadFinalizeFail", kegiatan);
            }

            TPUFinalizeModel finalize = new TPUFinalizeModel
            {
                ID = tema.ID,
                Finalize = 1
            };

            ViewBag.JudulTPU = tema.TPUName;
            ViewBag.jumlahKegiatan = tema.RefKegiatan.Count();
            ViewBag.unit = tema.RefUnitPJ.Detail;
            ViewBag.target = tema.RefQuarter.QuarterDetails;
            return PartialView("_loadFinalizeTema", finalize);
        }

        public ActionResult Tema(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.isThereActiveMaster = db.TransSchedule.Where(y => y.Locked == 0).Count() != 0;

            RefTPUJenis jenis = db.RefJenisTPU.Find(id);
            //var roli = Roles.GetRolesForUser(User.Identity.GetUserName()).FirstOrDefault();
            //ViewBag.role = roli;
            if (currentuser.RoleID != 1)
            {
                ViewBag.role = currentuser.RoleID;
                var refTPU = db.RefTPU.Include(r => r.RefEselon1).Include(r => r.RefPegawai).Include(r => r.RefQuarter).Include(r => r.RefTPUJenis).Include(r => r.RefTPUStatus).Include(r => r.RefUnitPJ)
                .Where(r => r.TransSchedule.Tahun == DateTime.Now.Year && r.TPUJenisID == id && r.TPUUnitPJID == currentuser.UnitID)
                .OrderBy(r => r.No);
                ViewBag.Title = jenis.JenisDetail;
                ViewBag.Jenis = id;
                return View(refTPU.ToList());
            }
            ViewBag.role = currentuser.RoleID;
            var TPU = db.RefTPU.Include(r=>r.RefEselon1).Include(r => r.RefPegawai).Include(r => r.RefQuarter).Include(r => r.RefTPUJenis).Include(r => r.RefTPUStatus).Include(r => r.RefUnitPJ)
                .Where(r=>r.TransSchedule.Tahun==DateTime.Now.Year&&r.TPUJenisID==id)
                .OrderBy(r=>r.No);
            ViewBag.Title = jenis.JenisDetail;
            ViewBag.Jenis = id;
            return View(TPU.ToList());
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult loadFinalizeTema([Bind(Include = "ID,Finalize")] TPUFinalizeModel tpu)
        {
            //if (refKegiatan.TransKegiatanOutput.Count > 0)
            //{
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if (tpu.Finalize == 0 || tpu.Finalize == 1)
                {
                    //db.Entry(refKegiatan).State = EntityState.Modified;
                    var dbTPU = db.RefTPU.FirstOrDefault(p => p.ID == tpu.ID);
                    if (dbTPU == null)
                    {
                        return RedirectToAction("NotFound", "ErrorPage", null);
                    }
                    if (dbTPU.RefKegiatan.Where(y => y.Finalize == 0).Count() != 0)
                    {
                        return RedirectToAction("Details", "TPU", new { tpu.ID });
                    }
                    dbTPU.Finalize = tpu.Finalize;
                    dbTPU.TPUStatusID = 3;
                    db.SaveChanges();
                    return RedirectToAction("Details", "TPU", new { tpu.ID });
                }

                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return RedirectToAction("Details", "TPU", new { id = tpu.ID });
            //    }
            //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });            
        }

        public ActionResult loadUnfinalizeTema(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefTPU tema = db.RefTPU.Find(id);

            //if (tema.RefKegiatan.Where(y => y.Finalize == 0).Count() != 0)
            //{
            //    var kegiatan = tema.RefKegiatan.Where(y => y.Finalize == 0).ToList();
            //    return PartialView("_loadFinalizeFail", kegiatan);
            //}

            TPUFinalizeModel finalize = new TPUFinalizeModel
            {
                ID = tema.ID,
                Finalize = 0
            };

            ViewBag.JudulTPU = tema.TPUName;
            ViewBag.jumlahKegiatan = tema.RefKegiatan.Count();
            ViewBag.unit = tema.RefUnitPJ.Detail;
            ViewBag.target = tema.RefQuarter.QuarterDetails;
            return PartialView("_loadUnfinalizeTema", finalize);
        }

        public ActionResult Unfinalize(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefTPU tema = db.RefTPU.Find(id);

            tema.Finalize = 0;
            tema.TPUStatusID = 2;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = tema.ID });
            //if (tema.RefKegiatan.Where(y => y.Finalize == 0).Count() != 0)
            //{
            //    var kegiatan = tema.RefKegiatan.Where(y => y.Finalize == 0).ToList();
            //    return PartialView("_loadFinalizeFail", kegiatan);
            //}

            //TPUFinalizeModel finalize = new TPUFinalizeModel
            //{
            //    ID = tema.ID,
            //    Finalize = 0
            //};

            //ViewBag.JudulTPU = tema.TPUName;
            //ViewBag.jumlahKegiatan = tema.RefKegiatan.Count();
            //ViewBag.unit = tema.RefUnitPJ.Detail;
            //ViewBag.target = tema.RefQuarter.QuarterDetails;
            //return PartialView("_loadUnfinalizeTema", finalize);
        }

        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public ActionResult loadUnfinalizeTema([Bind(Include = "ID,Finalize")] TPUFinalizeModel tpu)
        //{
        //    //if (refKegiatan.TransKegiatanOutput.Count > 0)
        //    //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (tpu.Finalize == 0 || tpu.Finalize == 1)
        //        {
        //            //db.Entry(refKegiatan).State = EntityState.Modified;
        //            var dbTPU = db.RefTPU.FirstOrDefault(p => p.ID == tpu.ID);
        //            if (dbTPU == null)
        //            {
        //                return RedirectToAction("NotFound", "ErrorPage", null);
        //            }
        //            //if (dbTPU.RefKegiatan.Where(y => y.Finalize == 0).Count() != 0)
        //            //{
        //            //    return RedirectToAction("Details", "TPU", new { tpu.ID });
        //            //}
        //            dbTPU.Finalize = tpu.Finalize;
        //            db.SaveChanges();
        //            return RedirectToAction("Details", "TPU", new { tpu.ID });
        //        }

        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return RedirectToAction("Details", "TPU", new { id = tpu.ID });
        //    //    }
        //    //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });            
        //}

        ////GET: TPU
        //public ActionResult DPU()
        //{
        //    var refTPU = db.RefTPU.Include(r => r.RefEselon1).Include(r => r.RefPegawai).Include(r => r.RefQuarter).Include(r => r.RefTPUJenis).Include(r => r.RefTPUStatus).Include(r => r.RefUnitPJ)
        //        .Where(r => r.TransSchedule.Tahun == DateTime.Now.Year && r.TPUJenisID == 3)
        //        .OrderBy(r => r.No);
        //    return View(refTPU.ToList());
        //}

        ////GET: TPU
        //public ActionResult CurrentIssues()
        //{
        //    var refTPU = db.RefTPU.Include(r => r.RefEselon1).Include(r => r.RefPegawai).Include(r => r.RefQuarter).Include(r => r.RefTPUJenis).Include(r => r.RefTPUStatus).Include(r => r.RefUnitPJ)
        //        .Where(r => r.TransSchedule.Tahun == DateTime.Now.Year && r.TPUJenisID == 5)
        //        .OrderBy(r => r.No);
        //    return View(refTPU.ToList());
        //}

        //public ActionResult Index2(string searchTerm = null, int page = 1)
        //{
            
        //    var refTPU = db.RefTPU
        //        .Include(r => r.RefPegawai)
        //        .Include(r => r.RefQuarter)
        //        .Include(r => r.RefTPUJenis)
        //        .Include(r => r.RefTPUStatus)
        //        .Include(r => r.RefUnitPJ)
        //        .OrderBy(i => i.ID)
        //        .Where(r => searchTerm == null || r.TPUName.Contains(searchTerm))
        //        .Select(y => new TPUListViewModel { 
        //            ID = y.ID,
        //            TPUName = y.TPUName,
        //            TPUJenis = y.RefTPUJenis.JenisDetail,
        //            TPUStatus = y.RefTPUStatus.Ket,
        //            UnitPJ = y.RefUnitPJ.Detail,
        //            TPUNamaPIC = y.RefPegawai.PegName
        //        }).ToPagedList(page, 10);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_TPUList", refTPU);
        //    }

        //    return View(refTPU);
        //}


        // GET: TPU/Details/5
        public ActionResult Details(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Index");
            } 
            
            var a="Belum ada kegiatan.";
            var b = "Sasaran belum ditetapkan.";
            //if (id == null)
            //{
            //    return RedirectToAction("NotFound", "ErrorPage", null);
            //}
            RefTPU refTPU = db.RefTPU.Find(id);
            //TransTPUTujuan coba = db.TransTPUTujuan
            //    .Where(r => r.TPUID == refTPU.ID)
            //    .LastOrDefault();
            //TransTPUTujuan sasaran = db.TransTPUTujuan
            //    .Where(r => r.RefTPU.ID == refTPU.ID)
            //    .LastOrDefault();
            if (refTPU == null)
            {
                //return RedirectToAction("NotFound", "ErrorPage", null);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (currentuser.RoleID != 1 && currentuser.UnitID != refTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            ViewBag.show = currentuser.RoleID;

            if (refTPU.RefKegiatan.Count() == 0)
            {
                if (refTPU.TransTPUTujuan.Count() == 0)
                {
                    ViewBag.msg = a;
                    ViewBag.msg2 = b;
                    return View(refTPU);
                }
                ViewBag.msg = a;
                return View(refTPU);
            }
            if (refTPU.TransTPUTujuan.Count() == 0)
            {
                ViewBag.msg2 = b;
                return View(refTPU);
            }
            //ViewBag.test = coba.TujuanTPU;
            return View(refTPU);
        }

        // GET: TPU/Report/7
        //public ActionResult Report(int id)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    RefTPU refTPU = db.RefTPU.Find(id);
        //    if (refTPU == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }

        //    if (currentuser.RoleID != 1 && currentuser.UnitID != refTPU.TPUUnitPJID)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    if (refTPU.RefKegiatan.Count() == 0)
        //    {
        //        ViewBag.msg = "Belum ada kegiatan.";
        //        return View(refTPU);
        //    }
        //    ViewBag.ReportCount = db.TransKegiatanOutput.Where(r => r.OutputJenisID == 1 && r.RefKegiatan.KegiatanTPUID == refTPU.ID).Count();
        //    return View(refTPU);
        //}

        // GET: TPU/Create
        public ActionResult Create(int id)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.Tahun).FirstOrDefault();

            if (master == null)
            {
                return RedirectToAction("Index", "Home", null);
            }

            RefTPUJenis jenis = db.RefJenisTPU.Find(id);

            if (jenis == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            ViewBag.role = currentuser.RoleID;

            ViewBag.Title = "Input "+jenis.JenisDetail;

            if (currentuser.RoleID == 1)
            {
                ViewBag.TPUPJID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true), "ID", "PegName");
                ViewBag.TPUQTargetID = new SelectList(db.RefQuarter, "ID", "QuarterDetails");
                ViewBag.TPUJenisID = jenis.JenisID;
                ViewBag.TPUStatusID = 1;
                ViewBag.TPUUnitPJID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail");
                ViewBag.TPUEselon1ID = new SelectList(db.RefEselon1.Where(y => y.Aktif == true), "ID", "Ket");
                ViewBag.PKPTID = new SelectList(db.TransSchedule.Where(y => y.Locked == 0), "ID", "Title");
                ViewBag.SysUsername = User.Identity.GetUserName();
                ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                ViewBag.SysTglEntry = DateTime.Now;
                return View();
            }

            else if (jenis.JenisID != 5)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TPUPJID = new SelectList(db.RefPegawai.Where(y=>y.Aktif==true&&y.PegUnitID==currentuser.UnitID), "ID", "PegName");
            ViewBag.TPUQTargetID = new SelectList(db.RefQuarter, "ID", "QuarterDetails");
            ViewBag.TPUJenisID = jenis.JenisID;
            ViewBag.TPUStatusID = 1;
            ViewBag.TPUUnitPJID = currentuser.UnitID;
            ViewBag.TPUEselon1ID = new SelectList(db.RefEselon1.Where(y => y.Aktif == true), "ID", "Ket");
            ViewBag.PKPTID = new SelectList(db.TransSchedule.Where(y => y.Locked == 0), "ID", "Title");
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: TPU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,No,TPUName,TPUEselon1ID,PKPTID,TPUTujuan,TPUPJID,TPUQTargetID,TPUStatusID,TPUUnitPJID,TPUJenisID,SysUsername,SysTglEntry,SysWorkstation")] RefTPU refTPU)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1&&currentuser.RoleID!=2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                if (currentuser.RoleID != 1 && refTPU.TPUJenisID != 5)
                {
                    return RedirectToAction("Login", "Account");
                }

                refTPU.TPUStatusID = 1;
                db.RefTPU.Add(refTPU);
                db.SaveChanges();
                return RedirectToAction("Tema", new { id=refTPU.TPUJenisID });
            }
            if (currentuser.RoleID == 1)
            {
                ViewBag.TPUPJID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true), "ID", "PegName", refTPU.TPUPJID);
                ViewBag.TPUEselon1ID = new SelectList(db.RefEselon1.Where(y => y.Aktif == true), "ID", "Ket", refTPU.TPUEselon1ID);
                ViewBag.TPUQTargetID = new SelectList(db.RefQuarter, "ID", "QuarterDetails", refTPU.TPUQTargetID);
                ViewBag.TPUJenisID = new SelectList(db.RefJenisTPU.Where(y => y.Aktif == true), "JenisID", "JenisDetail", refTPU.TPUJenisID);
                ViewBag.TPUStatusID = 1;
                ViewBag.TPUUnitPJID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refTPU.TPUUnitPJID);
                ViewBag.PKPTID = new SelectList(db.TransSchedule.Where(y => y.Locked == 0), "ID", "Title", refTPU.PKPTID);
                ViewBag.SysUsername = User.Identity.GetUserName();
                ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                ViewBag.SysTglEntry = DateTime.Now;
                ViewBag.alert = 1;
                return View(refTPU);            
            }
            ViewBag.TPUPJID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true && y.PegUnitID == currentuser.UnitID), "ID", "PegName", refTPU.TPUPJID);
            ViewBag.TPUEselon1ID = new SelectList(db.RefEselon1.Where(y => y.Aktif == true), "ID", "Ket", refTPU.TPUEselon1ID);
            ViewBag.TPUQTargetID = new SelectList(db.RefQuarter, "ID", "QuarterDetails", refTPU.TPUQTargetID);
            ViewBag.TPUJenisID = refTPU.TPUJenisID;
            ViewBag.TPUStatusID = 1;
            ViewBag.TPUUnitPJID = currentuser.UnitID;
            ViewBag.PKPTID = new SelectList(db.TransSchedule.Where(y => y.Locked == 0), "ID", "Title", refTPU.PKPTID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            ViewBag.alert = 1;
            return View(refTPU);            
        }

        public ActionResult FillPJ(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var PJs = db.RefPegawai.Where(y => y.PegUnitID == id);
            return Json(PJs, JsonRequestBehavior.AllowGet);
        }

        // GET: TPU/Edit/5
        public ActionResult Edit(int id)
        {
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
            //ViewBag.role = currentuser.RoleID;
            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Index");
            }

            ViewBag.TPUPJID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true||y.ID==refTPU.TPUPJID), "ID", "PegName", refTPU.TPUPJID);
            ViewBag.TPUEselon1ID = new SelectList(db.RefEselon1.Where(y => y.Aktif == true || y.ID == refTPU.TPUEselon1ID), "ID", "Ket", refTPU.TPUEselon1ID);
            ViewBag.TPUQTargetID = new SelectList(db.RefQuarter, "ID", "QuarterDetails", refTPU.TPUQTargetID);
            ViewBag.TPUJenisID = refTPU.TPUJenisID;
            ViewBag.TPUStatusID = refTPU.TPUStatusID;
            ViewBag.PKPTID = new SelectList(db.TransSchedule.Where(y=>y.Locked==0||y.Locked==1), "ID", "Title", refTPU.PKPTID);
            ViewBag.TPUUnitPJID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true || y.ID == refTPU.TPUUnitPJID), "ID", "Detail", refTPU.TPUUnitPJID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(refTPU);
        }

        // POST: TPU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,No,TPUName,TPUEselon1ID,PKPTID,TPUPJID,TPUQTargetID,TPUStatusID,TPUUnitPJID,TPUJenisID,SysUsername,SysTglEntry,SysWorkstation")] RefTPU refTPU)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                var dbTarget = db.RefTPU.FirstOrDefault(p => p.ID == refTPU.ID);
                if (dbTarget == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                var status = dbTarget.TPUStatusID;
                var jenis = dbTarget.TPUJenisID;
                db.Entry(refTPU).State = EntityState.Modified;
                dbTarget.TPUStatusID = status;
                dbTarget.TPUJenisID = jenis;
                dbTarget.SysUsername = User.Identity.GetUserName();
                dbTarget.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbTarget.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TPUPJID = new SelectList(db.RefPegawai.Where(y => y.Aktif == true || y.ID == refTPU.TPUPJID), "ID", "PegName", refTPU.TPUPJID);
            ViewBag.TPUEselon1ID = new SelectList(db.RefEselon1.Where(y => y.Aktif == true || y.ID == refTPU.TPUEselon1ID), "ID", "Ket", refTPU.TPUEselon1ID);
            ViewBag.TPUQTargetID = new SelectList(db.RefQuarter, "ID", "QuarterDetails", refTPU.TPUQTargetID);
            ViewBag.TPUJenisID = refTPU.TPUJenisID;
            ViewBag.TPUStatusID = refTPU.TPUStatusID;
            ViewBag.PKPTID = new SelectList(db.TransSchedule.Where(y => y.Locked == 0 || y.Locked == 1), "ID", "Title", refTPU.PKPTID);
            ViewBag.TPUUnitPJID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true || y.ID == refTPU.TPUUnitPJID), "ID", "Detail", refTPU.TPUUnitPJID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(refTPU);
        }

        // GET: TPU/Delete/5
        public ActionResult Delete(int id)
        {
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

            if (currentuser.RoleID != 1 && currentuser.UnitID != refTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            return View(refTPU);
        }

        // POST: TPU/Delete/5
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
            
            RefTPU refTPU = db.RefTPU.Find(id);
            if (currentuser.RoleID != 1 && currentuser.UnitID != refTPU.TPUUnitPJID)
            {
                return RedirectToAction("Index");
            }

            db.RefTPU.Remove(refTPU);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Finalize([Bind(Include = "ID,Judul,ManajerID,TanggalKasus,SysUsername,SysTglEntry,SysWorkstation,Finalize")] RefTPU tpu)
        //{
        //    //if (refKegiatan.TransKegiatanOutput.Count > 0)
        //    //{
        //    if (ModelState.IsValid)
        //    {
        //        //status yng dimasukkan bener2 ada di tabel referensi
        //        if (db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize) != null)
        //        {
        //            TransFlashKegiatan flash = db.TransFlashKegiatan.Find(flashKegiatan.ID);
        //            if (flash.TransFlashKegiatanProgress.Where(y => y.KegStatusID == flashKegiatan.Finalize).Count() != 0)
        //            {
        //                //db.Entry(refKegiatan).State = EntityState.Modified;
        //                var dbKeg = db.TransFlashKegiatan.Find(flashKegiatan.ID);
        //                if (dbKeg == null)
        //                {
        //                    return RedirectToAction("NotFound", "ErrorPage", null);
        //                }
        //                dbKeg.Finalize = flashKegiatan.Finalize;
        //                db.SaveChanges();
        //                TransFlashKegiatan act2 = db.TransFlashKegiatan.Find(flashKegiatan.ID);
        //                var judul2 = act2.Judul;
        //                RefFlashKegiatanStatus stat2 = db.RefFlashStatusKegiatan.Find(flashKegiatan.Finalize);
        //                var status2 = stat2.Ket;
        //                var notif = new TransFlashNotifikasi
        //                {
        //                    name = currentuser.FirstName+" "+currentuser.LastName,
        //                    Action = "Details",
        //                    Controller = "Flash",
        //                    RouteID = flashKegiatan.ID,
        //                    body = "Perubahan status kegiatan menjadi " + status2 + " untuk kegiatan berjudul " + judul2 + "...",
        //                    RoleID = 1,
        //                    Date = DateTime.Now,
        //                    NotifType = 4
        //                };
        //                db.TransFlashNotifikasi.Add(notif);
        //                db.SaveChanges();
        //                return RedirectToAction("Details", "Flash", new { flashKegiatan.ID });
        //            }

        //            return RedirectToAction("NotFound", "ErrorPage", null);
        //        }

        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return RedirectToAction("Details", "Flash", new { id = flashKegiatan.ID });
        //    //    }
        //    //    return RedirectToAction("Details", "Kegiatan", new { id = refKegiatan.ID });            
        //}

        //public ActionResult Autocomplete(string term) {
        //    var refTPU = db.RefTPU
        //        .Where(r=>r.TPUName.Contains(term))
        //        .Select(r=> new {
        //            label = r.TPUName
        //        });

        //    return Json(refTPU,JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Test(int ID)
        //{
        //    var refTPU = db.RefTPU
        //        .Where(r => r.ID==ID)
        //        .Select(r => new
        //        {
        //            ID = r.ID,
        //            Name = r.TPUName,
        //            Year = r.TransSchedule.Tahun
        //        });

        //    return Json(refTPU, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult JenisTPUPKPT(int id)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var jenisTPU = db1.v_ira_jenistpu_pkpt.Where(y=>y.JenisTpuID==id).FirstOrDefault();

            ViewBag.jenisTPU = jenisTPU.NamaJenisTpu;

            return PartialView("_jenisTPUPKPT");
        }

        public ActionResult UnitTPUPKPT(int id)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var unitTPU = db1.v_ira_unit_pkpt.Where(y=>y.UnitID==id).FirstOrDefault();

            ViewBag.unitTPU = unitTPU.NamaUnit;

            return PartialView("_unitTPUPKPT");
        }

        public ActionResult AplikasiPKPT()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var TPView = db1.v_ira_tpu.ToList();

            return View(TPView);
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