using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataAttendenceAccess
    {
        BoAttendenceAccess objAttend;

        public DataAttendenceAccess()
        { }

        public DataAttendenceAccess(BoAttendenceAccess AttendAccess)
        {
            objAttend = AttendAccess;
        }

        public void Insert(BoAttendenceAccess AttendAccess)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_attendencemenu_access", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_StateId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Str", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Str1", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Str2", OracleDbType.Varchar2);

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_StateId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Str"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Str1"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Str2"].Direction = ParameterDirection.Input;

            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = AttendAccess.UserId;
            Cmd.Parameters["in_StateId"].Value = AttendAccess.StateId;            
            Cmd.Parameters["in_Str"].Value = AttendAccess.Str;
            Cmd.Parameters["in_Str1"].Value = AttendAccess.Str1;
            Cmd.Parameters["in_Str2"].Value = AttendAccess.Str2;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            AttendAccess.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            AttendAccess.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
