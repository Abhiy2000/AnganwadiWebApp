using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class Menu
    {
        AnganwadiLib.Data.DataMenu DtMenu;

        public System.Data.DataTable TblList = new System.Data.DataTable();
        public System.Data.DataTable TblBranchList = new System.Data.DataTable();
        int _MenuId;
        String _PageTitle;
        int? _ParentId;
        String _PageType;
        String _PagePath = null;
        int _Status;
        String _InsBy;
        DateTime _InsDate;
        String _UpdBy;
        DateTime _UpdDate;
        int _ErrCode;
        String _ErrMsg;
        String _AccessString;
        String _UserLevelString;
        int _Mode;

        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }

        public int Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public String AccessString
        {
            get { return _AccessString; }
            set { _AccessString = value; }
        }

        public String UserLevelString
        {
            get { return _UserLevelString; }
            set { _UserLevelString = value; }
        }

        public String PageTitle
        {
            get { return _PageTitle; }
            set { _PageTitle = value; }
        }

        public Nullable<int> ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        public String PageType
        {
            get { return _PageType; }
            set { _PageType = value; }
        }

        public String PagePath
        {
            get { return _PagePath; }
            set { _PagePath = value; }
        }

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public String InsBy
        {
            get
            { return _InsBy; }
            set
            { _InsBy = value; }
        }

        public DateTime InsDate
        {
            get
            { return _InsDate; }
            set
            { _InsDate = value; }
        }

        public String UpDateBy
        {
            get
            { return _UpdBy; }
            set
            { _UpdBy = value; }
        }

        public DateTime UpDateDate
        {
            get
            { return _UpdDate; }
            set
            { _UpdDate = value; }
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

        public Menu()
        {
            DtMenu = new AnganwadiLib.Data.DataMenu(this);
        }

        public void Insert()
        {

            DtMenu.Insert(this);
        }

        public void UpDate()
        {

            // DtMenu.UpDate(this);
        }

        public void Find(int Id)
        {
            DtMenu.Find(Id);
        }

        public void GetBranchList()
        {
            DtMenu.GetBranchList();
        }
    }
}
