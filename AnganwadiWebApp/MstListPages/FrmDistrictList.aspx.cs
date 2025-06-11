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
    public partial class FrmDistrictList : System.Web.UI.Page
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
                String DistList = " select BRID, COMPANYNAME, BRANCHNAME, HOID, PARENTID, BRCATEGORY, COMPCODE from companyview where parentid = " + Session["GrdLevel"] + " and brcategory = 3 ";
                DataTable TblDistList = (DataTable)MstMethods.Query2DataTable.GetResult(DistList);                
                //AnganwadiLib.Methods.MstListPages.GetDistrictList(TblDistList, Session["TableName"].ToString(), Session["ColumnName"].ToString());
                GrdDistList.DataSource = TblDistList;
                GrdDistList.DataBind();
                //if (TblDistList.Rows.Count < 2)
                //{
                //    Session["GrdLevel"] = TblDistList.Rows[0]["HOID"];
                //    Response.Redirect("../MstListPages/FrmCDPOList.aspx");
                //}
            }
        }

        protected void GrdDistList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdDistList.SelectedRow;
            Session["GrdLevel"] = row.Cells[1].Text;
            Session["LblGrdHead"] = LblGrdHead.Text + " >> " + row.Cells[3].Text;

            if (Session["MasterType"].ToString() == "D")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            else
            {
                Response.Redirect("../MstListPages/FrmCDPOList.aspx");
            }
        }

        protected void GrdDistList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}