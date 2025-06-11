using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class Cls_Data_SavikaMast
    {
        Cls_Business_SavikaMast ObjSavikaMast;

        public Cls_Data_SavikaMast()
        { }

        public Cls_Data_SavikaMast(Cls_Business_SavikaMast BoSavikaMast)
        {
            ObjSavikaMast = BoSavikaMast;
        }

        public void Update(Cls_Business_SavikaMast BoSavikaMast)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            //OracleCommand Cmd = new OracleCommand("aoup_sevika_ins_local", Con.connection);
            OracleCommand Cmd = new OracleCommand("aoup_sevika_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_AnganId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Name", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_SevikaCode", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_EducId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_BirthDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_RetireDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_Address", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_MobileNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_PhoneNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_PanNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_AadharNo", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_JoinDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_OrderNo", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Orderdate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_DesigID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_PayScalID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_BranchId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_AccNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_Remark", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Active", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64);
            Cmd.Parameters.Add("in_CPSMSCode", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_ReligID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_CastID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_MiddleName", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Village", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_PinCode", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_maritstatid", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Reason", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Experience", OracleDbType.Int64);//----added on 24-03-18

            Cmd.Parameters.Add("in_Nom1Name", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom1RelatnId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom1Age", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom1Address", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom2Name", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom2RelationId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom2Age", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom2Address", OracleDbType.Varchar2);
           

            //----added on 18-04-19
            Cmd.Parameters.Add("in_Nom1BrID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom1Accno", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom1Ratio", OracleDbType.Int64);

            Cmd.Parameters.Add("in_Nom2BrID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom2Accno", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom2Ratio", OracleDbType.Int64);

            Cmd.Parameters.Add("in_Nom3Name", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom3RelationId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom3Age", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom3Address", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom3BrID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom3Accno", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom3Ratio", OracleDbType.Int64);
            // --------
            Cmd.Parameters.Add("in_ExitDate", OracleDbType.TimeStamp);  //added on 04-09-21
            Cmd.Parameters.Add("in_rejoinflag", OracleDbType.Varchar2); //added on 14-03-23

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_AnganId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Name"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_SevikaCode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_EducId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_BirthDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_RetireDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Address"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_MobileNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PhoneNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PanNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_AadharNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_JoinDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_OrderNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Orderdate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_DesigID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PayScalID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_BranchId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_AccNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Remark"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Active"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Mode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_CPSMSCode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_ReligID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_CastID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_MiddleName"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Village"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PinCode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_maritstatid"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Reason"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Experience"].Direction = ParameterDirection.Input; //----added on 24-03-18

            Cmd.Parameters["in_Nom1Name"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1RelatnId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1Age"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1Address"].Direction = ParameterDirection.Input;

            Cmd.Parameters["in_Nom2Name"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2RelationId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2Age"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2Address"].Direction = ParameterDirection.Input;

            //----added on 18-04-19
            Cmd.Parameters["in_Nom1BrID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1Accno"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1Ratio"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2BrID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2Accno"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2Ratio"].Direction = ParameterDirection.Input;

            Cmd.Parameters["in_Nom3Name"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom3RelationId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom3Age"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom3Address"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom3BrID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom3Accno"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom3Ratio"].Direction = ParameterDirection.Input;

            //---------------
            Cmd.Parameters["in_ExitDate"].Direction = ParameterDirection.Input;  //----added on 04-09-21

            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_CompID"].Value = BoSavikaMast.CompID;
            Cmd.Parameters["in_SevikaID"].Value = BoSavikaMast.SevikaID;
            Cmd.Parameters["in_UserId"].Value = BoSavikaMast.UserID;
            Cmd.Parameters["in_AnganId"].Value = BoSavikaMast.AnganId;
            Cmd.Parameters["in_Name"].Value = BoSavikaMast.Name;
            Cmd.Parameters["in_SevikaCode"].Value = BoSavikaMast.SevikaCode;

            if (BoSavikaMast.EducId != 0)
            {
                Cmd.Parameters["in_EducId"].Value = BoSavikaMast.EducId;
            }
            else
            {
                Cmd.Parameters["in_EducId"].Value = DBNull.Value;
            }
            if (BoSavikaMast.BirthDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_BirthDate"].Value = BoSavikaMast.BirthDate;
            }
            else
            {
                Cmd.Parameters["in_BirthDate"].Value = DBNull.Value;
            }
            if (BoSavikaMast.RetireDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_RetireDate"].Value = BoSavikaMast.RetireDate;
            }
            else
            {
                Cmd.Parameters["in_RetireDate"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_Address"].Value = BoSavikaMast.Address;
            Cmd.Parameters["in_MobileNo"].Value = BoSavikaMast.MobileNo;
            Cmd.Parameters["in_PhoneNo"].Value = BoSavikaMast.PhoneNo;
            Cmd.Parameters["in_PanNo"].Value = BoSavikaMast.PanNo;
            Cmd.Parameters["in_AadharNo"].Value = BoSavikaMast.AadharNo;
            if (BoSavikaMast.JoinDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_JoinDate"].Value = BoSavikaMast.JoinDate;
            }
            else
            {
                Cmd.Parameters["in_JoinDate"].Value = DBNull.Value;
            }

            Cmd.Parameters["in_OrderNo"].Value = BoSavikaMast.OrderNo;
            if (BoSavikaMast.Orderdate != DateTime.MinValue)
            {
                Cmd.Parameters["in_Orderdate"].Value = BoSavikaMast.Orderdate;
            }
            else
            {
                Cmd.Parameters["in_Orderdate"].Value = DBNull.Value;
            }

            if (BoSavikaMast.DesigID != 0)
            {
                Cmd.Parameters["in_DesigID"].Value = BoSavikaMast.DesigID;
            }
            else
            {
                Cmd.Parameters["in_DesigID"].Value = DBNull.Value;
            }

            if (BoSavikaMast.PayScalID != 0)
            {
                Cmd.Parameters["in_PayScalID"].Value = BoSavikaMast.PayScalID;
            }
            else
            {
                Cmd.Parameters["in_PayScalID"].Value = DBNull.Value;
            }

            if (BoSavikaMast.BranchId != 0)
            {
                Cmd.Parameters["in_BranchId"].Value = BoSavikaMast.BranchId;
            }
            else
            {
                Cmd.Parameters["in_BranchId"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_AccNo"].Value = BoSavikaMast.AccNo;
            Cmd.Parameters["in_Remark"].Value = BoSavikaMast.Remark;
            Cmd.Parameters["in_Active"].Value = BoSavikaMast.Active;
            Cmd.Parameters["in_Mode"].Value = BoSavikaMast.Mode;
            Cmd.Parameters["in_CPSMSCode"].Value = BoSavikaMast.CPSMSCode;

            if (BoSavikaMast.ReligID != 0)
            {
                Cmd.Parameters["in_ReligID"].Value = BoSavikaMast.ReligID;
            }
            else
            {
                Cmd.Parameters["in_ReligID"].Value = DBNull.Value;
            }

            if (BoSavikaMast.CastID != 0)
            {
                Cmd.Parameters["in_CastID"].Value = BoSavikaMast.CastID;
            }
            else
            {
                Cmd.Parameters["in_CastID"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_MiddleName"].Value = BoSavikaMast.MiddleName;
            Cmd.Parameters["in_Village"].Value = BoSavikaMast.Village;
            Cmd.Parameters["in_PinCode"].Value = BoSavikaMast.PinCode;

            if (BoSavikaMast.Maritstatid != 0)
            {
                Cmd.Parameters["in_maritstatid"].Value = BoSavikaMast.Maritstatid;
            }
            else
            {
                Cmd.Parameters["in_maritstatid"].Value = DBNull.Value;
            }

            if (BoSavikaMast.Reason != "")
            {
                Cmd.Parameters["in_Reason"].Value = BoSavikaMast.Reason;
            }
            else
            {
                Cmd.Parameters["in_Reason"].Value = DBNull.Value;
            }

            //-------------added on 24-03-18 -----------
            if (BoSavikaMast.Experience != 0)
            {
                Cmd.Parameters["in_Experience"].Value = BoSavikaMast.Experience;
            }
            else
            {
                Cmd.Parameters["in_Experience"].Value = DBNull.Value;
            }
            //-------------------------------------

           
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom1Name))
            {
                Cmd.Parameters["in_Nom1Name"].Value = BoSavikaMast.Nom1Name;
            }
            else
            {
                Cmd.Parameters["in_Nom1Name"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom1RelatnId != 0)
            {
                Cmd.Parameters["in_Nom1RelatnId"].Value = BoSavikaMast.Nom1RelatnId;
            }
            else
            {
                Cmd.Parameters["in_Nom1RelatnId"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom1Age != 0)
            {
                Cmd.Parameters["in_Nom1Age"].Value = BoSavikaMast.Nom1Age;
            }
            else
            {
                Cmd.Parameters["in_Nom1Age"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom1Address))
            {
                Cmd.Parameters["in_Nom1Address"].Value = BoSavikaMast.Nom1Address;
            }
            else
            {
                Cmd.Parameters["in_Nom1Address"].Value = DBNull.Value;
            }

            if (!string.IsNullOrEmpty(BoSavikaMast.Nom2Name))
            {
                Cmd.Parameters["in_Nom2Name"].Value = BoSavikaMast.Nom2Name;
            }
            else
            {
                Cmd.Parameters["in_Nom2Name"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom2RelationId != 0)
            {
                Cmd.Parameters["in_Nom2RelationId"].Value = BoSavikaMast.Nom2RelationId;
            }
            else
            {
                Cmd.Parameters["in_Nom2RelationId"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom2Age != 0)
            {
                Cmd.Parameters["in_Nom2Age"].Value = BoSavikaMast.Nom2Age;
            }
            else
            {
                Cmd.Parameters["in_Nom2Age"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom2Address))
            {
                Cmd.Parameters["in_Nom2Address"].Value = BoSavikaMast.Nom2Address;
            }
            else
            {
                Cmd.Parameters["in_Nom2Address"].Value = DBNull.Value;
            }

            //--- added on 18-04-19
            if (BoSavikaMast.Nom1BrID != 0)
            {
                Cmd.Parameters["in_Nom1BrID"].Value = BoSavikaMast.Nom1BrID;
            }
            else
            {
                Cmd.Parameters["in_Nom1BrID"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom1Accno))
            {
                Cmd.Parameters["in_Nom1Accno"].Value = BoSavikaMast.Nom1Accno;
            }
            else
            {
                Cmd.Parameters["in_Nom1Accno"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom1Ratio != 0)
            {
                Cmd.Parameters["in_Nom1Ratio"].Value = BoSavikaMast.Nom1Ratio;
            }
            else
            {
                Cmd.Parameters["in_Nom1Ratio"].Value = DBNull.Value;
            }

            if (BoSavikaMast.Nom2BrID != 0)
            {
                Cmd.Parameters["in_Nom2BrID"].Value = BoSavikaMast.Nom2BrID;
            }
            else
            {
                Cmd.Parameters["in_Nom2BrID"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom2Accno))
            {
                Cmd.Parameters["in_Nom2Accno"].Value = BoSavikaMast.Nom2Accno;
            }
            else
            {
                Cmd.Parameters["in_Nom2Accno"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom2Ratio != 0)
            {
                Cmd.Parameters["in_Nom2Ratio"].Value = BoSavikaMast.Nom2Ratio;
            }
            else
            {
                Cmd.Parameters["in_Nom2Ratio"].Value = DBNull.Value;
            }

            if (!string.IsNullOrEmpty(BoSavikaMast.Nom3name))
            {
                Cmd.Parameters["in_Nom3Name"].Value = BoSavikaMast.Nom3name;
            }
            else
            {
                Cmd.Parameters["in_Nom3Name"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom3relaid != 0)
            {
                Cmd.Parameters["in_Nom3RelationId"].Value = BoSavikaMast.Nom3relaid;
            }
            else
            {
                Cmd.Parameters["in_Nom3RelationId"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom3age != 0)
            {
                Cmd.Parameters["in_Nom3Age"].Value = BoSavikaMast.Nom3age;
            }
            else
            {
                Cmd.Parameters["in_Nom3Age"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom3address))
            {
                Cmd.Parameters["in_Nom3Address"].Value = BoSavikaMast.Nom3address;
            }
            else
            {
                Cmd.Parameters["in_Nom3Address"].Value = DBNull.Value;
            }

            if (BoSavikaMast.Nom3BrID != 0)
            {
                Cmd.Parameters["in_Nom3BrID"].Value = BoSavikaMast.Nom3BrID;
            }
            else
            {
                Cmd.Parameters["in_Nom3BrID"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoSavikaMast.Nom3Accno))
            {
                Cmd.Parameters["in_Nom3Accno"].Value = BoSavikaMast.Nom3Accno;
            }
            else
            {
                Cmd.Parameters["in_Nom3Accno"].Value = DBNull.Value;
            }
            if (BoSavikaMast.Nom3Ratio != 0)
            {
                Cmd.Parameters["in_Nom3Ratio"].Value = BoSavikaMast.Nom3Ratio;
            }
            else
            {
                Cmd.Parameters["in_Nom3Ratio"].Value = DBNull.Value;
            }
            if (BoSavikaMast.ExitDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_ExitDate"].Value = BoSavikaMast.ExitDate;
            }
            else
            {
                Cmd.Parameters["in_ExitDate"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_rejoinflag"].Value = BoSavikaMast.rejoinflag;
            Cmd.Parameters["out_Errorcode"].Value = BoSavikaMast.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoSavikaMast.ErrorMessage;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoSavikaMast.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoSavikaMast.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();

        }
    }
}
