using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagement
{
    public partial class FrmSessionLogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["@"].ToString() == "1")
            {
                PnlSessionExpire.Visible = false;
                PnlLogout.Visible = true;
            }

            if (Request.QueryString["@"].ToString() == "2")
            {
                PnlSessionExpire.Visible = true;
                PnlLogout.Visible = false;
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}