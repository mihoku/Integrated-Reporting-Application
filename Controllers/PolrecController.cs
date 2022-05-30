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
    public class PolrecController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Polrec
        //public ActionResult Index()
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var transKegiatanPolrec = db.TransKegiatanPolrec.Include(t => t.RefKegiatan);
        //    return View(transKegiatanPolrec.ToList());
        //}

        //// GET: Polrec/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransKegiatanPolrec transKegiatanPolrec = db.TransKegiatanPolrec.Find(id);
        //    if (transKegiatanPolrec == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(transKegiatanPolrec);
        //}

        // GET: Polrec/Create
        public ActionResult Create( int id )
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefKegiatan keg = db.RefKegiatan.Find(id);
            if (keg == null || keg.Finalize == 1 || keg.RefTPU.Finalize == 1 || keg.RefTPU.TransSchedule.Locked == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.KegiatanID = id;
            ViewBag.KegName = keg.KegName;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: Polrec/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Judul,Uraian,KegiatanID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanPolrec transKegiatanPolrec)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.TransKegiatanPolrec.Add(transKegiatanPolrec);
                db.SaveChanges();
                return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanPolrec.KegiatanID});
            }

            ViewBag.KegiatanID = transKegiatanPolrec.KegiatanID;
            ViewBag.KegName = transKegiatanPolrec.RefKegiatan.KegName;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanPolrec);
        }

        // GET: Polrec/Edit/5
        public ActionResult Edit(int? id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransKegiatanPolrec transKegiatanPolrec = db.TransKegiatanPolrec.Find(id);
            if (transKegiatanPolrec == null)
            {
                return HttpNotFound();
            }
            if (transKegiatanPolrec.RefKegiatan.Finalize == 1)
            {
                return HttpNotFound();
            }
            ViewBag.KegiatanID = transKegiatanPolrec.KegiatanID;
            ViewBag.KegName = transKegiatanPolrec.RefKegiatan.KegName;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanPolrec);
        }

        // POST: Polrec/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Uraian,Judul,KegiatanID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanPolrec transKegiatanPolrec)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(transKegiatanPolrec).State = EntityState.Modified;
                var dbPolrec = db.TransKegiatanPolrec.FirstOrDefault(p => p.ID == transKegiatanPolrec.ID);
                if (dbPolrec == null)
                {
                    return HttpNotFound();
                }
                dbPolrec.Uraian = transKegiatanPolrec.Uraian;
                db.SaveChanges();
                dbPolrec.SysUsername = User.Identity.GetUserName();
                dbPolrec.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbPolrec.SysTglEntry = DateTime.Now;
                return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanPolrec.KegiatanID});
            }
            ViewBag.KegiatanID = transKegiatanPolrec.KegiatanID;
            ViewBag.KegName = transKegiatanPolrec.RefKegiatan.KegName;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanPolrec);
        }

        // GET: Polrec/Delete/5
        public ActionResult Delete(int? id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransKegiatanPolrec transKegiatanPolrec = db.TransKegiatanPolrec.Find(id);
            if (transKegiatanPolrec == null)
            {
                return HttpNotFound();
            }
            if (transKegiatanPolrec.RefKegiatan.Finalize == 1)
            {
                return HttpNotFound();
            }
            return View(transKegiatanPolrec);
        }

        // POST: Polrec/Delete/5
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

            TransKegiatanPolrec transKegiatanPolrec = db.TransKegiatanPolrec.Find(id);
            db.TransKegiatanPolrec.Remove(transKegiatanPolrec);
            db.SaveChanges();
            return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanPolrec.KegiatanID});
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
