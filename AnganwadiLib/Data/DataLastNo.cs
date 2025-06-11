using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using AnganwadiLib.Business;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataLastNo
    {
        BoLastNo objLast;

        public DataLastNo()
        { }

        public DataLastNo(BoLastNo Last)
        {
            objLast = Last;
        }

        public void SetLastNo(BoLastNo Last)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_lastno_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_type", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Date", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_LastNo", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = Last.UserId;
            Cmd.Parameters["in_type"].Value = Last.Type;
            Cmd.Parameters["in_Date"].Value = Last.Date;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            Last.LastNo = Convert.ToInt32(Cmd.Parameters["out_LastNo"].Value.ToString());
            Last.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            Last.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
