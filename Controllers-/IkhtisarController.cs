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
using Microsoft.Owin.Security;

namespace ira.Controllers
{
    [Authorize]
    public class IkhtisarController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Ikhtisar
        //public ActionResult Index()
        //{
        //    var transIkhtisarProgresses = db.TransIkhtisarProgresses.Include(t => t.RefUniverseAudit);
        //    return View(transIkhtisarProgresses.ToList());
        //}

        // GET: Ikhtisar/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransIkhtisarProgress transIkhtisarProgress = db.TransIkhtisarProgresses.Find(id);
        //    if (transIkhtisarProgress == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transIkhtisarProgress);
        //}

        // GET: Ikhtisar/Create
        //public ActionResult Create()
        //{
        //    ViewBag.UniverseID = new SelectList(db.RefUniverseAudit, "ID", "Ket");
        //    return View();
        //}

        public ActionResult Submit(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 2&&currentuser.UnitID!=ikhtisar.RefUniverseAudit.UnitID)
            {
                return RedirectToAction("Pending", "Progress",null);
            }

            ikhtisar.Locked = true;
            db.SaveChanges();

            return RedirectToAction("Pending", "Progress", null);
        }

        public ActionResult Reject(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Pending", "Progress", null);
            }

            if (ikhtisar.Locked == false)
            {
                return RedirectToAction("Pending", "Progress", null);
            }

            ikhtisar.Locked = false;
            db.SaveChanges();

            return RedirectToAction("Pending", "Progress", null);
        }

        public ActionResult Accept(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Pending", "Progress", null);
            }

            if (ikhtisar.Locked == false)
            {
                return RedirectToAction("Pending", "Progress", null);
            }

            ikhtisar.Accepted = true;
            db.SaveChanges();

            return RedirectToAction("Pending", "Progress", null);
        }

        public ActionResult Entri(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }
            
            TransIkhtisarProgress transIkhtisarProgress = db.TransIkhtisarProgresses.Find(id);
            if (transIkhtisarProgress == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (currentuser.RoleID == 2 && currentuser.UnitID != transIkhtisarProgress.RefUniverseAudit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == transIkhtisarProgress.PeriodeID && y.PKPTID == transIkhtisarProgress.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd.Locked == true)
            {
                return RedirectToAction("Pending", "Progress", null);
            }

            if (transIkhtisarProgress.Locked==true)
            {
                return RedirectToAction("Pending", "Progress");
            }

            ViewBag.RencanaKerja = transIkhtisarProgress.RencanaKerja;
            ViewBag.HasilPengawasan = transIkhtisarProgress.HasilPengawasan;
            ViewBag.RencanaPengawasan = transIkhtisarProgress.RencanaPengawasan;
            ViewBag.Tahun = transIkhtisarProgress.TransSchedule.Tahun;
            ViewBag.Periode = transIkhtisarProgress.RefPeriode.Ket;
            ViewBag.Universe = transIkhtisarProgress.RefUniverseAudit.Ket;
            ViewBag.UniverseID = transIkhtisarProgress.UniverseID;
            ViewBag.PKPTID = transIkhtisarProgress.PKPTID;
            ViewBag.PeriodeID = transIkhtisarProgress.PeriodeID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entri([Bind(Include = "ID,RencanaKerja,HasilPengawasan,RencanaPengawasan,UniverseID,SysUsername,SysWorkstation,SysTglEntry,PKPTID,PeriodeID")] TransIkhtisarProgress transIkhtisarProgress)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(transIkhtisarProgress.ID);

            if (currentuser.RoleID == 2 && currentuser.UnitID != ikhtisar.RefUniverseAudit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == transIkhtisarProgress.PeriodeID && y.PKPTID == transIkhtisarProgress.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd.Locked == true)
            {
                return RedirectToAction("Pending", "Progress", null);
            }

            if (transIkhtisarProgress.Locked == true)
            {
                return RedirectToAction("Pending", "Progress");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(transIkhtisarProgress).State = EntityState.Modified;
                TransIkhtisarProgress target = db.TransIkhtisarProgresses.Find(transIkhtisarProgress.ID);
                target.RencanaKerja = transIkhtisarProgress.RencanaKerja;
                target.HasilPengawasan = transIkhtisarProgress.HasilPengawasan;
                target.RencanaPengawasan = transIkhtisarProgress.RencanaPengawasan;
                target.SysUsername = User.Identity.GetUserName();
                target.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                target.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Pending","Progress",null);
            }

            TransIkhtisarProgress target2 = db.TransIkhtisarProgresses.Find(transIkhtisarProgress.ID);
            ViewBag.RencanaKerja = transIkhtisarProgress.RencanaKerja;
            ViewBag.HasilPengawasan = transIkhtisarProgress.HasilPengawasan;
            ViewBag.RencanaPengawasan = transIkhtisarProgress.RencanaPengawasan;
            ViewBag.Tahun = transIkhtisarProgress.TransSchedule.Tahun;
            ViewBag.Periode = transIkhtisarProgress.RefPeriode.Ket;
            ViewBag.Universe = transIkhtisarProgress.RefUniverseAudit.Ket;
            ViewBag.UniverseID = target2.UniverseID;
            ViewBag.PKPTID = target2.PKPTID;
            ViewBag.PeriodeID = target2.PeriodeID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transIkhtisarProgress);
        }

        public ActionResult Amend(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransIkhtisarProgress transIkhtisarProgress = db.TransIkhtisarProgresses.Find(id);
            if (transIkhtisarProgress == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.RoleID == 2 && currentuser.UnitID != transIkhtisarProgress.RefUniverseAudit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            ViewBag.RencanaKerja = transIkhtisarProgress.RencanaKerja;
            ViewBag.HasilPengawasan = transIkhtisarProgress.HasilPengawasan;
            ViewBag.RencanaPengawasan = transIkhtisarProgress.RencanaPengawasan;
            ViewBag.Tahun = transIkhtisarProgress.TransSchedule.Tahun;
            ViewBag.Periode = transIkhtisarProgress.RefPeriode.Ket;
            ViewBag.Universe = transIkhtisarProgress.RefUniverseAudit.Ket;
            ViewBag.UniverseID = transIkhtisarProgress.UniverseID;
            ViewBag.PKPTID = transIkhtisarProgress.PKPTID;
            ViewBag.PeriodeID = transIkhtisarProgress.PeriodeID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Amend([Bind(Include = "ID,RencanaKerja,HasilPengawasan,RencanaPengawasan,UniverseID,SysUsername,SysWorkstation,SysTglEntry,PKPTID,PeriodeID")] TransIkhtisarProgress transIkhtisarProgress)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentuser.RoleID == 2 && currentuser.UnitID != transIkhtisarProgress.RefUniverseAudit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (ModelState.IsValid)
            {
                //db.Entry(transIkhtisarProgress).State = EntityState.Modified;
                TransIkhtisarProgress target = db.TransIkhtisarProgresses.Find(transIkhtisarProgress.ID);
                target.RencanaKerja = transIkhtisarProgress.RencanaKerja;
                target.HasilPengawasan = transIkhtisarProgress.HasilPengawasan;
                target.RencanaPengawasan = transIkhtisarProgress.RencanaPengawasan;
                target.SysUsername = User.Identity.GetUserName();
                target.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                target.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Progress", "Home", null);
            }
            return View(transIkhtisarProgress);
        }

        // POST: Ikhtisar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,RencanaKerja,HasilPengawasan,RencanaPengawasan,UniverseID,SysUsername,SysWorkstation,SysTglEntry")] TransIkhtisarProgress transIkhtisarProgress)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TransIkhtisarProgresses.Add(transIkhtisarProgress);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UniverseID = new SelectList(db.RefUniverseAudit, "ID", "Ket", transIkhtisarProgress.UniverseID);
        //    return View(transIkhtisarProgress);
        //}

        //public ActionResult CallforReport()
        //{
        //    TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

        //    var x = 3;

        //    foreach (var universe in db.RefUniverseAudit.Where(y => y.Aktif == true))
        //    {
        //        TransIkhtisarProgress feed = new TransIkhtisarProgress
        //        {
        //            UniverseID = universe.ID,
        //            PKPTID = master.ID,
        //            PeriodeID = x,
        //            SysTglEntry = DateTime.Now
        //        };
        //        db.TransIkhtisarProgresses.Add(feed);
        //    };
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // GET: Ikhtisar/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransIkhtisarProgress transIkhtisarProgress = db.TransIkhtisarProgresses.Find(id);
        //    if (transIkhtisarProgress == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    ViewBag.UniverseID = new SelectList(db.RefUniverseAudit, "ID", "Ket", transIkhtisarProgress.UniverseID);
        //    return View(transIkhtisarProgress);
        //}

        // POST: Ikhtisar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,RencanaKerja,HasilPengawasan,RencanaPengawasan,UniverseID,SysUsername,SysWorkstation,SysTglEntry,PKPTID,PeriodeID")] TransIkhtisarProgress transIkhtisarProgress)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(transIkhtisarProgress).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UniverseID = new SelectList(db.RefUniverseAudit, "ID", "Ket", transIkhtisarProgress.UniverseID);
        //    return View(transIkhtisarProgress);
        //}

        // GET: Ikhtisar/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransIkhtisarProgress transIkhtisarProgress = db.TransIkhtisarProgresses.Find(id);
        //    if (transIkhtisarProgress == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transIkhtisarProgress);
        //}

        // POST: Ikhtisar/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TransIkhtisarProgress transIkhtisarProgress = db.TransIkhtisarProgresses.Find(id);
        //    db.TransIkhtisarProgresses.Remove(transIkhtisarProgress);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
