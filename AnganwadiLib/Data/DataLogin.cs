using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;
using System.Diagnostics;

namespace AnganwadiLib.Data
{
    public class DataLogin
    {
        Login ObjUser;

        public DataLogin()
        { }

        public DataLogin(Login BoUser)
        {
            ObjUser = BoUser;
        }

        public void Login()
        {
            OracleConnection conn = Cls_Dbconnection.dbconn();
            OracleCommand com = new OracleCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else
                {
                    conn.Close();
                    conn.Open();
                }
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = conn;
                com.CommandText = "aoup_login_fetch";

                com.Parameters.Add(new OracleParameter("in_UserId", OracleDbType.Varchar2));
                com.Parameters["in_UserId"].Value = ObjUser.UserId;

                com.Parameters.Add(new OracleParameter("in_password", OracleDbType.Varchar2));
                com.Parameters["in_password"].Value = ObjUser.Password;

                com.Parameters.Add(new OracleParameter("Out_CompId", OracleDbType.Int64, 50));
                com.Parameters["Out_CompId"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_UserName", OracleDbType.Varchar2, 100));
                com.Parameters["Out_UserName"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_LastLogin", OracleDbType.Varchar2, 100));
                com.Parameters["Out_LastLogin"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_LastLogOut", OracleDbType.Varchar2, 100));
                com.Parameters["Out_LastLogOut"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_LastChangePwd", OracleDbType.Varchar2, 100));
                com.Parameters["Out_LastChangePwd"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_IsBlock", OracleDbType.Varchar2, 2));
                com.Parameters["Out_IsBlock"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_DesgId", OracleDbType.Int64, 50));
                com.Parameters["Out_DesgId"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_brid", OracleDbType.Int64, 50));
                com.Parameters["Out_brid"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_branchname", OracleDbType.Varchar2, 100));
                com.Parameters["Out_branchname"].Direction = ParameterDirection.Output;
                
                com.Parameters.Add(new OracleParameter("Out_compname", OracleDbType.Varchar2, 100));
                com.Parameters["Out_compname"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_desgname", OracleDbType.Varchar2, 100));
                com.Parameters["Out_desgname"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_brcategory", OracleDbType.Int64, 100));
                com.Parameters["Out_brcategory"].Direction = ParameterDirection.Output;
                
                com.Parameters.Add(new OracleParameter("out_ErrorCode", OracleDbType.Int64, 1000));
                com.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_ErrorMsg", OracleDbType.Varchar2, 3000));
                com.Parameters["Out_ErrorMsg"].Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();

                String BrId = com.Parameters["Out_brid"].Value.ToString();
                ObjUser.Brid = BrId;

                String Branchname = com.Parameters["Out_branchname"].Value.ToString();
                ObjUser.Branchname = Branchname;
                
                String CompName = com.Parameters["Out_compname"].Value.ToString();
                ObjUser.Compname = CompName;

                String Compid = com.Parameters["Out_CompId"].Value.ToString();
                ObjUser.CompId = Compid;

                String UserName = com.Parameters["Out_UserName"].Value.ToString();
                ObjUser.UserName = UserName;

                String ErrCode = com.Parameters["out_Errorcode"].Value.ToString().ToString();
                ObjUser.ErrCode = ErrCode;

                String ErrorMsg = com.Parameters["Out_ErrorMsg"].Value.ToString();
                ObjUser.ErrMsg = ErrorMsg;

                string LastLogOut = com.Parameters["Out_LastLogOut"].Value.ToString();
                ObjUser.LastLogout = LastLogOut;

                string LastLogin = com.Parameters["Out_LastLogin"].Value.ToString();
                ObjUser.LastLogin = LastLogin;

                string LastChangePwd = com.Parameters["Out_LastChangePwd"].Value.ToString();
                ObjUser.Lastchangepwd = LastChangePwd;

                String IsBlock = com.Parameters["Out_IsBlock"].Value.ToString();
                ObjUser.Blocked = IsBlock;
                
                String DesgId = com.Parameters["Out_DesgId"].Value.ToString();
                ObjUser.Desgid = DesgId;
                
                String DesgName = com.Parameters["Out_desgname"].Value.ToString();
                ObjUser.DesgName = DesgName;

                String brcategory = com.Parameters["Out_brcategory"].Value.ToString();
                ObjUser.brcategory = brcategory;
            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(st.FrameCount - 1);
                // Get the line number from the stack frame
                var line1 = frame.GetFileLineNumber();
                //MessageAlert(ex.Message.ToString() + "line:" + line + "c#line:" + line1, "");

                ObjUser.ErrCode = "0";
                ObjUser.ErrMsg = ex.Message.ToString() + "line:" + line1;
            }
            finally
            {
                conn.Close();
                com.Dispose();
                conn.Dispose();
            }
        }

        public void LoginRandomPassword()
        {
            OracleConnection conn = Cls_Dbconnection.dbconn();
            OracleCommand com = new OracleCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else
                {
                    conn.Close();
                    conn.Open();
                }
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = conn;
                com.CommandText = "aoup_login_RandomPassfetch";

                com.Parameters.Add(new OracleParameter("in_Empcode", OracleDbType.Varchar2));
                com.Parameters["in_Empcode"].Value = ObjUser.UserId;

                com.Parameters.Add(new OracleParameter("in_password", OracleDbType.Varchar2));
                com.Parameters["in_password"].Value = ObjUser.Password;

                com.Parameters.Add(new OracleParameter("Out_CompId", OracleDbType.Int64, 50));
                com.Parameters["Out_CompId"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_UserName", OracleDbType.Varchar2, 100));
                com.Parameters["Out_UserName"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_LastLogin", OracleDbType.TimeStamp, 100));
                com.Parameters["Out_LastLogin"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_LastLogOut", OracleDbType.TimeStamp, 100));
                com.Parameters["Out_LastLogOut"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_LastChangePwd", OracleDbType.TimeStamp, 100));
                com.Parameters["Out_LastChangePwd"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_IsBlock", OracleDbType.Varchar2, 2));
                com.Parameters["Out_IsBlock"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_Type", OracleDbType.Int64, 50));
                com.Parameters["Out_Type"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_DesgId", OracleDbType.Int64, 50));
                com.Parameters["Out_DesgId"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_brid", OracleDbType.Int64, 50));
                com.Parameters["Out_brid"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_branchname", OracleDbType.Varchar2, 100));
                com.Parameters["Out_branchname"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_brcompid", OracleDbType.Int64, 50));
                com.Parameters["Out_brcompid"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_compname", OracleDbType.Varchar2, 100));
                com.Parameters["Out_compname"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_typename", OracleDbType.Varchar2, 50));
                com.Parameters["Out_typename"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_desgname", OracleDbType.Varchar2, 100));
                com.Parameters["Out_desgname"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_brcategory", OracleDbType.Int64, 100));
                com.Parameters["Out_brcategory"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_role", OracleDbType.Varchar2, 100));
                com.Parameters["Out_role"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_UserId", OracleDbType.Varchar2, 100));
                com.Parameters["Out_UserId"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("out_ErrorCode", OracleDbType.Int64, 1000));
                com.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_ErrorMsg", OracleDbType.Varchar2, 3000));
                com.Parameters["Out_ErrorMsg"].Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();

                String BrId = com.Parameters["Out_brid"].Value.ToString();
                ObjUser.Brid = BrId;

                String Branchname = com.Parameters["Out_branchname"].Value.ToString();
                ObjUser.Branchname = Branchname;

                String BrCompId = com.Parameters["Out_brcompid"].Value.ToString();
                ObjUser.Brcompid = BrCompId;

                String CompName = com.Parameters["Out_compname"].Value.ToString();
                ObjUser.Compname = CompName;

                String Compid = com.Parameters["Out_CompId"].Value.ToString();
                ObjUser.CompId = Compid;

                String UserName = com.Parameters["Out_UserName"].Value.ToString();
                ObjUser.UserName = UserName;

                String ErrCode = com.Parameters["out_Errorcode"].Value.ToString().ToString();
                ObjUser.ErrCode = ErrCode;

                String ErrorMsg = com.Parameters["Out_ErrorMsg"].Value.ToString();
                ObjUser.ErrMsg = ErrorMsg;

                string LastLogOut = com.Parameters["Out_LastLogOut"].Value.ToString();
                ObjUser.LastLogout = LastLogOut;

                string LastLogin = com.Parameters["Out_LastLogin"].Value.ToString();
                ObjUser.LastLogin = LastLogin;

                string LastChangePwd = com.Parameters["Out_LastChangePwd"].Value.ToString();
                ObjUser.Lastchangepwd = LastChangePwd;

                String IsBlock = com.Parameters["Out_IsBlock"].Value.ToString();
                ObjUser.Blocked = IsBlock;

                String Type = com.Parameters["Out_Type"].Value.ToString();
                ObjUser.Type = Type;

                String DesgId = com.Parameters["Out_DesgId"].Value.ToString();
                ObjUser.Desgid = DesgId;

                String TypeName = com.Parameters["Out_typename"].Value.ToString();
                ObjUser.TypeName = TypeName;


                String DesgName = com.Parameters["Out_desgname"].Value.ToString();
                ObjUser.DesgName = DesgName;

                String brcategory = com.Parameters["Out_brcategory"].Value.ToString();
                ObjUser.brcategory = brcategory;


                String Role = com.Parameters["Out_role"].Value.ToString();
                ObjUser.Role = Role;

                String UserId = com.Parameters["Out_UserId"].Value.ToString();
                ObjUser.UserId = UserId;
            }
            catch (Exception ex)
            {
                ObjUser.ErrCode = "0";
                ObjUser.ErrMsg = ex.Message;
            }
            finally
            {
                conn.Close();
                com.Dispose();
                conn.Dispose();
            }
        }
    }
}
