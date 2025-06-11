using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmDemotionRef : System.Web.UI.Page
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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmDemotionRef.aspx");


            Session["BrToList"] = "../Master/FrmDemotionList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Master" + " >> " + "Demotion";                 //---- Label Heading : Master Name ---//
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

            else if (UserLevel == "4")  //CDPO
            {
                Response.Redirect(Session["BrToList"].ToString());
            }

            else if (UserLevel == "5")  //BIT
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}