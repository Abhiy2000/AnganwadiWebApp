using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoRetirementAge
    {
        Int32 _AgeId;
        Int32 _Age;
        Int32 _Desg;
        DateTime? _AffectDate;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public Int32 AgeId
        {
            get { return _AgeId; }
            set { _AgeId = value; }
        }

        public Int32 Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public Int32 Desg
        {
            get { return _Desg; }
            set { _Desg = value; }
        }

        public DateTime? AffectDate
        {
            get { return _AffectDate; }
            set { _AffectDate = value; }
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

        DataRetirementAge dtAge;

        public BoRetirementAge()
        {
            dtAge = new DataRetirementAge(this);
        }

        public void BoRetirementAge_1()
        {
            dtAge.InsertAge(this);
        }
    }
}
