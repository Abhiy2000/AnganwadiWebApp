using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class DesignationBrConfig
    {
        DataDesignationBrConfig DtDesignationBrConfig;
        //Int32 _BrId;
        Int32 _DesignationId;
        String _Designation;
        String _Code;
        String _flag;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrCode;
        String _ErrMsg;

        //public Int32 BrId
        //{
        //    get { return _BrId; }
        //    set { _BrId = value; }
        //}

        public Int32 DesignationId
        {
            get { return _DesignationId; }
            set { _DesignationId = value; }
        }

        public String Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public String Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public String Flag
        {
            get { return _flag; }
            set { _flag = value; }
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

        public DesignationBrConfig()
        {
            DtDesignationBrConfig = new AnganwadiLib.Data.DataDesignationBrConfig(this);
        }

        public void Insert()
        {
            DtDesignationBrConfig.Insert(this);
        }
    }
}
