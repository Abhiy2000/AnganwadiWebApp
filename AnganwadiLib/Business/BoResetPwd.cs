using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoResetPwd
    {
        String _UserId;
        String _NewPassword;
        String _insby;
        String _remark;
        int _ErrCode;
        String _ErrMsg;

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public String NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }

        public String Insby
        {
            get { return _insby; }
            set { _insby = value; }
        }

        public String Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public int ErrCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        DataResetPwd dtPwd;

        public BoResetPwd()
        {
            dtPwd = new DataResetPwd(this);
        }

        public void BoResetPwd_1()
        {
            dtPwd.Update(this);
        }
    }
}
