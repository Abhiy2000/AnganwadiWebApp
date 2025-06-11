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
    public partial class FrmDepartmentList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDepartmentRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Department List";

                String Query = "select num_department_deptid,var_department_dept ";
                Query += " from aoup_department_def where  num_department_brid=" + Session["GrdLevel"].ToString() + " order by var_department_dept ";

                DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (TblMenuList.Rows.Count > 0)
                {
                    GrdDeptList.DataSource = TblMenuList;
                    GrdDeptList.DataBind();
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmDeptMaster.aspx?@=1");
        }

        protected void GrdDeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdDeptList.SelectedRow;
            Session["DepartmentId"] = row.Cells[2].Text;
            Response.Redirect("../Master/FrmDeptMaster.aspx?@=2");
        }

        protected void GrdDeptList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }
}