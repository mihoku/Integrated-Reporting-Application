using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ira.Models;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace ira.Controllers
{
    [Authorize]
    public class STController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        private ViewSTEntities db1 = new ViewSTEntities();

        // GET: ST
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            var refST = db1.v_ira_st
                .Where(st=>st.tahunst==DateTime.Now.Year);
            return View(refST.ToList());
        }

        public ActionResult Create(int id) 
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            var keg = db.RefKegiatan.Find(id);

            var refST = db1.v_ira_st
                .Where(st => st.tahunst == keg.RefTPU.TransSchedule.Tahun);

            ViewBag.keg = keg.KegName;
            ViewBag.kegID = keg.ID;

            return View(refST.ToList());
        }

        public ActionResult Confirm(int? id, string token)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!id.HasValue)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }
            
            var keg = db.RefKegiatan.Find(id);

            var refST = db1.v_ira_st.Find(Encoding.Unicode.GetString(Convert.FromBase64String(token)));

            if (keg == null || refST == null || keg.Finalize == 1||keg.RefTPU.Finalize==1||keg.RefTPU.TransSchedule.Locked==1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            //var encode = Convert.ToBase64String(Encoding.Unicode.GetBytes(st));
            //var decode = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(encode));

            //ViewBag.maling = Convert.ToBase64String(Encoding.Unicode.GetBytes(st));
            //ViewBag.rampok = decode;

            ViewBag.kegID = keg.ID;
            ViewBag.keg = keg.KegName;
            ViewBag.NoST = refST.nost;
            ViewBag.JudulST = refST.halst;
            ViewBag.TanggalST = refST.tglst;
            ViewBag.TahunST = refST.tahunst;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "ID,NoST,JudulST,Tahun,TanggalST,KegiatanID,TglAwal,TglAkhir,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanST transKegiatanST)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.TransKegiatanST.Add(transKegiatanST);
                db.SaveChanges();
                return RedirectToAction("Details", "Kegiatan", new { id=transKegiatanST.KegiatanID});
            }

            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanST);
        }

        public ActionResult AddST(int id)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            var keg = db.RefKegiatan.Find(id);

            //var refST = db1.v_ira_st.Find(Encoding.Unicode.GetString(Convert.FromBase64String(token)));

            if (keg == null || keg.Finalize == 1 || keg.RefTPU.Finalize == 1 || keg.RefTPU.TransSchedule.Locked == 1)
            {
                return RedirectToAction("NotFound", "ErrorPage", null);
            }

            //var encode = Convert.ToBase64String(Encoding.Unicode.GetBytes(st));
            //var decode = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(encode));

            //ViewBag.maling = Convert.ToBase64String(Encoding.Unicode.GetBytes(st));
            //ViewBag.rampok = decode;

            ViewBag.kegID = keg.ID;
            ViewBag.keg = keg.KegName;
            //ViewBag.NoST = refST.nost;
            //ViewBag.JudulST = refST.halst;
            //ViewBag.TanggalST = refST.tglst;
            //ViewBag.TahunST = refST.tahunst;
            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddST([Bind(Include = "ID,NoST,JudulST,Tahun,TanggalST,KegiatanID,TglAwal,TglAkhir,SysUsername,SysTglEntry,SysWorkstation")] TransKegiatanST transKegiatanST)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.TransKegiatanST.Add(transKegiatanST);
                db.SaveChanges();
                return RedirectToAction("Details", "Kegiatan", new { id = transKegiatanST.KegiatanID });
            }

            ViewBag.SysUsername = User.Identity.GetUserName();
            ViewBag.SysWorkstation = Request.ServerVariables["REMOTE_HOST"];
            ViewBag.SysTglEntry = DateTime.Now;
            return View(transKegiatanST);
        }
    }
}