using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoRelation
    {
        Int32 _relationId;
        String _relationName;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public Int32 RelationId
        {
            get { return _relationId; }
            set { _relationId = value; }
        }

        public String RelationName
        {
            get { return _relationName; }
            set { _relationName = value; }
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

        DataRelation dtrel;

        public BoRelation()
        {
            dtrel = new DataRelation(this);
        }

        public void BoRelation_1()
        {
            dtrel.Insertrelation(this);
        }
    }
}
