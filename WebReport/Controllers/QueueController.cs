using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;

namespace PktReport.Controllers
{
    public class QueueController : Controller
    {
        DataAccess.DataAccessQueue oDa = new DataAccess.DataAccessQueue();
        // GET: Queue
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetQueue()
        {
            DataTable oData = oDa.GetQueue();
            var QueueData = from DataRow row in oData.Rows
                            select new DataStruct.QueueViewModel
                            {
                                NoLab = row["LabNo"].ToString(),
                                NoRM = row["NoMedRec"].ToString(),
                                PatientName = row["PatientName"].ToString(),
                                Status = row["StatusDisplay"].ToString()
                            };

            return Json(QueueData,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNik (string nik)
        {
            return null;
        }
    }
}