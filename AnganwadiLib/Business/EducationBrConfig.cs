using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class EducationBrConfig
    {
        //AnganwadiLib.Data.DataDepartmentBrConfig  DtDepartmentBrConfig; 
        AnganwadiLib.Data.DataEducationBrConfig DtEducationBrConfig;
        //Int32 _BrId;
        Int32 _EducationId;
        String _Education;
        String _UserId;
        Int32 _Mode;

        Int32 _ErrCode;
        String _ErrMsg;

        //public Int32 BrId
        //{
        //    get { return _BrId; }
        //    set { _BrId = value; }
        //}

        public Int32 EducationId
        {
            get { return _EducationId; }
            set { _EducationId = value; }
        }

        public String Education
        {
            get { return _Education; }
            set { _Education = value; }
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

        public Int32 Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public EducationBrConfig()
        {
            DtEducationBrConfig = new AnganwadiLib.Data.DataEducationBrConfig(this);
        }

        public void Insert()
        {
            DtEducationBrConfig.Insert(this);
        }
    }
}
