using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataAdditionalCharge
    {
        AnganwadiLib.Business.BoAdditionalCharge objAdditionalCharge;
        public DataAdditionalCharge()
        {

        }
        public void Insert(AnganwadiLib.Business.BoAdditionalCharge _BoAdditionalCharge)
        {
            this.objAdditionalCharge = _BoAdditionalCharge;
            AnganwadiLib.Data.GetCon Con = new GetCon();
            OracleCommand cmd = new OracleCommand("Aoup_AdditionalCharge_ins", Con.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_COMPID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_BITCODE", OracleDbType.Varchar2).Direction = ParameterDirection.Input; 
            cmd.Parameters.Add("in_ANGANID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_SEVIKAID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NEWANGANID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_FROMDATE", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_TODATE", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_REASON", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            
            cmd.Parameters["in_UserId"].Value = objAdditionalCharge.UserName;
            cmd.Parameters["in_COMPID"].Value = objAdditionalCharge.COMPID;
            cmd.Parameters["in_BITCODE"].Value = objAdditionalCharge.BITCODE;
            cmd.Parameters["in_ANGANID"].Value = objAdditionalCharge.ANGANID;
            cmd.Parameters["in_SEVIKAID"].Value = objAdditionalCharge.SEVIKAID;
            cmd.Parameters["in_NEWANGANID"].Value = objAdditionalCharge.NEWANGANID;
            cmd.Parameters["in_FROMDATE"].Value = objAdditionalCharge.FROMDATE;
            cmd.Parameters["in_TODATE"].Value = objAdditionalCharge.TODATE;
            cmd.Parameters["in_REASON"].Value = objAdditionalCharge.REASON;

            Con.OpenConn();
            cmd.ExecuteNonQuery();
            Con.CloseConn();


            objAdditionalCharge.ErrorCode = Convert.ToInt32(cmd.Parameters["out_Errorcode"].Value.ToString());
            objAdditionalCharge.ErrorMessage = cmd.Parameters["out_ErrorMessage"].Value.ToString();

        }

    }
}
