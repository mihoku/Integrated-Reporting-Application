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
    public class ScheduleController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Schedule
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule LastRecord = db.TransSchedule.OrderByDescending(p=>p.Tahun).FirstOrDefault();

            if (LastRecord == null)
            {
                TransSchedule feedData = new TransSchedule
                {
                    Title = "Core Entry",
                    Tahun = DateTime.Now.Year-1,
                    Locked = 3
                };

                db.TransSchedule.Add(feedData);
                db.SaveChanges();

                TransSchedule LastRecordnew = db.TransSchedule.OrderByDescending(p => p.Tahun).FirstOrDefault();

                ViewBag.Tahun = LastRecordnew.Tahun + 1;

                return View(db.TransSchedule.Where(p => p.Locked == 1 || p.Locked == 0).ToList());
            }

            ViewBag.Tahun = LastRecord.Tahun + 1;

            return View(db.TransSchedule.Where(p=>p.Locked==1||p.Locked==0).ToList());
        }

        // GET: Schedule/Details/5
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
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            TransSchedule transSchedule = db.TransSchedule.Find(id);
            if (transSchedule == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.Schedule = transSchedule.Title;
            ViewBag.Tahun = transSchedule.Tahun;
            ViewBag.Locked = transSchedule.Locked;
            ViewBag.ScheduleID = transSchedule.ID;
            if (transSchedule.Locked != 0 && transSchedule.Locked != 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (transSchedule.Locked == 0)
            {
                return View(db.RefUnitPJ
                    .Where(y=>y.Aktif==true)
                    //.OrderBy(y=>y.Detail)
                    .ToList());
            }
            return View(db.RefUnitPJ.Include(y=>y.RefTPU).ToList());
        }

        //// GET: Schedule/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Schedule/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Title,Tahun,Locked")] TransSchedule transSchedule)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TransSchedule.Add(transSchedule);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(transSchedule);
        //}

        //// GET: Schedule/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    TransSchedule transSchedule = db.TransSchedule.Find(id);
        //    if (transSchedule == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transSchedule);
        //}

        //// POST: Schedule/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Title,Tahun,Locked")] TransSchedule transSchedule)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(transSchedule).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(transSchedule);
        //}

        // GET: Schedule/Delete/5
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
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            TransSchedule transSchedule = db.TransSchedule.Find(id);
            if (transSchedule == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transSchedule);
        }

        public ActionResult Lock( int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule schedule = db.TransSchedule.Find(id);
            if (schedule == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            schedule.Locked = 1;
            schedule.Tahun = schedule.Tahun;
            schedule.Title = schedule.Title;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Unlock(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule schedule = db.TransSchedule.Find(id);
            if (schedule == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            schedule.Locked = 0;
            schedule.Tahun = schedule.Tahun;
            schedule.Title = schedule.Title;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Schedule/Delete/5
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

            TransSchedule transSchedule = db.TransSchedule.Find(id);
            db.TransSchedule.Remove(transSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Schedule([Bind(Include = "ID,Title,Tahun,Locked")] TransSchedule transSchedule)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                TransSchedule LastRecord = db.TransSchedule.OrderByDescending(p => p.Tahun).FirstOrDefault();

                transSchedule.Tahun = LastRecord.Tahun + 1;
                transSchedule.Title = "Master Tema Pengawasan Tahun " + transSchedule.Tahun;
                transSchedule.Locked = 0;
                db.TransSchedule.Add(transSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transSchedule);
        }

        //public ActionResult Schedule()
        //{

        //    var dbEntry = db.TransSchedule.Create();
        //    dbEntry.Tahun = 2014;
        //    dbEntry.Title = "Tema Pengawasan Unggulan " + dbEntry.Tahun;
        //    dbEntry.Locked = 0;
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
