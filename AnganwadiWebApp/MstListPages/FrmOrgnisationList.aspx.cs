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
    public partial class FrmOrgnisationList : System.Web.UI.Page
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
                String OrgList = " select BRID, COMPANYNAME, BRANCHNAME, HOID, PARENTID, BRCATEGORY, COMPCODE from companyview where parentid = " + Session["brid"] + " and brcategory in (0,1) ";
                DataTable TblOrgList = (DataTable)MstMethods.Query2DataTable.GetResult(OrgList);
                //AnganwadiLib.Methods.MstListPages.GetOrganisationList(TblOrgList, Session["TableName"].ToString(), Session["ColumnName"].ToString());
                GrdOrgList.DataSource = TblOrgList;
                GrdOrgList.DataBind();
                //if (TblOrgList.Rows.Count < 2)
                //{
                //    Session["GrdLevel"] = TblOrgList.Rows[0]["HOID"];
                //    Response.Redirect("../MstListPages/FrmDivisionList.aspx");
                //}
            }
        }

        protected void GrdOrgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GrdOrgList.SelectedRow;
            Session["GrdLevel"] = row.Cells[1].Text;
            //Session["LblGrdHead"] = LblGrdHead.Text + " >> " + row.Cells[2].Text;

            if (Session["MasterType"].ToString() == "O")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            else
            {
                Response.Redirect("../MstListPages/FrmDivisionList.aspx");
            }
        }

        protected void GrdOrgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}