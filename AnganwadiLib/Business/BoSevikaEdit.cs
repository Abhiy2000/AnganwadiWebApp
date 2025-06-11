using AnganwadiLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AnganwadiLib.Business
{
    public class BoSevikaEdit
    {
        DataSevikaEdit objD;
        public BoSevikaEdit()
        {
            objD = new DataSevikaEdit(this);
        }
        public string userid { get; set; }
        public int comp_id { get; set; }
        public Int64 sevikaid { get; set; }
        public DateTime? sevikaDob_old { get; set; }
        public DateTime ? sevikaDoJ_old { get; set; }
        public Int64 ? sevikaAadharNo_old { get; set; }

        public DateTime ? sevikaDob_new { get; set; }
        public DateTime ? sevikaDoJ_new { get; set; }
        public Int64    ? sevikaAadharNo_new { get; set; }

        public Int64 sevika_divid { get; set; }
        public Int64 sevika_distrid { get; set; }
        public Int64 sevika_cdpoid { get; set; }

        public int ErrorCode { get; set; }
        public int SevikaEdtId { get; set; }
        public String ErrorMessage { get; set; }

        public string StrCDPO { get; set; }
        public string FlagMode { get; set; }

        public Int64 In_Mode { get; set; }
        public void Insert()
        {
            objD.Insert(this);
        }
        public void InsertCdpo()
        {
            objD.InsertCdpo(this);
        }

    }
}
