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
    public class PegawaiController : Controller
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

        // GET: Pegawai
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var refPegawai = db.RefPegawai.Where(y => y.Aktif == true).Include(r => r.RefUnitPJ)
                .OrderBy(r=>r.PegNIP);
            return View(refPegawai.ToList());
        }

        // GET: Pegawai
        public ActionResult Inaktif()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var refPegawai = db.RefPegawai.Where(y => y.Aktif == false).Include(r => r.RefUnitPJ)
                .OrderBy(r => r.PegNIP);
            return View(refPegawai.ToList());
        }

        public ActionResult Deactivate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefPegawai pegawai = db.RefPegawai.Find(id);
            if (pegawai == null)
            {
                return HttpNotFound();
            }
            pegawai.Aktif = false;
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

            RefPegawai pegawai = db.RefPegawai.Find(id);
            if (pegawai == null)
            {
                return HttpNotFound();
            }
            pegawai.Aktif = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Pegawai/Details/5
        public ActionResult Details(int? id)
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
            RefPegawai refPegawai = db.RefPegawai.Find(id);
            if (refPegawai == null)
            {
                return HttpNotFound();
            }
            return View(refPegawai);
        }

        // GET: Pegawai/Create
        public ActionResult Create()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.PegUnitID = new SelectList(db.RefUnitPJ.Where(y=>y.Aktif==true), "ID", "Detail");
            return View();
        }

        // POST: Pegawai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PegName,PegNIP,PegUnitID,PegEmailKemenkeu")] RefPegawai refPegawai)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                refPegawai.Aktif = true;
                db.RefPegawai.Add(refPegawai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PegUnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refPegawai.PegUnitID);
            return View(refPegawai);
        }

        // GET: Pegawai/Edit/5
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
            RefPegawai refPegawai = db.RefPegawai.Find(id);
            if (refPegawai == null)
            {
                return HttpNotFound();
            }
            ViewBag.PegUnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refPegawai.PegUnitID);
            return View(refPegawai);
        }

        // POST: Pegawai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PegName,PegNIP,PegUnitID,PegEmailKemenkeu")] RefPegawai refPegawai)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(refPegawai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PegUnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", refPegawai.PegUnitID);
            return View(refPegawai);
        }

        // GET: Pegawai/Delete/5
        public ActionResult Delete(int? id)
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
            RefPegawai refPegawai = db.RefPegawai.Find(id);
            if (refPegawai == null)
            {
                return HttpNotFound();
            }
            return View(refPegawai);
        }

        // POST: Pegawai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefPegawai refPegawai = db.RefPegawai.Find(id);
            db.RefPegawai.Remove(refPegawai);
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
