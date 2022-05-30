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
    [Authorize]
    public class Eselon1Controller : Controller
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

        // GET: Eselon1
        public ActionResult Index()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefEselon1.Where(y => y.Aktif == true).ToList());
        }

        // GET: Eselon1 with inaktif status
        public ActionResult Inaktif()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefEselon1.Where(y => y.Aktif == false).ToList());
        }

        public ActionResult Deactivate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefEselon1 unit = db.RefEselon1.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            unit.Aktif = false;
            unit.Ket = unit.Ket;
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

            RefEselon1 unit = db.RefEselon1.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
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
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefEselon1 refEselon1 = db.RefEselon1.Find(id);
        //    if (refEselon1 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refEselon1);
        //}

        // GET: Eselon1/Create
        public ActionResult Create()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Eselon1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ket,Aktif")] RefEselon1 refEselon1)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.RefEselon1.Add(refEselon1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refEselon1);
        }

        // GET: Eselon1/Edit/5
        public ActionResult Edit(int? id)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefEselon1 refEselon1 = db.RefEselon1.Find(id);
            if (refEselon1 == null)
            {
                return HttpNotFound();
            }
            return View(refEselon1);
        }

        // POST: Eselon1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ket,Aktif")] RefEselon1 refEselon1)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(refEselon1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refEselon1);
        }

        //// GET: Eselon1/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefEselon1 refEselon1 = db.RefEselon1.Find(id);
        //    if (refEselon1 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refEselon1);
        //}

        //// POST: Eselon1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RefEselon1 refEselon1 = db.RefEselon1.Find(id);
        //    db.RefEselon1.Remove(refEselon1);
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
