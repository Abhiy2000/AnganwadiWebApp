using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    public class DataAngnMst
    {
        BoAnganMst objangn;

        public DataAngnMst()
        { }

        public DataAngnMst(BoAnganMst angn)
        {
            objangn = angn;
        }

        public void InsertAngn(BoAnganMst angn)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();

            OracleCommand Cmd = new OracleCommand("aoup_angnwadimst_ins", Con.connection);
            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_BrId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AngnID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PrjTypeId", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AngnName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_AngnCode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Address", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Email", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_MobileNo", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PhoneNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Active", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_PinCode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64).Direction = ParameterDirection.Output;
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2,500).Direction = ParameterDirection.Output;
            
            Cmd.Parameters["in_BrId"].Value=objangn.BrId;
            Cmd.Parameters["in_AngnID"].Value = objangn.AngnID;
            if (objangn.PrjTypeId == 0 || objangn.PrjTypeId == null)
            {
                Cmd.Parameters["in_PrjTypeId"].Value = DBNull.Value;
            }
            else
            {
                Cmd.Parameters["in_PrjTypeId"].Value = objangn.PrjTypeId;
            }

            Cmd.Parameters["in_AngnName"].Value = objangn.AngnName;
            Cmd.Parameters["in_AngnCode"].Value = objangn.AngnCode;
            Cmd.Parameters["in_Address"].Value = objangn.Address;
            Cmd.Parameters["in_Email"].Value = objangn.Email;

            if (objangn.MobileNo != 0 && objangn.MobileNo != null)
            {
                Cmd.Parameters["in_MobileNo"].Value = objangn.MobileNo;
            }
            else
            {
                Cmd.Parameters["in_MobileNo"].Value = DBNull.Value;
            }

            if (objangn.PhoneNo != "" && objangn.PhoneNo != null)
            {
                Cmd.Parameters["in_PhoneNo"].Value = objangn.PhoneNo;
            }
            else
            {
                Cmd.Parameters["in_PhoneNo"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_Active"].Value = objangn.Active;

            if (objangn.PinCode != 0 && objangn.PinCode !=null)
            {
                Cmd.Parameters["in_PinCode"].Value = objangn.PinCode;
            }
            else
            {
                Cmd.Parameters["in_PinCode"].Value = DBNull.Value;
            }
            Cmd.Parameters["in_UserId"].Value = objangn.UserId;
            Cmd.Parameters["in_Mode"].Value = objangn.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();

            angn.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());//-100
            angn.ErrorMsg = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }
    }
}
