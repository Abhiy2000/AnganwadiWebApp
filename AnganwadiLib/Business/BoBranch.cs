using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoBranch
    {
        String _UserId;
        Int32 _bankid;
        Int32 _branchid;
        String _branchname;
        String _ifscnumber;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 Bankid
        {
            get { return _bankid; }
            set { _bankid = value; }
        }

        public Int32 Branchid
        {
            get { return _branchid; }
            set { _branchid = value; }
        }

        public String Branchname
        {
            get { return _branchname; }
            set { _branchname = value; }
        }

        public String Ifscnumber
        {
            get { return _ifscnumber; }
            set { _ifscnumber = value; }
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

        DataBranch dtBranch;

        public BoBranch()
        {
            dtBranch = new DataBranch(this);
        }

        public void BoBranch_1()
        {
            dtBranch.InsertBranch(this);
        }
    }
}
