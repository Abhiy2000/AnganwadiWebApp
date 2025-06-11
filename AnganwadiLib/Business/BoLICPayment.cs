using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
     public class BoLICPayment
    {
        Int32 _SevikaID;
        String _UserID;
        String _Str;
        Int32 _ErrCode;
        String _ErrMsg;


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
            get { return _Str; }
            set { _Str = value; }
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

        AnganwadiLib.Data.DataLICPayment DtSevikaLICPayment;
        public BoLICPayment()
        {
            DtSevikaLICPayment = new AnganwadiLib.Data.DataLICPayment();
        }


        public void InsertLICPayment()
        {
            DtSevikaLICPayment.InsertLICPayment(this);
        }
    }
}
