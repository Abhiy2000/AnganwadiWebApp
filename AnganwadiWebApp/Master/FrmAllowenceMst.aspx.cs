using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Master
{
    public partial class FrmAllowenceMst : System.Web.UI.Page
    {
        BoAllowence objAllow = new BoAllowence();

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

        Int32 Inmode = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmAlloenceRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Allowence Master";
                //string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];

                //DataTable TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

                //if (TblGetHOid.Rows.Count > 0)
                //{
                MstMethods.Dropdown.Fill(ddlprjtype, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", " num_projecttype_compid ='" + Session["GrdLevel"] + "'", "");
                //}
                MstMethods.Dropdown.Fill(ddleducation, "aoup_education_def order by trim(var_education_educname)", "var_education_educname", "num_education_educid", "", "");
                //MstMethods.Dropdown.Fill(ddldesg, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "", "");

                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    ddlprjtype.Enabled = false;
                    //ddldesg.Enabled = false;
                    ddleducation.Enabled = false;
                    grdAllowenceList_set();
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlprjtype.SelectedValue == "")
            {
                MessageAlert(" Please Select Project Type ", "");
                ddlprjtype.Focus();
                return;
            }
            //if (ddldesg.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Designation ", "");
            //    ddldesg.Focus();
            //    return;
            //}
            if (ddleducation.SelectedValue == "")
            {
                MessageAlert(" Please Select Education ", "");
                ddleducation.Focus();
                return;
            }
            if (txtAllowName.Text == "")
            {
                MessageAlert(" Please Enter Allowence Name ", "");
                txtAllowName.Focus();
                return;
            }
            if (txtAmt.Text == "")
            {
                MessageAlert(" Please Enter Amount ", "");
                txtAmt.Focus();
                return;
            }

            objAllow.CompId = Convert.ToInt32(Session["GrdLevel"]);
            objAllow.UserId = Session["UserId"].ToString();
            objAllow.PrjId = Convert.ToInt32(ddlprjtype.SelectedValue);
            //objAllow.DesgId = Convert.ToInt32(ddldesg.SelectedValue);
            objAllow.DesgId = 0;
            objAllow.EduId = Convert.ToInt32(ddleducation.SelectedValue);
            objAllow.AllowName = txtAllowName.Text.Trim();
            objAllow.Amt = Convert.ToInt32(txtAmt.Text);
            if (Request.QueryString["@"] == "1")
            {
                objAllow.Mode = 1;
            }
            else
            {
                objAllow.Mode = 2;
            }
            objAllow.BoAllowence_1();

            if (objAllow.ErrorCode == -100)
            {
                MessageAlert(objAllow.ErrorMsg, "../Master/FrmAllowenceList.aspx");
                return;
            }
            else
            {
                MessageAlert(objAllow.ErrorMsg, "");
                return;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmAllowenceList.aspx");
        }

        public void grdAllowenceList_set()
        {
            String query = " select num_allowance_prjtypeid prjid,num_allowance_desigid desgid,num_allowance_educid eduid,var_projecttype_prjtype prjtype, ";
            query += " var_designation_desig desg,var_education_educname education,var_allowance_name allowname,num_allowance_amount amt ";
            query += " from aoup_allowance_def a inner join aoup_projecttype_def b on b.num_projecttype_compid=a.num_allowance_compid and ";
            query += " a.num_allowance_prjtypeid=b.num_projecttype_prjtypeid left join aoup_designation_def c on a.num_allowance_desigid=c.num_designation_desigid ";
            query += " left join aoup_education_def d on a.num_allowance_educid=d.num_education_educid where num_allowance_compid = " + Session["GrdLevel"];
            query += " and num_allowance_prjtypeid=" + Session["PrjId"] + " and num_allowance_educid= " + Session["EduId"];
            query += " order by var_allowance_name ";

            DataTable TblAllow = (DataTable)MstMethods.Query2DataTable.GetResult(query);

            if (TblAllow.Rows.Count > 0)
            {
                if (TblAllow.Rows[0]["prjid"].ToString() != "" && TblAllow.Rows[0]["prjid"].ToString() != null)
                {
                    ddlprjtype.SelectedValue = TblAllow.Rows[0]["prjid"].ToString();
                }
                //if (TblAllow.Rows[0]["desgid"].ToString() != "" && TblAllow.Rows[0]["desgid"].ToString() != null)
                //{
                //    ddldesg.SelectedValue = TblAllow.Rows[0]["desgid"].ToString();
                //}
                if (TblAllow.Rows[0]["eduid"].ToString() != "" && TblAllow.Rows[0]["eduid"].ToString() != null)
                {
                    ddleducation.SelectedValue = TblAllow.Rows[0]["eduid"].ToString();
                }
                if (TblAllow.Rows[0]["allowname"].ToString() != "" && TblAllow.Rows[0]["allowname"].ToString() != null)
                {
                    txtAllowName.Text = TblAllow.Rows[0]["allowname"].ToString();
                }
                if (TblAllow.Rows[0]["amt"].ToString() != "" && TblAllow.Rows[0]["amt"].ToString() != null)
                {
                    txtAmt.Text = TblAllow.Rows[0]["amt"].ToString();
                }
            }
        }
    }
}