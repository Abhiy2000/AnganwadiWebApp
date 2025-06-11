using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.MstListPages
{
    public partial class FrmBITList : System.Web.UI.Page
    {
        static string SessiongrdLevel = "";

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
            SessiongrdLevel = Session["GrdLevel"].ToString();

            if (Session["UserFullName"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();
                String BITList = " select BRID, COMPANYNAME, BRANCHNAME, HOID, PARENTID, BRCATEGORY, COMPCODE from companyview where parentid = " + Session["GrdLevel"] + " and brcategory = 5 ";
                DataTable TblBITList = (DataTable)MstMethods.Query2DataTable.GetResult(BITList);                
                GrdBITList.DataSource = TblBITList;
                GrdBITList.DataBind();
            }
        }

        protected void GrdBITList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdBITList.SelectedRow;
            Session["GrdLevel"] = row.Cells[1].Text;
            Session["LblGrdHead"] = LblGrdHead.Text + " >> " + row.Cells[3].Text;
            Response.Redirect(Session["BrToList"].ToString());
        }

        protected void GrdBITList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}