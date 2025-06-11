using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;

namespace ProjectManagement.Master
{
    public partial class FrmUserAccessMst : System.Web.UI.Page
    {
        UserAccess ObjAccess = new UserAccess();

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
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"User Access";

                String Query = " select num_menumst_menuid,var_menumst_menuname,num_menucompmst_compid from aoup_menumst_def a ";
                Query += " inner join aoup_menucompmst_def c on a.num_menumst_menuid=c.num_menucompmst_menuid ";
                Query += " inner join companyview Ho on ho.hoid=c.num_menucompmst_compid ";
                Query += " where a.var_menumst_pagepath is not null and ho.brid=" + Session["AccessBrId"].ToString();

                DataTable TblUserDet = (DataTable)MstMethods.Query2DataTable.GetResult(Query);

                if (TblUserDet.Rows.Count > 0)
                {
                    ViewState["TblAccess"] = TblUserDet;
                    GrdAccess.DataSource = TblUserDet;
                    GrdAccess.DataBind();
                    SetGrdAccess();
                }
            }
        }

        protected void GrdAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        public void SetGrdAccess()
        {
            DataTable dt = (DataTable)ViewState["TblAccess"];

            Int32 cnt = 0;

            if (dt != null)
            {
                String str = "select num_menuusersmst_menuid from aoup_menuusersmst_def where var_menuusersmst_userid='" + Session["AccessUserId"].ToString() + "'";

                DataTable Tblmenuuserlevel = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                if (Tblmenuuserlevel.Rows.Count > 0)
                {
                    for (int i = 0; i < Tblmenuuserlevel.Rows.Count; i++)
                    {
                        for (int l = 0; l < dt.Rows.Count; l++)
                        {
                            if (Tblmenuuserlevel.Rows[i]["num_menuusersmst_menuid"].ToString() == dt.Rows[l]["num_menumst_menuid"].ToString())
                            {
                                CheckBox ChkProcess = (CheckBox)GrdAccess.Rows[l].Cells[1].FindControl("ChkProcess");

                                ChkProcess.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            ObjAccess.BrId = Convert.ToInt32(Session["GrdLevel"].ToString());
            ObjAccess.UserCode = Session["AccessUserId"].ToString();
            ObjAccess.UserId = Session["UserId"].ToString();
            ObjAccess.TblAccess = (DataTable)ViewState["TblAccess"];
            String AccessString = "";
            for (int i = 0; i < GrdAccess.Rows.Count; i++)
            {
                CheckBox ChkMenu = (CheckBox)GrdAccess.Rows[i].Cells[1].FindControl("ChkProcess");
                if (ChkMenu.Checked == true)
                {
                    AccessString += GrdAccess.Rows[i].Cells[2].Text + "#";
                }
            }

            if (AccessString != "")
            {
                ObjAccess.AccessString = AccessString.Remove(AccessString.Length - 1);
            }
            ObjAccess.UpDate();

            if (ObjAccess.ErrorCode == -100)
            {
                MessageAlert(ObjAccess.ErrorMessage, "../Master/FrmUserAcessList.aspx");
                return;
            }

            else
            {
                MessageAlert(ObjAccess.ErrorMessage, "");
                return;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HomePage/FrmDashboard.aspx");
        }
    }
}