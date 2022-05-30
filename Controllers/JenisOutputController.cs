using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ira.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace ira.Controllers
{
    [Authorize]
    public class JenisOutputController : Controller
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

        // GET: JenisOutput
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefKegiatanOutputJenis.Where(y => y.Aktif == true).ToList());
        }

        // GET: JenisOutput with inaktif status
        public ActionResult Inaktif()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefKegiatanOutputJenis.Where(y => y.Aktif == false).ToList());
        }

        public ActionResult Deactivate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefKegiatanOutputJenis output = db.RefKegiatanOutputJenis.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            output.Aktif = false;
            output.Ket = output.Ket;
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

            RefKegiatanOutputJenis output = db.RefKegiatanOutputJenis.Find(id);
            if (output == null)
            {
                return HttpNotFound();
            }
            output.Aktif = true;
            output.Ket = output.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: JenisOutput/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefKegiatanOutputJenis refKegiatanOutputJenis = db.RefKegiatanOutputJenis.Find(id);
        //    if (refKegiatanOutputJenis == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refKegiatanOutputJenis);
        //}

        // GET: JenisOutput/Create
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

        // POST: JenisOutput/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ket,Aktif")] RefKegiatanOutputJenis refKegiatanOutputJenis)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.RefKegiatanOutputJenis.Add(refKegiatanOutputJenis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refKegiatanOutputJenis);
        }

        // GET: JenisOutput/Edit/5
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
            RefKegiatanOutputJenis refKegiatanOutputJenis = db.RefKegiatanOutputJenis.Find(id);
            if (refKegiatanOutputJenis == null)
            {
                return HttpNotFound();
            }
            return View(refKegiatanOutputJenis);
        }

        // POST: JenisOutput/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ket,Aktif")] RefKegiatanOutputJenis refKegiatanOutputJenis)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(refKegiatanOutputJenis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refKegiatanOutputJenis);
        }

        //// GET: JenisOutput/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefKegiatanOutputJenis refKegiatanOutputJenis = db.RefKegiatanOutputJenis.Find(id);
        //    if (refKegiatanOutputJenis == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refKegiatanOutputJenis);
        //}

        //// POST: JenisOutput/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RefKegiatanOutputJenis refKegiatanOutputJenis = db.RefKegiatanOutputJenis.Find(id);
        //    db.RefKegiatanOutputJenis.Remove(refKegiatanOutputJenis);
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
