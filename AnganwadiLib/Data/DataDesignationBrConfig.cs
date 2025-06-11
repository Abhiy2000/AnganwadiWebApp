using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataDesignationBrConfig
    {
        DesignationBrConfig ObjDesignationBrConfig;

        public DataDesignationBrConfig()
        { }

        public DataDesignationBrConfig(DesignationBrConfig BoDesignationBrConfig)
        {
            ObjDesignationBrConfig = BoDesignationBrConfig;
        }

        public void Insert(DesignationBrConfig BoDesignationBrConfig)
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
                com.CommandText = "aoup_designation_ins";

                //com.Parameters.Add(new OracleParameter("in_BrId", OracleDbType.Varchar2));
                //com.Parameters["in_BrId"].Value = ObjDesignationBrConfig.BrId;
                com.Parameters.Add(new OracleParameter("in_DesignationId", OracleDbType.Varchar2));
                com.Parameters.Add(new OracleParameter("in_Designation", OracleDbType.Varchar2));
                com.Parameters.Add(new OracleParameter("in_Code", OracleDbType.Varchar2));
                com.Parameters.Add(new OracleParameter("in_flag", OracleDbType.Varchar2));
                com.Parameters.Add(new OracleParameter("in_UserId", OracleDbType.Varchar2));
                com.Parameters.Add(new OracleParameter("in_Mode", OracleDbType.Varchar2));
                com.Parameters.Add(new OracleParameter("out_ErrorCode", OracleDbType.Int64, 100));
                com.Parameters.Add(new OracleParameter("out_ErrorMsg", OracleDbType.Varchar2, 100));

                com.Parameters["in_DesignationId"].Value = ObjDesignationBrConfig.DesignationId;
                com.Parameters["in_Designation"].Value = ObjDesignationBrConfig.Designation;
                com.Parameters["in_Code"].Value = ObjDesignationBrConfig.Code;
                com.Parameters["in_flag"].Value = ObjDesignationBrConfig.Flag;
                com.Parameters["in_UserId"].Value = ObjDesignationBrConfig.UserId;
                com.Parameters["in_Mode"].Value = ObjDesignationBrConfig.Mode;

                com.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
                com.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();

                String Errcode = com.Parameters["out_Errorcode"].Value.ToString().ToString();
                ObjDesignationBrConfig.ErrCode = Convert.ToInt32(Errcode);

                String Errmsg = com.Parameters["out_ErrorMsg"].Value.ToString();
                ObjDesignationBrConfig.ErrMsg = Errmsg;
            }
            catch (Exception ex)
            {
                ObjDesignationBrConfig.ErrCode = 0;
                ObjDesignationBrConfig.ErrMsg = ex.Message;
            }
            finally
            {
                com.Dispose();
                conn.Dispose();
            }
        }
    }
}
