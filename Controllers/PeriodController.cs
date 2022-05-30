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
using System.Web.Security;

namespace ira.Controllers
{
    public class PeriodController : Controller
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

        // GET: Periode
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefPeriode.Where(y => y.Aktif == true).ToList());
        }

        // GET: Periode with Inaktif status
        public ActionResult Inaktif()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefPeriode.Where(y => y.Aktif == false).ToList());
        }

        public ActionResult Deactivate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefPeriode periode = db.RefPeriode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            periode.Aktif = false;
            periode.Ket = periode.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Activate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefPeriode periode = db.RefPeriode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            periode.Aktif = true;
            periode.Ket = periode.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: Periode/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefPeriode refPeriode = db.RefPeriode.Find(id);
        //    if (refPeriode == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refPeriode);
        //}

        //// GET: Periode/Create
        //public ActionResult Create()
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    return View();
        //}

        //// POST: Periode/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Ket,Aktif")] RefPeriode refPeriode)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.RefPeriode.Add(refPeriode);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(refPeriode);
        //}

        //// GET: Periode/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefPeriode refPeriode = db.RefPeriode.Find(id);
        //    if (refPeriode == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refPeriode);
        //}

        //// POST: Periode/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Ket,Aktif")] RefPeriode refPeriode)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(refPeriode).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(refPeriode);
        //}

        //// GET: Periode/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefPeriode refPeriode = db.RefPeriode.Find(id);
        //    if (refPeriode == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refPeriode);
        //}

        //// POST: Periode/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RefPeriode refPeriode = db.RefPeriode.Find(id);
        //    db.RefPeriode.Remove(refPeriode);
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
