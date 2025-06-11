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
    public partial class FrmCDPOList : System.Web.UI.Page
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
                String CDPOList = " select BRID, COMPANYNAME, BRANCHNAME, HOID, PARENTID, BRCATEGORY, COMPCODE from companyview where parentid = " + Session["GrdLevel"] + " and brcategory = 4 ";
                DataTable TblCDPOList = (DataTable)MstMethods.Query2DataTable.GetResult(CDPOList);   
                //AnganwadiLib.Methods.MstListPages.GetCDPOList(TblCDPOList, Session["TableName"].ToString(), Session["ColumnName"].ToString());
                GrdCDPOList.DataSource = TblCDPOList;
                GrdCDPOList.DataBind();
                //if (TblCDPOList.Rows.Count < 2)
                //{
                //    Session["GrdLevel"] = TblCDPOList.Rows[0]["HOID"];
                //    Response.Redirect("../MstListPages/FrmBITList.aspx");
                //}
            }
        }

        protected void GrdCDPOList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdCDPOList.SelectedRow;
            Session["GrdLevel"] = row.Cells[1].Text;
            Session["LblGrdHead"] = LblGrdHead.Text + " >> " + row.Cells[3].Text;

            if (Session["MasterType"].ToString() == "C")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            else
            {
                Response.Redirect("../MstListPages/FrmBITList.aspx");
            }
        }

        protected void GrdCDPOList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}