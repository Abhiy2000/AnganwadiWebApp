using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagement.WebUserControls
{
    public partial class Date : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDate.Attributes.Add("readonly", "readonly");
        }

        public String Text
        {
            get { return txtDate.Text; }
            set { txtDate.Text = value; }
        }

        public Nullable<DateTime> Value
        {
            get
            {
                if (txtDate.Text == "")
                {
                    return null;
                }
                else
                {
                    return Convert.ToDateTime(ConvertDate(txtDate.Text));
                }
            }
        }

        public static object ConvertDate(String strdt)
        {
            DateTime stdate = new DateTime();
            if (strdt.Length == 10)
            {
                String dt = (String)(strdt.Substring(3, 2) + "/" + strdt.Substring(0, 2) + "/" + strdt.Substring(6, 4));
                if (strdt.Substring(2, 1) == strdt.Substring(5, 1))
                {
                    if (strdt.Substring(5, 1) == "/" || strdt.Substring(5, 1) == "-" || strdt.Substring(5, 1) == ".")
                    {
                        stdate = Convert.ToDateTime(dt);
                    }
                }
            }
            return stdate;
        }

        public void DisableControl()
        {
            System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
            System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");

            txtDate.Enabled = false;
            txtDate.BackColor = backcolor;
            txtDate.ForeColor = forecolor;
            BtnDate.Enabled = false;
        }
        public void PostBackControl()
        {
            txtDate.AutoPostBack = true;

        }
        public void EnableControl()
        {
            System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f9f9f9");
            //System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");

            txtDate.Enabled = true;
            txtDate.BackColor = backcolor;
            //txtDate.ForeColor = forecolor;
            BtnDate.Enabled = true;
        }
    }
}