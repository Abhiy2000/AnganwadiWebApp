using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoLastNo
    {
        String _Type;
        DateTime? _Date;
        String _UserId;
        Int32 _LastNo;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public DateTime? Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 LastNo
        {
            get { return _LastNo; }
            set { _LastNo = value; }
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

        DataLastNo objLastl;

        public BoLastNo()
        {
            objLastl =new DataLastNo(this);
        }

        public void BoLastNo_1()
        {
            objLastl.SetLastNo(this);
        }
    }
}
