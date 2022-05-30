using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ira.Models
{
    [Authorize]
    public class SasaranController : Controller
    {
        private IRADbContext db = new IRADbContext();

        // GET: Sasaran
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            var transTPUTujuan = db.TransTPUTujuan.Include(t => t.RefTPU);
            return View(transTPUTujuan.ToList());
        }

        // GET: Sasaran/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            TransTPUTujuan transTPUTujuan = db.TransTPUTujuan.Find(id);
            if (transTPUTujuan == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transTPUTujuan);
        }

        // GET: Sasaran/Create
        public ActionResult Create(int ? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
             
            if (!id.HasValue)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
             //if (id == null)
             //{
             //    ViewBag.SysUsername = User.Identity.GetUserName();
             //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
             //    ViewBag.SysTglEntry = DateTime.Now;
             //    ViewBag.TPUID = id;
             //    return View();
             //}
             RefTPU tpu = db.RefTPU.Find(id);
             if (tpu == null)
             {
                 return RedirectToAction("NotFound", "ErrorPage", null);
             }
           ViewBag.TPUID = id;
           ViewBag.TPUJudul = tpu.TPUName;
           ViewBag.SysUsername = User.Identity.GetUserName();
           ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
           ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: Sasaran/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TujuanTPU,TPUID,SysUsername,SysTglEntry,SysWorkstation")] TransTPUTujuan transTPUTujuan)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                db.TransTPUTujuan.Add(transTPUTujuan);
                db.SaveChanges();
                return RedirectToAction("Details", "TPU", new { id=transTPUTujuan.TPUID });
            }

            ViewBag.TPUID = transTPUTujuan.TPUID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transTPUTujuan);
        }

        // GET: Sasaran/Edit/5
        public ActionResult Edit(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (id == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            TransTPUTujuan transTPUTujuan = db.TransTPUTujuan.Find(id);
            if (transTPUTujuan == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.TPUID = new SelectList(db.RefTPU, "ID", "TPUName", transTPUTujuan.TPUID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transTPUTujuan);
        }

        // POST: Sasaran/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TujuanTPU,TPUID,SysUsername,SysTglEntry,SysWorkstation")] TransTPUTujuan transTPUTujuan)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                //db.Entry(transTPUTujuan).State = EntityState.Modified;
                var dbTarget = db.TransTPUTujuan.FirstOrDefault(p => p.ID == transTPUTujuan.ID);
                if (dbTarget == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                dbTarget.TujuanTPU = transTPUTujuan.TujuanTPU;
                db.SaveChanges();
                dbTarget.SysUsername = User.Identity.GetUserName();
                dbTarget.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbTarget.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", "TPU", new { id = transTPUTujuan.TPUID});
            }
            ViewBag.TPUID = new SelectList(db.RefTPU, "ID", "TPUName", transTPUTujuan.TPUID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transTPUTujuan);
        }

        // GET: Sasaran/Delete/5
        public ActionResult Delete(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            TransTPUTujuan transTPUTujuan = db.TransTPUTujuan.Find(id);
            if (transTPUTujuan == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transTPUTujuan);
        }

        // POST: Sasaran/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransTPUTujuan transTPUTujuan = db.TransTPUTujuan.Find(id);
            db.TransTPUTujuan.Remove(transTPUTujuan);
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
