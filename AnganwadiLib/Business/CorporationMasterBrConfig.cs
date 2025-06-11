using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class CorporationMasterBrConfig
    {
        AnganwadiLib.Data.DataCorporationMasterBrConfig DtCorporationMasterBrConfig;

        Int32 _BrId;
        //Int32 _CorpId;
        Int32 _ParentID;
        String _CorporationName;
        String _Class;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrCode;
        String _CorpBranch;
        String _ErrMsg;

        public Int32 BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

        //public Int32 CorpId
        //{
        //    get { return _CorpId; }
        //    set { _CorpId = value; }
        //}

        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public String CorporationName
        {
            get { return _CorporationName; }
            set { _CorporationName = value; }
        }

        public String Class
        {
            get { return _Class; }
            set { _Class = value; }
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

        public String CorpBranch
        {
            get { return _CorpBranch; }
            set { _CorpBranch = value; }
        }

        public CorporationMasterBrConfig()
        {
            DtCorporationMasterBrConfig = new AnganwadiLib.Data.DataCorporationMasterBrConfig(this);
        }

        public void Insert()
        {
            DtCorporationMasterBrConfig.Insert(this);
        }
    }
}
