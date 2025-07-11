﻿using AnganwadiLib.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Master
{
    public partial class FrmSevikaEditScrnRef : System.Web.UI.Page
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
            //../Transaction/FrmSevikaEdtListScrn.aspx
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmSevikaEditScrnRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            Session["BrToList"] = "../Transaction/FrmSevikaEdtListScrn.aspx";    //---- To Redirect sevika edit MainList ---//
            Session["LblGrdHead"] = "Master" + " >> " + "Sevika Edit Screen";                 //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                    //---- Master Type : Society Master or Branch Master ---//            
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
            else
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}