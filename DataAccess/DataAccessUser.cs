using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Newtonsoft.Json;
using System.Web;

namespace DataAccess
{
    public class DataAccessUser : DataAccessBase
    {
        public DataTable GetUserForLogin(string username, string password)
        {
            try
            {
                DataTable dtData = GetDataTable($"SELECT * FROM Users WHERE userid='{username}'");
                if (dtData.Rows.Count == 0)
                    return null;

                if (Convert.ToBoolean(dtData.Rows[0]["isLock"]) == true)
                {
                        return null;
                }
                else
                {
                    return dtData;
                    //if (Convert.ToString(dtData.Rows[0]["Password"]) == HashClass.GenerateSHA256String(password))
                    //    return dtData;
                    //else
                    //    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
