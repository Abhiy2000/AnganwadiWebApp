using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmMaritStatMst : System.Web.UI.Page
    {
        BoMaritStat objstatus = new BoMaritStat();

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmMaritStatRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Marital Status Master"; //Session["LblGrdHead"].ToString();//

                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
                }
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
                if (txtStatus.Text.Trim() == "")
                {
                    MessageAlert(" Please enter Marital Status ", "");
                    txtStatus.Focus();
                    return;
                }
                objstatus.UserId = Session["UserId"].ToString();
                objstatus.MaritStatId = Convert.ToInt32(Session["MaritStatId"]);
                objstatus.MaritStatName = txtStatus.Text.Trim();

                if (Request.QueryString["@"] == "1")
                {
                    objstatus.Mode = 1;
                }
                else
                {
                    objstatus.Mode = 2;
                }

                objstatus.BoMaritStat_1();

                if (objstatus.ErrorCode == -100)
                {
                    MessageAlert(objstatus.ErrorMsg, "../Master/FrmMaritStatList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objstatus.ErrorMsg, "");
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
            Response.Redirect("../Master/FrmMaritStatList.aspx");
            return;
        }

        public void filldata()
        {
            String query = " select num_maritstat_maritstatid maritId,var_maritstat_maritstat status from aoup_maritstat_def ";
            query += " where num_maritstat_maritstatid=" + Convert.ToInt32(Session["MaritStatId"]) + " order by num_maritstat_maritstatid ";

            DataTable dtStatusList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtStatusList.Rows.Count > 0)
            {
                if (dtStatusList.Rows[0]["status"].ToString() != "" && dtStatusList.Rows[0]["status"].ToString() != null)
                {
                    txtStatus.Text = dtStatusList.Rows[0]["status"].ToString();
                }
            }
        }
    }
}