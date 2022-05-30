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

namespace ira.Controllers
{
    public class OutputFlashController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: OutputFlash
        //public ActionResult Index()
        //{
        //    var transFlashKegiatanOutputs = db.TransFlashKegiatanOutputs.Include(t => t.RefKegiatanOutputJenis).Include(t => t.TransFlashKegiatan);
        //    return View(transFlashKegiatanOutputs.ToList());
        //}

        // GET: OutputFlash/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("NotFound", "Error", null);
        //    }
        //    TransFlashKegiatanOutput transFlashKegiatanOutput = db.TransFlashKegiatanOutputs.Find(id);
        //    if (transFlashKegiatanOutput == null)
        //    {
        //        return RedirectToAction("NotFound", "Error", null);
        //    }
        //    return View(transFlashKegiatanOutput);
        //}

        // GET: JenisOutput
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefKegiatanOutputJenis.Where(y => y.Aktif == true).ToList());
        }

        // GET: JenisOutput with inaktif status
        public ActionResult Inaktif()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(db.RefKegiatanOutputJenis.Where(y => y.Aktif == false).ToList());
        }

        public ActionResult Deactivate(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

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
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

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

        // GET: OutputFlash/Create
        public ActionResult Create( int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatan keg = db.TransFlashKegiatan.Find(id);
            if (keg == null)
            {
                return RedirectToAction("NotFound", "Error", null);
            }
            if (keg.Finalize == 3)
            {
                return RedirectToAction("NotFound", "Error", null);
            }

            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis, "ID", "Ket");
            ViewBag.KegName = keg.Judul;
            ViewBag.KegiatanID = id;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View();
        }

        // POST: OutputFlash/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nomor,TanggalTerbit,Judul,Uraian,KegiatanID,OutputJenisID,SysUsername,SysTglEntry,SysWorkstation")] TransFlashKegiatanOutput transFlashKegiatanOutput)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.TransFlashKegiatanOutputs.Add(transFlashKegiatanOutput);
                db.SaveChanges();
                return RedirectToAction("Details", "Flash", new { id=transFlashKegiatanOutput.KegiatanID });
            }

            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis, "ID", "Ket", transFlashKegiatanOutput.OutputJenisID);
            ViewBag.KegName = transFlashKegiatanOutput.TransFlashKegiatan.Judul;
            ViewBag.KegiatanID = transFlashKegiatanOutput.KegiatanID;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transFlashKegiatanOutput);
        }

        // GET: OutputFlash/Edit/5
        public ActionResult Edit(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatanOutput transFlashKegiatanOutput = db.TransFlashKegiatanOutputs.Find(id);
            if (transFlashKegiatanOutput == null)
            {
                return RedirectToAction("NotFound", "Error", null);
            }
            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis, "ID", "Ket", transFlashKegiatanOutput.OutputJenisID);
            return View(transFlashKegiatanOutput);
        }

        // POST: OutputFlash/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nomor,TanggalTerbit,Judul,Uraian,KegiatanID,OutputJenisID,SysUsername,SysTglEntry,SysWorkstation")] TransFlashKegiatanOutput transFlashKegiatanOutput)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Entry(transFlashKegiatanOutput).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OutputJenisID = new SelectList(db.RefKegiatanOutputJenis, "ID", "Ket", transFlashKegiatanOutput.OutputJenisID);
            return View(transFlashKegiatanOutput);
        }

        // GET: OutputFlash/Delete/5
        public ActionResult Delete(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("NotFound", "Error", null);
            }
            TransFlashKegiatanOutput transFlashKegiatanOutput = db.TransFlashKegiatanOutputs.Find(id);
            if (transFlashKegiatanOutput == null)
            {
                return RedirectToAction("NotFound", "Error", null);
            }
            return View(transFlashKegiatanOutput);
        }

        // POST: OutputFlash/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashKegiatanOutput transFlashKegiatanOutput = db.TransFlashKegiatanOutputs.Find(id);
            db.TransFlashKegiatanOutputs.Remove(transFlashKegiatanOutput);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: JenisOutput/Create
        public ActionResult Construct()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

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
        public ActionResult Construct([Bind(Include = "ID,Ket,Aktif")] RefKegiatanOutputJenis refKegiatanOutputJenis)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

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
        public ActionResult Amend(int? id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

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
        public ActionResult Amend([Bind(Include = "ID,Ket,Aktif")] RefKegiatanOutputJenis refKegiatanOutputJenis)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

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
