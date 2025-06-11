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
    public partial class FrmMaritStatList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmMaritStatRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Marital Status Master";// Session["LblGrdHead"].ToString();// 
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmMaritStatMst.aspx?@=1");
        }

        protected void grdMaritStatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdMaritStatList.SelectedRow;
            Session["MaritStatId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmMaritStatMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_maritstat_maritstatid maritId,var_maritstat_maritstat status from aoup_maritstat_def order by var_maritstat_maritstat ";

            DataTable tblMaritlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblMaritlist.Rows.Count > 0)
            {
                grdMaritStatList.DataSource = tblMaritlist;
                grdMaritStatList.DataBind();
                grdMaritStatList.Visible = true;
            }
            else
            {
                grdMaritStatList.Visible = false;
            }
        }

        protected void grdMaritStatList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}