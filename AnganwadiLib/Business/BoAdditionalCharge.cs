using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class BoAdditionalCharge
    {
        AnganwadiLib.Data.DataAdditionalCharge objAdditionalCharge;
        public BoAdditionalCharge()
        {
            objAdditionalCharge = new AnganwadiLib.Data.DataAdditionalCharge();
        }
        
        public string UserName { get; set; }
        public int COMPID { get; set; }
        public String BITCODE { get; set; }
        public int ANGANID { get; set; }
        public int SEVIKAID { get; set; }
        public int NEWANGANID { get; set; }
        public DateTime FROMDATE { get; set; }
        public DateTime TODATE { get; set; }
        public String REASON { get; set; }
        public int ErrorCode { get; set; }
        public String ErrorMessage { get; set; }
        public void Insert()
        {
            objAdditionalCharge.Insert(this);
        }
    }
}
