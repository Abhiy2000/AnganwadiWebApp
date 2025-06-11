using AnganwadiLib.Business;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AnganwadiLib.Data
{
   public class DataSevikaEdit
    {
        BoSevikaEdit objDt;
        public DataSevikaEdit(BoSevikaEdit objDtls)
        {
            objDt = objDtls;
        }
        public void Insert(BoSevikaEdit objDtl)
        {

            AnganwadiLib.Data.GetCon Con = new GetCon();
            OracleCommand cmd = new OracleCommand("Aoup_SevikaEdit_ins", Con.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_COMPID", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_cdpoid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_distid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_divid", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_oldDob", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_oldDoj", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_oldAadharNo", OracleDbType.Int64).Direction = ParameterDirection.Input;

            cmd.Parameters.Add("in_NewDob", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewDoj", OracleDbType.TimeStamp).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_NewAadharNo", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_sevikaid", OracleDbType.Int64).Direction = ParameterDirection.Input;

            cmd.Parameters.Add("in_mode", OracleDbType.Int64).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_sevikaedit_id", OracleDbType.Int64).Direction = ParameterDirection.Input;

            cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_sevikaedit_id", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;

            cmd.Parameters["in_UserId"].Value = objDtl.userid;
            cmd.Parameters["in_COMPID"].Value = objDtl.comp_id;
            cmd.Parameters["in_cdpoid"].Value = objDtl.sevika_cdpoid;
            cmd.Parameters["in_distid"].Value = objDtl.sevika_distrid;

            cmd.Parameters["in_divid"].Value = objDtl.sevika_divid;
            cmd.Parameters["in_oldDob"].Value = objDtl.sevikaDob_old;
            cmd.Parameters["in_oldDoj"].Value = objDtl.sevikaDoJ_old;
            cmd.Parameters["in_oldAadharNo"].Value = objDtl.sevikaAadharNo_old;

            if (objDtl.sevikaDob_new == null)
            {
                cmd.Parameters["in_NewDob"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["in_NewDob"].Value = objDtl.sevikaDob_new;
            }

            if (objDtl.sevikaDoJ_new == null)
            {
                cmd.Parameters["in_NewDoj"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["in_NewDoj"].Value = objDtl.sevikaDoJ_new;
            }

            if (objDtl.sevikaAadharNo_new == null)
            {
                cmd.Parameters["in_NewAadharNo"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["in_NewAadharNo"].Value = objDtl.sevikaAadharNo_new;
            }

 
            cmd.Parameters["in_sevikaid"].Value = objDtl.sevikaid;

            cmd.Parameters["in_mode"].Value = objDtl.In_Mode;
            cmd.Parameters["in_sevikaedit_id"].Value = objDtl.SevikaEdtId;

            Con.OpenConn();
            cmd.ExecuteNonQuery();
            Con.CloseConn();


            objDtl.ErrorCode = Convert.ToInt32(cmd.Parameters["out_Errorcode"].Value.ToString());
            objDtl.ErrorMessage = cmd.Parameters["out_ErrorMessage"].Value.ToString();
            objDtl.SevikaEdtId = Convert.ToInt32(cmd.Parameters["out_sevikaedit_id"].Value.ToString());
        }

        public void InsertCdpo(BoSevikaEdit objDtl)
        {

            GetCon Con = new GetCon();
            OracleCommand cmd = new OracleCommand("Aoup_SevikaEdit_auth", Con.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_FlagMode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("in_StrDtls", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Input;
           
            cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("out_ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
         
            cmd.Parameters["in_UserId"].Value = objDtl.userid;
            cmd.Parameters["in_FlagMode"].Value = objDtl.FlagMode;
            cmd.Parameters["in_StrDtls"].Value = objDtl.StrCDPO;

            Con.OpenConn();
            cmd.ExecuteNonQuery();
            Con.CloseConn();

            objDtl.ErrorCode = Convert.ToInt32(cmd.Parameters["out_Errorcode"].Value.ToString());
            objDtl.ErrorMessage = cmd.Parameters["out_ErrorMessage"].Value.ToString();
        }
        
    }
}
