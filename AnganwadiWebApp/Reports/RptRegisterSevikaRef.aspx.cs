using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Reports
{
    public partial class RptRegisterSevikaRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            Session["BrToList"] = "../Reports/RptRegisterSevika.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Reports" + " >> " + "Un-Registerd Sevika Report";                 //---- Label Heading : Master Name ---//
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