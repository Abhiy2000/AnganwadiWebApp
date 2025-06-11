using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Reports
{
    public partial class FrmActivityLogListRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }


            Session["BrToList"] = "../Reports/RptActLogListRpt.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Reports" + " >> " + "Activity Log List Report";                 //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                  //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();              //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];

            // Session["GrdLevel"] = 13;


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