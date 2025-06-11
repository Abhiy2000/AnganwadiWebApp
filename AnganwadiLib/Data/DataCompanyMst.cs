using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace AnganwadiLib.Data
{
    class DataCompanyMst
    {
        AnganwadiLib.Business.CompanyMst ObjCompanyMst;

        public DataCompanyMst()
        { }

        public DataCompanyMst(AnganwadiLib.Business.CompanyMst BoCompanyMst)
        {
            ObjCompanyMst = BoCompanyMst;
        }

        public void Insert(AnganwadiLib.Business.CompanyMst BoCompanyMst)
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
                com.CommandText = "aoup_company_ins";

                com.Parameters.Add(new OracleParameter("in_Category", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_Parent", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_CorpId", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_CompanyName", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_BranchName", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_CompCode", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_Address", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_PhoneNumber", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_EmailAddress", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_BankBranchId", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_BankAccNo", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_ProjectTypeId", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_OfficerNM", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_PinCode", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_PrjCode", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_DistCode", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_CPSMSCode", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_Mode", OracleDbType.Int64)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("in_UserId", OracleDbType.Varchar2)).Direction = ParameterDirection.Input;
                com.Parameters.Add(new OracleParameter("out_ErrorCode", OracleDbType.Int64, 100)).Direction = ParameterDirection.Output;
                com.Parameters.Add(new OracleParameter("out_ErrorMsg", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;

                if (ObjCompanyMst.Category == null)
                {
                    com.Parameters["in_Category"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_Category"].Value = ObjCompanyMst.Category;
                }
                if (ObjCompanyMst.Parent == null)
                {

                    com.Parameters["in_Parent"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_Parent"].Value = ObjCompanyMst.Parent;
                }

                if (ObjCompanyMst.CorpId == null)
                {
                    com.Parameters["in_CorpId"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_CorpId"].Value = ObjCompanyMst.CorpId;
                }

                if (ObjCompanyMst.CompanyName == null || ObjCompanyMst.CompanyName == "")
                {
                    com.Parameters["in_CompanyName"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_CompanyName"].Value = ObjCompanyMst.CompanyName;
                }
               
                if (ObjCompanyMst.BranchName == null || ObjCompanyMst.BranchName == "")
                {
                    com.Parameters["in_BranchName"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_BranchName"].Value = ObjCompanyMst.BranchName;
                }
                if (ObjCompanyMst.Code == null || ObjCompanyMst.Code == "")
                {
                    com.Parameters["in_CompCode"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_CompCode"].Value = ObjCompanyMst.Code;
                }
                if (ObjCompanyMst.Address == null || ObjCompanyMst.Address == "")
                {
                    com.Parameters["in_Address"].Value = ObjCompanyMst.Address;
                }
                else
                {
                    com.Parameters["in_Address"].Value = ObjCompanyMst.Address;
                }

                if (ObjCompanyMst.PhoneNumber == 0 || ObjCompanyMst.PhoneNumber == null)
                {
                    com.Parameters["in_PhoneNumber"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_PhoneNumber"].Value = ObjCompanyMst.PhoneNumber;
                }

                if (ObjCompanyMst.EmailAddress == "" || ObjCompanyMst.EmailAddress == null)
                {
                    com.Parameters["in_EmailAddress"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_EmailAddress"].Value = ObjCompanyMst.EmailAddress;
                }

                if (ObjCompanyMst.BankBranchId == 0 || ObjCompanyMst.BankBranchId == null)
                {
                    com.Parameters["in_BankBranchId"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_BankBranchId"].Value = ObjCompanyMst.BankBranchId;
                }

                if (ObjCompanyMst.BankAccNo == "" || ObjCompanyMst.BankAccNo == null)
                {
                    com.Parameters["in_BankAccNo"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_BankAccNo"].Value = ObjCompanyMst.BankAccNo;
                }

                if (ObjCompanyMst.ProjectTypeId == 0 || ObjCompanyMst.ProjectTypeId == 0)
                {
                    com.Parameters["in_ProjectTypeId"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_ProjectTypeId"].Value = ObjCompanyMst.ProjectTypeId;
                }

                if (ObjCompanyMst.OfficerNM == "" || ObjCompanyMst.OfficerNM ==null)
                {
                    com.Parameters["in_OfficerNM"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_OfficerNM"].Value = ObjCompanyMst.OfficerNM;
                }

                if (ObjCompanyMst.PinCode == 0 || ObjCompanyMst.PinCode == null)
                {
                    com.Parameters["in_PinCode"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_PinCode"].Value = ObjCompanyMst.PinCode;
                }
                if (ObjCompanyMst.PrjCode == 0 || ObjCompanyMst.PrjCode == null)
                {
                    com.Parameters["in_PrjCode"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_PrjCode"].Value = ObjCompanyMst.PrjCode;
                }
                if (ObjCompanyMst.DistCode == 0 || ObjCompanyMst.DistCode == null)
                {
                    com.Parameters["in_DistCode"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_DistCode"].Value = ObjCompanyMst.DistCode;
                }

                if (ObjCompanyMst.CPSMSCode == null)
                {

                    com.Parameters["in_CPSMSCode"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["in_CPSMSCode"].Value = ObjCompanyMst.CPSMSCode;
                }

                com.Parameters["in_Mode"].Value = ObjCompanyMst.Mode;
                com.Parameters["in_UserId"].Value = ObjCompanyMst.UserId;

                com.ExecuteNonQuery();

                String Errcode = com.Parameters["out_Errorcode"].Value.ToString().ToString();
                ObjCompanyMst.ErrCode = Convert.ToInt32(Errcode);

                String Errmsg = com.Parameters["out_ErrorMsg"].Value.ToString();
                ObjCompanyMst.ErrMsg = Errmsg;
            }

            catch (Exception ex)
            {
                ObjCompanyMst.ErrCode = 0;
                ObjCompanyMst.ErrMsg = ex.Message;
            }

            finally
            {
                com.Dispose();
                conn.Dispose();
            }
        }
    }
}
