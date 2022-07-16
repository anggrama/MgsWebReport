using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace DataAccess
{
    public class DataAccessQueue : DataAccessBase
    {
        public DataTable GetQueue()
        {
            try
            {
                //string sql = $"select RegId,PatientId,PatientName,RegType From vSearch_registration where Convert(varchar(10),regdate,120)='{DateTime.Now.ToString("yyyy-MM-dd")}' ORDER BY RegId DESC";
                string sql = $"select TOP(15) LabNo,PatientName,NoMedRec,StatusDisplay From vc_patientStatusDisplay where RegType='RJ' ORDER BY UpdatedDate DESC";
                return GetDataTable(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataStruct.SearchResult GetResultByNik(string nik)
        {
            try
            {
                //string sql = $"select RegId,PatientId,PatientName,RegType From vSearch_registration where Convert(varchar(10),regdate,120)='{DateTime.Now.ToString("yyyy-MM-dd")}' ORDER BY RegId DESC";
                string sql = $"select TOP 1 * FROM vSearch_Registration WHERE PatientID='{nik}' ORDER BY RegDate DESC";
                var dtHeader =  GetDataTable(sql);

                if (dtHeader != null)
                {

                }

                string sqlDetail = $"SELECT * FROM Tr_Hasil WHERE RegID='{"test"}'";
                var dtDetail = GetDataTable(sqlDetail);

                var data = new DataStruct.SearchResult();
                data.NoRm = "";
                data.PatientName = "";
                data.RegId = "";
                data.ResultDetail = new List<DataStruct.SearchResultDetail>();
                foreach (var row in dtDetail.Rows)
                {

                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
