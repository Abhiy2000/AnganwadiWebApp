using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataAllowence
    {
        BoAllowence objallow;

        public DataAllowence()
        { }

        public DataAllowence(BoAllowence allow)
        {
            objallow = allow;
        }

        public void InsertAllow(BoAllowence allow)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_allowence_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_DesgId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PrjTypeId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_EduId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AllowName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AllowAmt", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2,500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = allow.UserId;
            Cmd.Parameters["in_CompID"].Value = allow.CompId;
            Cmd.Parameters["in_DesgId"].Value = allow.DesgId;
            Cmd.Parameters["in_PrjTypeId"].Value = allow.PrjId;
            Cmd.Parameters["in_EduId"].Value = allow.EduId;
            Cmd.Parameters["in_AllowName"].Value = allow.AllowName;
            Cmd.Parameters["in_AllowAmt"].Value = allow.Amt;
            Cmd.Parameters["in_Mode"].Value = allow.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            allow.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            allow.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
