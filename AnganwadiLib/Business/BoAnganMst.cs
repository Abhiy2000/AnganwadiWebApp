using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoAnganMst
    {
        Int32 _BrId;
        Int32 _AngnID;
        Int32 _PrjTypeId;
        String _AngnName;
        String _AngnCode;
        String _Address;
        String _Email;
        Int64 _MobileNo;
        String _PhoneNo;
        String _Active;
        Int32 _PinCode;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public Int32 BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

        public Int32 AngnID
        {
            get { return _AngnID; }
            set { _AngnID = value; }
        }

        public Int32 PrjTypeId
        {
            get { return _PrjTypeId; }
            set { _PrjTypeId = value; }
        }

        public String AngnName
        {
            get { return _AngnName; }
            set { _AngnName = value; }
        }

        public String AngnCode
        {
            get { return _AngnCode; }
            set { _AngnCode = value; }
        }

        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public Int64 MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        public String PhoneNo
        {
            get { return _PhoneNo; }
            set { _PhoneNo = value; }
        }

        public String Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public Int32 PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
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

        DataAngnMst dtangn;

        public BoAnganMst()
        {
            dtangn = new DataAngnMst(this);
        }

        public void BoAnganMst_1()
        {

            dtangn.InsertAngn(this);
        }
    }
}
