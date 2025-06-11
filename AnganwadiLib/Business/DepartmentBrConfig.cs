using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
  
     
    public class DepartmentBrConfig
    {
        AnganwadiLib.Data.DataDepartmentBrConfig  DtDepartmentBrConfig;
        Int32 _BrId;
        Int32 _DepartmentId;
        String _Department;
        Int32 _UserId;
        Int32 _Mode;

        Int32 _ErrCode;
        String _ErrMsg;

        public Int32 BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

        public Int32 DepartmentId
        {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }
       



        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        public Int32 UserId
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
        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public DepartmentBrConfig()
        {
            DtDepartmentBrConfig = new AnganwadiLib.Data.DataDepartmentBrConfig(this);
        }

        public void Insert()
        {
            DtDepartmentBrConfig.Insert(this);
        }

    }
}
