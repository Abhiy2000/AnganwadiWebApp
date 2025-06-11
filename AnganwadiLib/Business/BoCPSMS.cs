using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoCPSMS
    {
        String _UserId;
        String _FileName;
        String _Str;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public String FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public String Str
        {
            get { return _Str; }
            set { _Str = value; }
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

        DataCPSMS dtCpsms;

        public BoCPSMS()
        {
            dtCpsms = new DataCPSMS(this);
        }

        public void BoCPSMS_1()
        {
            dtCpsms.UpdateCPSMS(this);
        }
    }
}
