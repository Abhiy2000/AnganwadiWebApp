using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoTransferSevika
    {
        Int32 _OldBitId;
        Int32 _NewBitId;
        Int32 _OldAnganwadiId;
        Int32 _NewAnganwadiId;
        String _AadharNo;
        Int32 _SevikaId;
        String _Status;
        String _TransferDate;
        String _Remark;
        String _UserId;
        Int32 _ErrorCode;
        String _ErrMsg;

        public Int32 OldBitId
        {
            get { return _OldBitId; }
            set { _OldBitId = value; }
        }

        public Int32 NewBitId
        {
            get { return _NewBitId; }
            set { _NewBitId = value; }
        }

        public Int32 OldAnganwadiId
        {
            get { return _OldAnganwadiId; }
            set { _OldAnganwadiId = value; }
        }

        public Int32 NewAnganwadiId
        {
            get { return _NewAnganwadiId; }
            set { _NewAnganwadiId = value; }
        }

        public String AadharNo
        {
            get { return _AadharNo; }
            set { _AadharNo = value; }
        }

        public Int32 SevikaId
        {
            get { return _SevikaId; }
            set { _SevikaId = value; }
        }

        public String Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public String TransferDate
        {
            get { return _TransferDate; }
            set { _TransferDate = value; }
        }

        public String Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public Int32 ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        public String ErrMsg
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        DataTransferSevika objTrfSev;

        public BoTransferSevika()
        {
            objTrfSev = new DataTransferSevika(this);
        }

        public void BoTransferSevika_1()
        {
            objTrfSev.Insert(this);
        }

        public void Authorized()
        {
            objTrfSev.Authorized(this);
        }
    }
}
