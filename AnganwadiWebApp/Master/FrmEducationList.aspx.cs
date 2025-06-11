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
    public partial class FrmEducationList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmEducationRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Qualification Master";// Session["LblGrdHead"].ToString(); //
                LoadGrid();
            }
        }

        protected void LoadGrid()
        {
            String Query = "select num_Education_Educid EduId,var_Education_EducName ";
            Query += " from aoup_Education_def order by var_Education_EducName ";

            DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (TblMenuList.Rows.Count > 0)
            {
                GrdEducationList.DataSource = TblMenuList;
                GrdEducationList.DataBind();
            }
            else
            {
                GrdEducationList.DataSource = null;
                GrdEducationList.DataBind();
                //MessageAlert("No record found", "");
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmEducationMaster.aspx?@=1");
        }

        protected void GrdEducationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        protected void GrdEducationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdEducationList.SelectedRow;
            Session["EduId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmEducationMaster.aspx?@=2");
        }
    }
}