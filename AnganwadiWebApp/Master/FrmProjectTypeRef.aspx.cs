using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Master
{
    public partial class FrmProjectTypeRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            Session["BrToList"] = "../Master/FrmProjectTypeList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Master" + " >> " + "ProjectType Master";                 //---- Label Heading : Master Name ---//
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
        }
    }
}