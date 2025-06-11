using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
     
    public class DataLICPayment
    {
        BoLICPayment ObjLICPayment;

        public DataLICPayment()
        { }

        public DataLICPayment(BoLICPayment BoLICPay)
        {
            ObjLICPayment = BoLICPay;
        }


        public void InsertLICPayment(BoLICPayment BoLICPaymt)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_LicPayment_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_SevikaId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Str", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_SevikaId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Str"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_SevikaId"].Value = BoLICPaymt.SevikaID;
            Cmd.Parameters["in_UserId"].Value = BoLICPaymt.UserID;
            Cmd.Parameters["in_Str"].Value = BoLICPaymt.Str;
            Cmd.Parameters["out_Errorcode"].Value = BoLICPaymt.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICPaymt.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICPaymt.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICPaymt.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }
    }
}
