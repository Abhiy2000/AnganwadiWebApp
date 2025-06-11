using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataCPSMS
    {
        BoCPSMS obcpsms;

        public DataCPSMS()
        { }

        public DataCPSMS(BoCPSMS cpsms)
        {
            obcpsms = cpsms;
        }

        public void UpdateCPSMS(BoCPSMS cpsms)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_cpsms_updt", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_FileName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("in_ParamStr", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = cpsms.UserId;
            Cmd.Parameters["in_FileName"].Value = cpsms.FileName;

            Cmd.Parameters["in_ParamStr"].Value = cpsms.Str;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            cpsms.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            cpsms.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
