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
    public class OutputController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Output
        //public ActionResult Index()
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var transKegiatanOutput = db.TransKegiatanOutput.Include(t => t.RefKegiatan);
        //    return View(transKegiatanOutput.ToList());
        //}

         //GET: Output/Details/5
        public ActionResult Details(int? id)
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
            TransKegiatanOutput transKegiatanOutput = db.TransKegiatanOutput.Find(id);
            if (transKegiatanOutput == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transKegiatanOutput);
        }

        // GET: Output/Create
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
            RefKegiatan keg = db.RefKegiatan.Find(id);
            if (keg == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (keg.Finalize == 1 || keg.RefTPU.Finalize == 1 || keg.RefTPU.TransSchedule.Locked == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.KegiatanID = id;
            ViewBag.KegName = keg.KegName;
            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis.Where(y=>y.Aktif==true), "ID", "Ket");
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: Output/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OutputJenisID,Nomor,TanggalTerbit,Judul,Uraian,KegiatanID,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanOutput transKegiatanOutput)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            } 
            
            if (ModelState.IsValid)
            {
                db.TransKegiatanOutput.Add(transKegiatanOutput);
                db.SaveChanges();
                return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanOutput.KegiatanID });
            }
            ViewBag.KegName = transKegiatanOutput.RefKegiatan.KegName;
            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis.Where(y=>y.Aktif==true), "ID", "Ket", transKegiatanOutput.OutputJenisID);
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanOutput);
        }

        // GET: Output/Edit/5
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
            TransKegiatanOutput transKegiatanOutput = db.TransKegiatanOutput.Find(id);
            if (transKegiatanOutput == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (transKegiatanOutput.RefKegiatan.Finalize == 1 || transKegiatanOutput.RefKegiatan.RefTPU.Finalize == 1 || transKegiatanOutput.RefKegiatan.RefTPU.TransSchedule.Locked == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis.Where(y => y.Aktif == true || y.ID == transKegiatanOutput.OutputJenisID), "ID", "Ket", transKegiatanOutput.OutputJenisID);
            return View(transKegiatanOutput);
        }

        // POST: Output/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OutputJenisID,Nomor,TanggalTerbit,Uraian,KegiatanID,Judul,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanOutput transKegiatanOutput)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(transKegiatanOutput).State = EntityState.Modified;
                var dbOutput = db.TransKegiatanOutput.FirstOrDefault(p => p.ID == transKegiatanOutput.ID);
                if (dbOutput == null)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                dbOutput.Nomor = transKegiatanOutput.Nomor;
                dbOutput.Uraian = transKegiatanOutput.Uraian;
                dbOutput.OutputJenisID = transKegiatanOutput.OutputJenisID;
                db.SaveChanges();
                dbOutput.SysUsername = User.Identity.GetUserName();
                dbOutput.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
                dbOutput.SysTglEntry = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis.Where(y => y.Aktif == true || y.ID == transKegiatanOutput.OutputJenisID), "ID", "Ket", transKegiatanOutput.OutputJenisID);
            return View(transKegiatanOutput);
        }

        // GET: Output/Delete/5
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
            TransKegiatanOutput transKegiatanOutput = db.TransKegiatanOutput.Find(id);
            if (transKegiatanOutput == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            if (transKegiatanOutput.RefKegiatan.Finalize == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            return View(transKegiatanOutput);
        }

        // POST: Output/Delete/5
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

            TransKegiatanOutput transKegiatanOutput = db.TransKegiatanOutput.Find(id);
            db.TransKegiatanOutput.Remove(transKegiatanOutput);
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
