using ira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Globalization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace ira.Controllers
{
    public class HomeController : Controller
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

        //controller untuk menampilkan informasi umum tentang IRA di halaman home
        //start

        public ActionResult Index()
        {
            ViewBag.ProgressReport = "Menyajikan perkembangan kegiatan pengawasan secara umum yang dilakukan oleh tiap-tiap unit di lingkungan Inspektorat Jenderal sesuai dengan Tema Pengawasan Unggulan. Data yang akan dituangkan dalam Progress Report bersumber dari laporan masing-masing unit Inspektorat/Bagian.";

            ViewBag.FlashReport = "Menyajikan perkembangan kegiatan investigasi dan penanganan permasalahan khusus yang ditangani Inspektorat Jenderal di luar Tema Pengawasan Unggulan. Data bersumber dari laporan yang dibuat oleh masing-masing unit Inspektorat.";

            return View();
        }

        //controller untuk menampilkan informasi umum tentang IRA di halaman home
        //end

        //<!--kumpulan fungsi untuk dashboard flash-->
        //start

        //controller untuk chart rekap data kegiatan investigasi per tahun
        //start

        [Authorize]
        public ActionResult rekapDataInvestigasi()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentuser.RoleID == 1)
            {
                var earliestCase = db.TransFlashKegiatan.OrderBy(y => y.TanggalKasus).FirstOrDefault();

                if (earliestCase == null)
                {
                    ViewBag.earliestYear = DateTime.Now.Year;

                    ViewBag.latestYear = ViewBag.earliestYear - 5;

                    return PartialView("_flashDataEmpty");
                }

                var latestCase = db.TransFlashKegiatan.OrderByDescending(y => y.TanggalKasus).FirstOrDefault();

                ViewBag.earliestYear = earliestCase.TanggalKasus.Year;

                ViewBag.latestYear = latestCase.TanggalKasus.Year;

                return PartialView("_rekapDataInvestigasi");
            }

            var EarliestCase = db.TransFlashKegiatan.Where(y => y.UnitID == currentuser.UnitID).OrderBy(y => y.TanggalKasus).FirstOrDefault();

            if (EarliestCase == null)
            {
                ViewBag.earliestYear = DateTime.Now.Year;

                ViewBag.latestYear = ViewBag.earliestYear - 5;

                return PartialView("_flashDataEmpty");
            }

            var LatestCase = db.TransFlashKegiatan.Where(y => y.UnitID == currentuser.UnitID).OrderByDescending(y => y.TanggalKasus).FirstOrDefault();

            ViewBag.earliestYear = EarliestCase.TanggalKasus.Year;

            ViewBag.latestYear = LatestCase.TanggalKasus.Year;

            return PartialView("_rekapDataInvestigasi");

        }

        [Authorize]
        public ActionResult rekapDataInvestigasiPointChart(int year)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.year = year;

            if (currentuser.RoleID == 1)
            {
                ViewBag.result = db.TransFlashKegiatan.Where(y => y.TanggalKasus.Year == year).Count();

                return PartialView("_rekapInvestigasiPointChart");
            }

            ViewBag.result = db.TransFlashKegiatan.Where(y => y.TanggalKasus.Year == year && y.UnitID == currentuser.UnitID).Count();

            return PartialView("_rekapInvestigasiPointChart");

        }

        //controller untuk chart rekap data kegiatan investigasi per tahun
        //end

        //controller untuk chart status kegiatan investigasi per unit
        //start

        [Authorize]
        public ActionResult statusKegiatanInvestigasiPerUnit()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            var status = db.RefFlashStatusKegiatan.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList();

            ViewBag.empty = false;
            return PartialView("_statusKegiatanInvestigasiPerUnit", status);
        }

        [Authorize]
        public ActionResult dataChartStatusFlashPerUnit(int id)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefTPUStatus status = db.RefStatusTPU.Find(id);

            if (status == null)
            {
                return PartialView("_NotFound");
            }

            var units = db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false).ToList();

            ViewBag.status = status.Ket;
            ViewBag.statusID = status.ID;
            return PartialView("_dataChartStatusFlashPerUnit", units);
        }

        [Authorize]
        public ActionResult dataPointsChartStatusFlashPerUnit(int id, int statusID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefTPUStatus status = db.RefStatusTPU.Find(statusID);
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (unit == null || status == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.TransFlashKegiatan.Where(y => y.UnitID == id && y.Finalize == status.ID).Count();
            return PartialView("_dataPointsChartStatusFlashPerUnit");
        }

        //controller untuk chart status kegiatan investigasi per unit
        //end

        //controller untuk chart rekapitulasi berdasarkan status kegiatan
        //start

        [Authorize]
        public ActionResult statusKegiatanFlash()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            var status = db.RefFlashStatusKegiatan.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList();

            ViewBag.empty = false;
            return PartialView("_statusKegiatanFlash", status);
        }

        [Authorize]
        public ActionResult dataPointStatusKegiatanFlash(int id)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefFlashKegiatanStatus status = db.RefFlashStatusKegiatan.Find(id);

            if (currentuser.RoleID == 1)
            {
                ViewBag.status = status.Ket;
                ViewBag.result = db.TransFlashKegiatan.Where(y => y.Finalize == status.ID).Count();
                return PartialView("_dataPointStatusKegiatanFlash");
            }

            //ViewBag.empty = false;
            ViewBag.status = status.Ket;
            ViewBag.result = db.TransFlashKegiatan.Where(y => y.Finalize == status.ID && y.UnitID == currentuser.UnitID).Count();
            return PartialView("_dataPointStatusKegiatanFlash");
        }

        //controller untuk chart rekapitulasi berdasarkan status kegiatan
        //end

        //controller untuk chart jumlah kegiatan per unit
        //start

        [Authorize]
        public ActionResult jumlahKegiatanFlash()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            var unit = db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false).OrderBy(y => y.ID).ToList();

            ViewBag.empty = false;
            return PartialView("_jumlahKegiatanFlash", unit);
        }

        [Authorize]
        public ActionResult rekapKegUnitFlash(int id)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (unit == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.TransFlashKegiatan.Where(y => y.UnitID == id).Count();
            return PartialView("_rekapKegUnitFlash");
        }

        //controller untuk chart jumlah kegiatan per unit
        //end

        //<!--kumpulan fungsi untuk dashboard flash-->
        //end

        [Authorize]
        public ActionResult ListingTPU(int id)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.Tahun).FirstOrDefault();
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (unit == null)
            {
                return PartialView("_NotFound");
            }


            if (currentuser.RoleID == 1)
            {
                var tema = db.RefTPU.Where(y => y.PKPTID == master.ID).ToList();
                ViewBag.empty = false;
                return PartialView("_listingTP", tema);
            }

            var tp = db.RefTPU.Where(y => y.PKPTID == master.ID && y.TPUUnitPJID == unit.ID).ToList();
            ViewBag.empty = false;
            return PartialView("_listingTP", tp);
        }

        [Authorize]
        public ActionResult jumlahKegiatan()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.Tahun).FirstOrDefault();

            var unit = db.RefUnitPJ.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList();
            var unitmax = db.RefUnitPJ.Where(y => y.Aktif == true).OrderByDescending(y => y.ID).FirstOrDefault();

            if (master == null)
            {
                ViewBag.empty = true;
                return PartialView("_jumlahKegiatan", unit);
            }

            ViewBag.maxID = unitmax.ID;
            ViewBag.masterID = master.ID;
            ViewBag.empty = false;
            return PartialView("_jumlahKegiatan", unit);
        }

        [Authorize]
        public ActionResult rekapKegUnit(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (master == null || unit == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.RefKegiatan.Where(y => y.RefTPU.PKPTID == masterID && y.RefTPU.TPUUnitPJID == id).Count();
            return PartialView("_rekapKegUnit");
        }

        [Authorize]
        public ActionResult rekapKegiatanUnit(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (master == null || unit == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.RefKegiatan.Where(y => y.RefTPU.PKPTID == masterID && y.RefTPU.TPUUnitPJID == id).Count();
            return PartialView("_rekapKegUnit_");
        }

        [Authorize]
        public ActionResult jumlahTPU()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.Tahun).FirstOrDefault();

            var unit = db.RefUnitPJ.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList();
            var unitmax = db.RefUnitPJ.Where(y => y.Aktif == true).OrderByDescending(y => y.ID).FirstOrDefault();

            if (master == null)
            {
                ViewBag.empty = true;
                return PartialView("_jumlahTPU", unit);
            }

            ViewBag.maxID = unitmax.ID;
            ViewBag.masterID = master.ID;
            ViewBag.empty = false;
            return PartialView("_jumlahTPU", unit);
        }

        [Authorize]
        public ActionResult rekapTPUUnit(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (master == null || unit == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUUnitPJID == id).Count();
            return PartialView("_rekapTPUnit");
        }

        [Authorize]
        public ActionResult rekapTPUnit(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (master == null || unit == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUUnitPJID == id).Count();
            return PartialView("_rekapTPUnit_");
        }

        [Authorize]
        public ActionResult statusTPU()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.Tahun).FirstOrDefault();
            RefUnitPJ unit = db.RefUnitPJ.Find(currentuser.UnitID);

            var statusTP = db.RefStatusTPU.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList();
            var statusTPmax = db.RefStatusTPU.Where(y => y.Aktif == true).OrderByDescending(y => y.ID).FirstOrDefault();

            if (master == null)
            {
                ViewBag.empty = true;
                return PartialView("_statusTPU", statusTP);
            }

            ViewBag.role = currentuser.RoleID;
            ViewBag.unit = unit.Detail;
            ViewBag.maxID = statusTPmax.ID;
            ViewBag.masterID = master.ID;
            ViewBag.empty = false;
            return PartialView("_statusTPU", statusTP);
        }

        [Authorize]
        public ActionResult datachartTP(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefTPUStatus status = db.RefStatusTPU.Find(id);

            if (master == null || status == null)
            {
                return PartialView("_NotFound");
            }

            var units = db.RefUnitPJ.Where(y => y.Aktif == true).ToList();

            ViewBag.status = status.Ket;
            ViewBag.masterID = master.ID;
            ViewBag.statusID = status.ID;
            return PartialView("_dataChartTP", units);
        }

        [Authorize]
        public ActionResult dataPointsChartTP(int id, int masterID, int statusID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            RefTPUStatus status = db.RefStatusTPU.Find(statusID);
            TransSchedule master = db.TransSchedule.Find(masterID);
            RefUnitPJ unit = db.RefUnitPJ.Find(id);

            if (master == null || unit == null||status==null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.unit = unit.DetailShort;
            ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUUnitPJID == id && y.TPUStatusID == status.ID).Count();
            return PartialView("_dataPointsChartTP");
        }

        [Authorize]
        public ActionResult statusTP()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y=>y.Tahun).FirstOrDefault();
            RefUnitPJ unit = db.RefUnitPJ.Find(currentuser.UnitID);

            var statusTP = db.RefStatusTPU.Where(y => y.Aktif == true).OrderBy(y=>y.ID).ToList();
            var statusTPmax = db.RefStatusTPU.Where(y => y.Aktif == true).OrderByDescending(y=>y.ID).FirstOrDefault();

            ViewBag.role = currentuser.RoleID;
            ViewBag.unit = unit.Detail;

            if (master == null)
            {
                ViewBag.empty = true;
                return PartialView("_statusTP", statusTP);
            }

            ViewBag.maxID = statusTPmax.ID;
            ViewBag.masterID = master.ID;
            ViewBag.empty = false;
            ViewBag.unitID = currentuser.UnitID;
            return PartialView("_statusTP", statusTP);
        }

        [Authorize]
        public ActionResult rekapStatusTP(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefTPUStatus status = db.RefStatusTPU.Find(id);

            if (master == null || status == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.status = status.Ket;

            if (currentuser.RoleID == 1)
            {
                ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUStatusID == id).Count();
                return PartialView("_rekapStatusTP");
            }

            //if not admin then display only related data.
            ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUStatusID == id&&y.TPUUnitPJID==currentuser.UnitID).Count();
            return PartialView("_rekapStatusTP");
        }

        [Authorize]
        public ActionResult rekapStatusTPU(int id, int masterID)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransSchedule master = db.TransSchedule.Find(masterID);
            RefTPUStatus status = db.RefStatusTPU.Find(id);

            if (master == null || status == null)
            {
                return PartialView("_NotFound");
            }

            ViewBag.status = status.Ket;

            if (currentuser.RoleID == 1)
            {
                ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUStatusID == id).Count();
                return PartialView("_rekapStatusTP");
            }

            //if not admin then display only related data.
            ViewBag.result = db.RefTPU.Where(y => y.PKPTID == masterID && y.TPUStatusID == id).Count();
            return PartialView("_rekapStatusTP");
        }

        public ActionResult GeneratePDF()
        {
            var cookies = Request.Cookies.AllKeys.ToDictionary(k => k, k => Request.Cookies[k].Value);

            return new Rotativa.ActionAsPdf("Index")
            {
                FormsAuthenticationCookieName = System.Web.Security.FormsAuthentication.FormsCookieName,
                Cookies = cookies,
                //FileName = "Test.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4
            };
        }

        public ActionResult Loading()
        {
            return PartialView("_loading");
        }

        [Authorize]
        public ActionResult Progress()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            var Popup = db.RefPopUpText.Where(y => y.ModulID == 1).FirstOrDefault();

            ViewBag.PopupText = Popup.Message;

            ViewBag.PopupDate = Popup.Airing.ToOADate();

            ViewBag.role = currentuser.RoleID;

            return View();
        }

        [Authorize]
        public ActionResult Flash()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            var Popup = db.RefPopUpText.Where(y => y.ModulID == 2).FirstOrDefault();

            ViewBag.PopupText = Popup.Message;

            ViewBag.PopupDate = Popup.Airing.ToOADate();

            return View();
        }

        [Authorize]
        public ActionResult Notification()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);
            //                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            var name = active.UserName;
            TransNotifClick data = db.TransNotifClick
                .Where(y => y.UserName == name)
                .OrderByDescending(y => y.Date)
                .FirstOrDefault();

            if (currentuser.RoleID == 1)
            {
                var notif = db.TransNotifikasi.Where(y=>y.RoleID==1).OrderByDescending(y => y.Date).Take(4).ToList();

                if (data == null)
                {
                    ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 1).Count();

                    return PartialView("_NotificationPartial", notif);
                }

                ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 1 && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_NotificationPartial", notif);
            }

            if (currentuser.RoleID == 2)
            {
                var notif = db.TransNotifikasi.Where(y => y.RoleID == 2 && y.UnitID == currentuser.UnitID).OrderByDescending(y => y.Date).Take(4).ToList();
                if (data == null)
                {
                    ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 2&&y.UnitID==currentuser.UnitID).Count();

                    return PartialView("_NotificationPartial", notif);
                }

                ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 2 && y.UnitID == currentuser.UnitID && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_NotificationPartial", notif);
            }
            //ViewBag.last = data.Date;

            var notif2 = db.TransNotifikasi.Where(y => y.RoleID == 0).OrderByDescending(y => y.Date).Take(4).ToList();
            return PartialView("_NotificationPartial", notif2);
        }

        [Authorize]
        public ActionResult DashboardNotification()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);
            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            var name = active.UserName;
            TransNotifClick data = db.TransNotifClick
                .Where(y => y.UserName == name)
                .OrderByDescending(y => y.Date)
                .FirstOrDefault();

            if (currentuser.RoleID == 1)
            {
                var notif = db.TransNotifikasi.Where(y => y.RoleID == 1).OrderByDescending(y => y.Date).Take(3).ToList();

                if (data == null)
                {
                    ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 1).Count();

                    return PartialView("_NotificationDashboard", notif);
                }

                ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 1 && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_NotificationDashboard", notif);
            }

            if (currentuser.RoleID == 2)
            {
                var notif = db.TransNotifikasi.Where(y => y.RoleID == 2 && y.UnitID == currentuser.UnitID).OrderByDescending(y => y.Date).Take(3).ToList();
                if (data == null)
                {
                    ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 2 && y.UnitID == currentuser.UnitID).Count();

                    return PartialView("_NotificationDashboard", notif);
                }

                ViewBag.NotifCount = db.TransNotifikasi.Where(y => y.RoleID == 2 && y.UnitID == currentuser.UnitID && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_NotificationDashboard", notif);
            }
            //ViewBag.last = data.Date;

            var notif2 = db.TransNotifikasi.Where(y => y.RoleID == 0).OrderByDescending(y => y.Date).Take(4).ToList();
            return PartialView("_NotificationDashboard", notif2);
        }

        [Authorize]
        public ActionResult DashboardPending()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == 0)
            //{
            //    return RedirectToAction("Pending", null);
            //}

            TransSchedule master = db.TransSchedule.Where(y => y.Locked == 0).OrderByDescending(y => y.ID).FirstOrDefault();

            if (master == null)
            {
                ViewBag.Title = "Permintaan Data Progress Report";
                ViewBag.empty = true;
                ViewBag.kirimNDPermintaanEnabled = false;

                if (currentuser.RoleID == 1)
                {
                    ViewBag.hide = false;
                    ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang dikirim.";
                    return PartialView("_Pending");
                }

                ViewBag.hide = true;
                ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang diterima.";
                return PartialView("_Pending");
            }

            ViewBag.kirimNDPermintaanEnabled = true;

            TransNDPermintaan nd = db.TransNDPermintaan.Where(y => y.PKPTID == master.ID && y.Locked == false).OrderByDescending(y => y.PeriodeID).FirstOrDefault();

            ViewBag.masterID = master.ID;

            if (nd == null)
            {
                ViewBag.Title = "Permintaan Data Progress Report";
                ViewBag.empty = true;

                if (currentuser.RoleID == 1)
                {
                    ViewBag.hide = false;
                    ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang dikirim.";
                    return PartialView("_Pending");
                }

                ViewBag.hide = true;
                ViewBag.message = "Belum ada ND Permintaan Progress Report baru yang diterima.";
                return PartialView("_Pending");
            }

            RefPeriode periode = db.RefPeriode.Find(nd.PeriodeID);

            ViewBag.empty = false;
            ViewBag.nd = nd.ID;
            ViewBag.ndno = nd.NomorND;

            if (currentuser.RoleID == 1)
            {
                RefUnitPJ example = db.RefUnitPJ.Where(y => y.Aktif == true).OrderBy(y => y.ID).FirstOrDefault();
                ViewBag.example = example.ID;
                ViewBag.hide = false;
                ViewBag.Title = "Rekap Hasil Permintaan Data Progress Report Periode " + periode.Ket + " " + nd.TransSchedule.Tahun;
                return PartialView("_Pending",db.RefUnitPJ.Where(y => y.Aktif == true).OrderBy(y => y.ID).ToList());
            }

            RefUnitPJ example2 = db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID).OrderBy(y => y.ID).FirstOrDefault();
            ViewBag.example = example2.ID;
            ViewBag.hide = true;
            ViewBag.Title = "Permintaan Data Progress Report Periode " + periode.Ket + " " + nd.TransSchedule.Tahun;
            return PartialView("_Pending",db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID).ToList());
        }

        [Authorize]
        public ActionResult DashboardPendingFlash()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            //if (id == 0)
            //{
            //    return RedirectToAction("Pending", null);
            //}

            TransNDPermintaanFlash nd = db.TransNDPermintaanFlash.Where(y => y.Locked == false).OrderByDescending(y => y.ID).FirstOrDefault();

            if (nd == null)
            {
                ViewBag.Title = "Permintaan Data Flash Report";
                ViewBag.empty = true;

                if (currentuser.RoleID == 1)
                {
                    ViewBag.hide = false;
                    ViewBag.message = "Belum ada ND Permintaan Flash Report baru yang dikirim.";
                    return PartialView("_PendingFlash");
                }

                ViewBag.hide = true;
                ViewBag.message = "Belum ada ND Permintaan Flash Report baru yang diterima.";
                return PartialView("_PendingFlash");
            }

            ViewBag.YearND = nd.Tahun;
            ViewBag.PeriodND = nd.PeriodeID;

            RefPeriode periode = db.RefPeriode.Find(nd.PeriodeID);

            ViewBag.empty = false;
            ViewBag.nd = nd.ID;
            ViewBag.ndno = nd.NomorND;
            ViewBag.unitID = currentuser.UnitID;

            if (currentuser.RoleID == 1)
            {
                RefUnitPJ example = db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false).OrderBy(y => y.ID).FirstOrDefault();
                ViewBag.example = example.ID;
                ViewBag.hide = false;
                ViewBag.Title = "Rekap Hasil Permintaan Data Flash Report Periode " + periode.Ket + " " + nd.Tahun;
                return PartialView("_PendingFlash", db.RefUnitPJ.Where(y => y.Aktif == true && y.isPrimeMover == false).OrderBy(y => y.ID).ToList());
            }

            RefUnitPJ example2 = db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID && y.isPrimeMover == false).OrderBy(y => y.ID).FirstOrDefault();
            ViewBag.example = example2.ID;
            ViewBag.hide = true;
            ViewBag.Title = "Permintaan Data Flash Report Periode " + periode.Ket + " " + nd.Tahun;
            return PartialView("_PendingFlash", db.RefUnitPJ.Where(y => y.Aktif == true && y.ID == currentuser.UnitID && y.isPrimeMover == false).ToList());
        }

        [Authorize]
        public ActionResult NotifClick(int id)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            TransNotifikasi notifikasi = db.TransNotifikasi.Find(id);
            TransNotifClick click = new TransNotifClick
            {
                Date = DateTime.Now,
                UserName = active.UserName
            };
            db.TransNotifClick.Add(click);
            db.SaveChanges();

            return RedirectToAction(notifikasi.Action, notifikasi.Controller, new { id = notifikasi.RouteID });
        }

        [Authorize]
        public ActionResult DashboardNotificationFlash()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);
            //var currentuser = manager.FindById(User.Identity.GetUserId()); if (currentuser.isRevoked) { return RedirectToAction("Logout", "Account", null); }

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            var name = active.UserName;
            TransFlashNotifClick data = db.TransFlashNotifClick
                .Where(y => y.UserName == name)
                .OrderByDescending(y => y.Date)
                .FirstOrDefault();

            if (currentuser.RoleID == 1)
            {
                var notif = db.TransFlashNotifikasi.Where(y => y.RoleID == 1).OrderByDescending(y => y.Date).Take(3).ToList();

                if (data == null)
                {
                    ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 1).Count();

                    return PartialView("_FlashNotificationDashboard", notif);
                }

                ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 1 && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_FlashNotificationDashboard", notif);

            }

            else if (currentuser.RoleID == 3)
            {
                var notif = db.TransFlashNotifikasi.Where(y => y.RoleID == 3 && y.UnitID == currentuser.UnitID).OrderByDescending(y => y.Date).Take(3).ToList();

                if (data == null)
                {
                    ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 3 && y.UnitID == currentuser.UnitID).Count();

                    return PartialView("_FlashNotificationDashboard", notif);
                }

                ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 3 && y.UnitID == currentuser.UnitID && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_FlashNotificationDashboard", notif);
            }

            var notif2 = db.TransFlashNotifikasi.Where(y => y.RoleID == 0).OrderByDescending(y => y.Date).Take(3).ToList();
            return PartialView("_FlashNotificationDashboard", notif2);
        }

        [Authorize]
        public ActionResult FlashNotification()
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);
            //                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            var name = active.UserName;
            TransFlashNotifClick data = db.TransFlashNotifClick
                .Where(y => y.UserName == name)
                .OrderByDescending(y => y.Date)
                .FirstOrDefault();

            if (currentuser.RoleID == 1)
            {
                var notif = db.TransFlashNotifikasi.Where(y=>y.RoleID==1).OrderByDescending(y => y.Date).Take(4).ToList();

                if (data == null)
                {
                    ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 1).Count();

                    return PartialView("_FlashNotificationPartial", notif);
                }

                ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 1 && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_FlashNotificationPartial", notif);

            }

            else if(currentuser.RoleID==3)
            {
                var notif = db.TransFlashNotifikasi.Where(y => y.RoleID == 3 && y.UnitID == currentuser.UnitID).OrderByDescending(y => y.Date).Take(4).ToList();

                if (data == null)
                {
                    ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 3&&y.UnitID==currentuser.UnitID).Count();

                    return PartialView("_FlashNotificationPartial", notif);
                }

                ViewBag.NotifCount = db.TransFlashNotifikasi.Where(y => y.RoleID == 3 && y.UnitID == currentuser.UnitID && y.Date.CompareTo(data.Date) >= 0).Count();

                return PartialView("_FlashNotificationPartial", notif);
            }

            var notif2 = db.TransFlashNotifikasi.Where(y => y.RoleID == 0).OrderByDescending(y => y.Date).Take(4).ToList();
            return PartialView("_FlashNotificationPartial", notif2);
        }

        [Authorize]
        public ActionResult FlashNotifClick(int id)
        {
            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //                        var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            if (currentuser.RoleID != 1 && currentuser.RoleID != 3)
            {
                return RedirectToAction("Login", "Account");
            }

            TransFlashNotifikasi notifikasi = db.TransFlashNotifikasi.Find(id);
            TransFlashNotifClick click = new TransFlashNotifClick
            {
                Date = DateTime.Now,
                UserName = active.UserName
            };
            db.TransFlashNotifClick.Add(click);
            db.SaveChanges();

            return RedirectToAction(notifikasi.Action, notifikasi.Controller, new { id = notifikasi.RouteID });
        }

        [Authorize]
        public ActionResult Name()
        {
            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            ViewBag.name = currentuser.FirstName + " " + currentuser.LastName;

            return PartialView("_NamePartial");
        }

        [Authorize]
        public ActionResult Unit()
        {


            //ViewBag.name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.GetUserName().Replace(".", " "));

            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId());

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            var nama = active.UserName;
            
            RefUnitPJ unit = db.RefUnitPJ.Find(currentuser.UnitID);

            ViewBag.unit = unit.Detail;
            return PartialView("_UnitPartial");
        }

        [Authorize]
        public ActionResult Role()
        {


            //ViewBag.name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.GetUserName().Replace(".", " "));

            //            MembershipUser active = Membership.GetUser(User.Identity.Name);

            //var currentuser = manager.FindById(User.Identity.GetUserId());

            MembershipUser active = Membership.GetUser(User.Identity.Name);

            var currentuser = _db.Users.Where(y => y.UserName == active.UserName && y.isRevoked == false).FirstOrDefault();

            var nama = User.Identity.GetUserName();
            
            RefRole role = db.RefRole.Find(currentuser.RoleID);

            ViewBag.role = role.Detail;
            return PartialView("_RolePartial");
        }

    }
}