using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataUser
    {
        User1 ObjUser;

        public DataUser()
        { }

        public DataUser(User1 BoUser)
        {
            ObjUser = BoUser;
        }

        public void Insert(User1 BoUser)
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
                com.CommandText = "aoup_user_ins";

                com.Parameters.Add(new OracleParameter("in_brid", OracleDbType.Int64));
                com.Parameters["in_brid"].Value = ObjUser.Brid;

                if (ObjUser.UserId == "" || ObjUser.UserId == null)
                {
                    com.Parameters.Add(new OracleParameter("in_userid", OracleDbType.Varchar2));
                    com.Parameters["in_userid"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters.Add(new OracleParameter("in_userid", OracleDbType.Varchar2));
                    com.Parameters["in_userid"].Value = ObjUser.UserId;
                }

                com.Parameters.Add(new OracleParameter("in_username", OracleDbType.Varchar2));
                com.Parameters["in_username"].Value = ObjUser.UserName;

                com.Parameters.Add(new OracleParameter("in_password", OracleDbType.Varchar2));
                com.Parameters["in_password"].Value = ObjUser.Password;

                com.Parameters.Add(new OracleParameter("in_activeFrm", OracleDbType.TimeStamp));
                com.Parameters["in_activeFrm"].Value = ObjUser.ValidFrom;

                com.Parameters.Add(new OracleParameter("in_activeTill", OracleDbType.TimeStamp));
                com.Parameters["in_activeTill"].Value = ObjUser.ValidUpto;

                com.Parameters.Add(new OracleParameter("in_desgid", OracleDbType.Int64));
                com.Parameters["in_desgid"].Value = ObjUser.UserDesignation;

                com.Parameters.Add(new OracleParameter("in_deptid", OracleDbType.Int64));
                com.Parameters["in_deptid"].Value = ObjUser.UserDepartment;

                com.Parameters.Add(new OracleParameter("in_mode", OracleDbType.Int64));
                com.Parameters["in_mode"].Value = ObjUser.Mode;

                com.Parameters.Add(new OracleParameter("Out_errorCode", OracleDbType.Int64, 100));
                com.Parameters["Out_errorCode"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("Out_ErrorMsg", OracleDbType.Varchar2, 100));
                com.Parameters["Out_ErrorMsg"].Direction = ParameterDirection.Output;

               
                com.ExecuteNonQuery();

                String Errcode = com.Parameters["Out_errorcode"].Value.ToString().ToString();
                ObjUser.ErrCode = Convert.ToInt32(Errcode);

                String Errmsg = com.Parameters["Out_ErrorMsg"].Value.ToString();
                ObjUser.ErrMsg = Errmsg;
            }
            catch (Exception ex)
            {
                ObjUser.ErrCode = 0;
                ObjUser.ErrMsg = ex.Message;
            }
            finally
            {
                com.Dispose();
                conn.Dispose();
            }
        }
    }
}
