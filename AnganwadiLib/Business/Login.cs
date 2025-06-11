using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class Login
    {
        DataLogin ObjDataLogin;
        String _CompId;
        String _UserId;
        String _Password;
        String _UserName;
        String _LastLogin;
        String _LastLogout;
        String _Lastchangepwd;
        Int64 _MobileNo;
        String _Email;
        String _Validfrom;
        String _ValidUpto;
        String _Blocked;
        String _Type;
        String _Desgid;

        String _Brid;
        String _Compname;
        String _Brcompid;
        String _Branchname;
        String _TypeName;
        String _DesgName;
        String _brcategory;
        String _Role;
        String _ErrCode;
        String _ErrMsg;

        public String Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        public String brcategory
        {
            get { return _brcategory; }
            set { _brcategory = value; }
        }

        public String TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        public String DesgName
        {
            get { return _DesgName; }
            set { _DesgName = value; }
        }

        public String Brid
        {
            get { return _Brid; }
            set { _Brid = value; }
        }

        public String Compname
        {
            get { return _Compname; }
            set { _Compname = value; }
        }

        public String Brcompid
        {
            get { return _Brcompid; }
            set { _Brcompid = value; }
        }

        public String Branchname
        {
            get { return _Branchname; }
            set { _Branchname = value; }
        }

        public String Desgid
        {
            get { return _Desgid; }
            set { _Desgid = value; }
        }

        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public String CompId
        {
            get { return _CompId; }
            set { _CompId = value; }
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

        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public String LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }

        public String LastLogout
        {
            get { return _LastLogout; }
            set { _LastLogout = value; }
        }

        public String Lastchangepwd
        {
            get { return _Lastchangepwd; }
            set { _Lastchangepwd = value; }
        }

        public Int64 MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public String Validfrom
        {
            get { return _Validfrom; }
            set { _Validfrom = value; }
        }

        public String ValidUpto
        {
            get { return _ValidUpto; }
            set { _ValidUpto = value; }
        }

        public String Blocked
        {
            get { return _Blocked; }
            set { _Blocked = value; }
        }

        public String ErrCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        public Login()
        {
            ObjDataLogin = new DataLogin(this);
        }

        public void UserLogin()
        {
            ObjDataLogin.Login();
        }

        public void UserLoginRandomPassword()
        {
            ObjDataLogin.LoginRandomPassword();
        }
    }
}
