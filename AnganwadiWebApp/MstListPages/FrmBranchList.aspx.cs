using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProjectManagement.MstListPages
{
    public partial class FrmBranchList : System.Web.UI.Page
    {
        static string SessiongrdLevel = "";
        static string brvendor = "";

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

                String str = "select branchname,brid CompanyId, companyname, brcategory BrVendor ";
                str += "from companyview where compid = " + Session["GrdLevel"].ToString() + " ";
                str += " and brcategory not in (0) ";             
                str += "order by branchname";

                DataTable TblCompanyList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                AnganwadiLib.Methods.MstMethods.Dropdown.Fill(DdlbranchList, "", "", "", "", str);
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {

            if (DdlbranchList.SelectedValue == "" || DdlbranchList.SelectedValue == "0")
            {
                MessageAlert("Select Branch", "");
            }
            else
            {
                Session["LblGrdHead"] = LblGrdHead.Text + " >> " + DdlbranchList.SelectedItem;
                Session["GrdLevel"] = DdlbranchList.SelectedValue;
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}