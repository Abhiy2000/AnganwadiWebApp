using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmSevikaTransferRef : System.Web.UI.Page
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

            Session["BrToList"] = "../Transaction/FrmSevikaTransfer.aspx";    //---- To Redirect Master MainList ---//
            Session["LblGrdHead"] = "Transaction" + " >> " + "Transfer of Sevika";                            //---- Label Heading : Master Name ---//
            Session["MasterType"] = "O";                                        //---- Master Type : Society Master or Branch Master ---//
            UserLevel = Session["brcategory"].ToString();                       //---- Branch Level : to redirect as per branch level ---//

            Session["GrdLevel"] = Session["brid"];
            /*
            if (UserLevel == "0")
            {
                Session["GrdLevel"] = "0";
                Response.Redirect("../MstListPages/FrmOrgnisationList.aspx");
            }*/
            if (UserLevel == "5")
            {
                Response.Redirect(Session["BrToList"].ToString());
            }
            else
            {
                MessageAlert("Only BIT Level user can Access this Page", "");
                return;
            }
        }
    }
}