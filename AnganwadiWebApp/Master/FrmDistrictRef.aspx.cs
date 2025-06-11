using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmDistrictRef : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDistrictRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            Session["BrToList"] = "../Master/FrmDistrictList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Master" + " >> " + "District Master";                 //---- Label Heading : Master Name ---//
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

            else if (UserLevel == "2")  //Div
            {
                Response.Redirect(Session["BrToList"].ToString());
            }

            else if (UserLevel == "3")  //Dist
            {
                Response.Redirect("../MstListPages/FrmCDPOList.aspx");
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