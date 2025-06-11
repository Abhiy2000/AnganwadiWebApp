using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoSevikaAuth
    {
        Int32 _CompId;
        String _SevikaCode;
        String _UserId;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public Int32 CompId
        {
            get { return _CompId; }
            set { _CompId = value; }
        }

        public String SevikaCode
        {
            get { return _SevikaCode; }
            set { _SevikaCode = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        public String ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }
        }

        DataSevikaAuth dtSevAuth;

        public BoSevikaAuth()
        {
            dtSevAuth = new DataSevikaAuth(this);
        }

        public void BoSevikaAuth_1()
        {
            dtSevAuth.AuthSevika(this);
        }
    }
}
