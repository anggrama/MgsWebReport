using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PktReport.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username,string password)
        {

            //cari dulu user nya
            DataAccess.DataAccessUser oUsers = new DataAccess.DataAccessUser();
            System.Data.DataTable oUser = oUsers.GetUserForLogin(username, password);

            if (oUser != null)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                var authTicket = new FormsAuthenticationTicket(1, oUser.Rows[0]["username"].ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, "admin");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                // buat pak andi
                var customCookie = new HttpCookie("UserInfo");
                customCookie.Values.Add("Fullname", oUser.Rows[0]["username"].ToString());
                customCookie.Values.Add("PoliklinikID", oUser.Rows[0]["PoliklinikID"].ToString());
                customCookie.Values.Add("Role", oUser.Rows[0]["UserRoleId"].ToString());
                // end

                //Session["PoliklinikID"] = Convert.ToString(oUser.Rows[0]["PoliklinikID"]);
                //Session["Role"] = Convert.ToString(oUser.Rows[0]["UserRoleId"]);

                HttpContext.Response.Cookies.Add(authCookie);
                HttpContext.Response.Cookies.Add(customCookie); // sama ini
                //Session["FullName"] = ;
                return RedirectToAction("Index", "Result", new { type = "lab", startDate =DateTime.Now.ToString("yyyy-MM-dd"), endDate=DateTime.Now.ToString("yyyy-MM-dd") });

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }


}