﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Admin
{
    public partial class FrmResetPwdRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            Session["BrToList"] = "../Admin/FrmResetPwdList.aspx";     //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Admin" + " >> " + "Reset Password";                  //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                               //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();              //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];
            if (UserLevel == "0")
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmOrgnisationList.aspx");
            }
            else
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}