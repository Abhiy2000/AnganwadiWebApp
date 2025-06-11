using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagement.User
{
    public partial class FrmUserRef : System.Web.UI.Page
    {
        #region "MeesageAlert"
        public void MessageAlert(String Message, String WindowsLocation)
        {
            String str = "";

            str = "alert('|| " + Message + " ||');";

            if (WindowsLocation != "")
            {
                str += "window.location = '" + WindowsLocation + "';";
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, str, true);
            return;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            Session["BrToList"] = "../User/FrmUserList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "User" + " >> " + "User Creation Master";                 //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                   //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();              //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];
            if (UserLevel == "0")
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmOrgnisationList.aspx");
            }
            //else if (UserLevel == "1")
            //{
            //    Response.Redirect("../MstListPages/FrmDivisionList.aspx");
            //}

            //else if (UserLevel == "2")
            //{
            //    Response.Redirect("../MstListPages/FrmDistrictList.aspx");
            //}

            //else if (UserLevel == "3")
            //{
            //    Response.Redirect("../MstListPages/FrmCDPOList.aspx");
            //}

            //else if (UserLevel == "4")
            //{
            //    Response.Redirect("../MstListPages/FrmBITList.aspx");
            //}

            else //if (UserLevel == "5")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
        }
    }
}