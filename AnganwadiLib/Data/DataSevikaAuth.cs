using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataSevikaAuth
    {
        BoSevikaAuth objSevAuth;

        public DataSevikaAuth()
        { }

        public DataSevikaAuth(BoSevikaAuth SevAuth)
        {
            objSevAuth = SevAuth;
        }

        public void AuthSevika(BoSevikaAuth SevAuth)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_sevika_auth", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_CompId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SevikaCode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_CompId"].Value = SevAuth.CompId;
            Cmd.Parameters["in_SevikaCode"].Value = SevAuth.SevikaCode;
            Cmd.Parameters["in_UserId"].Value = SevAuth.UserId;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            SevAuth.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            SevAuth.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
