using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ira.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ira.Web.Utilities;
using System.Web.Security;

namespace ira.Controllers
{
    [Authorize]
    public class SendMailController : Controller
    {
        private IRADbContext db = new IRADbContext();
        private ApplicationDbContext _db = new ApplicationDbContext();

        ////GET: /SendMail/Config
        //public ActionResult Config()
        //{
        //    //ConfigMail configMail = db.ConfigMail.OrderByDescending(y=>y.ID).FirstOrDefault();
        //    //if (configMail == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}

        //    //ConfigMailView data = new ConfigMailView
        //    //{
        //    //    ID = configMail.ID,
        //    //    Email = configMail.Email,
        //    //    Password = configMail.Password
        //    //};

        //    return View();
        //}

        //public ActionResult loadConfig()
        //{
        //    ConfigMail configMail = db.ConfigMail.OrderByDescending(y => y.ID).FirstOrDefault();
        //    if (configMail == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    //ConfigMailView data = new ConfigMailView
        //    //{
        //    //    ID = configMail.ID,
        //    //    Email = configMail.Email,
        //    //    Password = configMail.Password
        //    //};

        //    return PartialView("_loadConfig", configMail);
        //}

        // POST: Mail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ActionName("Config")]
        //[ValidateAntiForgeryToken]
        //public ActionResult loadConfig([Bind(Include = "ID,Email,Password")] ConfigMail configMail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.Entry(configMail).State = EntityState.Modified;
        //        ConfigMail config = db.ConfigMail.Find(configMail.ID);
        //        config.Email = configMail.Email;
        //        config.Password = configMail.Password;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(configMail);
        //}

        //GET: /SendMail/Config
        //public ActionResult loadAdvancedConfig(int id)
        //{
        //    ConfigMail config = db.ConfigMail.Find(id);

        //    if (config == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView("_loadAdvancedConfig", config);
        //}

        //[HttpPost]
        //[ActionName("Config")]
        //[ValidateAntiForgeryToken]
        //public ActionResult loadAdvancedConfig([Bind(Include = "ID,Email,Password,Host,Port,isBodyHtml,useDefaultCredential,enableSSL")] ConfigMail configMail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ConfigMail config = db.ConfigMail.Find(configMail.ID);
        //        config.Email = configMail.Email;
        //        config.Password = configMail.Password;
        //        config.isBodyHtml = configMail.isBodyHtml;
        //        config.Port = configMail.Port;
        //        config.Host = configMail.Host;
        //        config.useDefaultCredential = configMail.useDefaultCredential;
        //        config.enableSSL = configMail.enableSSL;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return HttpNotFound();
        //}

        public ActionResult Config()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Progress", "Home", null);
            }

            ConfigMail config = db.ConfigMail.OrderByDescending(y => y.ID).FirstOrDefault();

            if (config == null)
            {
                return HttpNotFound();
            }

            return View(config);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Config([Bind(Include = "ID,Email,Password,Host,Port,isBodyHtml,useDefaultCredential,enableSSL")] ConfigMail configMail)
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Progress", "Home", null);
            }

            if (ModelState.IsValid)
            {
                ConfigMail config = db.ConfigMail.Find(configMail.ID);
                config.Email = configMail.Email;
                config.Password = configMail.Password;
                config.isBodyHtml = configMail.isBodyHtml;
                config.Port = configMail.Port;
                config.Host = configMail.Host;
                config.useDefaultCredential = configMail.useDefaultCredential;
                config.enableSSL = configMail.enableSSL;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return HttpNotFound();
        }

        // GET: /SendMail/  
        public ActionResult Index()
        {
                        MembershipUser active = Membership.GetUser(User.Identity.Name);

                                    var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1)
            {
                return RedirectToAction("Progress", "Home", null);
            }

            return View();
        }  

        //[HttpPost]
        //public ViewResult Index(MailModel _objModelMail)
        //{
        //                MembershipUser active = Membership.GetUser(User.Identity.Name);

        //                            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

        //    if (currentuser.RoleID != 1)
        //    {
        //        return View("Progress", "Home", null);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        //MailMessage mail = new MailMessage();
        //        //mail.To.Add(_objModelMail.To);
        //        //mail.From = new MailAddress(_objModelMail.From.ToString());
        //        //mail.Subject = _objModelMail.Subject;
        //        //string Body = _objModelMail.Body;
        //        //mail.Body = "<b>"+Body+"</b>";
        //        //mail.IsBodyHtml = true;
        //        //SmtpClient smtp = new SmtpClient();
        //        //smtp.Host = "smtp.depkeu.go.id";
        //        //smtp.Port = 25;
        //        //smtp.UseDefaultCredentials = false;
        //        //smtp.Credentials = new System.Net.NetworkCredential
        //        //("teammate.itjen", "P4$$w0rd");// Enter seders User name and password
        //        //smtp.EnableSsl = false;
        //        //smtp.Send(mail);
        //        MailSender.Notify(_objModelMail);
        //        return View("Index", _objModelMail);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
    }
}
