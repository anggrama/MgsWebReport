using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DataAccessBase
    {
        SqlConnection oCn;

        public DataAccessBase()
        {
            string cnstr = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ToString();
            oCn = new SqlConnection(cnstr);
        }
        public DataTable GetDataTable(string query)
        {
            SqlDataAdapter oDa = new SqlDataAdapter(query, oCn);
            oDa.SelectCommand.CommandTimeout = 1000;
            try
            {
                DataTable dt = new DataTable();
                oDa.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetDataTable(string query, string tableName)
        {
            SqlDataAdapter oDa = new SqlDataAdapter(query, oCn);
            oDa.SelectCommand.CommandTimeout = 600;
            try
            {
                DataTable dt = new DataTable(tableName);
                oDa.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object ExecuteScalar(string query)
        {
            SqlCommand oCmd = new SqlCommand(query, oCn);
            oCmd.CommandTimeout = 600;
            try
            {
                oCn.Open();
                return oCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oCn.Close();
            }
        }
        public void ExecuteQuery(string query)
        {
            SqlCommand oCmd = new SqlCommand(query, oCn);
            oCmd.CommandTimeout = 600;
            try
            {
                oCn.Open();
                oCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oCn.Close();
            }
        }
        public object[] ExecuteSp(string spName, SqlParameter[] param, int[] indexOut)
        {

            SqlTransaction oTrans = null;
            SqlCommand oCmd = new SqlCommand(spName, oCn);
            oCmd.CommandType = CommandType.StoredProcedure;

            if (param != null)
                oCmd.Parameters.AddRange(param);

            try
            {
                oCn.Open();
                oTrans = oCn.BeginTransaction();
                oCmd.Transaction = oTrans;

                oCmd.ExecuteNonQuery();

                oTrans.Commit();

                object[] value = null;
                if (indexOut != null)
                {
                    value = new object[indexOut.Length];
                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] = param[indexOut[i]].Value;
                    }
                }

                return value;
            }
            catch (Exception)
            {
                oTrans.Rollback();
                throw;
            }
            finally
            {
                oCn.Close();
            }
        }
    }
}
