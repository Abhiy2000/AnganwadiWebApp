using AnganwadiLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class BoPolicyUpdate
    {        
        public string UserId { get; set; }
        public Int32 DivId { get; set; }
        public Int32 DistId { get; set; }
        public Int32 CDPOId { get; set; }
        public Int32 BitCode { get; set; }
        public String ParamStr { get; set; }
        public Int32 ErrorCode { get; set; }
        public String ErrorMsg { get; set; }

        DataPolicyUpdate dtPolicyUpdate;

        public BoPolicyUpdate()
        {
            dtPolicyUpdate = new DataPolicyUpdate(this);
        }

        public void InsertPolicy()
        {
            dtPolicyUpdate.InsertPolicy(this);
        }
    }
}
