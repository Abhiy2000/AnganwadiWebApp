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
    public partial class FrmDivisionList : System.Web.UI.Page
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
                String DivList = " select BRID, COMPANYNAME, BRANCHNAME, HOID, PARENTID, BRCATEGORY, COMPCODE from companyview where parentid = " + Session["GrdLevel"] + " and brcategory = 2";
                DataTable TblDivList = (DataTable)MstMethods.Query2DataTable.GetResult(DivList);
                //AnganwadiLib.Methods.MstListPages.GetDivisionList(TblDivList, Session["TableName"].ToString(), Session["ColumnName"].ToString());
                GrdDiviList.DataSource = TblDivList;
                GrdDiviList.DataBind();
                //if (TblDivList.Rows.Count < 2)
                //{
                //    Session["GrdLevel"] = TblDivList.Rows[0]["HOID"];
                //    Response.Redirect("../MstListPages/FrmDistrictList.aspx");
                //}
            }
        }

        protected void GrdDiviList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdDiviList.SelectedRow;
            Session["GrdLevel"] = row.Cells[1].Text;
            Session["LblGrdHead"] = LblGrdHead.Text + " >> " + row.Cells[3].Text;

            if (Session["MasterType"].ToString() == "d")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            else
            {
                Response.Redirect("../MstListPages/FrmDistrictList.aspx");
            }
        }

        protected void GrdDiviList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}