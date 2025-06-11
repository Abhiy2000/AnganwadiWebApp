using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagement.Master
{
    public partial class FrmContractMasterRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");

            }

            Session["BrToList"] = "../Master/FrmContractorMaster.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Contractor Master";                 //---- Label Heading : Master Name ---//
            Session["MasterType"] = 2;                                   //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();              //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];
            if (UserLevel == "0")
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmCompanyListAdmin.aspx");
            }
            else
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}