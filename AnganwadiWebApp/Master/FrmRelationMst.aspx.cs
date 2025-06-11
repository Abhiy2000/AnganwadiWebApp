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
    public partial class FrmRelationMst : System.Web.UI.Page
    {
        BoRelation objrel = new BoRelation();

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
                LblGrdHead.Text = "Master" + " >> " + "Relation Master"; //Session["LblGrdHead"].ToString();// 

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
                    MessageAlert(" Please enter Relation Name ", "");
                    txtRelName.Focus();
                    return;
                }
                objrel.UserId = Session["UserId"].ToString();
                objrel.RelationId = Convert.ToInt32(Session["RelationId"]);
                objrel.RelationName = txtRelName.Text.Trim();

                if (Request.QueryString["@"] == "1")
                {
                    objrel.Mode = 1;
                }
                else
                {
                    objrel.Mode = 2;
                }

                objrel.BoRelation_1();

                if (objrel.ErrorCode == -100)
                {
                    MessageAlert(objrel.ErrorMsg, "../Master/FrmRelationList.aspx");
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
            Response.Redirect("../Master/FrmRelationList.aspx");
            return;
        }

        public void filldata()
        {
            String query = " select num_relation_relationid relationid,var_relation_relation relationNM from aoup_relation_def ";
            query += " where num_relation_relationid=" + Convert.ToInt32(Session["RelationId"]) + " order by num_relation_relationid ";

            DataTable dtRelationList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtRelationList.Rows.Count > 0)
            {
                if (dtRelationList.Rows[0]["relationNM"].ToString() != "" && dtRelationList.Rows[0]["relationNM"].ToString() != null)
                {
                    txtRelName.Text = dtRelationList.Rows[0]["relationNM"].ToString();
                }
            }
        }
    }
}