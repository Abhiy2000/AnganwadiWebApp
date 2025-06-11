using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp.HomePage
{
    public partial class FrmtablegraphRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Convert.ToString(Session["UserId"]) == "admin" || Convert.ToString(Session["UserId"]) == "ICDSAdmin" || Convert.ToString(Session["UserId"]) == "ICDS_HO_1" || Convert.ToString(Session["UserId"]) == "ICDS_HO_2")
                {
                    Response.Redirect("~/HomePage/tablegraph.aspx");
                }
                else
                {
                    Response.Redirect("~/HomePage/FrmDashboard.aspx");

                }
                


            }
        }
    }
}