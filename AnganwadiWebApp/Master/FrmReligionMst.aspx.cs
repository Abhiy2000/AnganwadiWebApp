using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmReligionMst : System.Web.UI.Page
    {
        BoReligion objrel = new BoReligion();

        int Inmode = 0;

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
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Religion Master"; // Session["LblGrdHead"].ToString(); // 

                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    filldata();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRelName.Text.Trim() == "")
                {
                    MessageAlert(" Please enter Religion Name ", "");
                    txtRelName.Focus();
                    return;
                }
                objrel.UserId = Session["UserId"].ToString();
                objrel.ReligionId = Convert.ToInt32(Session["ReliId"]);
                objrel.ReligionName = txtRelName.Text.Trim();

                if (Request.QueryString["@"] == "1")
                {
                    objrel.Mode = 1;
                }
                else
                {
                    objrel.Mode = 2;
                }

                objrel.BoReligion_1();

                if (objrel.ErrorCode == -100)
                {
                    MessageAlert(objrel.ErrorMsg, "../Master/FrmReligionList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objrel.ErrorMsg, "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
        }

        protected void btnCancel_Cilck(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmReligionList.aspx");
            return;
        }

        public void filldata()
        {
            String query = " select num_religion_religid relid,var_religion_religname relname from aoup_religion_def ";
            query += " where num_religion_religid="+Convert.ToInt32 (Session["ReliId"])+"  order by num_religion_religid ";

            DataTable dtrelList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtrelList.Rows.Count > 0)
            {
                if (dtrelList.Rows[0]["relname"].ToString() != "" && dtrelList.Rows[0]["relname"].ToString() != null)
                {
                    txtRelName.Text = dtrelList.Rows[0]["relname"].ToString();
                }
            }
        }
    }
}