using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmTransferAuthRef : System.Web.UI.Page
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

            Session["BrToList"] = "../Transaction/FrmTransferAuthList.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Transaction" + " >> " + "Sevika Transfer Authorization";                            //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                        //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();                       //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];
            /*
            if (UserLevel == "0")
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmOrgnisationList.aspx");
            }*/
            if (UserLevel == "4")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            else
            {
                MessageAlert("Only CDPO Level user can Access this page", "");
            }
        }
    }
}