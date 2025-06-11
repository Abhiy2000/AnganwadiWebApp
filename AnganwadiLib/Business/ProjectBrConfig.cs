using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class ProjectBrConfig
    {
        
       AnganwadiLib.Data.DataProjectBrConfig DtProjectBrConfig;
           Int32 _BrId;
        Int32 _ProjectId;
        String _Project;
        String  _UserId;
        Int32 _Mode;

        Int32 _ErrCode;
        String _ErrMsg;

        public Int32 BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

        public Int32 ProjectId
        {
            get { return _ProjectId; }
            set { _ProjectId = value; }
        }




        public String Project
        {
            get { return _Project; }
            set { _Project = value; }
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

        public ProjectBrConfig()
        {
            DtProjectBrConfig = new AnganwadiLib.Data.DataProjectBrConfig(this);
        }

        public void Insert()
        {
            DtProjectBrConfig.Insert(this);
        }
    }
}
