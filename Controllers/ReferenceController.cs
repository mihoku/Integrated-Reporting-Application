using ira.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace ira.Controllers
{
    [Authorize]
    public class ReferenceController : Controller
    {
        // GET: Reference

        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Flash()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult PopUp()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var refPopup = db.RefPopUpText.Where(y => y.ModulID == 1).FirstOrDefault();
            ViewBag.ModulID = 1;
            return View(refPopup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PopUp([Bind(Include = "ID,Message,Airing")] RefPopupText refPopup)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(refPopup).State = EntityState.Modified;
                var Popup = db.RefPopUpText.Where(y => y.ModulID == 1).FirstOrDefault();
                Popup.Airing = refPopup.Airing;
                Popup.ModulID = 1;
                Popup.Message = refPopup.Message;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModulID = 1;
            return View(refPopup);
        }

        //public ActionResult Autocomplete (string term) {
        //    var refPegawai = db.RefPegawai
        //        .Where(r => r.PegName.Contains(term))
        //        //.Take(10)
        //        .Select(r => new
        //    {
        //        label = r.PegName
        //    });

        //    return Json(refPegawai, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Pegawai(string searchTerm = null, int page = 1)
        ////public ActionResult Pegawai()
        //{
        //    //var refPegawai = db.RefPegawai.ToList();
        //    var refPegawai = db.RefPegawai
        //        .OrderBy(i=>i.ID)
        //        .Where(r => searchTerm == null || r.PegName.Contains(searchTerm) || r.RefUnitPJ.Detail.Contains(searchTerm) || r.PegNIP.Contains(searchTerm) || r.PegEmailKemenkeu.Contains(searchTerm))
        //        .Select(r => new PegawaiListViewModel
        //    {
        //        ID = r.ID,
        //        PegName = r.PegName,
        //        PegNIP = r.PegNIP,
        //        PegEmailKemenkeu = r.PegEmailKemenkeu,
        //        RefUnitPJ = r.RefUnitPJ.Detail
        //    }).ToPagedList(page, 10);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_PegawaiList", refPegawai);
        //    }

        //    return View(refPegawai);
        //}
    }
}