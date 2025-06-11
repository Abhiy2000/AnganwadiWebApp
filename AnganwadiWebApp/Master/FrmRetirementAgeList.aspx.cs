using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmRetirementAgeList : System.Web.UI.Page
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
                LblGrdHead.Text = "Master" + " >> " + "Retirement Age Master";// Session["LblGrdHead"].ToString();//
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmRetirementAgeMst.aspx?@=1");
        }

        protected void grdAgeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdAgeList.SelectedRow;
            Session["AgeId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmRetirementAgeMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_retirementage_ageid ageid,num_retirementage_age age,num_retirementage_desgid desgid,var_designation_desig desg, ";
            det += " date_retirementage_affdate AffDt from aoup_RetirementAge_def a ";
            det += " left join aoup_designation_def b on a.num_retirementage_desgid=b.num_designation_desigid ";
            det += " order by date_retirementage_affdate ";

            DataTable tblCastlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblCastlist.Rows.Count > 0)
            {
                grdAgeList.DataSource = tblCastlist;
                grdAgeList.DataBind();
                grdAgeList.Visible = true;
            }
            else
            {
                grdAgeList.Visible = false;
            }
        }

        protected void grdAgeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
        }

        protected void grdAgeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAgeList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }
    }
}