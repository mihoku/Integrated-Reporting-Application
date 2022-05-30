//using System.Net.Mail;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ira.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System.Net;
//using Microsoft.Owin.Security;

//namespace ira.Web.Utilities
//{
//    public class Authenticator
//    {
//        private static IRADbContext db = new IRADbContext();

//        private static ApplicationDbContext _db = new ApplicationDbContext();

//        //private IAuthenticationManager AuthenticationManager { get; }

//        public static void authenticator(string userId)
//        {
//                        MembershipUser active = Membership.GetUser(User.Identity.Name);

//            var currentuser = manager.FindById(userId);

//            //if (currentuser.isRevoked)
//            //{
//            //    //AuthenticationManager.SignOut();
//            //}

//        }

//    }
//}