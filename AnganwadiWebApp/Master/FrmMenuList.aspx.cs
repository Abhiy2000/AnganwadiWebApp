using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace ProjectManagement.Master
{
    public partial class FrmMenuList : System.Web.UI.Page
    {
        #region "MeesageAlert"
        public void MessageAlert(string message, string WindowsLocation)
        {
            lblPopupResponse.Text = "|| " + message + "||";
            popMsg.Show();

            if (WindowsLocation != "")
            {
                lblredirect.Text = WindowsLocation;
            }
            else
            {
                lblredirect.Text = "";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmMenuMstRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Menu List";
                String Query = "  select a.var_menumst_menuname menutitle,c.var_menumst_menuname as parent,a.var_menumst_pagepath pagepath, ";
                Query += "  a.num_menumst_menuid menuid,a.num_menumst_parentid  parentid from aoup_menumst_def a left outer join aoup_menumst_def c on ";
                Query += "   c.num_menumst_menuid=a.num_menumst_parentid where a.num_menumst_parentid<>0 order by c.var_menumst_menuname ";//
                DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);
                if (TblMenuList.Rows.Count > 0)
                {
                    GrdMenuList.DataSource = TblMenuList;
                    GrdMenuList.DataBind();
                }
            }
        }

        protected void GrdMenuList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdMenuList.SelectedRow;
            Session["MenuId"] = row.Cells[4].Text;
            Session["PMenuId"] = row.Cells[5].Text;
            Response.Redirect("../Master/FrmMenuMst.aspx?@=2");
        }

        protected void GrdMenuList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmMenuMst.aspx?@=1");
        }
    }
}