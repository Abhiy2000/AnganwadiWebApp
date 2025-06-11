using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProjectManagement.MstListPages
{
    public partial class FrmCompanyList : System.Web.UI.Page
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
            if (Session["UserFullName"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
                String str = "select companyname, brid CompanyId from companyview where brcategory = 1 and compid=" + Session["CompId"].ToString() + " order by companyname";

                DataTable TblCompanyList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                GrdCompanyList.DataSource = TblCompanyList;
                GrdCompanyList.DataBind();
            }
        }

        protected void GrdCompanyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdCompanyList.SelectedRow;

            Session["GrdLevel"] = row.Cells[1].Text;
            Session["LblGrdHead"] = LblGrdHead.Text + " >> " + row.Cells[2].Text;
            if (Session["MasterType"].ToString() == "1")
            {
                Response.Redirect("../MstListPages/FrmBranchList.aspx");
            }

            else
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }

        protected void GrdCompanyList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}