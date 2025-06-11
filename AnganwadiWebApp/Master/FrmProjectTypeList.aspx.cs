using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmProjectTypeList : System.Web.UI.Page
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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"ProjectType List";
                LoadGrid();
            }
        }

        protected void LoadGrid()
        {
            String Query = "select num_ProjectType_PrjTypeid ProId,var_ProjectType_PrjType ";
            Query += " from aoup_ProjectType_def  where  num_ProjectType_CompId =" + Session["GrdLevel"].ToString() + " order by var_ProjectType_PrjType ";

            DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (TblMenuList.Rows.Count > 0)
            {
                GrdProjectList.DataSource = TblMenuList;
                GrdProjectList.DataBind();
            }
            else
            {
                GrdProjectList.DataSource = null;
                GrdProjectList.DataBind();
                //MessageAlert("No record found", "");
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmProjTypeMaster.aspx?@=1");
        }

        protected void GrdProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        protected void GrdProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdProjectList.SelectedRow;
            Session["ProId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmProjTypeMaster.aspx?@=2");
        }
    }
}