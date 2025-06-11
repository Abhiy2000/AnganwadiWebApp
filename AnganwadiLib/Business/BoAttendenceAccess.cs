using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoAttendenceAccess
    {
        String _UserId;
        Int32 _StateId;
        String _Str;
        String _Str1;
        String _Str2;
        String _ErrorMsg;
        Int32 _ErrorCode;

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }

        public String Str
        {
            get { return _Str; }
            set { _Str = value; }
        }

        public String Str1
        {
            get { return _Str1; }
            set { _Str1 = value; }
        }

        public String Str2
        {
            get { return _Str2; }
            set { _Str2 = value; }
        }

        public String ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }
        }

        public Int32 ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        DataAttendenceAccess objAttend;

        public BoAttendenceAccess()
        {
            objAttend = new DataAttendenceAccess(this);
        }

        public void BoAttendenceAccess_1()
        {
            objAttend.Insert(this);
        }
    }
}
