using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using AnganwadiLib.Data;

namespace AnganwadiLib.Methods
{
    public class MstListPages
    {
        public static void GetBranchAsPerLevel(out System.Data.DataTable TableResult, Int32 BrId, Int32 BranchCategory, out String StrBranch)
        {
            System.Data.DataTable TableBranch = new System.Data.DataTable();
            StrBranch = "";
            if (BranchCategory == (int)AnganwadiLib.Methods.ENums.BranchLevels.Organisation || BranchCategory == (int)AnganwadiLib.Methods.ENums.BranchLevels.Administrator)
            {
                StrBranch = "OrgId";
            }
            else if (BranchCategory == (int)AnganwadiLib.Methods.ENums.BranchLevels.Division)
            {
                StrBranch = "DiviId";
            }
            else if (BranchCategory == (int)AnganwadiLib.Methods.ENums.BranchLevels.District)
            {
                StrBranch = "DistId";
            }
            else if (BranchCategory == (int)AnganwadiLib.Methods.ENums.BranchLevels.CDPO)
            {
                StrBranch = "CDPOId";
            }
            else if (BranchCategory == (int)AnganwadiLib.Methods.ENums.BranchLevels.BIT)
            {
                StrBranch = "BITId";
            }

            String BranchQuery = " select brid,brname from branchlevelinfo where " + StrBranch + "=" + BrId;
            AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(BranchQuery);
            TableResult = TableBranch;
        }

        public static object GetOrganisationList(System.Data.DataTable TableResult, String JoinTableName, String JoinColumnName)
        {
            Data.GetCon Con = new GetCon();

            String query = "";

            OracleCommand cmd = new OracleCommand(query, Con.connection);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(TableResult);
            Con.CloseConn();
            return TableResult;
        }

        public static object GetDivisionList(System.Data.DataTable TableResult, String JoinTableName, String JoinColumnName)
        {
            Data.GetCon Con = new GetCon();

            String query = "";

            OracleCommand cmd = new OracleCommand(query, Con.connection);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(TableResult);
            Con.CloseConn();
            return TableResult;
        }

        public static object GetDistrictList(System.Data.DataTable TableResult, String JoinTableName, String JoinColumnName)
        {
            Data.GetCon Con = new GetCon();

            String query = "";

            OracleCommand cmd = new OracleCommand(query, Con.connection);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(TableResult);
            Con.CloseConn();
            return TableResult;
        }

        public static object GetCDPOList(System.Data.DataTable TableResult, String JoinTableName, String JoinColumnName)
        {
            Data.GetCon Con = new GetCon();

            String query = "";

            OracleCommand cmd = new OracleCommand(query, Con.connection);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(TableResult);
            Con.CloseConn();
            return TableResult;
        }

        public static object GetBITList(System.Data.DataTable TableResult, String JoinTableName, String JoinColumnName)
        {
            Data.GetCon Con = new GetCon();

            String query = "";

            OracleCommand cmd = new OracleCommand(query, Con.connection);
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(TableResult);
            Con.CloseConn();
            return TableResult;
        }
    }
}
