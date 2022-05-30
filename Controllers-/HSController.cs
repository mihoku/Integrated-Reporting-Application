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

namespace ira.Controllers
{
    [Authorize]
    public class HSController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //// GET: HS
        //public ActionResult Index()
        //{
        //    var transHambatanSolusiKegiatan = db.TransHambatanSolusiKegiatan.Include(t => t.RefKegiatan);
        //    return View(transHambatanSolusiKegiatan.ToList());
        //}

        //// GET: HS/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TransKegiatanHS transKegiatanHS = db.TransHambatanSolusiKegiatan.Find(id);
        //    if (transKegiatanHS == null)
        //    {
        //        return RedirectToAction("NotFound", "ErrorPage", null);
        //    }
        //    return View(transKegiatanHS);
        //}

        // GET: HS/Create
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
            //    ViewBag.KegiatanID = id;
            //    ViewBag.SysUsername = "Mas Ganteng";
            //    ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            //    ViewBag.SysTglEntry = DateTime.Now;
            //    return View();
            //}
            RefKegiatan keg = db.RefKegiatan.Find(id);
            if (keg == null || keg.Finalize == 1 || keg.RefTPU.Finalize == 1 || keg.RefTPU.TransSchedule.Locked == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (keg.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.KegName = keg.KegName;
            ViewBag.KegiatanID = id;
            ViewBag.SysUsername = "Mas Ganteng";
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: HS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Hambatan,Solusi,KegiatanID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanHS transKegiatanHS)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.TransHambatanSolusiKegiatan.Add(transKegiatanHS);
                db.SaveChanges();
                return RedirectToAction("Details","Kegiatan", new { id=transKegiatanHS.KegiatanID });
            }

            ViewBag.SysUsername = "Mas Ganteng";
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanHS);
        }

        // GET: HS/Edit/5
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
            TransKegiatanHS transKegiatanHS = db.TransHambatanSolusiKegiatan.Find(id);
            if (transKegiatanHS == null || transKegiatanHS.RefKegiatan.Finalize == 1 || transKegiatanHS.RefKegiatan.RefTPU.Finalize == 1 || transKegiatanHS.RefKegiatan.RefTPU.TransSchedule.Locked == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (transKegiatanHS.RefKegiatan.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.KegName = transKegiatanHS.RefKegiatan.KegName;
            ViewBag.KegiatanID = transKegiatanHS.KegiatanID;
            ViewBag.SysUsername = "Mas Ganteng";
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanHS);
        }

        // POST: HS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Hambatan,Solusi,KegiatanID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanHS transKegiatanHS)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(transKegiatanHS).State = EntityState.Modified;
                var dbHS = db.TransHambatanSolusiKegiatan.FirstOrDefault(p => p.ID == transKegiatanHS.ID);
                if (dbHS == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                dbHS.Hambatan = transKegiatanHS.Hambatan;
                dbHS.Solusi = transKegiatanHS.Solusi;
                db.SaveChanges();
                dbHS.SysUsername = "Mas Ganteng";
                dbHS.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbHS.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", "Kegiatan", new { id=transKegiatanHS.KegiatanID});
            }

            ViewBag.SysUsername = "Mas Ganteng";
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanHS);
        }

        // GET: HS/Delete/5
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
            TransKegiatanHS transKegiatanHS = db.TransHambatanSolusiKegiatan.Find(id);
            if (transKegiatanHS == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (transKegiatanHS.RefKegiatan.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transKegiatanHS);
        }

        // POST: HS/Delete/5
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

            TransKegiatanHS transKegiatanHS = db.TransHambatanSolusiKegiatan.Find(id);
            db.TransHambatanSolusiKegiatan.Remove(transKegiatanHS);
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
