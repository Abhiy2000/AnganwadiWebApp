using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
   public class BankBrConfig
    {
        
       AnganwadiLib.Data.DataBankBrConfig DtBankBrConfig;
         
        Int32 _BankId;
        String _Bank;
        String _UserId;
        Int32 _Mode;

        Int32 _ErrCode;
        String _ErrMsg;


        public Int32 BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }




        public String Bank
        {
            get { return _Bank; }
            set { _Bank = value; }
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

        public BankBrConfig()
        {
            DtBankBrConfig = new AnganwadiLib.Data.DataBankBrConfig(this);
        }

        public void Insert()
        {
            DtBankBrConfig.Insert(this);
        }
    }
}
