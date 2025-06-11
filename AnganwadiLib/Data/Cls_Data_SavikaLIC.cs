using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class Cls_Data_SavikaLIC
    {
        Cls_Business_SavikaLIC ObjSavikaLIC;

        public Cls_Data_SavikaLIC()
        { }

        public Cls_Data_SavikaLIC(Cls_Business_SavikaLIC BoSavikaLIC)
        {
            ObjSavikaLIC = BoSavikaLIC;
        }

        public void UpdateLIC(Cls_Business_SavikaLIC BoSavikaLIC)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_sevikaLIC_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            //Cmd.Parameters.Add("in_CompID", OracleDbType.Int64);
            //Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Str", OracleDbType.Varchar2);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Str"].Direction = ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_UserId"].Value = BoSavikaLIC.UserID;
            //Cmd.Parameters["in_CompID"].Value = BoSavikaLIC.CompID;
            //Cmd.Parameters["in_SevikaID"].Value = BoSavikaLIC.SevikaID;
            Cmd.Parameters["in_Str"].Value = BoSavikaLIC.Str;
            Cmd.Parameters["out_Errorcode"].Value = BoSavikaLIC.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoSavikaLIC.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoSavikaLIC.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoSavikaLIC.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }
    }
}
