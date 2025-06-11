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
    public partial class FrmCastList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmCastRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Category Master";// Session["LblGrdHead"].ToString();//
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmCastMst.aspx?@=1");
        }

        protected void grdCastList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdCastList.SelectedRow;
            Session["CastId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmCastMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_cast_castid castid,var_cast_castname castname from aoup_cast_def order by var_cast_castname ";

            DataTable tblCastlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblCastlist.Rows.Count > 0)
            {
                grdCastList.DataSource = tblCastlist;
                grdCastList.DataBind();
                grdCastList.Visible = true;
            }
            else
            {
                grdCastList.Visible = false;
            }
        }

        protected void grdCastList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}