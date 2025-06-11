using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataBillGen
    {
        BoBillGen objbillgen;

        public DataBillGen()
        { }

        public DataBillGen(BoBillGen BillGen)
        {
            objbillgen = BillGen;
        }

        public void GenerateBill(BoBillGen BillGen)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_BillGen_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SalDate", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_BillDate", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_DesgType", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SalSlab", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SevikaType", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_ParentId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2,500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BillGen.UserId;
            Cmd.Parameters["in_CompID"].Value = BillGen.CompID;
            Cmd.Parameters["in_SalDate"].Value = BillGen.SalDate.ToString("dd-MMM-yyyy");
            Cmd.Parameters["in_BillDate"].Value = BillGen.BillDate.ToString("dd-MMM-yyyy");
            Cmd.Parameters["in_DesgType"].Value = BillGen.DesgType;
            Cmd.Parameters["in_SalSlab"].Value = BillGen.SalSlab;
            Cmd.Parameters["in_SevikaType"].Value = BillGen.SevikaType;
            Cmd.Parameters["in_ParentId"].Value = BillGen.ParentId;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            BillGen.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            BillGen.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }

        public void GenerateDiffBill(BoBillGen BillGen)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_DiffBillGen_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SalDate", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_BillDate", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_DesgType", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SalSlab", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_SevikaType", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_ParentId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BillGen.UserId;
            Cmd.Parameters["in_CompID"].Value = BillGen.CompID;
            Cmd.Parameters["in_SalDate"].Value = BillGen.SalDate.ToString("dd-MMM-yyyy");
            Cmd.Parameters["in_BillDate"].Value = BillGen.BillDate.ToString("dd-MMM-yyyy");
            Cmd.Parameters["in_DesgType"].Value = BillGen.DesgType;
            Cmd.Parameters["in_SalSlab"].Value = BillGen.SalSlab;
            Cmd.Parameters["in_SevikaType"].Value = BillGen.SevikaType;
            Cmd.Parameters["in_ParentId"].Value = BillGen.ParentId;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            BillGen.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            BillGen.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
