using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReport.Controllers
{
    public class BaseController : Controller
    {
        [Authorize]
        public ActionResult DownloadFile(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/GenerateDoc"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
    }
}