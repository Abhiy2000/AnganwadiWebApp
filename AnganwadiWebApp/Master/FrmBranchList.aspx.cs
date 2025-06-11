using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;

namespace AnganwadiLib.Masters
{
    public partial class FrmBranchList : System.Web.UI.Page
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

            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmBranchRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            LblGrdHead.Text = "Master" + " >> " + "Branch List";// Session["LblGrdHead"].ToString();
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
                }
                MstMethods.Dropdown.Fill(ddlBank, "aoup_Bank_def order by trim(var_bank_bankname)", "var_bank_bankname", "num_bank_bankid ", "", "");
            }
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBank.SelectedIndex >= 1)
            {
                if (ddlBank.SelectedValue.ToString() != "" && ddlBank.SelectedValue.ToString() != "0")
                {
                    LoadGrid();
                }
            }
        }

        protected void LoadGrid()
        {
            String query = "select a.num_bankbranch_branchid branchid,a.var_bankbranch_branchname branchname,a.num_bankbranch_bankid bankid,a.var_bankbranch_IFSCcode ifsccode, ";
            query += " b.var_bank_bankname bankname from aoup_BankBranch_def a inner join aoup_Bank_def b on a.num_bankbranch_bankid=b.num_bank_bankid ";
            query += " where num_bankbranch_bankid='" + ddlBank.SelectedValue + "' order by a.var_bankbranch_branchname ";

            DataTable dtBranchList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtBranchList.Rows.Count > 0)
            {
                Session["bankid"] = dtBranchList.Rows[0]["bankid"].ToString();
                grdBranchList.DataSource = dtBranchList;
                grdBranchList.DataBind();
            }
            else
            {
                grdBranchList.DataSource = null;
                grdBranchList.DataBind();
                //  MessageAlert("No record found", "");
            }
        }

        protected void grdBranchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBranchList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void grdBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdBranchList.SelectedRow;
            Session["branchid"] = row.Cells[2].Text;
            Response.Redirect("../Master/FrmBranchMst.aspx?@=2");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/FrmBranchMst.aspx?@=1");
        }

        protected void grdBranchList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}