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
    public partial class FrmAllowenceList : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmAlloenceRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Allowence List";
                LoadGrid();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmAllowenceMst.aspx?@=1");
        }

        protected void grdAllowList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdAllowList.SelectedRow;
            Session["PrjId"] = row.Cells[2].Text;
            //Session["DesgId"] = row.Cells[2].Text;
            Session["EduId"] = row.Cells[4].Text;

            Response.Redirect("../Master/FrmAllowenceMst.aspx?@=2");
        }

        public void LoadGrid()
        {
            String det = " select num_allowance_prjtypeid prjid,num_allowance_desigid desgid,num_allowance_educid eduid,var_projecttype_prjtype prjtype, ";
            det += " var_designation_desig desg,var_education_educname education,var_allowance_name allowname,num_allowance_amount amt ";
            det += " from aoup_allowance_def a left join aoup_projecttype_def b on b.num_projecttype_compid=a.num_allowance_compid and ";
            det += " a.num_allowance_prjtypeid=b.num_projecttype_prjtypeid left join aoup_designation_def c on a.num_allowance_desigid=c.num_designation_desigid ";
            det += " left join aoup_education_def d on a.num_allowance_educid=d.num_education_educid ";
            det += " where num_allowance_compid=" + Session["GrdLevel"] + " order by var_allowance_name ";

            DataTable tblAllowlist = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(det);

            if (tblAllowlist.Rows.Count > 0)
            {
                grdAllowList.DataSource = tblAllowlist;
                grdAllowList.DataBind();
                grdAllowList.Visible = true;
            }
            else
            {
                grdAllowList.Visible = false;
            }
        }

        protected void grdAllowList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}