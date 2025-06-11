using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataCorporationMasterBrConfig
    {
        CorporationMasterBrConfig ObjCorporationMasterBrConfig;        
        
        public DataCorporationMasterBrConfig()
        { }

        public DataCorporationMasterBrConfig(CorporationMasterBrConfig BoCorporationMasterBrConfig)
        {
            ObjCorporationMasterBrConfig = BoCorporationMasterBrConfig;
        }

        public void Insert(CorporationMasterBrConfig BoCorporationMasterBrConfig)
        {
            OracleConnection conn = Cls_Dbconnection.dbconn();
            OracleCommand com = new OracleCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else
                {
                    conn.Close();
                    conn.Open();
                }
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = conn;
                com.CommandText = "aoup_corporation_master_ins";

                com.Parameters.Add(new OracleParameter("in_BrId", OracleDbType.Int64));
                com.Parameters["in_BrId"].Value = ObjCorporationMasterBrConfig.BrId;

                //com.Parameters.Add(new OracleParameter("in_CorpId", OracleDbType.Int64));
                //com.Parameters["in_CorpId"].Value = ObjCorporationMasterBrConfig.CorpId;

                com.Parameters.Add(new OracleParameter("in_ParentId", OracleDbType.Int64));
                com.Parameters["in_ParentId"].Value = ObjCorporationMasterBrConfig.ParentID;

                com.Parameters.Add(new OracleParameter("in_CorporationName", OracleDbType.Varchar2));
                com.Parameters["in_CorporationName"].Value = ObjCorporationMasterBrConfig.CorporationName;

                com.Parameters.Add(new OracleParameter("in_Class", OracleDbType.Varchar2));
                com.Parameters["in_Class"].Value = ObjCorporationMasterBrConfig.Class;

                com.Parameters.Add(new OracleParameter("in_CorpBranch", OracleDbType.Varchar2));
                com.Parameters["in_CorpBranch"].Value = ObjCorporationMasterBrConfig.CorpBranch;

                com.Parameters.Add(new OracleParameter("in_UserId", OracleDbType.Varchar2));
                com.Parameters["in_UserId"].Value = ObjCorporationMasterBrConfig.UserId;

                com.Parameters.Add(new OracleParameter("out_ErrorCode", OracleDbType.Int64, 100));
                com.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("out_ErrorMsg", OracleDbType.Varchar2, 100));
                com.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();

                String Errcode = com.Parameters["out_Errorcode"].Value.ToString().ToString();
                ObjCorporationMasterBrConfig.ErrCode = Convert.ToInt32(Errcode);

                String Errmsg = com.Parameters["out_ErrorMsg"].Value.ToString();
                ObjCorporationMasterBrConfig.ErrMsg = Errmsg;
            }
            catch (Exception ex)
            {
                ObjCorporationMasterBrConfig.ErrCode = 0;
                ObjCorporationMasterBrConfig.ErrMsg = ex.Message;
            }
            finally
            {
                com.Dispose();
                conn.Dispose();
            }
        }
    }
}
