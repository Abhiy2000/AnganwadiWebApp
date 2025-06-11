using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{

    public class Data_LIC_Sevika
    {
        BoLIC_Sevika ObjLICSavikaMast;

        public Data_LIC_Sevika()
        { }

        public Data_LIC_Sevika(BoLIC_Sevika BoLICSavikaMast)
        {
            ObjLICSavikaMast = BoLICSavikaMast;
        }

        public void Insert(BoLIC_Sevika BoLICSavikaMast)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_LICsevika_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_CompID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_SevikaID", OracleDbType.Int64);
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_AnganId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Name", OracleDbType.Varchar2);
            //Cmd.Parameters.Add("in_SevikaCode", OracleDbType.Varchar2);
            // Cmd.Parameters.Add("in_EducId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_BirthDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_Address", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_MobileNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_PhoneNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_PanNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_AadharNo", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_BranchId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_AccNo", OracleDbType.NVarchar2);
            Cmd.Parameters.Add("in_Reason", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64);
            Cmd.Parameters.Add("in_MiddleName", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Village", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_PinCode", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_DesigCode", OracleDbType.Varchar2);

            Cmd.Parameters.Add("in_Nom1Name", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom1RelatnId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom1Age", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom1Address", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom2Name", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Nom2RelationId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom2Age", OracleDbType.Int64);
            Cmd.Parameters.Add("in_Nom2Address", OracleDbType.Varchar2);


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

            Cmd.Parameters.Add("in_JoinDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_RetireDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_LastSalary", OracleDbType.Int32);
            Cmd.Parameters.Add("in_Active", OracleDbType.Varchar2);

            Cmd.Parameters.Add("in_ExitDate", OracleDbType.TimeStamp);
            Cmd.Parameters.Add("in_OldTSoftwareNo", OracleDbType.Varchar2);

            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int32, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);
            Cmd.Parameters.Add("out_SevikaID", OracleDbType.Int32, 5);

            Cmd.Parameters["in_CompID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_SevikaID"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_UserId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_AnganId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Name"].Direction = ParameterDirection.Input;
            //Cmd.Parameters["in_SevikaCode"].Direction = ParameterDirection.Input;
            // Cmd.Parameters["in_EducId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_BirthDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Address"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_MobileNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PhoneNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PanNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_AadharNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_BranchId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_AccNo"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Reason"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Mode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_MiddleName"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Village"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_PinCode"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_DesigCode"].Direction = ParameterDirection.Input;

            Cmd.Parameters["in_Nom1Name"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1RelatnId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1Age"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom1Address"].Direction = ParameterDirection.Input;

            Cmd.Parameters["in_Nom2Name"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2RelationId"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2Age"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_Nom2Address"].Direction = ParameterDirection.Input;

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

            Cmd.Parameters["in_JoinDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_RetireDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_LastSalary"].Direction = ParameterDirection.Input;

            Cmd.Parameters["in_ExitDate"].Direction = ParameterDirection.Input;
            Cmd.Parameters["in_OldTSoftwareNo"].Direction = ParameterDirection.Input;

            Cmd.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;
            Cmd.Parameters["out_SevikaID"].Direction = ParameterDirection.Output;

            Cmd.Parameters["in_CompID"].Value = BoLICSavikaMast.CompID;
            Cmd.Parameters["in_SevikaID"].Value = BoLICSavikaMast.SevikaID;
            Cmd.Parameters["in_UserId"].Value = BoLICSavikaMast.UserID;
            Cmd.Parameters["in_AnganId"].Value = BoLICSavikaMast.AnganId;
            Cmd.Parameters["in_Name"].Value = BoLICSavikaMast.Name;

            if (BoLICSavikaMast.BirthDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_BirthDate"].Value = BoLICSavikaMast.BirthDate;
            }
            else
            {
                Cmd.Parameters["in_BirthDate"].Value = DBNull.Value;
            }

            Cmd.Parameters["in_Address"].Value = BoLICSavikaMast.Address;
            Cmd.Parameters["in_MobileNo"].Value = BoLICSavikaMast.MobileNo;
            Cmd.Parameters["in_PhoneNo"].Value = BoLICSavikaMast.PhoneNo;
            Cmd.Parameters["in_PanNo"].Value = BoLICSavikaMast.PanNo;
            Cmd.Parameters["in_AadharNo"].Value = BoLICSavikaMast.AadharNo;

            if (BoLICSavikaMast.BranchId != 0)
            {
                Cmd.Parameters["in_BranchId"].Value = BoLICSavikaMast.BranchId;
            }
            else
            {
                Cmd.Parameters["in_BranchId"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_AccNo"].Value = BoLICSavikaMast.AccNo;




            if (BoLICSavikaMast.Reason != "")
            {
                Cmd.Parameters["in_Reason"].Value = BoLICSavikaMast.Reason;
            }
            else
            {
                Cmd.Parameters["in_Reason"].Value = DBNull.Value;
            }

            Cmd.Parameters["in_Mode"].Value = BoLICSavikaMast.Mode;
            Cmd.Parameters["in_MiddleName"].Value = BoLICSavikaMast.MiddleName;
            Cmd.Parameters["in_Village"].Value = BoLICSavikaMast.Village;
            Cmd.Parameters["in_PinCode"].Value = BoLICSavikaMast.PinCode;
            Cmd.Parameters["in_DesigCode"].Value = BoLICSavikaMast.DesigID;

            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom1Name))
            {
                Cmd.Parameters["in_Nom1Name"].Value = BoLICSavikaMast.Nom1Name;
            }
            else
            {
                Cmd.Parameters["in_Nom1Name"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom1RelatnId != 0)
            {
                Cmd.Parameters["in_Nom1RelatnId"].Value = BoLICSavikaMast.Nom1RelatnId;
            }
            else
            {
                Cmd.Parameters["in_Nom1RelatnId"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom1Age != 0)
            {
                Cmd.Parameters["in_Nom1Age"].Value = BoLICSavikaMast.Nom1Age;
            }
            else
            {
                Cmd.Parameters["in_Nom1Age"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom1Address))
            {
                Cmd.Parameters["in_Nom1Address"].Value = BoLICSavikaMast.Nom1Address;
            }
            else
            {
                Cmd.Parameters["in_Nom1Address"].Value = DBNull.Value;
            }

            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom2Name))
            {
                Cmd.Parameters["in_Nom2Name"].Value = BoLICSavikaMast.Nom2Name;
            }
            else
            {
                Cmd.Parameters["in_Nom2Name"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom2RelationId != 0)
            {
                Cmd.Parameters["in_Nom2RelationId"].Value = BoLICSavikaMast.Nom2RelationId;
            }
            else
            {
                Cmd.Parameters["in_Nom2RelationId"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom2Age != 0)
            {
                Cmd.Parameters["in_Nom2Age"].Value = BoLICSavikaMast.Nom2Age;
            }
            else
            {
                Cmd.Parameters["in_Nom2Age"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom2Address))
            {
                Cmd.Parameters["in_Nom2Address"].Value = BoLICSavikaMast.Nom2Address;
            }
            else
            {
                Cmd.Parameters["in_Nom2Address"].Value = DBNull.Value;
            }

            //--- added on 18-04-19
            if (BoLICSavikaMast.Nom1BrID != 0)
            {
                Cmd.Parameters["in_Nom1BrID"].Value = BoLICSavikaMast.Nom1BrID;
            }
            else
            {
                Cmd.Parameters["in_Nom1BrID"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom1Accno))
            {
                Cmd.Parameters["in_Nom1Accno"].Value = BoLICSavikaMast.Nom1Accno;
            }
            else
            {
                Cmd.Parameters["in_Nom1Accno"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom1Ratio != 0)
            {
                Cmd.Parameters["in_Nom1Ratio"].Value = BoLICSavikaMast.Nom1Ratio;
            }
            else
            {
                Cmd.Parameters["in_Nom1Ratio"].Value = DBNull.Value;
            }

            if (BoLICSavikaMast.Nom2BrID != 0)
            {
                Cmd.Parameters["in_Nom2BrID"].Value = BoLICSavikaMast.Nom2BrID;
            }
            else
            {
                Cmd.Parameters["in_Nom2BrID"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom2Accno))
            {
                Cmd.Parameters["in_Nom2Accno"].Value = BoLICSavikaMast.Nom2Accno;
            }
            else
            {
                Cmd.Parameters["in_Nom2Accno"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom2Ratio != 0)
            {
                Cmd.Parameters["in_Nom2Ratio"].Value = BoLICSavikaMast.Nom2Ratio;
            }
            else
            {
                Cmd.Parameters["in_Nom2Ratio"].Value = DBNull.Value;
            }

            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom3name))
            {
                Cmd.Parameters["in_Nom3Name"].Value = BoLICSavikaMast.Nom3name;
            }
            else
            {
                Cmd.Parameters["in_Nom3Name"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom3relaid != 0)
            {
                Cmd.Parameters["in_Nom3RelationId"].Value = BoLICSavikaMast.Nom3relaid;
            }
            else
            {
                Cmd.Parameters["in_Nom3RelationId"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom3age != 0)
            {
                Cmd.Parameters["in_Nom3Age"].Value = BoLICSavikaMast.Nom3age;
            }
            else
            {
                Cmd.Parameters["in_Nom3Age"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom3address))
            {
                Cmd.Parameters["in_Nom3Address"].Value = BoLICSavikaMast.Nom3address;
            }
            else
            {
                Cmd.Parameters["in_Nom3Address"].Value = DBNull.Value;
            }

            if (BoLICSavikaMast.Nom3BrID != 0)
            {
                Cmd.Parameters["in_Nom3BrID"].Value = BoLICSavikaMast.Nom3BrID;
            }
            else
            {
                Cmd.Parameters["in_Nom3BrID"].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(BoLICSavikaMast.Nom3Accno))
            {
                Cmd.Parameters["in_Nom3Accno"].Value = BoLICSavikaMast.Nom3Accno;
            }
            else
            {
                Cmd.Parameters["in_Nom3Accno"].Value = DBNull.Value;
            }
            if (BoLICSavikaMast.Nom3Ratio != 0)
            {
                Cmd.Parameters["in_Nom3Ratio"].Value = BoLICSavikaMast.Nom3Ratio;
            }
            else
            {
                Cmd.Parameters["in_Nom3Ratio"].Value = DBNull.Value;
            }

            if (BoLICSavikaMast.JoinDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_JoinDate"].Value = BoLICSavikaMast.JoinDate;
            }
            else
            {
                Cmd.Parameters["in_JoinDate"].Value = DBNull.Value;
            }
            //if (BoLICSavikaMast.RetireDate != DateTime.MinValue)
            //{
            //    Cmd.Parameters["in_RetireDate"].Value = BoLICSavikaMast.RetireDate;
            //}
            //else
            //{
            Cmd.Parameters["in_RetireDate"].Value = DBNull.Value;
            //  }

            if (BoLICSavikaMast.LastSalary != 0)
            {
                Cmd.Parameters["in_LastSalary"].Value = BoLICSavikaMast.LastSalary;
            }
            else
            {
                Cmd.Parameters["in_LastSalary"].Value = DBNull.Value;
            }

            Cmd.Parameters["in_Active"].Value = BoLICSavikaMast.Active;

            if (BoLICSavikaMast.ExitDate != DateTime.MinValue)
            {
                Cmd.Parameters["in_ExitDate"].Value = BoLICSavikaMast.ExitDate;
            }
            else
            {
                Cmd.Parameters["in_ExitDate"].Value = DBNull.Value;
            }

            Cmd.Parameters["in_OldTSoftwareNo"].Value = BoLICSavikaMast.SoftwareNo;

            Cmd.Parameters["out_Errorcode"].Value = BoLICSavikaMast.ErrorCode;
            Cmd.Parameters["out_ErrorMsg"].Value = BoLICSavikaMast.ErrorMessage;
            Cmd.Parameters["out_SevikaID"].Value = BoLICSavikaMast.OutSevikaID;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoLICSavikaMast.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoLICSavikaMast.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
            BoLICSavikaMast.OutSevikaID = Convert.ToInt32(Cmd.Parameters["out_SevikaID"].Value.ToString());

        }


    }
}
