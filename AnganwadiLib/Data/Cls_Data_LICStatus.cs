using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class Cls_Data_LICStatus
    {
        Cls_Business_LICStatus ObjLICStatus;

        public Cls_Data_LICStatus()
        { }

        public Cls_Data_LICStatus(Cls_Business_LICStatus BoLICStatus)
        {
            ObjLICStatus = BoLICStatus;
        }

        public void UpdateLICStatus(Cls_Business_LICStatus BoLICStatus)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_LICstatus_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            //Cmd.Parameters.Add("in_CompID", OracleDbType.Int64);
            //Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_apprvstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Rejctstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_apprvstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Rejctstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BoLICStatus.UserID;
            //Cmd.Parameters["in_CompID"].Value = BoSavikaLIC.CompID;
            //Cmd.Parameters["in_SevikaID"].Value = BoSavikaLIC.SevikaID;
            Cmd.Parameters["in_apprvstr"].Value = BoLICStatus.Apprvstr;
            Cmd.Parameters["in_Rejctstr"].Value = BoLICStatus.Rejctstr;
            Cmd.Parameters["out_Errorcode"].Value  = BoLICStatus.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICStatus.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICStatus.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICStatus.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }

        public void UpdateLICHoStatus(Cls_Business_LICStatus BoLICStatus)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_LIC_Ho_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            //Cmd.Parameters.Add("in_CompID", OracleDbType.Int64);
            //Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_apprvstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Rejctstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_apprvstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Rejctstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BoLICStatus.UserID;
            //Cmd.Parameters["in_CompID"].Value = BoSavikaLIC.CompID;
            //Cmd.Parameters["in_SevikaID"].Value = BoSavikaLIC.SevikaID;
            Cmd.Parameters["in_apprvstr"].Value = BoLICStatus.Apprvstr;
            Cmd.Parameters["in_Rejctstr"].Value = BoLICStatus.Rejctstr;
            Cmd.Parameters["out_Errorcode"].Value = BoLICStatus.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICStatus.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICStatus.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICStatus.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }

        public void UpdateLICApproval(Cls_Business_LICStatus BoLICStatus)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_LIC_Approval_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            //Cmd.Parameters.Add("in_CompID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_apprvstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Rejctstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_apprvstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Rejctstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BoLICStatus.UserID;
            //Cmd.Parameters["in_CompID"].Value = BoSavikaLIC.CompID;
            Cmd.Parameters["in_SevikaID"].Value = BoLICStatus.SevikaID;
            Cmd.Parameters["in_apprvstr"].Value = BoLICStatus.Apprvstr;
            Cmd.Parameters["in_Rejctstr"].Value = BoLICStatus.Rejctstr;
            Cmd.Parameters["out_Errorcode"].Value = BoLICStatus.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICStatus.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICStatus.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICStatus.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }

        public void UpdateLICDPOStatus(Cls_Business_LICStatus BoLICStatus)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_LIC_DPO_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            //Cmd.Parameters.Add("in_CompID", OracleType.Number);
            //Cmd.Parameters.Add("in_SevikaID", OracleType.Number);
            Cmd.Parameters.Add("in_apprvstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Rejctstr", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_licflag", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int32, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_apprvstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Rejctstr"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_licflag"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BoLICStatus.UserID;
            //Cmd.Parameters["in_CompID"].Value = BoSavikaLIC.CompID;
            //Cmd.Parameters["in_SevikaID"].Value = BoSavikaLIC.SevikaID;
            Cmd.Parameters["in_apprvstr"].Value = BoLICStatus.Apprvstr;
            Cmd.Parameters["in_Rejctstr"].Value = BoLICStatus.Rejctstr;
            Cmd.Parameters["in_licflag"].Value = BoLICStatus.LICFLAG;
            Cmd.Parameters["out_ErrorCode"].Value = BoLICStatus.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICStatus.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICStatus.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICStatus.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }

        //added
        public void ApproveOTP(Cls_Business_LICStatus BoLICStatus)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_Approve_otp", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_userid", OracleDbType.Varchar2);  
            Cmd.Parameters.Add("in_Mobile", OracleDbType.Int64);
            Cmd.Parameters.Add("in_otp", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_mode", OracleDbType.Int32);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_userid"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Mobile"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_otp"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_mode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_userid"].Value = BoLICStatus.UserID;
            Cmd.Parameters["in_Mobile"].Value = BoLICStatus.Mobile;
            if (BoLICStatus.otp == null)
            {
                Cmd.Parameters["in_otp"].Value = DBNull.Value;
            }
            else
            {
                Cmd.Parameters["in_otp"].Value = BoLICStatus.otp;
            } 
            Cmd.Parameters["in_mode"].Value = BoLICStatus.mode;
            Cmd.Parameters["out_Errorcode"].Value = BoLICStatus.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICStatus.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICStatus.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICStatus.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }
    }
}
