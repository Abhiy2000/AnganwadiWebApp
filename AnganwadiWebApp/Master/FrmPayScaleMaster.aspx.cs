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
    public partial class FrmPayScaleMaster : System.Web.UI.Page
    {
        PayScaleBrConfig objPayScaleBrConfig = new AnganwadiLib.Business.PayScaleBrConfig();

        Int32 Inmode = 1;
        Int32 PayId = 0;
        Double CenterAmt = 0, StateAmt = 0;

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
                dtFrom.PostBackControl();
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "PayScale Master";
                txtPayCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                txtPayCode.BackColor = backcolor;
                txtPayCode.ForeColor = forecolor;

                string HoId = "select hoid from companyview where brid=" + Session["GrdLevel"];
                DataTable TblGetHoId = (DataTable)MstMethods.Query2DataTable.GetResult(HoId);
                if (TblGetHoId.Rows.Count > 0)
                {
                    MstMethods.Dropdown.Fill(ddlProjname, "aoup_projecttype_def", "var_projecttype_prjtype", "num_projecttype_prjtypeid", "num_projecttype_compid=" + TblGetHoId.Rows[0]["hoid"].ToString() + " ORDER BY trim(var_projecttype_prjtype) ", "");
                }
                MstMethods.Dropdown.Fill(ddlEduName, "aoup_Education_def order by trim(var_education_educname) ", "var_education_educname", "num_education_educid ", "", "");
                MstMethods.Dropdown.Fill(ddlDesg, "aoup_designation_def order by trim(var_designation_desig) ", "var_designation_desig", "num_designation_desigid ", "", "");
                
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    PayId = 0;
                    ddlProjname.Visible = true;
                    ddlProjname.Enabled = true;
                    ddlEduName.Visible = true;
                    ddlEduName.Enabled = true;

                    dtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    grdPayList_set();
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPayscale.Text == "")
            {
                MessageAlert("PayScale cannot be blank", "");
                return;
            }

            Int32 Total = 0;
            Total = Convert.ToInt32(txtWages.Text) + Convert.ToInt32(txtFixed.Text);
            if (Convert.ToInt32(txtPayscale.Text) != Total)
            {
                MessageAlert("Wages and Fixed amount do not match with Payscale", "");
                return;
            }

            if (rbdActive.Checked == true)
            {
                CallProc();
            }
            else
            {
                popMsg1.Show();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmPayScaleList.aspx");
        }

        protected void btnTemp_Click(object sender, EventArgs e)
        {
            if (txtWages.Text != "" && txtWages.Text != "0")
            {
                CenterAmt = Convert.ToDouble(txtWages.Text) / 100 * (Convert.ToDouble(txtCentral.Text));
                txtState.Text = (100 - Convert.ToDouble(txtCentral.Text)).ToString();
                StateAmt = Convert.ToDouble(txtWages.Text) - CenterAmt;
                txtCentralAmt.Text = (Convert.ToInt32(CenterAmt)).ToString();
                txtStateAmt.Text = (Convert.ToInt32(StateAmt)).ToString();
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            CallProc();
        }

        private void grdPayList_set()
        {
            #region "Load Grid"

            String query = "select a.num_payscal_payscalid PayId,a.num_payscal_prjtypeid projid, a.num_payscal_educid eduid,a.var_payscal_payscal payscale,a.num_payscal_wages wages,";
            query += "  a.num_payscal_central central,a.num_payscal_state state,a.num_payscal_fixed fixed,a.var_payscal_active active,";
            query += " num_payscal_desigid desgid,num_payscal_expfrom expfrm,num_payscal_expto expto,date_payscal_effectivefrmdt effectivefrmdt ";
            query += " from aoup_payscal_def a left outer join aoup_Education_def b  on a.num_payscal_educid= b.num_education_educid ";
            query += " left outer join aoup_ProjectType_def c on a.num_payscal_prjtypeid = c.num_projecttype_prjtypeid ";
            query += " left outer join aoup_designation_def d on a.num_payscal_desigid=d.num_designation_desigid ";
            query += " where num_payscal_payscalid='" + Session["PayId"] + "' order by num_payscal_payscalid";

            DataTable TblEduId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (TblEduId.Rows.Count > 0)
            {
                if (TblEduId.Rows[0]["PayId"].ToString() != "" && TblEduId.Rows[0]["PayId"].ToString() != null)
                {
                    txtPayCode.Text = TblEduId.Rows[0]["PayId"].ToString();
                }
                if (TblEduId.Rows[0]["projid"].ToString() != "" && TblEduId.Rows[0]["projid"].ToString() != null)
                {
                    ddlProjname.SelectedValue = TblEduId.Rows[0]["projid"].ToString();
                }
                if (TblEduId.Rows[0]["eduid"].ToString() != "" && TblEduId.Rows[0]["eduid"].ToString() != null)
                {
                    ddlEduName.SelectedValue = TblEduId.Rows[0]["eduid"].ToString();
                }
                if (TblEduId.Rows[0]["payscale"].ToString() != "" && TblEduId.Rows[0]["payscale"].ToString() != null)
                {
                    txtPayscale.Text = TblEduId.Rows[0]["payscale"].ToString();
                }
                if (TblEduId.Rows[0]["wages"].ToString() != "" && TblEduId.Rows[0]["wages"].ToString() != null)
                {
                    txtWages.Text = TblEduId.Rows[0]["wages"].ToString();
                }
                if (TblEduId.Rows[0]["central"].ToString() != "" && TblEduId.Rows[0]["central"].ToString() != null)
                {
                    txtCentral.Text = TblEduId.Rows[0]["central"].ToString();
                }
                if (TblEduId.Rows[0]["state"].ToString() != "" && TblEduId.Rows[0]["state"].ToString() != null)
                {
                    txtState.Text = TblEduId.Rows[0]["state"].ToString();
                }
                if (TblEduId.Rows[0]["wages"].ToString() != "" && TblEduId.Rows[0]["wages"].ToString() != "0")
                {
                    CenterAmt = Convert.ToDouble(txtWages.Text) / 100 * (Convert.ToDouble(txtCentral.Text));
                    StateAmt = Convert.ToDouble(txtWages.Text) - CenterAmt;
                    txtCentralAmt.Text = (Convert.ToInt32(CenterAmt)).ToString();
                    txtStateAmt.Text = (Convert.ToInt32(StateAmt)).ToString();
                }
                if (TblEduId.Rows[0]["fixed"].ToString() != "" && TblEduId.Rows[0]["fixed"].ToString() != null)
                {
                    txtFixed.Text = TblEduId.Rows[0]["fixed"].ToString();
                }
                if (TblEduId.Rows[0]["active"].ToString() != "" && TblEduId.Rows[0]["active"].ToString() != null)
                {
                    if (TblEduId.Rows[0]["active"].ToString() == "Y")
                    {
                        rbdActive.Checked = true;
                    }
                    else
                    {
                        rbdInactive.Checked = true;
                    }
                }
                if (TblEduId.Rows[0]["desgid"].ToString() != "" && TblEduId.Rows[0]["desgid"].ToString() != null)
                {
                    ddlDesg.SelectedValue = TblEduId.Rows[0]["desgid"].ToString();
                }
                if (TblEduId.Rows[0]["expfrm"].ToString() != "" && TblEduId.Rows[0]["expfrm"].ToString() != null)
                {
                    txtExpFrm.Text = TblEduId.Rows[0]["expfrm"].ToString();
                }
                if (TblEduId.Rows[0]["expto"].ToString() != "" && TblEduId.Rows[0]["expto"].ToString() != null)
                {
                    txtExpTo.Text = TblEduId.Rows[0]["expto"].ToString();
                }
                if (TblEduId.Rows[0]["effectivefrmdt"].ToString() != "" && TblEduId.Rows[0]["effectivefrmdt"].ToString() != null)
                {
                    dtFrom.Text = Convert.ToDateTime(TblEduId.Rows[0]["effectivefrmdt"]).ToString("dd/MM/yyyy");
                }
                
            }
            #endregion
        }

        public void CallProc()
        {
            objPayScaleBrConfig.BrId = Convert.ToInt32(Session["GrdLevel"]);
            objPayScaleBrConfig.UserId = Session["UserId"].ToString();
            objPayScaleBrConfig.ProjId = Convert.ToInt32(ddlProjname.SelectedValue);
            objPayScaleBrConfig.EduId = Convert.ToInt32(ddlEduName.SelectedValue);
            objPayScaleBrConfig.Pay = txtPayscale.Text.Trim();
            objPayScaleBrConfig.Wages = Convert.ToInt32(txtWages.Text.Trim());
            objPayScaleBrConfig.Central = Convert.ToInt32(txtCentral.Text.Trim());
            objPayScaleBrConfig.State = Convert.ToInt32(txtState.Text.Trim());
            objPayScaleBrConfig.fixed1 = Convert.ToInt32(txtFixed.Text.Trim());

            objPayScaleBrConfig.Desigid = Convert.ToInt32(ddlDesg.SelectedValue);
            objPayScaleBrConfig.Expfrom = Convert.ToInt32(txtExpFrm.Text.Trim());
            objPayScaleBrConfig.Expto = Convert.ToInt32(txtExpTo.Text.Trim());

            if (rbdInactive.Checked == true)
            {
                objPayScaleBrConfig.Active = "N";
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>alert(' You are saving PayScale in InActive Mode ');</script>", false);
            }
            else
            {
                objPayScaleBrConfig.Active = "Y";
            }

            objPayScaleBrConfig.EffectiveFrmDt = Convert.ToDateTime(dtFrom.Value);

            if (Request.QueryString["@"] == "1")
            {
                objPayScaleBrConfig.Mode = 1;
                objPayScaleBrConfig.PayId = PayId;
            }
            else
            {
                objPayScaleBrConfig.Mode = 2;
                objPayScaleBrConfig.PayId = Convert.ToInt32(txtPayCode.Text.Trim());
            }
            objPayScaleBrConfig.Insert();

            if (objPayScaleBrConfig.ErrCode == -100)
            {
                MessageAlert(objPayScaleBrConfig.ErrMsg, "../Master/FrmPayScaleList.aspx");
                return;
            }
            else
            {
                MessageAlert(objPayScaleBrConfig.ErrMsg, "");
                return;
            }
        }
    }
}