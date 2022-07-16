using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Web;

namespace PktDataAccess
{
    public class DataAccessExport : DataAccessCommon
    {

        
        //private string _parent = "";
        //private void GetParent(string parentId, DataTable dtFolder)
        //{
        //    DataRow[] parentRow = dtFolder.Select($"ID='{parentId}'");
        //    if (parentRow.Length != 0)
        //    {
        //        _parent = parentRow[0]["DOCUMENT_NAME"].ToString() + "\\" + _parent;
        //        GetParent(parentRow[0]["PARENT_ID"].ToString(), dtFolder);
        //    }

        //}

        //public DataSet GetDocumentMetadata(string operatorCode, string location, string docProfile, string startDate, string endDate,string searchText, bool isExport)
        //{
        //    string sqlWhereDate = "1=1";
        //    if (startDate != "")
        //        sqlWhereDate = $" (FORMAT(a.CREATE_DATE, 'yyyy-MM-dd') >= '{startDate}' and FORMAT(a.CREATE_DATE, 'yyyy-MM-dd') <= '{endDate}')";

        //    string sqlWhereOperator = "1=1";
        //    if (operatorCode != "all")
        //        sqlWhereOperator = $" a.CREATOR_ID={operatorCode}";

        //    string sqlWhereDocLocation = "1=1";
        //    if (location != "all")
        //        sqlWhereDocLocation = $" a.ROOT_ID={location}";

        //    List<PktViewModel.DropdownModel> listProfile = null;
        //    if (docProfile != "all")
        //        listProfile = GetDocumentProfile(docProfile);
        //    else
        //        listProfile = GetDocumentProfile();
            

        //    string sqlWhereMetadata = "1=1";
        //    if (searchText != "")
        //        sqlWhereMetadata = $" a.ID IN (SELECT DISTINCT DOCUMENT_ID FROM udv_document_index WHERE FIELD_VALUE LIKE '%{searchText}%')";

        //    string sqlWhere = "";
        //    if (sqlWhereDate != "" || sqlWhereOperator != "all")
        //        sqlWhere = $" AND {sqlWhereDate} AND {sqlWhereOperator} AND {sqlWhereDocLocation} AND {sqlWhereMetadata}";


        //    DataSet dsMetadata = new DataSet();
        //    //enumerate all or spesific profile
        //    foreach (PktViewModel.DropdownModel profileRow in listProfile)
        //    {
        //        //get detail column for each profile
        //        DataTable dtProfileDetail = GetProfileDetail(profileRow.Id);
        //        string sField = "";

        //        foreach (DataRow detailRow in dtProfileDetail.Rows)
        //        {
        //            string field_name = detailRow["field_name"].ToString();
        //            string formattedField = "FIELD_VALUE";
        //            if (detailRow["field_type"].ToString() == "D")
        //                if (isExport)
        //                    formattedField = "FORMAT(CONVERT(datetime,FIELD_VALUE),'yyyy-MM-dd')";
        //                else
        //                    formattedField = "FORMAT(CONVERT(datetime,FIELD_VALUE),'dd-MMM-yyyy')";

        //            sField += $",MAX(CASE WHEN FIELD_NAME = '{field_name}' THEN {formattedField} ELSE '' END) AS \"{field_name}\"";

        //        }

        //        string path = "";
        //        if (isExport)
        //            path = HttpContext.Current.Server.MapPath("~/Query/FieldMetadataExport.txt");
        //        else
        //            path = HttpContext.Current.Server.MapPath("~/Query/FieldMetadata.txt");

        //        string userfield = System.IO.File.ReadAllText(path);

        //        string sqlMain = $"SELECT {userfield} FROM (SELECT * FROM udv_document WHERE USER_DEFINED_FIELD_ID = {profileRow.Id}) a ";
        //        string sqlInner = $"INNER JOIN (SELECT DOCUMENT_ID{sField} FROM udv_document_index WHERE USER_DEFINED_FIELD_ID = {profileRow.Id} GROUP BY DOCUMENT_ID) b ON a.ID = b.DOCUMENT_ID WHERE 1=1";

        //        string sql = sqlMain + sqlInner + sqlWhere;

        //        DataTable dtData = GetDataTable(sql, profileRow.Name);
                
        //        if (dtData.Rows.Count > 0)
        //        {

        //            //remove unnecessary columns
        //            try
        //            {
        //                dtData.Columns.Remove("DOCUMENT_ID");
        //                dtData.Columns.Remove("Tanggal Mulai Berlaku1");
        //                dtData.Columns.Remove("Tanggal Akhir Berlaku1");
        //            }
        //            catch (Exception)
        //            {
        //                //do nothing
        //            }
                    

        //            //DataTable dtCloned = dtData.Clone();
        //            //foreach (DataColumn coll in dtCloned.Columns)
        //            //{
        //            //    if (coll.ColumnName.Contains("Tanggal") || coll.ColumnName.Contains("date"))
        //            //        coll.DataType = typeof(DateTime);
        //            //}

        //            //foreach (DataRow row in dtData.Rows)
        //            //{
        //            //    dtCloned.ImportRow(row);
        //            //}

        //            //add to dataset
        //            dsMetadata.Tables.Add(dtData);
        //        }

        //    }
        //    return dsMetadata;

        //}
    }
}
