using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmReligionList : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Religion Master";// Session["LblGrdHead"].ToString(); // 
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmReligionMst.aspx?@=1");
        }

        protected void grdReliList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdReliList.SelectedRow;
            Session["ReliId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmReligionMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = "   select num_religion_religid relid,var_religion_religname relname from aoup_religion_def order by var_religion_religname ";

            DataTable tblRellist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblRellist.Rows.Count > 0)
            {
                grdReliList.DataSource = tblRellist;
                grdReliList.DataBind();
                grdReliList.Visible = true;
            }
            else
            {
                grdReliList.Visible = false;
            }
        }

        protected void grdReliList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}