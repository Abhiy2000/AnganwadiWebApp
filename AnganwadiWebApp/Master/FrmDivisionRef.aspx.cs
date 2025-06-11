using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmDivisionRef : System.Web.UI.Page
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
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDivisionRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            Session["BrToList"] = "../Master/FrmDivisionList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Master" + " >> " + "Division Master";                 //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                   //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();              //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];
            if (UserLevel == "0")
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmOrgnisationList.aspx");
            }
            else if (UserLevel == "1")
            {
                Response.Redirect("../MstListPages/FrmDivisionList.aspx");
            }

            else if (UserLevel == "2")
            {
                Response.Redirect("../MstListPages/FrmDistrictList.aspx");
            }

            else if (UserLevel == "3")
            {
                Response.Redirect("../MstListPages/FrmCDPOList.aspx");
            }

            else if (UserLevel == "4")
            {
                Response.Redirect("../MstListPages/FrmBITList.aspx");
            }

            else if (UserLevel == "5")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}