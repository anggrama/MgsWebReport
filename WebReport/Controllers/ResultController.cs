using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebReport.Controllers
{
    public class ResultController : Controller
    {
        DataAccess.DataAccessHome oDa = new DataAccess.DataAccessHome();

        [Authorize]
        public ActionResult Index(string type, string startDate, string endDate)
        {
            //buat pak andi
            //var fullname = Request.Cookies["UserInfo"].Values["Fullname"];

            //if (Session["PoliklinikID"] != null)
            if (Request.Cookies["UserInfo"].Values["PoliklinikID"] != null)
            {
                DataTable oData = oDa.GetRegistration(type,startDate, endDate, Request.Cookies["UserInfo"].Values["PoliklinikID"], Request.Cookies["UserInfo"].Values["Role"]);
                var oVm = new DataStruct.ResultViewModel()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Data = oData,
                    Type = type
                };

                return View(oVm);
            }
            else
            {
                throw new Exception("Poliklinik Null");
            }

        }

        [Authorize]
        public ActionResult ResultLab(string type,string regNo)
        {

            if (Request.Cookies["UserInfo"].Values["PoliklinikID"] != null)
            {
                DataTable oHeader = oDa.GetRegistration(type,regNo, Request.Cookies["UserInfo"].Values["PoliklinikID"], Request.Cookies["UserInfo"].Values["Role"]);
                DataTable oData = null;
                if (oHeader.Rows.Count >= 1)
                {
                    if (type == "lab")
                    {
                        oData = oDa.GetRegistrationDetail(regNo);
                    }
                    else
                    {
                        oData = oDa.GetRegistrationDetailPa(regNo);
                    }
                }
                var oVm = new DataStruct.ResultDetailViewModel()
                {
                    HeaderData = oHeader,
                    ResultData = oData
                };
                
                if (type=="lab")
                {
                    return View(oVm);
                }
                else
                {
                    return View("ResultLabPa",oVm);
                }
                
            }
            else
            {
                throw new Exception("Poliklinik Null");
            }


        }

        [Authorize]
        public ActionResult ResultDoc(string regNo, string barcode)
        {

            //var path = $"{Server.MapPath("/ ")}\\GeneratedFile\\{ Guid.NewGuid()}.pdf";
            byte[] blob = null;
            try
            {
                blob = oDa.GetBlobPA(regNo, barcode);
            }
            catch (Exception)
            {
                return Content("No Result Available");
            }
            
            var fileStream = new System.IO.MemoryStream(blob);

            //System.IO.FileStream docxFileStream = new System.IO.FileStream("E:\\test.docx", System.IO.FileMode.Create);
            //byte[] byteInfos = fileStream.ToArray();
            //docxFileStream.Write(byteInfos, 0, byteInfos.Length);
            //docxFileStream.Position = 0;
            //docxFileStream.Flush();
            //docxFileStream.Close();
            //docxFileStream.Dispose();
            //return null;

            //System.IO.FileStream pdfFileStream = new System.IO.FileStream(path, System.IO.FileMode.Create);


            //---- start konversi
            //var pdfFileStream = new System.IO.MemoryStream();

            //var lm = new GdPicture14.LicenseManager();
            //lm.RegisterKEY("211883860501001421116010749430779");

            //using (GdPicture14.GdPictureDocumentConverter oConverter = new GdPicture14.GdPictureDocumentConverter())
            //{
            //    GdPicture14.GdPictureStatus status = oConverter.LoadFromStream(fileStream, GdPicture14.DocumentFormat.DocumentFormatDOCX);
            //    oConverter.SaveAsPDF(pdfFileStream);
            //}

            byte[] byteInfo = fileStream.ToArray();
            fileStream.Write(byteInfo, 0, byteInfo.Length);
            fileStream.Position = 0;
            
            return new FileStreamResult(fileStream, "application/pdf");

            //string outputDirectory = System.IO.Directory.GetCurrentDirectory();
            //new GroupDocs.Viewer.License().SetLicense(@"D:\Running Project\2019\MGS\Web Result\SourceCode\WebReport\GroupDocs.Total.lic");

            //GemBox.Document.ComponentInfo.SetLicense("anything-you-want");

            //var document = GemBox.Document.DocumentModel.Load(fileStream, GemBox.Document.LoadOptions.DocxDefault);

            //var oContent = new DataStruct.ResultBlob()
            //{
            //    ResultFile = document.Content.ToString()
            //};

            //using (GroupDocs.Viewer.Viewer viewer = new GroupDocs.Viewer.Viewer(fileStream))
            //{
            //    GroupDocs.Viewer.Options.HtmlViewOptions options = GroupDocs.Viewer.Options.HtmlViewOptions.ForEmbeddedResources(@"D:\Running Project\2019\MGS\Web Result\SourceCode\WebReport\page_{0}.html");
            //    viewer.View(options);
            //}



            
            //return View("ResultDocViewer", oContent);

        }

    }
}