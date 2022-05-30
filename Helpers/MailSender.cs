using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ira.Models;

namespace ira.Web.Utilities
{
    public class MailSender
    {
        private static IRADbContext db = new IRADbContext();

        private static ApplicationDbContext _db = new ApplicationDbContext();

        public static void Notify(MailModel _objModelMail)
        {
            ConfigMail config = db.ConfigMail.OrderByDescending(y=>y.ID).FirstOrDefault();
            //ConfigMailAdvanced advanced = db.ConfigMailAdvanced.OrderByDescending(y => y.ID).FirstOrDefault();

                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(config.Email);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = "<b>"+Body+"</b>";
                mail.IsBodyHtml = config.isBodyHtml;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = config.Host;
                smtp.Port = config.Port;
                smtp.UseDefaultCredentials = config.useDefaultCredential;
                smtp.Credentials = new System.Net.NetworkCredential(config.Email, config.Password);// Enter seders User name and password
                smtp.EnableSsl = config.enableSSL;
                smtp.Send(mail);
        }

        public static void FlashNotify(TransNDPermintaanFlash nd)
        {
            ConfigMail config = db.ConfigMail.OrderByDescending(y => y.ID).FirstOrDefault();
            //ConfigMailAdvanced advanced = db.ConfigMailAdvanced.OrderByDescending(y => y.ID).FirstOrDefault();

            foreach (var user in _db.Users.Where(y => y.RoleID == 3 && !y.isRevoked))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress(config.Email);
                mail.Subject = "Permintaan Data Flash Report periode "+nd.RefPeriode.Ket+" "+nd.Tahun+".";
                string Body = "Yth. Bapak/Ibu " + user.FirstName + " " + user.LastName + ",<br/><br/>Sehubungan dengan Nota Dinas Nomor " + nd.NomorND + " terkait dengan permintaan bahan Flash Report periode " + nd.RefPeriode.Ket + " " + nd.Tahun + ", diharapkan agar Bapak/Ibu dapat menyampaikan bahan Flash Report tersebut di aplikasi IRA. Rincian permintaan Flash Report dapat dilihat di aplikasi IRA yang dapat diakses di alamat:<br/><br/><a href='http://ijportal.kemenkeu.go.id/ira/flash/pending'>http://ijportal.kemenkeu.go.id/ira/flash/pending</a>.<br/><br/>Demikian kami sampaikan. Apabila ada pertanyaan terkait penyampaian bahan Flash Report Anda dapat menghubungi Subbagian Pelaporan Bagian Organisasi dan Kinerja Sekretariat Inspektorat Jenderal melalui nomor Internal +0008.";
                mail.Body = Body;
                mail.IsBodyHtml = config.isBodyHtml;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = config.Host;
                smtp.Port = config.Port;
                smtp.UseDefaultCredentials = config.useDefaultCredential;
                smtp.Credentials = new System.Net.NetworkCredential(config.Email, config.Password);// Enter seders User name and password
                smtp.EnableSsl = config.enableSSL;
                smtp.Send(mail);
            }
        }

        public static void ProgressNotify(TransNDPermintaan nd)
        {
            ConfigMail config = db.ConfigMail.OrderByDescending(y => y.ID).FirstOrDefault();
            //ConfigMailAdvanced advanced = db.ConfigMailAdvanced.OrderByDescending(y => y.ID).FirstOrDefault();

            foreach (var user in _db.Users.Where(y => y.RoleID == 2 && !y.isRevoked))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress(config.Email);
                mail.Subject = "Permintaan Data Progress Report periode " + nd.RefPeriode.Ket + " " + nd.TransSchedule.Tahun + ".";
                string Body = "Yth. Bapak/Ibu " + user.FirstName + " " + user.LastName + ",<br/><br/>Sehubungan dengan Nota Dinas Nomor " + nd.NomorND + " terkait dengan permintaan bahan Progress Report periode " + nd.RefPeriode.Ket + " " + nd.TransSchedule.Tahun + ", diharapkan agar Bapak/Ibu dapat menyampaikan bahan Progress Report tersebut di aplikasi IRA. Rincian permintaan Progress Report dapat dilihat di aplikasi IRA yang dapat diakses di alamat:<br/><br/><a href='http://ijportal.kemenkeu.go.id/ira/progress/pending'>http://ijportal.kemenkeu.go.id/ira/progress/pending</a>.<br/><br/>Demikian kami sampaikan. Apabila ada pertanyaan terkait penyampaian bahan Progress Report Anda dapat menghubungi Subbagian Pelaporan Bagian Organisasi dan Kinerja Sekretariat Inspektorat Jenderal melalui nomor Internal +0008.";
                mail.Body = Body;
                mail.IsBodyHtml = config.isBodyHtml;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = config.Host;
                smtp.Port = config.Port;
                smtp.UseDefaultCredentials = config.useDefaultCredential;
                smtp.Credentials = new System.Net.NetworkCredential(config.Email, config.Password);// Enter seders User name and password
                smtp.EnableSsl = config.enableSSL;
                smtp.Send(mail);
            }
        }
    }
}