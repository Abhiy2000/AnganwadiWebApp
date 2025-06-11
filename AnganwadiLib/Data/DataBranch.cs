using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using AnganwadiLib.Business;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataBranch
    {
        BoBranch objBranch;

        public DataBranch()
        { }

        public DataBranch(BoBranch Branch)
        {
            objBranch = Branch;
        }

        public void InsertBranch(BoBranch Branch)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_bankbranch_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
           
            Cmd.Parameters.Add("in_BranchId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_BranchName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_BankId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_IFSCCode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 5000000).Direction = ParameterDirection.Output;

            //Cmd.Parameters["in_UserId"].Value = Branch.UserId;
            Cmd.Parameters["in_BranchId"].Value = Branch.Branchid;
            Cmd.Parameters["in_BranchName"].Value = Branch.Branchname;
            Cmd.Parameters["in_BankId"].Value = Branch.Bankid;
            Cmd.Parameters["in_IFSCCode"].Value = Branch.Ifscnumber;
            Cmd.Parameters["in_UserId"].Value = Branch.UserId;
            Cmd.Parameters["in_Mode"].Value = Branch.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            Branch.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            Branch.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
