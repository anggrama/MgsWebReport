using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.DirectoryServices.Protocols;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Configuration;

namespace DataAccess
{
    public class HelperClass
    {
        static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
        public static string RemoveAccent(string txt)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
            return $"{Years} Tahun {Months} Bulan {Days} Hari";
        }

        public static string CalculateYourAge(DateTime Dob, DateTime regDate)
        {
            DateTime NowDate = regDate;
            int Years = new DateTime(NowDate.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == NowDate)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= NowDate)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = NowDate.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = NowDate.Subtract(PastYearDate).Hours;
            int Minutes = NowDate.Subtract(PastYearDate).Minutes;
            int Seconds = NowDate.Subtract(PastYearDate).Seconds;
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
            return $"{Years} Tahun {Months} Bulan {Days} Hari";
        }
    }

    public static class HashClass
    {
        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }


    }

    public static class LdapClass
    {
        public static bool LdapAuthentication(string usr, string pwd)
        {
            bool authenticated = false;
            string serverLdap = System.Configuration.ConfigurationManager.AppSettings["ldap"].ToString();

            try
            {
                DirectoryEntry entry = new DirectoryEntry($"LDAP://{serverLdap}",usr,pwd);
                object nativeObject = entry.NativeObject;

                DirectorySearcher dSearch = new DirectorySearcher(entry);
                dSearch.Filter = "(&(objectClass=user)(cn=" + usr + "))";
                SearchResult sResultSet = dSearch.FindOne();

                authenticated = true;
            }
            catch (DirectoryServicesCOMException cex)
            {
                Console.WriteLine(cex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return authenticated;
        }

        public static bool validateUserByBind(string username, string password)
        {
            string serverLdap = System.Configuration.ConfigurationManager.AppSettings["ldap"].ToString();
            bool result = true;
            var credentials = new System.Net.NetworkCredential(username, password);
            //var serverId = new LdapDirectoryIdentifier(connection.SessionOptions.HostName);
            var serverId = new LdapDirectoryIdentifier(serverLdap, 389);

            var conn = new LdapConnection(serverId, credentials);
            try
            {
                conn.Bind();
            }
            catch (Exception ex)
            {
                result = false;
            }

            conn.Dispose();

            return result;
        }
    }

    public static class MailClass
    {
       
        public static void SendEmail(string strTo,string cc, string strSubject, string strMessage)
        {

            string _SmtpPort = ConfigurationManager.AppSettings["smtp_port"];
            string _smtpServer = ConfigurationManager.AppSettings["smtp_server"];
            string _email = ConfigurationManager.AppSettings["email"];
            string _userId = ConfigurationManager.AppSettings["email_userid"];
            string _password = ConfigurationManager.AppSettings["email_password"];
            string _needAuthentication = ConfigurationManager.AppSettings["need_auth"];
            string _isSSL = ConfigurationManager.AppSettings["is_ssl"];


            //This procedure overrides the first procedure and accepts a single
            //string for the recipient and file attachement 
            try
            {
                MailMessage mess = new MailMessage(_email, strTo, strSubject, strMessage);
                mess.CC.Add(cc);
                mess.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(_smtpServer, Convert.ToInt32(_SmtpPort));

                if (_needAuthentication == "0")
                {
                    smtp.UseDefaultCredentials = false;
                }
                else
                {
                    smtp.Credentials = new NetworkCredential(_userId, _password);
                    //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                }

                if (_isSSL == "1")
                    smtp.EnableSsl = true;

                smtp.Send(mess);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static class LogClass
    {
        public static void writeLog(string message)
        {
            string fileName = DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
            string strMessage = string.Format("{0:yyyy-MM-dd HH:mm:ss} - {1}", DateTime.Now, message);

            string path = System.IO.Directory.GetCurrentDirectory() + "\\Log";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            System.IO.File.AppendAllLines(path + "\\" + fileName, new string[] { strMessage });
        }
    }
}