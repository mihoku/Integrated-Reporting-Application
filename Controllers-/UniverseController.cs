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

namespace ira.Controllers
{
    [Authorize]
    public class UniverseController : Controller
    {
        private IRADbContext db = new IRADbContext();

        // GET: Eselon1
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID == 1)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == true).Include(y => y.RefUnitPJ).ToList());
            }

            else if (currentuser.RoleID == 2)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == true&&y.RefUnitPJ.ID==currentuser.UnitID).Include(y => y.RefUnitPJ).ToList());
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Eselon1 with inaktif status
        public ActionResult Inaktif()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID == 1)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == false).Include(y => y.RefUnitPJ).ToList());
            }

            else if (currentuser.RoleID == 2)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == false && y.RefUnitPJ.ID == currentuser.UnitID).Include(y => y.RefUnitPJ).ToList());
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Deactivate(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1&&currentuser.RoleID!=3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUniverseAudit unit = db.RefUniverseAudit.Find(id);
            if (unit == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.UnitID != unit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            unit.Aktif = false;
            unit.Ket = unit.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Activate(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1&&currentuser.RoleID!=3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUniverseAudit unit = db.RefUniverseAudit.Find(id);
            if (unit == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.UnitID != unit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            unit.Aktif = true;
            unit.Ket = unit.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: Eselon1/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    RefUniverseAudit RefUniverseAudit = db.RefUniverseAudit.Find(id);
        //    if (RefUniverseAudit == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(RefUniverseAudit);
        //}

        // GET: Eselon1/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1&&currentuser.RoleID!=3)
            {
                return RedirectToAction("Login", "Account");
            }

            else if (currentuser.RoleID == 1)
            {
                ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail");
                return View();
            }

            else
            {
                RefUnitPJ unit = db.RefUnitPJ.Find(currentuser.UnitID);
                if (unit == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                ViewBag.UnitID = unit.ID;
                ViewBag.UnitDetail = unit.Detail;
                return View();
            }
        }

        // POST: Eselon1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ket,Aktif,UnitID")] RefUniverseAudit refUniverseAudit)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            

            if (ModelState.IsValid)
            {
                db.RefUniverseAudit.Add(refUniverseAudit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refUniverseAudit.UnitID);
            return View(refUniverseAudit);
        }

        // GET: Eselon1/Edit/5
        public ActionResult Edit(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            RefUniverseAudit refUniverseAudit = db.RefUniverseAudit.Find(id);
            if (refUniverseAudit == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refUniverseAudit.UnitID);
            return View(refUniverseAudit);
        }

        // POST: Eselon1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ket,Aktif,UnitID")] RefUniverseAudit refUniverseAudit)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(refUniverseAudit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refUniverseAudit.UnitID);
            return View(refUniverseAudit);
        }

        //// GET: Eselon1/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    RefUniverseAudit RefUniverseAudit = db.RefUniverseAudit.Find(id);
        //    if (RefUniverseAudit == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(RefUniverseAudit);
        //}

        //// POST: Eselon1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RefUniverseAudit RefUniverseAudit = db.RefUniverseAudit.Find(id);
        //    db.RefUniverseAudit.Remove(RefUniverseAudit);
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
