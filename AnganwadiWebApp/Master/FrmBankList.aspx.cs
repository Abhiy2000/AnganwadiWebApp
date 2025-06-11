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
    public partial class FrmBankList : System.Web.UI.Page
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

            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmBankRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Bank List";// Session["LblGrdHead"].ToString(); //
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
                }
                LoadGrid();
            }
        }

        protected void LoadGrid()
        {
            String Query = "select num_bank_bankid BankId,var_bank_bankname ";
            Query += " from aoup_Bank_def order by var_bank_bankname";
            // Query += " from aoup_Bank_def  where  num_Education_CompId=" + Session["GrdLevel"].ToString() + " order by num_Education_Educid ";

            DataTable TblMenuList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

            if (TblMenuList.Rows.Count > 0)
            {
                GrdBankList.DataSource = TblMenuList;
                GrdBankList.DataBind();
            }
            else
            {
                GrdBankList.DataSource = null;
                GrdBankList.DataBind();
                // MessageAlert("No record found", "");
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmBankMaster.aspx?@=1");
        }

        protected void GrdBankList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void GrdBankList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Visible = false;
            }
        }

        protected void GrdBankList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdBankList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void GrdBankList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow row = GrdBankList.SelectedRow;
            Session["BankId"] = row.Cells[2].Text;

            Response.Redirect("../Master/FrmBankMaster.aspx?@=2");
        }
    }
}