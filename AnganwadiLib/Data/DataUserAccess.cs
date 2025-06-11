using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;

namespace AnganwadiLib.Data
{
    public class DataUserAccess
    {
        UserAccess ObjUserAccess;

        public DataUserAccess()
        {
        }

        public DataUserAccess(UserAccess BoUserAccess)
        {
            ObjUserAccess = BoUserAccess;
        }

        public void UpDate(UserAccess BoUserAccess)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();
            OracleCommand Cmd = new OracleCommand("aoup_useraccess_ins", Con.connection);

            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_BrId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_UserCode", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_AccessString", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_BrId"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_UserCode"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_AccessString"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_UserId"].Direction = System.Data.ParameterDirection.Input;

            Cmd.Parameters["out_ErrorCode"].Direction = System.Data.ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = System.Data.ParameterDirection.Output;

            Cmd.Parameters["in_BrId"].Value = ObjUserAccess.BrId;
            Cmd.Parameters["in_Usercode"].Value = ObjUserAccess.UserCode;
            Cmd.Parameters["in_AccessString"].Value = ObjUserAccess.AccessString;
            Cmd.Parameters["in_UserId"].Value = ObjUserAccess.UserId;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            ObjUserAccess.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            ObjUserAccess.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
