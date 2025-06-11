using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnganwadiWebApp
{
    public partial class DefaultError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }
    }
}