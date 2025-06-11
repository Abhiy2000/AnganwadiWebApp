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
    public partial class FrmDesignationList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDesignationRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Designation List";

                String Query = "select num_designation_desigid,var_designation_desig,var_designation_code ";
                Query += " from aoup_designation_def order by var_designation_desig ";

                DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (TblMenuList.Rows.Count > 0)
                {
                    GrdDesignationList.DataSource = TblMenuList;
                    GrdDesignationList.DataBind();
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDesignationMaster.aspx?@=1");
        }

        protected void GrdDesignationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdDesignationList.SelectedRow;
            Session["DesignationId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmDesignationMaster.aspx?@=2");
        }

        protected void GrdDesignationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
        }
    }
}