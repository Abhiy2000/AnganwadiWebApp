using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class ConractorBrConfig
    {
        AnganwadiLib.Data.DataContractorBrConfig DtContractorBrConfig;
        Int32 _BrId;
        String _ContractName;
        String _Address;
        String _Email;
        Int32 ContractId;        
        Int64 _ContactPerson;
        String _UserId;
        Int32 _Mode;
        Int32 _ErrCode;
        String _ErrMsg;

        public String UserId1
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        
        public Int64 ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

         public Int32 BrId
        {
            get { return _BrId; }
            set { _BrId = value; }
        }

         public String ContractName
        {
            get { return _ContractName; }
            set { _ContractName = value; }
        }

         public String Address
         {
             get { return _Address; }
             set { _Address = value; }
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

        public ConractorBrConfig()
        {
          DtContractorBrConfig = new AnganwadiLib.Data.DataContractorBrConfig(this);
        }

        public void Insert()
        {
          DtContractorBrConfig.Insert(this);
        }
    }
}
