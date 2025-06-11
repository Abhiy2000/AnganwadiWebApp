using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoReligion
    {
        //Int32 _CompId;
        String _UserId;
        Int32 _ReligionId;
        String _ReligionName;
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

        public Int32 ReligionId
        {
            get { return _ReligionId; }
            set { _ReligionId = value; }
        }

        public String ReligionName
        {
            get { return _ReligionName; }
            set { _ReligionName = value; }
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

        DataReligion dtCast;

        public BoReligion()
        {
            dtCast = new DataReligion(this);
        }

        public void BoReligion_1()
        {
            dtCast.Insertreligion(this);
        }
    }
}
