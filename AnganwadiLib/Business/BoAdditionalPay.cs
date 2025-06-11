using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class BoAdditionalPay
    {
        AnganwadiLib.Data.DataAdditionalPay objDataAdditionalPay;
        public BoAdditionalPay()
        {
            objDataAdditionalPay = new AnganwadiLib.Data.DataAdditionalPay();
        }

        public int Mode { get; set; }
        public string UserName { get; set; }
        public int COMPID { get; set; }
        public int SEVIKAID { get; set; }
        public String TYPE { get; set; }
        public DateTime FROMDATE { get; set; }
        public DateTime TODATE { get; set; }
        public String STATUS { get; set; }
        public int ErrorCode { get; set; }
        public String ErrorMessage { get; set; }
        public int AddPayUniqID { get; set; }

        public void InsertAdditionPay()
        {
            objDataAdditionalPay.Insert(this);
        }
    }
}
