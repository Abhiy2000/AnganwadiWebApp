using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoBillGen
    {
        String _UserId;
        Int32 _CompID;
        DateTime _SalDate;
        DateTime _BillDate;
        String _DesgType;
        String _SalSlab;
        String _SevikaType;
        Int32 _ParentId;
        Int32 _ErrorCode;
        String _ErrorMsg;

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 CompID
        {
            get { return _CompID; }
            set { _CompID = value; }
        }

        public DateTime SalDate
        {
            get { return _SalDate; }
            set { _SalDate = value; }
        }

        public DateTime BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }

        public String DesgType
        {
            get { return _DesgType; }
            set { _DesgType = value; }
        }

        public String SalSlab
        {
            get { return _SalSlab; }
            set { _SalSlab = value; }
        }
        
        public String SevikaType
        {
            get { return _SevikaType; }
            set { _SevikaType = value; }
        }

        public Int32 ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
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

        DataBillGen dtBillGen;

        public BoBillGen()
        {
            dtBillGen = new DataBillGen(this);
        }

        public void BoBillGen_1()
        {
            dtBillGen.GenerateBill(this);
        }
        public void BoBillGen_2()
        {
            dtBillGen.GenerateDiffBill(this);
        }
    }
}
