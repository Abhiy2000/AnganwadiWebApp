using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class Cls_Business_ChangePassword
    {
        String _UserId;
        String _OldPassword;
        String _NewPassword;
        int _ErrCode;
        String _ErrMsg;

        public String OldPassword
        {
            get
            { return _OldPassword; }
            set
            { _OldPassword = value; }
        }
        public String NewPassword
        {
            get
            { return _NewPassword; }
            set
            { _NewPassword = value; }
        }

        public String UserId
        {
            get
            { return _UserId; }
            set
            { _UserId = value; }
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

        AnganwadiLib.Data.Cls_Data_ChangePassword DtChangePass;

        public Cls_Business_ChangePassword()
        {
            DtChangePass = new AnganwadiLib.Data.Cls_Data_ChangePassword();
        }

        public void UpDate()
        {
            DtChangePass.Update(this);
        }
    }
}
