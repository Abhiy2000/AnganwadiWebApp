using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Microsoft.VisualBasic;
using AnganwadiLib.Business;

namespace AnganwadiLib.Data
{
    public class DataMenu
    {
        Menu ObjMenu;

        public DataMenu()
        { }

        public DataMenu(Menu BoMenu)
        {
            ObjMenu = BoMenu;
        }

        public void Insert(Menu BoMenu)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();
            OracleCommand Cmd = new OracleCommand("aoup_menu_ins", Con.connection);

            Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Cmd.Parameters.Add("in_MenuId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_ParentMenuId", OracleDbType.Int64);
            Cmd.Parameters.Add("in_PageTitle", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_PagePath", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_AccessString", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_UserId", OracleDbType.Varchar2);
            Cmd.Parameters.Add("in_Mode", OracleDbType.Int64);
            Cmd.Parameters.Add("out_ErrorCode", OracleDbType.Int64, 5);
            Cmd.Parameters.Add("out_ErrorMsg", OracleDbType.Varchar2, 300);

            Cmd.Parameters["in_MenuId"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_ParentMenuId"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_PageTitle"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_PagePath"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_AccessString"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_UserId"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["in_Mode"].Direction = System.Data.ParameterDirection.Input;
            Cmd.Parameters["out_ErrorCode"].Direction = System.Data.ParameterDirection.Output;
            Cmd.Parameters["out_ErrorMsg"].Direction = System.Data.ParameterDirection.Output;

            Cmd.Parameters["in_MenuId"].Value = BoMenu.MenuId;

            if (BoMenu.ParentId == null)
            {
                Cmd.Parameters["in_ParentMenuId"].Value = DBNull.Value;
            }

            else
            {
                Cmd.Parameters["in_ParentMenuId"].Value = BoMenu.ParentId;
            }

            Cmd.Parameters["in_PageTitle"].Value = BoMenu.PageTitle;
            Cmd.Parameters["in_PagePath"].Value = BoMenu.PagePath;

            if (BoMenu.AccessString == null || BoMenu.AccessString == "")
            {
                Cmd.Parameters["in_AccessString"].Value = DBNull.Value;
            }

            else
            {
                Cmd.Parameters["in_AccessString"].Value = BoMenu.AccessString;
            }

            Cmd.Parameters["in_UserId"].Value = BoMenu.InsBy;
            Cmd.Parameters["in_Mode"].Value = BoMenu.Mode;

            Cmd.ExecuteNonQuery();
            Con.CloseConn();
            BoMenu.ErrorCode = Convert.ToInt32(Cmd.Parameters["out_Errorcode"].Value.ToString());
            BoMenu.ErrorMessage = Cmd.Parameters["out_ErrorMsg"].Value.ToString();
        }

        public void Find(int Id)
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();
            OracleTransaction Trns;
            Trns = Con.connection.BeginTransaction();
            OracleDataReader Rdr;

            String query = "  select a.num_menumst_menuid menuid,a.num_menumst_parentid parentid,a.var_menumst_menuname menuname,a.var_menumst_pagepath pagepath, ";
            query += " s.num_corporation_corpid compid,s.var_corporation_corpname compname,s.num_corporation_parentid brcategory from ";
            query += " aoup_menumst_def a  LEFT OUTER JOIN aoup_menucompmst_def C ON a.num_menumst_menuid=C.num_menucompmst_compid  ";
            query += " LEFT OUTER JOIN aoup_corporation_mas S ON S.num_corporation_corpid=C.num_menucompmst_compid where ";
            query += "a.num_menumst_menuid=" + Id + " order by S.num_corporation_corpid";

            OracleCommand Cmd = new OracleCommand(query, Con.connection);
            Cmd.Transaction = Trns;
            Rdr = Cmd.ExecuteReader();
            if (Rdr.HasRows == true)
            {
                ObjMenu.TblList.Columns.Add("Brid", typeof(Int32));
                ObjMenu.TblList.Columns.Add("BrName", typeof(String));
                ObjMenu.TblList.Columns.Add("Category", typeof(String));
                ObjMenu.TblList.Columns.Add("Address", typeof(String));

                while (Rdr.Read())
                {
                    ObjMenu.MenuId = Id;
                    ObjMenu.PagePath = Rdr["pagepath"].ToString();
                    ObjMenu.PageTitle = Rdr["menuname"].ToString();

                    if (Information.IsDBNull(Rdr["parentid"]) == false)
                    {
                        ObjMenu.ParentId = Convert.ToInt16(Rdr["parentid"]);
                    }
                    if (Information.IsDBNull(Rdr["compid"]) == false)
                    {
                        ObjMenu.TblList.Rows.Add(Rdr["compid"], Rdr["compname"], Rdr["brcategory"], Rdr["address"]);
                    }
                }
            }
            Rdr.Dispose();
            Con.CloseConn();
        }

        public void GetBranchList()
        {
            Data.GetCon Con = new GetCon();
            Con.OpenConn();
            OracleTransaction Trns;
            Trns = Con.connection.BeginTransaction();
            OracleDataReader Rdr;
            String query = "  select num_corporation_corpid corpid,var_corporation_corpname corpname from aoup_corporation_mas ";
            query += " where num_corporation_parentid = 1 or num_corporation_parentid = 0 order by num_corporation_corpid ";
            OracleCommand Cmd = new OracleCommand(query, Con.connection);
            Cmd.Transaction = Trns;
            Rdr = Cmd.ExecuteReader();
            if (Rdr.HasRows == true)
            {
                ObjMenu.TblBranchList.Columns.Add("Brid", typeof(Int32));
                ObjMenu.TblBranchList.Columns.Add("BrName", typeof(String));


                while (Rdr.Read())
                {
                    ObjMenu.TblBranchList.Rows.Add(Rdr["corpid"], Rdr["corpname"]);
                }
            }
            Rdr.Dispose();
            Con.CloseConn();
        }
    }
}
