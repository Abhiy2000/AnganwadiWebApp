using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class CompanyMst
    {
        AnganwadiLib.Data.DataCompanyMst DtCompanyMst;

        Int32 _CorpId;
        Int32 _Category;
        Int32 _Parent;
        String _CompanyName;
        String _BranchName;
        String _Code;
        String _Address;
        Int64 _PhoneNumber;
        String _EmailAddress;
        String _Status;
        Int32 _BankBranchId;
        String _BankAccNo;
        Int32 _ProjectTypeId;
        String _OfficerNM;
        Int32 _PinCode;
        Int32 _PrjCode;
        Int64 _DistCode;
        String _CPSMSCode;
        Int32 _Mode;
        String _UserId;
        Int32 _ErrCode;
        String _ErrMsg;

        public Int32 CorpId
        {
            get { return _CorpId; }
            set { _CorpId = value; }
        }

        public Int32 Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        public Int32 Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        
        public String BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }

        public String Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public Int64 PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        public String Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Int32 BankBranchId
        {
            get { return _BankBranchId; }
            set { _BankBranchId = value; }
        }

        public String BankAccNo
        {
            get { return _BankAccNo; }
            set { _BankAccNo = value; }
        }

        public Int32 ProjectTypeId
        {
            get { return _ProjectTypeId; }
            set { _ProjectTypeId = value; }
        }

        public String OfficerNM
        {
            get { return _OfficerNM; }
            set { _OfficerNM = value; }
        }

        public Int32 PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }

        public Int32 PrjCode
        {
            get { return _PrjCode; }
            set { _PrjCode = value; }
        }

        public Int64 DistCode
        {
            get { return _DistCode; }
            set { _DistCode = value; }
        }

        public String CPSMSCode
        {
            get { return _CPSMSCode; }
            set { _CPSMSCode = value; }
        }

        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 ErrCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        public CompanyMst()
        {
            DtCompanyMst = new AnganwadiLib.Data.DataCompanyMst(this);
        }

        public void Insert()
        {
            DtCompanyMst.Insert(this);
        }
    }
}
