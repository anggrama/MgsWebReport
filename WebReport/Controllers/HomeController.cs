﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;

namespace PktReport.Controllers
{
    public class HomeController : Controller
    {

        


        [Authorize]
        public ActionResult Index(string startDate, string endDate)
        {
            return null;
            
        }

       


    }
}