using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmRelationList : System.Web.UI.Page
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
                LblGrdHead.Text = "Master" + " >> " + "Relation Master";// Session["LblGrdHead"].ToString();//
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmRelationMst.aspx?@=1");
        }

        protected void grdRelationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdRelationList.SelectedRow;
            Session["RelationId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmRelationMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_relation_relationid relationid,var_relation_relation relationNM from aoup_relation_def order by var_relation_relation ";

            DataTable tblRellist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblRellist.Rows.Count > 0)
            {
                grdRelationList.DataSource = tblRellist;
                grdRelationList.DataBind();
                grdRelationList.Visible = true;
            }
            else
            {
                grdRelationList.Visible = false;
            }
        }

        protected void grdRelationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}