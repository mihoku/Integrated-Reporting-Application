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
    public class UnitController : Controller
    {
        private IRADbContext db = new IRADbContext();

        // GET: Unit
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefUnitPJ.Where(y => y.Aktif == true).ToList());
        }

        // GET: Unit with Inaktif status
        public ActionResult Inaktif()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefUnitPJ.Where(y => y.Aktif == false).ToList());
        }

        //// GET: Unit/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefUnitPJ refUnitPJ = db.RefUnitPJ.Find(id);
        //    if (refUnitPJ == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refUnitPJ);
        //}

        // GET: Unit/Create
        public ActionResult Create()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Detail,DetailShort,isPrimeMover")] RefUnitPJ refUnitPJ)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                refUnitPJ.Aktif = true;
                db.RefUnitPJ.Add(refUnitPJ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refUnitPJ);
        }

        // GET: Unit/Edit/5
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefUnitPJ refUnitPJ = db.RefUnitPJ.Find(id);
            if (refUnitPJ == null)
            {
                return HttpNotFound();
            }
            return View(refUnitPJ);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Detail,DetailShort")] RefUnitPJ refUnitPJ)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var unit = db.RefUnitPJ.Find(refUnitPJ.ID);
                unit.Detail = refUnitPJ.Detail;
                unit.DetailShort = refUnitPJ.DetailShort;
                //db.Entry(refUnitPJ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refUnitPJ);
        }

        //// GET: Unit/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefUnitPJ refUnitPJ = db.RefUnitPJ.Find(id);
        //    if (refUnitPJ == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refUnitPJ);
        //}

        //// POST: Unit/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RefUnitPJ refUnitPJ = db.RefUnitPJ.Find(id);
        //    db.RefUnitPJ.Remove(refUnitPJ);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Deactivate(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            unit.Aktif = false;
            unit.Detail = unit.Detail;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Activate(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            unit.Aktif = true;
            unit.Detail = unit.Detail;
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
