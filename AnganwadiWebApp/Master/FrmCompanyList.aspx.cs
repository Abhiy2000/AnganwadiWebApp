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
    public partial class FrmCompanyList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmCompanyList.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "State Master";// Session["LblGrdHead"].ToString();//
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmCompanyMst.aspx?@=1");
        }

        protected void grdStateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdStateList.SelectedRow;
            Session["StateId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmCompanyMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_corporation_corpid corpid,var_company_compcode Code,var_corporation_corpname Name,var_corporation_branch Branch, ";
            det += " var_company_address address,var_projecttype_prjtype PrjType from aoup_corporation_mas a left join aoup_projecttype_def b ";
            det += " on a.num_company_prjtypeid=b.num_projecttype_prjtypeid where num_corporation_parentid=0 ";
            det += " and num_company_category in (0,1)";

            DataTable tblCastlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblCastlist.Rows.Count > 0)
            {
                grdStateList.DataSource = tblCastlist;
                grdStateList.DataBind();
                grdStateList.Visible = true;
            }
            else
            {
                grdStateList.Visible = false;
            }
        }

        protected void grdStateList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
    }
}