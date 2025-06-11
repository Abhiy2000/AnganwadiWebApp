using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Data;

namespace AnganwadiLib.Business
{
    public class UserAccess
    {
        public System.Data.DataTable TblAccess = new System.Data.DataTable();

        int _BrId;
        String _UserId;
        String _InsBy;
        int _ErrCode;
        String _ErrMsg;
        String _UserCode;
        String _AccessString;
        public int BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

        public String UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public String UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        public String AccessString
        {
            get { return _AccessString; }
            set { _AccessString = value; }
        }
        public Int32 ErrorCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public String ErrorMessage
        {
            get { return _ErrMsg; }
            set { _ErrMsg = value; }
        }

        DataUserAccess DaUserAccess;

        public UserAccess()
        {
            TblAccess.Columns.Add("MenuId", typeof(Int32));


            DaUserAccess = new DataUserAccess(this);
        }
        public void UpDate()
        {
            DaUserAccess.UpDate(this);
        }

    }
}
