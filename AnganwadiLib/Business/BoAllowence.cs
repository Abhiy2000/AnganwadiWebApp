using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoAllowence
    {
        Int32 _CompId;
        Int32 _PrjId;
        Int32 _DesgId;
        Int32 _EduId;
        String _AllowName;
        Int32 _Amt;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public Int32 CompId
        {
            get { return _CompId; }
            set { _CompId = value; }
        }

        public Int32 PrjId
        {
            get { return _PrjId; }
            set { _PrjId = value; }
        }

        public Int32 DesgId
        {
            get { return _DesgId; }
            set { _DesgId = value; }
        }

        public Int32 EduId
        {
            get { return _EduId; }
            set { _EduId = value; }
        }

        public String AllowName
        {
            get { return _AllowName; }
            set { _AllowName = value; }
        }

        public Int32 Amt
        {
            get { return _Amt; }
            set { _Amt = value; }
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

        DataAllowence dtAllow;

        public BoAllowence()
        {
            dtAllow = new DataAllowence(this);
        }

        public void BoAllowence_1()
        {
            dtAllow.InsertAllow(this);
        }
    }
}
