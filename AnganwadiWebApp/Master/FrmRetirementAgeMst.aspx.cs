using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;
using System.Data;

namespace AnganwadiWebApp.Master
{
    public partial class FrmRetirementAgeMst : System.Web.UI.Page
    {
        BoRetirementAge objAge = new BoRetirementAge();

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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Retirement Age"; //
                MstMethods.Dropdown.Fill(ddlDesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "", "");

                if (Request.QueryString["@"] == "2")
                {
                    filldata();
                    ddlDesg.Enabled = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ddlDesg.SelectedValue == "")
                //{
                //    MessageAlert(" Please Select Designation ", "");
                //    return;
                //}
                if (txtRetireAge.Text == "")
                {
                    MessageAlert(" Please Enter Age ", "");
                    return;
                }
                if (dtAffectFrm.Text == "")
                {
                    MessageAlert(" Please Select date", "");
                    return;
                }
                objAge.AgeId = Convert.ToInt32(Session["AgeId"]);
                objAge.Age = Convert.ToInt32(txtRetireAge.Text);
                if (ddlDesg.SelectedItem.Text == "")
                {
                    objAge.Desg = 0;
                }
                else
                {
                    objAge.Desg = Convert.ToInt32(ddlDesg.SelectedValue);
                }
                objAge.AffectDate = Convert.ToDateTime(dtAffectFrm.Value);
                objAge.UserId = Session["UserId"].ToString();
                if (Request.QueryString["@"] == "1")
                {
                    objAge.Mode = 1;
                }
                else
                {
                    objAge.Mode = 2;
                }

                objAge.BoRetirementAge_1();

                if (objAge.ErrorCode == -100)
                {
                    MessageAlert(objAge.ErrorMsg, "../Master/FrmRetirementAgeList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objAge.ErrorMsg, "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }

        public void filldata()
        {
            String qry = " select num_retirementage_ageid ageid,num_retirementage_age age,num_retirementage_desgid desgid,var_designation_desig desg, ";
            qry += " date_retirementage_affdate AffDt from aoup_RetirementAge_def a left join aoup_designation_def b on a.num_retirementage_desgid=b.num_designation_desigid ";
            qry += " where num_retirementage_ageid=" + Convert.ToInt32(Session["AgeId"]);
            qry += " order by date_retirementage_affdate ";

            DataTable dtAgeList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(qry);

            if (dtAgeList.Rows.Count > 0)
            {
                if (dtAgeList.Rows[0]["desgid"].ToString() != "" && dtAgeList.Rows[0]["desgid"].ToString() != null)
                {
                    ddlDesg.SelectedValue = dtAgeList.Rows[0]["desgid"].ToString();
                }
                if (dtAgeList.Rows[0]["age"].ToString() != "" && dtAgeList.Rows[0]["age"].ToString() != null)
                {
                    txtRetireAge.Text = dtAgeList.Rows[0]["age"].ToString();
                }
                if (dtAgeList.Rows[0]["AffDt"].ToString() != "" && dtAgeList.Rows[0]["AffDt"].ToString() != null)
                {
                    dtAffectFrm.Text = Convert.ToDateTime(dtAgeList.Rows[0]["AffDt"]).ToString("dd/MM/yyyy");
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmRetirementAgeList.aspx");
        }
    }
}