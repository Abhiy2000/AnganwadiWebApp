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
    public partial class FrmCastMst : System.Web.UI.Page
    {
        BoCast objcast = new BoCast();

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmCastRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Category Master"; // Session["LblGrdHead"].ToString();//

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
                if (txtCastName.Text.Trim() == "")
                {
                    MessageAlert(" Please enter Category Name ", "");
                    txtCastName.Focus();
                    return;
                }
                int cast = 0;
                objcast.UserId = Session["UserId"].ToString();
                objcast.CastId = Convert.ToInt32(Session["CastId"]);
                objcast.CastName = txtCastName.Text.Trim();

                if (Request.QueryString["@"] == "1")
                {
                    objcast.Mode = 1;
                }
                else
                {
                    objcast.Mode = 2;
                }

                objcast.BoCast_1();

                if (objcast.ErrorCode == -100)
                {
                    MessageAlert(objcast.ErrorMsg, "../Master/FrmCastList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objcast.ErrorMsg, "");
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
            Response.Redirect("../Master/FrmCastList.aspx");
            return;
        }

        public void filldata()
        {
            String query = " select num_Cast_CastId castid,var_Cast_CastName castname ";
            query += " from aoup_Cast_Def where num_Cast_CastId='" + Session["CastId"] + "' order by num_Cast_CastId";

            DataTable dtCastList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtCastList.Rows.Count > 0)
            {
                if (dtCastList.Rows[0]["castname"].ToString() != "" && dtCastList.Rows[0]["castname"].ToString() != null)
                {
                    txtCastName.Text = dtCastList.Rows[0]["castname"].ToString();
                }
            }
        }
    }
}