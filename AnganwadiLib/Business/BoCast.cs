using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoCast
    {
        //Int32 _CompId;
        String _UserId;
        Int32 _CastId;
        String _CastName;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        //public Int32 CompId
        //{
        //    get { return _CompId; }
        //    set { _CompId = value; }
        //}

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 CastId
        {
            get { return _CastId; }
            set { _CastId = value; }
        }

        public String CastName
        {
            get { return _CastName; }
            set { _CastName = value; }
        }

        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
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

        DataCast dtCast;

        public BoCast()
        {
            dtCast = new DataCast(this);
        }

        public void BoCast_1()
        {
            dtCast.InsertCast(this);
        }
    }
}
