using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmDivisionList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDivisionRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Division List";
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDivisionMst.aspx?@=1");
        }

        protected void grdDivisionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdDivisionList.SelectedRow;
            Session["DivisionId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmDivisionMst.aspx?@=2");
        }

        protected void grdDivisionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
        }

        public void LoadGrid()
        {
            String det = " select num_corporation_corpid corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            det += " var_company_address address,var_projecttype_prjtype PrjType from aoup_corporation_mas a left join aoup_projecttype_def b ";
            det += " on a.num_company_prjtypeid=b.num_projecttype_prjtypeid where num_corporation_parentid=" + Session["GrdLevel"];
            det += " and num_company_category=2 order by var_corporation_branch";

            DataTable tblDivlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblDivlist.Rows.Count > 0)
            {
                grdDivisionList.DataSource = tblDivlist;
                grdDivisionList.DataBind();
                //lblrowCount.Text = "Total No. Of Records : " + grdDivisionList.Rows.Count.ToString();
                grdDivisionList.Visible = true;
            }
            else
            {
                grdDivisionList.Visible = false;
            }
        }
    }
}