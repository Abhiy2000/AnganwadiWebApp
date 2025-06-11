using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataAdditionalPay
    {
        AnganwadiLib.Business.BoAdditionalPay objBoAdditionalPay;
        public DataAdditionalPay()
        {

        }
        public void Insert(AnganwadiLib.Business.BoAdditionalPay _BoAdditionalPay)
        {
            this.objBoAdditionalPay = _BoAdditionalPay;
            AnganwadiLib.Data.GetCon Con = new GetCon();
            OracleCommand cmd = new OracleCommand("Aoup_AddPay_ins", Con.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_AddPayUniqID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_COMPID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            

            cmd.Parameters.Add("in_SEVIKAID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_TYPE", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_FROMDATE", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_TODATE", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_STATUS", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.Parameters["in_Mode"].Value = objBoAdditionalPay.Mode;
            cmd.Parameters["in_UserId"].Value = objBoAdditionalPay.UserName;
            cmd.Parameters["in_AddPayUniqID"].Value = objBoAdditionalPay.AddPayUniqID;

            cmd.Parameters["in_COMPID"].Value = objBoAdditionalPay.COMPID;
            cmd.Parameters["in_SEVIKAID"].Value = objBoAdditionalPay.SEVIKAID;
            cmd.Parameters["in_TYPE"].Value = objBoAdditionalPay.TYPE;
            cmd.Parameters["in_FROMDATE"].Value = objBoAdditionalPay.FROMDATE;
            cmd.Parameters["in_TODATE"].Value = objBoAdditionalPay.TODATE;
            cmd.Parameters["in_STATUS"].Value = objBoAdditionalPay.STATUS;

            Con.OpenConn();
            cmd.ExecuteNonQuery();
            Con.CloseConn();


            objBoAdditionalPay.ErrorCode = Convert.ToInt32(cmd.Parameters["out_Errorcode"].Value.ToString());
            objBoAdditionalPay.ErrorMessage = cmd.Parameters["out_ErrorMessage"].Value.ToString();

        }

    }
}
