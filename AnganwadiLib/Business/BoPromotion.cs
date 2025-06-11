using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class BoPromotion
    {
        AnganwadiLib.Data.DataPromotion objDataPromotion;
        public BoPromotion()
        {
            objDataPromotion = new AnganwadiLib.Data.DataPromotion();
        }

        public int Mode { get; set; }
        public string UserName { get; set; }
        public int COMPID { get; set; }
        public int SEVIKAID { get; set; }
        public DateTime PromoteDT { get; set; }
        public String OldQualification { get; set; }
        public String NewQualification { get; set; }
        public String OldDesFlag { get; set; }
        public String NewDesFlag { get; set; }
        public String OldDesID { get; set; }
        public String NewDesID { get; set; }
        public String OldPayscale { get; set; }
        public String NewPayscale { get; set; }
        public String OldExp { get; set; }
        public int NewExpID { get; set; }
        public int ErrorCode { get; set; }
        public String ErrorMessage { get; set; }

        public void InsertPromotion()
        {
            objDataPromotion.Insert(this);
        }

        public void InsertDemotion()
        {
            objDataPromotion.Demotion(this);
        }
    }
}
