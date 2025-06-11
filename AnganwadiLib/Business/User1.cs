using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class User1
    {
        DataUser DtUser;

        String _brid;
        String _UserId;
        String _UserName;
        Int64 _UserDesignation;
        Int64 _UserDepartment;
        Int32 _Mode;
        Int32 _ErrCode;
        String _ErrMsg;
        DateTime _ValidFrom;
        DateTime _ValidUpto;
        String _Password;

        public DateTime ValidUpto
        {
            get { return _ValidUpto; }
            set { _ValidUpto = value; }
        }

        public DateTime ValidFrom
        {
            get { return _ValidFrom; }
            set { _ValidFrom = value; }
        }
        
        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public Int64 UserDepartment
        {
            get { return _UserDepartment; }
            set { _UserDepartment = value; }
        }

        public String Brid
        {
            get { return _brid; }
            set { _brid = value; }
        }

        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public Int64 UserDesignation
        {
            get { return _UserDesignation; }
            set { _UserDesignation = value; }
        }

        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public Int32 ErrCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        public User1()
        {
            DtUser = new DataUser(this);
        }

        public void Insert()
        {

            DtUser.Insert(this);
        }
    }
}
