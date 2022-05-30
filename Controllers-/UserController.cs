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
using Microsoft.Owin.Security;

namespace ira.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IRADbContext db = new IRADbContext();

        private ApplicationDbContext _db = new ApplicationDbContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult roleTabs()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var roles = db.RefRole.ToList();

            return PartialView("_roleTabs", roles);
        }

        public ActionResult userList(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefRole role = db.RefRole.Find(id);

            if (role == null)
            {
                return RedirectToAction("NotFound", "Home", null);
            }

            var users = _db.Users.Where(y => y.isRevoked == false && y.RoleID == role.ID).ToList();

            return PartialView("_usersList", users);
        }

        public ActionResult Revoked()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            if (currentuser.isRevoked)
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Login", "Account");
            }

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult roleTabsRevoked()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var roles = db.RefRole.ToList();

            return PartialView("_roleTabsRevoked", roles);
        }

        public ActionResult userListRevoked(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefRole role = db.RefRole.Find(id);

            if (role == null)
            {
                return RedirectToAction("NotFound", "Home", null);
            }

            var users = _db.Users.Where(y => y.isRevoked == true && y.RoleID == role.ID).ToList();

            return PartialView("_usersListRevoked", users);
        }

        public ActionResult userUnit(int id)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (unit == null)
            {
                return RedirectToAction("NotFound", "Home", null);
            }

            return PartialView("_userUnit", unit);
        }

        //public ActionResult RevokeConfirmation(string id)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    ApplicationUser user = _db.Users.Find(Encoding.Unicode.GetString(Convert.FromBase64String(id)));

        //    ViewBag.confirmation = "Are you sure you want to revoke "+user.FirstName+" "+user.LastName+"'s access? The revoked user will no longer be able to log into the app.";

        //    ViewBag.token = id;

        //    return PartialView("_confirmRevoke");
        //}

        //public ActionResult Revoke(string id)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //                var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

        //    if (currentuser.RoleID != 1)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    ApplicationUser user = _db.Users.Find(Encoding.Unicode.GetString(Convert.FromBase64String(id)));

        //    user.isRevoked = true;
        //    _db.SaveChanges();

        //    return PartialView("_successRevoke");
        //}

        public ActionResult Revoke(string token)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            ApplicationUser user = _db.Users.Find(Encoding.Unicode.GetString(Convert.FromBase64String(token)));

            if (user == null)
            {
                return HttpNotFound();
            }

            user.isRevoked = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string token)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            ApplicationUser user = _db.Users.Find(Encoding.Unicode.GetString(Convert.FromBase64String(Encoding.Unicode.GetString(Convert.FromBase64String(token)))));

            if (user == null)
            {
                return HttpNotFound();
            }

            EditUserViewModel data = new EditUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UnitID = user.UnitID,
                RoleID = user.RoleID,
                Id = token
            };

            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", data.UnitID);
            ViewBag.RoleID = new SelectList(db.RefRole, "ID", "Detail", data.RoleID);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FirstName,LastName,RoleID,UnitID,Email,Id")] EditUserViewModel user)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                        var currentuser = manager.FindById(User.Identity.GetUserId()); if(currentuser.isRevoked){return RedirectToAction("Logout","Account",null);}

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Login", "Account");
            }

            ApplicationUser target = _db.Users.Find(Encoding.Unicode.GetString(Convert.FromBase64String(Encoding.Unicode.GetString(Convert.FromBase64String(user.Id)))));

            if (target == null)
            {
                return RedirectToAction("NotFound", "Home", null);
            }

            if (ModelState.IsValid)
            {
                target.FirstName = user.FirstName;
                target.LastName = user.LastName;
                target.Email = user.Email;
                target.UnitID = user.UnitID;
                target.RoleID = user.RoleID;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UnitID = new SelectList(db.RefUnitPJ.Where(y => y.Aktif == true), "ID", "Detail", user.UnitID);
            ViewBag.RoleID = new SelectList(db.RefRole, "ID", "Detail", user.RoleID);

            return View(user);
        }
    }
}