//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ira.Models;
//using System.Web.Security;

//namespace ira.Controllers
//{
//    [Authorize]
//    public class ObjekController : Controller
//    {
//        private IRADbContext db = new IRADbContext();
//        private ApplicationDbContext _db = new ApplicationDbContext();

//        // GET: Objek
//        public ActionResult Index()
//        {
//            MembershipUser active = Membership.GetUser(User.Identity.Name);

//            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

//            if (currentuser.RoleID != 1|| currentuser.RoleID != 2)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            if (currentuser.RoleID == 1)
//            {
//                var refUniverseAudit = db.RefUniverseAudit.Where(y => y.Aktif).Include(r => r.RefUnitPJ);
//                return View(refUniverseAudit.ToList());
//            }

//            var refUniverseAudit1 = db.RefUniverseAudit.Where(y => y.UnitID == currentuser.UnitID && y.Aktif).Include(r => r.RefUnitPJ);
//            return View(refUniverseAudit1.ToList());
//        }

//        // GET: Objek/Details/5
//        public ActionResult Details(int id)
//        {
//            MembershipUser active = Membership.GetUser(User.Identity.Name);

//            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

//            if (currentuser.RoleID != 1 || currentuser.RoleID != 2)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            RefUniverseAudit refUniverseAudit = db.RefUniverseAudit.Find(id);
//            if (refUniverseAudit == null)
//            {
//                return RedirectToAction("Index");
//            }

//            if (refUniverseAudit.UnitID != currentuser.UnitID&&currentuser.RoleID!=1)
//            {
//                return RedirectToAction("Index");
//            }

//            return View(refUniverseAudit);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
