using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class Cls_Business_LICStatus
    {
        Int32 _CompID;
        Int32 _SevikaID;
        String _UserID;
        String _Apprvstr;
        String _Rejctstr;
        Int32 _ErrCode;
        String _ErrMsg;
        Int64 _Mobile;
        String _otp;
        Int32 _Mode;
        String _LICFLAG;

        public Int32 CompID
        {
            get { return _CompID; }
            set { _CompID = value; }
        }

        public Int32 SevikaID
        {
            get { return _SevikaID; }
            set { _SevikaID = value; }
        }

        public String UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }


        public String Apprvstr
        {
            get { return _Apprvstr; }
            set { _Apprvstr = value; }
        }
        public String Rejctstr
        {
            get { return _Rejctstr; }
            set { _Rejctstr = value; }
        }
        //added
        public Int64 Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public Int32 mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public String otp
        {
            get { return _otp; }
            set { _otp = value; }
        }
        public Int32 ErrorCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrorMessage
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }
        //added
        public String LICFLAG
        {
            get { return _LICFLAG; }
            set { _LICFLAG = value; }
        }
        AnganwadiLib.Data.Cls_Data_LICStatus DtSavikaLICStatus;
        public Cls_Business_LICStatus()
        {
            DtSavikaLICStatus = new AnganwadiLib.Data.Cls_Data_LICStatus();
        }

        public void UpDateLICStatus()
        {
            DtSavikaLICStatus.UpdateLICStatus(this);
        }

        public void UpDateLICHoStatus()
        {
            DtSavikaLICStatus.UpdateLICHoStatus(this);
        }

        public void UpDateLICApproval()
        {
            DtSavikaLICStatus.UpdateLICApproval(this);
        }

        public void UpDateDPOApproval()
        {
            DtSavikaLICStatus.UpdateLICDPOStatus(this);
        }
        public void ApproveOTP()
        {
            DtSavikaLICStatus.ApproveOTP(this);
        }
    }
}
