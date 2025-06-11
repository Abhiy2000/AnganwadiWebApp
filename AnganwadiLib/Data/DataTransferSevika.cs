using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataTransferSevika
    {
        BoTransferSevika objTrfSev;

        public DataTransferSevika()
        { }

        public DataTransferSevika(BoTransferSevika TrfSev)
        {
            objTrfSev = TrfSev;
        }

        public void Insert(BoTransferSevika TrfSev)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_transfer_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_OldBitId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_NewBitId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_OldAnganwadiId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_NewAnganwadiId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AadharNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SevikaId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_OldBitId"].Value = TrfSev.OldBitId;
            Cmd.Parameters["in_NewBitId"].Value = TrfSev.NewBitId;
            Cmd.Parameters["in_OldAnganwadiId"].Value = TrfSev.OldAnganwadiId;
            Cmd.Parameters["in_NewAnganwadiId"].Value = TrfSev.NewAnganwadiId;
            Cmd.Parameters["in_AadharNo"].Value = TrfSev.AadharNo;
            Cmd.Parameters["in_SevikaId"].Value = TrfSev.SevikaId;
            Cmd.Parameters["in_UserId"].Value = TrfSev.UserId;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            TrfSev.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            TrfSev.ErrMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }

        public void Authorized(BoTransferSevika TrfSev)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_transfer_auth", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_OldBitId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_NewBitId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_OldAnganwadiId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_NewAnganwadiId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AadharNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SevikaId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Status", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Remark", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_TransferDate", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_OldBitId"].Value = TrfSev.OldBitId;
            Cmd.Parameters["in_NewBitId"].Value = TrfSev.NewBitId;
            Cmd.Parameters["in_OldAnganwadiId"].Value = TrfSev.OldAnganwadiId;
            Cmd.Parameters["in_NewAnganwadiId"].Value = TrfSev.NewAnganwadiId;
            Cmd.Parameters["in_AadharNo"].Value = TrfSev.AadharNo;
            Cmd.Parameters["in_SevikaId"].Value = TrfSev.SevikaId;
            Cmd.Parameters["in_Status"].Value = TrfSev.Status;
            Cmd.Parameters["in_Remark"].Value = TrfSev.Remark;
            Cmd.Parameters["in_TransferDate"].Value = TrfSev.TransferDate;
            Cmd.Parameters["in_UserId"].Value = TrfSev.UserId;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            TrfSev.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            TrfSev.ErrMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
