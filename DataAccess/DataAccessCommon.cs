using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace PktDataAccess
{
    public  class DataAccessCommon : DataAccessBase
    {
        //public List<PktViewModel.DropdownModel> GetDocumentProfile(string profileId = "")
        //{
        //    try
        //    {
        //        string sql = "select ID,USER_DEFINED_TYPE from sys_user_defined_index where id>0 and OBJECT_TYPE='D' and RECORD_STATUS='A'";

        //        if (profileId != "")
        //            sql += $"AND ID={profileId}";

        //        DataTable dtData = GetDataTable(sql);
        //        List<PktViewModel.DropdownModel> oData = new List<PktViewModel.DropdownModel>();

        //        foreach (DataRow row in dtData.Rows)
        //        {
        //            oData.Add(new PktViewModel.DropdownModel
        //            {
        //                Id = row["ID"].ToString(),
        //                Name = row["USER_DEFINED_TYPE"].ToString()
        //            });
        //        }

        //        return oData;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public List<PktViewModel.DropdownModel> GetLocationNew()
        //{
        //    try
        //    {

        //        //get all document parent
        //        string sqlLocationDoc = "select id,root_name from dms_root where RECORD_STATUS='A' and root_type='U' and Id > 0";
        //        DataTable dtLocationDoc = GetDataTable(sqlLocationDoc);

        //        List<PktViewModel.DropdownModel> oListLocation = new List<PktViewModel.DropdownModel>();

        //        foreach (DataRow row in dtLocationDoc.Rows)
        //        {
        //            oListLocation.Add(new PktViewModel.DropdownModel()
        //            {
        //                Id = row["ID"].ToString(),
        //                Name = row["root_name"].ToString()
        //            });
        //        }

        //        return oListLocation;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public List<PktViewModel.DropdownModel> GetCreator()
        //{
        //    try
        //    {
        //        DataTable dtData = GetDataTable("select ID,FULL_NAME from user_record WHERE RECORD_STATUS='A'");
        //        List<PktViewModel.DropdownModel> oData = new List<PktViewModel.DropdownModel>();

        //        foreach (DataRow row in dtData.Rows)
        //        {
        //            oData.Add(new PktViewModel.DropdownModel
        //            {
        //                Id = row["ID"].ToString(),
        //                Name = row["FULL_NAME"].ToString()
        //            });
        //        }

        //        return oData;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public DataTable GetProfileDetail(string profileId)
        //{
        //    try
        //    {
        //        string sql = $"SELECT id,field_name,field_type FROM sys_user_defined_index_detail where user_defined_id={profileId} order by display_seq";
        //        DataTable dtData = GetDataTable(sql);
                
        //        return dtData;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
