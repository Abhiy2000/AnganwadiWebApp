using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Data
{
    public class DataONLApp
    {
        BoONLApp objONLApp;

        public DataONLApp()
        { }

        public DataONLApp(BoONLApp ONLApp)
        {
            objONLApp = ONLApp;
        }

        public void Insert(BoONLApp ONLApp)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("pr_appli_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_compid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_levelid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_appliid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_divid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_distid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_projid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_appname", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_angnid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_address", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_portid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_dob", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_handicap", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_disbage", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_disability", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_age", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_maritalid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_eduqualid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_aadharno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_panno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_religion", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_cast", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_subcast", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_docverify", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_authstatus", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_authremark", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_strmarks", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 5000000).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_appliid", OracleDbType.Int64, 5).Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = ONLApp.UserId;
            Cmd.Parameters["in_compid"].Value = ONLApp.corpId;
            Cmd.Parameters["in_levelid"].Value = ONLApp.levelId;
            Cmd.Parameters["in_appliid"].Value = ONLApp.applid;
            Cmd.Parameters["in_divid"].Value = ONLApp.DivId;
            Cmd.Parameters["in_distid"].Value = ONLApp.DistId;
            Cmd.Parameters["in_projid"].Value = ONLApp.ProjId;
            Cmd.Parameters["in_appname"].Value = ONLApp.AppName;
            Cmd.Parameters["in_angnid"].Value = ONLApp.Anganwadiid;
            Cmd.Parameters["in_address"].Value = ONLApp.Appaddress;
            Cmd.Parameters["in_portid"].Value = ONLApp.poritid;
            Cmd.Parameters["in_dob"].Value = ONLApp.dob;
            Cmd.Parameters["in_handicap"].Value = ONLApp.handicapeid;
            Cmd.Parameters["in_disbage"].Value = ONLApp.disabilityage;
            Cmd.Parameters["in_disability"].Value = ONLApp.disability;
            Cmd.Parameters["in_age"].Value = ONLApp.age;
            Cmd.Parameters["in_maritalid"].Value = ONLApp.maritalid;
            Cmd.Parameters["in_eduqualid"].Value = ONLApp.eduQualid;
            Cmd.Parameters["in_aadharno"].Value = ONLApp.aadharno;
            Cmd.Parameters["in_panno"].Value = ONLApp.panno;
            Cmd.Parameters["in_religion"].Value = ONLApp.religion;
            Cmd.Parameters["in_cast"].Value = ONLApp.cast;
            Cmd.Parameters["in_subcast"].Value = ONLApp.subcast;
            Cmd.Parameters["in_docverify"].Value = ONLApp.docverify;
            Cmd.Parameters["in_authstatus"].Value = ONLApp.authstatus;
            Cmd.Parameters["in_authremark"].Value = ONLApp.authremark;
            Cmd.Parameters["in_strmarks"].Value = ONLApp.strMarks;
            Cmd.Parameters["in_mode"].Value = ONLApp.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            ONLApp.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_ErrorCode"].Value.ToString());
            ONLApp.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
            ONLApp.applicationid = Cmd.Parameters["out_appliid"].Value.ToString();
        }
    }
}
