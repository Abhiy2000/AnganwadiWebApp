using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Data
{
    public class DataPolicyUpdate
    {
        BoPolicyUpdate objPolicyUpdate;

        public DataPolicyUpdate()
        { }

        public DataPolicyUpdate(BoPolicyUpdate PolicyUpdate)
        {
            objPolicyUpdate = PolicyUpdate;
        }

        public void InsertPolicy(BoPolicyUpdate _PolicyUpdate)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_PolicyUpd_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_divid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_distid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_cdpoid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_bitcode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_str", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 5000000).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = _PolicyUpdate.UserId;
            Cmd.Parameters["in_divid"].Value = _PolicyUpdate.DivId;
            Cmd.Parameters["in_distid"].Value = _PolicyUpdate.DistId;
            Cmd.Parameters["in_cdpoid"].Value = _PolicyUpdate.CDPOId;
            Cmd.Parameters["in_bitcode"].Value = _PolicyUpdate.BitCode;
            Cmd.Parameters["in_str"].Value = _PolicyUpdate.ParamStr;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            _PolicyUpdate.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_ErrorCode"].Value.ToString());
            _PolicyUpdate.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
