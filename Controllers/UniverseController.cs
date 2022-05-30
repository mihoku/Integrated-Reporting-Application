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
    public class UniverseController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Eselon1
        public ActionResult Index()
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID == 1)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == true).Include(y => y.RefUnitPJ).ToList());
            }

            else if (currentuser.RoleID == 2)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif && y.RefUnitPJ.ID == currentuser.UnitID).Include(y => y.RefUnitPJ).ToList());
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Eselon1 with inaktif status
        public ActionResult Inaktif()
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID == 1)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == false).Include(y => y.RefUnitPJ).ToList());
            }

            else if (currentuser.RoleID == 2)
            {
                return View(db.RefUniverseAudit.Where(y => y.Aktif == false && y.RefUnitPJ.ID == currentuser.UnitID).Include(y => y.RefUnitPJ).ToList());
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Deactivate(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1&&currentuser.RoleID!=2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUniverseAudit unit = db.RefUniverseAudit.Find(id);
            if (unit == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.UnitID != unit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
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

            if (currentuser.RoleID != 1&&currentuser.RoleID!=2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUniverseAudit unit = db.RefUniverseAudit.Find(id);
            if (unit == null)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            if (currentuser.UnitID != unit.UnitID)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            unit.Aktif = true;
            unit.Ket = unit.Ket;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: Universe/Details/5
        public ActionResult Details(int id)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUniverseAudit refUniverseAudit = db.RefUniverseAudit.Find(id);
            if (refUniverseAudit == null)
            {
                return RedirectToAction("Index");
            }

            if (refUniverseAudit.UnitID != currentuser.UnitID && currentuser.RoleID != 1)
            {
                return RedirectToAction("Index");
            }

            return View(refUniverseAudit);
        }

        // GET: Eselon1/Create
        public ActionResult Create()
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            RefUnitPJ myUnit = db.RefUnitPJ.Find(currentuser.UnitID);

            if (currentuser.RoleID != 1&&currentuser.RoleID!=2)
            {
                return RedirectToAction("Login", "Account");
            }

            else if (currentuser.RoleID!=1&&myUnit.isPrimeMover)
            {
                return RedirectToAction("Progress", "Home", null); 
            }

            else if (currentuser.RoleID == 1)
            {
                ViewBag.enableUnitSelection = true;
                ViewBag.Aktif = true;
                ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif && !y.isPrimeMover), "ID", "Detail");
                return View();
            }

            else
            {
                ViewBag.enableUnitSelection = false;
                RefUnitPJ unit = db.RefUnitPJ.Find(currentuser.UnitID);
                if (unit == null || !unit.Aktif)
                {
                    return RedirectToAction("NotFound", "ErrorPage", null);
                }
                ViewBag.UnitID = unit.ID;
                ViewBag.Aktif = true;
                ViewBag.UnitDetail = unit.Detail;
                return View();
            }
        }

        // POST: Eselon1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ket,Aktif,UnitID")] RefUniverseAudit refUniverseAudit)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if (currentuser.RoleID == 1)
                {
                    refUniverseAudit.Aktif = true;
                    db.RefUniverseAudit.Add(refUniverseAudit);
                    db.SaveChanges();

                    TransNDPermintaan nd = db.TransNDPermintaan.Where(y => !y.Locked).OrderByDescending(y=>y.TransSchedule.Tahun).FirstOrDefault();
                    //check if there is a currently unlocked ND Permintaan
                    if (nd != null)
                    {
                        TransIkhtisarProgress data = new TransIkhtisarProgress
                        {
                            PKPTID = nd.PKPTID,
                            SysTglEntry = DateTime.Now,
                            SysUsername = currentuser.UserName,
                            SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                            PeriodeID = nd.PeriodeID,
                            UniverseID = refUniverseAudit.ID
                        };

                        db.TransIkhtisarProgresses.Add(data);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                refUniverseAudit.UnitID = currentuser.UnitID;
                refUniverseAudit.Aktif = true;
                db.RefUniverseAudit.Add(refUniverseAudit);
                db.SaveChanges();

                TransNDPermintaan nd2 = db.TransNDPermintaan.Where(y => !y.Locked).OrderByDescending(y => y.TransSchedule.Tahun).FirstOrDefault();
                //check if there is a currently unlocked ND Permintaan
                if (nd2 != null)
                {
                    TransIkhtisarProgress data = new TransIkhtisarProgress
                    {
                        PKPTID = nd2.PKPTID,
                        SysTglEntry = DateTime.Now,
                        SysUsername = currentuser.UserName,
                        SysWorkstation = Request.ServerVariables["REMOTE_HOST"],
                        PeriodeID = nd2.PeriodeID,
                        UniverseID = refUniverseAudit.ID
                    };

                    db.TransIkhtisarProgresses.Add(data);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            if (currentuser.RoleID == 1)
            {
                ViewBag.enableUnitSelection = true;
                ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif && !y.isPrimeMover), "ID", "Detail", refUniverseAudit.UnitID);
                ViewBag.Aktif = true;
                return View(refUniverseAudit);
            }

            ViewBag.enableUnitSelection = false;
            RefUnitPJ unit = db.RefUnitPJ.Find(currentuser.UnitID);
            ViewBag.UnitID = unit.ID;
            ViewBag.Aktif = true;
            ViewBag.UnitDetail = unit.Detail;
            return View(refUniverseAudit);
        }

        // GET: Eselon1/Edit/5
        public ActionResult Edit(int id)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == null)
            //{
            //    return RedirectToAction("Index");
            //}
            RefUniverseAudit refUniverseAudit = db.RefUniverseAudit.Find(id);
            if (refUniverseAudit == null || !refUniverseAudit.Aktif)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Aktif = refUniverseAudit.Aktif;
            ViewBag.UnitID = refUniverseAudit.UnitID;
            return View(refUniverseAudit);
        }

        // POST: Eselon1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ket,UnitID,Aktif")] RefUniverseAudit refUniverseAudit)
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                RefUniverseAudit selecteddata = db.RefUniverseAudit.Find(refUniverseAudit.ID);
                //selecteddata.Ket = refUniverseAudit.Ket;
                refUniverseAudit.UnitID = selecteddata.UnitID;
                refUniverseAudit.Aktif = selecteddata.Aktif;
                //db.Entry(refUniverseAudit).State = EntityState.Modified;
                db.Entry(selecteddata).CurrentValues.SetValues(refUniverseAudit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Aktif = refUniverseAudit.Aktif;
            ViewBag.UnitID = refUniverseAudit.UnitID;
            return View(refUniverseAudit);
        }

        public ActionResult loadIkhtisar(int id)
        {
            TransIkhtisarProgress ikhtisar = db.TransIkhtisarProgresses.Find(id);

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PeriodeID == ikhtisar.PeriodeID && y.PKPTID == ikhtisar.PKPTID).OrderByDescending(y => y.ID).FirstOrDefault();

            ViewBag.nd = nd.ID;

            return PartialView("_loadIkhtisar", ikhtisar);
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
