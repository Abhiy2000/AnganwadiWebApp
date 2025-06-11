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
    public partial class FrmCDPORef : System.Web.UI.Page
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
            String UserLevel = "";
            String CDPOLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmCDPORef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            Session["BrToList"] = "../Master/FrmCDPOMstList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Master" + " >> " + "CDPO Master";                 //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                   //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();              //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];            

            if (UserLevel == "0")   //Admin
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmOrgnisationList.aspx");
            }
            else if (UserLevel == "1")  //State
            {
                Response.Redirect(Session["BrToList"].ToString());
            }

            else if (UserLevel == "2")  //DIV
            {
                Response.Redirect(Session["BrToList"].ToString());
            }

            else if (UserLevel == "3")  //DIST
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            String str = "select brid from companyview where brcategory=" + UserLevel + " and brid=" + Session["GrdLevel"].ToString();
            DataTable dt = (DataTable)MstMethods.Query2DataTable.GetResult(str);
            if (dt.Rows.Count > 0)
            {
                CDPOLevel = dt.Rows[0]["brid"].ToString();
            }
            if (CDPOLevel == Session["brid"].ToString())
            {
                Session["MasterType"] = "C";
                Response.Redirect(Session["BrToList"].ToString());
            }

            else if (UserLevel == "4")  //CDPO
            {
                Response.Redirect("../MstListPages/FrmBITList.aspx");
            }

            else if (UserLevel == "5")  //BIT
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}