using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace DataAccess
{
    public class DataAccessHome : DataAccessBase
    {
       public DataTable GetRegistration(string type, string startDate, string endDate, string poliklinikID,string userRole)
        {
            try
            {
                string tableName = "vWeb_Search_Registration";
                if (type == "pa")
                {
                    tableName = "vWeb_Search_Registration_PA";
                }
                string sql = $"select * From {tableName} where Convert(varchar(10),regdate,120)>='{startDate}' and Convert(varchar(10),regdate,120)<='{endDate}' AND IsValid=1";
                if (userRole.ToLower() != "su" && userRole.ToLower() != "admin")
                {
                    sql += $" AND PoliklinikID = '{poliklinikID}'";
                }

                if (type=="lab")
                {
                    sql += " AND IsValidResult=1";
                }
                sql += " ORDER BY RegId DESC";


                return GetDataTable(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetRegistrationDetail(string regNo)
        {
          
            try
            {
                string sql = $"select * from vWeb_Hasil_Detail where RegId = '{regNo}' ORDER BY GroupSeq,TestSeq,FractionID";
               
                return GetDataTable(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable GetRegistrationDetailPa(string regNo)
        {
            try
            {
                string sql = $"select RegId,BarcodeJenis,CreatedDate from Tr_HasilPA_Binary where RegId = '{regNo}' ORDER BY CreatedDate";

                return GetDataTable(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] GetBlobPA(string regNo,string barcodeJenis)
        {
            try
            {
                string sql = $"select Result_PDF from Tr_HasilPA_Binary where RegId = '{regNo}' and BarcodeJenis='{barcodeJenis}'";
                var fileArray =  (byte[])ExecuteScalar(sql);


                return fileArray;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetRegistration(string type,string regNo,string poliklinikID, string userRole)
        {
            string tableName = "vWeb_Search_Registration";
            if (type == "pa")
            {
                tableName = "vWeb_Search_Registration_PA";
            }

            string sql = $"select * from {tableName} where RegId = '{regNo}'  AND IsValid=1";
            if (userRole.ToLower() != "su" && userRole.ToLower() != "admin")
            {
                sql += $" AND poliklinikID='{poliklinikID}'";
            }

            try
            {
                return GetDataTable(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
