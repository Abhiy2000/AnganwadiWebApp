using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataMaritStat
    {
        BoMaritStat objMaritStat;

        public DataMaritStat()
        { }

        public DataMaritStat(BoMaritStat MaritStat)
        {
            objMaritStat = MaritStat;
        }

        public void InsertMaritStat(BoMaritStat MaritStat)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_maritstat_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_MaritStatId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_MaritStatName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_MaritStatId"].Value = MaritStat.MaritStatId;
            Cmd.Parameters["in_MaritStatName"].Value = MaritStat.MaritStatName;
            Cmd.Parameters["in_UserId"].Value = MaritStat.UserId;
            Cmd.Parameters["in_Mode"].Value = MaritStat.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            MaritStat.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            MaritStat.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
