using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class BoMonAttend
    {
        String _UserId;
        Int32 _CompID;
        Int32 _SevikaID;
        String _Date;
        Int32 _AnganID;
        Int32 _PrjTypeId;
        Int32 _TotalDays;
        String _ParamStr;
        Int32 _Mode;
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

        public Int32 SevikaID
        {
            get { return _SevikaID; }
            set { _SevikaID = value; }
        }

        public String Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public Int32 AnganID
        {
            get { return _AnganID; }
            set { _AnganID = value; }
        }

        public Int32 PrjTypeId
        {
            get { return _PrjTypeId; }
            set { _PrjTypeId = value; }
        }

        public Int32 TotalDays
        {
            get { return _TotalDays; }
            set { _TotalDays = value; }
        }

        public String ParamStr
        {
            get { return _ParamStr; }
            set { _ParamStr = value; }
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

        DataMonAttend dtattend;

        public BoMonAttend()
        {
            dtattend = new DataMonAttend(this);
        }

        public void BoMonAttend_1()
        {

            dtattend.InsertAttend(this);
        }

        public void AuthorisedSalaryCal()
        {
            dtattend.AuthorisedSalaryCal(this);
        }

        public void DeleteAttendance()
        {

            dtattend.DeleteAttendance(this);
        }
    }
}
