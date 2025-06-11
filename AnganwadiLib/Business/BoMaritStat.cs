using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoMaritStat
    {
        Int32 _MaritStatId;
        String _MaritStatName;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public Int32 MaritStatId
        {
            get { return _MaritStatId; }
            set { _MaritStatId = value; }
        }

        public String MaritStatName
        {
            get { return _MaritStatName; }
            set { _MaritStatName = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
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

        DataMaritStat dtrel;

        public BoMaritStat()
        {
            dtrel = new DataMaritStat(this);
        }

        public void BoMaritStat_1()
        {
            dtrel.InsertMaritStat(this);
        }
    }
}
