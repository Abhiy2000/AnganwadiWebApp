using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace AnganwadiLib.Data
{
    public class DataBankBrConfig
    {
        AnganwadiLib.Business.BankBrConfig ObjBankBrConfig;
        public DataBankBrConfig()
        { }

        public DataBankBrConfig(AnganwadiLib.Business.BankBrConfig BoBankBrConfig)
        {
            ObjBankBrConfig = BoBankBrConfig;
        }

        public void Insert(AnganwadiLib.Business.BankBrConfig BoBankBrConfig)
        {
            OracleConnection conn = Cls_Dbconnection.dbconn();
            OracleCommand com = new OracleCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else
                {
                    conn.Close();
                    conn.Open();
                }
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = conn;
                com.CommandText = "aoup_bank_ins";



                com.Parameters.Add(new OracleParameter("in_BankId", OracleDbType.Varchar2));
                com.Parameters["in_BankId"].Value = ObjBankBrConfig.BankId;



                com.Parameters.Add(new OracleParameter("in_BankName", OracleDbType.Varchar2));
                com.Parameters["in_BankName"].Value = ObjBankBrConfig.Bank;


                com.Parameters.Add(new OracleParameter("in_UserId", OracleDbType.Varchar2));
                com.Parameters["in_UserId"].Value = ObjBankBrConfig.UserId;

                com.Parameters.Add(new OracleParameter("in_Mode", OracleDbType.Varchar2));
                com.Parameters["in_Mode"].Value = ObjBankBrConfig.Mode;


                com.Parameters.Add(new OracleParameter("out_ErrorCode", OracleDbType.Int64, 100));
                com.Parameters["out_ErrorCode"].Direction = ParameterDirection.Output;

                com.Parameters.Add(new OracleParameter("out_ErrorMsg", OracleDbType.Varchar2, 100));
                com.Parameters["out_ErrorMsg"].Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();

                String Errcode = com.Parameters["out_Errorcode"].Value.ToString().ToString();
                ObjBankBrConfig.ErrCode = Convert.ToInt32(Errcode);

                String Errmsg = com.Parameters["out_ErrorMsg"].Value.ToString();
                ObjBankBrConfig.ErrMsg = Errmsg;


            }
            catch (Exception ex)
            {
                ObjBankBrConfig.ErrCode = 0;
                ObjBankBrConfig.ErrMsg = ex.Message;
            }
            finally
            {
                com.Dispose();
                conn.Dispose();
            }
        }
    }
}
