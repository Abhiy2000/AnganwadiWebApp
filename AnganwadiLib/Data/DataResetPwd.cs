using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataResetPwd
    {
        BoResetPwd ObjResetPwd;

        public DataResetPwd()
        { }

        public DataResetPwd(BoResetPwd BoResetPwd)
        {
            ObjResetPwd = BoResetPwd;
        }

        public void Update(BoResetPwd BoResetPwd)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_resetpassword_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_NewPassword", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_insby", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_remark", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_NewPassword"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_insby"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_remark"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BoResetPwd.UserId;
            Cmd.Parameters["in_NewPassword"].Value = BoResetPwd.NewPassword;
            Cmd.Parameters["in_insby"].Value = BoResetPwd.Insby;
            Cmd.Parameters["in_remark"].Value = BoResetPwd.Remark;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoResetPwd.ErrCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoResetPwd.ErrMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
