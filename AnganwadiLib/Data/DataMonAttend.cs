using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataMonAttend
    {
        BoMonAttend objattend;

        public DataMonAttend()
        { }

        public DataMonAttend(BoMonAttend attend)
        {
            objattend = attend;
        }

        public void InsertAttend(BoMonAttend attend)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_salcalc_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            //Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Date", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AnganID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PrjTypeId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_TotalDays", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_ParamStr", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = attend.UserId;
            Cmd.Parameters["in_CompID"].Value = attend.CompID;
            //Cmd.Parameters["in_SevikaID"].Value = attend.SevikaID;
            Cmd.Parameters["in_Date"].Value = attend.Date;
            if (attend.AnganID == 0)
            {
                Cmd.Parameters["in_AnganID"].Value = DBNull.Value;
            }
            else
            {
                Cmd.Parameters["in_AnganID"].Value = attend.AnganID;
            }
            Cmd.Parameters["in_PrjTypeId"].Value = attend.PrjTypeId;
            Cmd.Parameters["in_TotalDays"].Value = attend.TotalDays;
            Cmd.Parameters["in_ParamStr"].Value = attend.ParamStr;
            Cmd.Parameters["in_Mode"].Value = attend.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            attend.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            attend.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }

        public void AuthorisedSalaryCal(BoMonAttend attend)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_Authsalcalc_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Date", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PrjTypeId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_TotalDays", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_ParamStr", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = attend.UserId;
            Cmd.Parameters["in_CompID"].Value = attend.CompID;
            Cmd.Parameters["in_Date"].Value = attend.Date;
            Cmd.Parameters["in_PrjTypeId"].Value = attend.PrjTypeId;
            Cmd.Parameters["in_TotalDays"].Value = attend.TotalDays;
            Cmd.Parameters["in_ParamStr"].Value = attend.ParamStr;


            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            attend.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            attend.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }

        public void DeleteAttendance(BoMonAttend attend)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_AttendanceDelete_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Date", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PrjTypeId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_TotalDays", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_ParamStr", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = attend.UserId;
            Cmd.Parameters["in_CompID"].Value = attend.CompID;
            Cmd.Parameters["in_Date"].Value = attend.Date;
            Cmd.Parameters["in_PrjTypeId"].Value = attend.PrjTypeId;
            Cmd.Parameters["in_TotalDays"].Value = attend.TotalDays;
            Cmd.Parameters["in_ParamStr"].Value = attend.ParamStr;


            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            attend.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            attend.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
        
    }
}
