using AnganwadiLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Business
{
    public class BoONLApp
    {
        public string UserId { get; set; }
        public Int64 DivId { get; set; }
        public Int64 DistId { get; set; }
        public Int64 levelId { get; set; }
        public Int64 corpId { get; set; }
        public Int64 ProjId { get; set; }
        public Int64 Anganwadiid { get; set; }
        public Int64 poritid { get; set; }
        public Int64 applid { get; set; }
        public Int64 maritalid { get; set; }
        public Int64 eduQualid { get; set; }
        public Int64 age { get; set; }
        public Int64 disabilityage { get; set; }
        public DateTime? dob { get; set; }
        public String handicapeid { get; set; }
        public String AppName { get; set; }
        public String disability { get; set; }
        public String Appaddress { get; set; }
        public String aadharno { get; set; }
        public String panno { get; set; }
        public String religion { get; set; }
        public String cast { get; set; }
        public String subcast { get; set; }
        public Int64 ErrorCode { get; set; }
        public String ErrorMsg { get; set; }
        public String applicationid { get; set; }
        public String docverify { get; set; }
        public String authstatus { get; set; }
        public String authremark { get; set; }
        public String strMarks { get; set; }
        public Int64 Mode { get; set; }


        DataONLApp dtONLApp;

        public BoONLApp()
        {
            dtONLApp = new DataONLApp(this);
        }

        public void Insert()
        {
            dtONLApp.Insert(this);
        }
    }
}
