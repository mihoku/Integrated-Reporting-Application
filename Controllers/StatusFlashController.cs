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
using System.Web.Security;

namespace ira.Controllers
{
    [Authorize]
    public class StatusFlashController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: StatusKegiatan
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefFlashStatusKegiatan.Where(y => y.Aktif == true).ToList());
        }

        // GET: StatusKegiatan with status inaktif
        public ActionResult Inaktif()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefFlashStatusKegiatan.Where(y => y.Aktif == false).ToList());
        }

        public ActionResult Deactivate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefFlashKegiatanStatus status = db.RefFlashStatusKegiatan.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            status.Aktif = false;
            status.Ket = status.Ket;
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

            RefFlashKegiatanStatus status = db.RefFlashStatusKegiatan.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            status.Aktif = true;
            status.Ket = status.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: StatusKegiatan/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefKegiatanStatus refKegiatanStatus = db.RefStatusKegiatan.Find(id);
        //    if (refKegiatanStatus == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refKegiatanStatus);
        //}

        // GET: StatusKegiatan/Create
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

        // POST: StatusKegiatan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ket")] RefFlashKegiatanStatus refKegiatanStatus)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.RefFlashStatusKegiatan.Add(refKegiatanStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(refKegiatanStatus);
        }

        // GET: StatusKegiatan/Edit/5
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
            RefFlashKegiatanStatus refKegiatanStatus = db.RefFlashStatusKegiatan.Find(id);
            if (refKegiatanStatus == null)
            {
                return HttpNotFound();
            }
            return View(refKegiatanStatus);
        }

        // POST: StatusKegiatan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ket")] RefFlashKegiatanStatus refKegiatanStatus)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(refKegiatanStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refKegiatanStatus);
        }

        //// GET: StatusKegiatan/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RefKegiatanStatus refKegiatanStatus = db.RefStatusKegiatan.Find(id);
        //    if (refKegiatanStatus == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(refKegiatanStatus);
        //}

        //// POST: StatusKegiatan/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RefKegiatanStatus refKegiatanStatus = db.RefStatusKegiatan.Find(id);
        //    db.RefStatusKegiatan.Remove(refKegiatanStatus);
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
