using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class Cls_Business_SavikaLIC
    {
        Int32 _CompID;
        Int32 _SevikaID;
        String _UserID;
        String _str;
        Int32 _ErrCode;
        String _ErrMsg;

     
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

        public String UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public String Str
        {
            get { return _str; }
            set { _str = value; }
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

       AnganwadiLib.Data.Cls_Data_SavikaLIC DtSavikaLIC;
       public Cls_Business_SavikaLIC()
        {
            DtSavikaLIC = new AnganwadiLib.Data.Cls_Data_SavikaLIC();
        }

        public void UpDateLIC()
        {
            DtSavikaLIC.UpdateLIC(this);
        }
    }
}
