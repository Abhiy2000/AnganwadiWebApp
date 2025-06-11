using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.Globalization;
using AnganwadiLib.Business;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmSevikaAuthMst : System.Web.UI.Page
    {
        BoSevikaAuth objSevikaAuth = new BoSevikaAuth();
        int days = 0;

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
            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";

            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }
            if (!IsPostBack)
            {
                Session["ResetGrd"] = Session["GrdLevel"];

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);
                grdSavikaList_set();
                disable();
                lblReason.Visible = false;
                Label2.Visible = false;
                ddlReason.Visible = false;
            }
        }

        protected void BtnNextTab1_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void BtnBackTab2_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);
        }

        protected void BtnNextTab2_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void BtnBackTab3_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void btnAuth_Click(object sender, EventArgs e)
        {
            if (validation() == 1)
            {
                objSevikaAuth.CompId = Convert.ToInt32(Session["GrdLevel"]);
                objSevikaAuth.SevikaCode = TxtSevikaCode.Text.Trim();
                objSevikaAuth.UserId = Session["UserId"].ToString();
                objSevikaAuth.BoSevikaAuth_1();
                if (objSevikaAuth.ErrorCode == -100)
                {
                    MessageAlert(objSevikaAuth.ErrorMsg, "../Transaction/FrmSevikaAuthList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objSevikaAuth.ErrorMsg, "");
                    return;
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Transaction/FrmSevikaAuthList.aspx");
        }

        protected void btnTextBoxEventHandler_Click(object sender, EventArgs e)
        {
            if (txtAadharNo.Text != "")
            {
                TxtSevikaCode.Text = ViewState["CDPO"].ToString() + txtAadharNo.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            }
            else
            {
                MessageAlert(" Please Enter Aadhar No. First ", "");
                txtAadharNo.Focus();
            }
        }

        private void grdSavikaList_set()
        {
            #region "Load Grid"

            String query = " SELECT a.num_sevikamaster_sevikaid,l.var_angnwadimst_angnname, a.var_sevikamaster_name,a.var_sevikamaster_sevikacode,  m.var_education_educname, ";
            query += " a.date_sevikamaster_birthdate,a.date_sevikamaster_retiredate, a.var_sevikamaster_address,a.num_sevikamaster_mobileno, a.var_sevikamaster_phoneno,var_bank_bankname, var_bankbranch_branchname, ";
            query += " var_bankbranch_ifsccode, ";
            query += " a.var_sevikamaster_panno, a.var_sevikamaster_aadharno,a.date_sevikamaster_joindate, a.var_sevikamaster_orderno, ";
            query += " a.date_sevikamaster_orderdate, n.var_designation_desig,p.var_payscal_payscal, a.num_sevikamaster_branchid, ";
            query += " a.var_sevikamaster_accno, a.var_sevikamaster_remark,a.var_sevikamaster_active, ";
            query += " a.var_sevikamaster_cpsmscode,h.var_religion_religname,i.var_cast_castname,a.var_sevikamaster_middlename, a.var_sevikamaster_village, ";
            query += " a.var_sevikamaster_pincode, j.var_maritstat_maritstat , a.num_sevikamaster_religid, a.num_sevikamaster_castid,a.num_sevikamaster_maritstatid, ";
            query += " k.var_sevikanominee_nom1name,q.var_relation_relation var_relation_relation1,k.var_sevikanominee_nom1age,k.var_sevikanominee_nom1address,";
            query += " k.var_sevikanominee_nom2name,r.var_relation_relation var_relation_relation2,k.var_sevikanominee_nom2age,k.var_sevikanominee_nom2address ";
            query += " ,s.var_experience_text,n.var_designation_flag,a.var_sevikamaster_reason ";
            query += " FROM aoup_sevikamaster_def a  left join aoup_bankbranch_def b on a.num_sevikamaster_branchid=b.num_bankbranch_branchid ";
            query += " left join aoup_bank_def g on b.num_bankbranch_bankid=g.num_bank_bankid ";
            query += " left join aoup_religion_def h on num_religion_religid=num_sevikamaster_religid  ";
            query += " left join aoup_cast_def i on num_cast_castid=num_sevikamaster_castid ";
            query += " left join aoup_maritstat_def j on  num_maritstat_maritstatid=num_sevikamaster_maritstatid ";
            query += " left join aoup_sevikanominee_def k on a.num_sevikamaster_sevikaid=k.num_sevikanominee_sevikaid ";
            query += " left join aoup_angnwadimst_def l on num_angnwadimst_compid=num_sevikamaster_compid and num_angnwadimst_angnid=num_sevakmaster_anganid ";
            query += " left join aoup_education_def m on m.num_education_educid=a.num_sevikamaster_educid ";
            query += " left join aoup_designation_def n on n.num_designation_desigid=num_sevikamaster_desigid ";
            query += " left join corpinfo o on bitid=num_sevikamaster_compid ";
            query += " left join aoup_payscal_def p on num_payscal_compid=stateid and num_payscal_payscalid=num_sevikamaster_payscalid ";
            query += " left join aoup_relation_def q on q.num_relation_relationid= k.var_sevikanominee_nom1relaid ";
            query += " left join aoup_relation_def r on r.num_relation_relationid=k.var_sevikanominee_nom2relaid ";
            query += " left join aoup_experience_def s on a.num_sevikamaster_experience=s.num_experience_id ";
            query += " where num_sevikamaster_compid='" + Session["GrdLevel"].ToString() + "' and num_sevikamaster_sevikaid='" + Session["SevikaId"] + "'";

            DataTable dtSavikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtSavikaList.Rows.Count > 0)
            {
                if (dtSavikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString() != null)
                {
                    txtSevikaId.Text = dtSavikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_angnwadimst_angnname"].ToString() != "" && dtSavikaList.Rows[0]["var_angnwadimst_angnname"].ToString() != null)
                {
                    txtAngan.Text = dtSavikaList.Rows[0]["var_angnwadimst_angnname"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_name"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_name"].ToString() != null)
                {
                    txtName.Text = dtSavikaList.Rows[0]["var_sevikamaster_name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_sevikacode"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_sevikacode"].ToString() != null)
                {
                    TxtSevikaCode.Text = dtSavikaList.Rows[0]["var_sevikamaster_sevikacode"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_education_educname"].ToString() != "" && dtSavikaList.Rows[0]["var_education_educname"].ToString() != null)
                {
                    txtEdu.Text = dtSavikaList.Rows[0]["var_education_educname"].ToString();
                }
                if (dtSavikaList.Rows[0]["date_sevikamaster_birthdate"].ToString() != "" && dtSavikaList.Rows[0]["date_sevikamaster_birthdate"].ToString() != null)
                {
                    //DateTime dt = DateTime.Parse(dtSavikaList.Rows[0]["date_sevikamaster_birthdate"].ToString());
                    //DtDob.Text = string.Format("{0:dd/MM/yyyy}", dt);
                    DtDob.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_birthdate"]).ToString("dd/MM/yyyy");
                }
                if (dtSavikaList.Rows[0]["date_sevikamaster_retiredate"].ToString() != "" && dtSavikaList.Rows[0]["date_sevikamaster_retiredate"].ToString() != null)
                {
                    txtRetireDt.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_retiredate"]).ToString("dd/MM/yyyy");
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_address"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_address"].ToString() != null)
                {
                    txtAddr.Text = dtSavikaList.Rows[0]["var_sevikamaster_address"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikamaster_mobileno"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_mobileno"].ToString() != null)
                {
                    TxtMobNo.Text = dtSavikaList.Rows[0]["num_sevikamaster_mobileno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_phoneno"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_phoneno"].ToString() != null)
                {
                    txtphone.Text = dtSavikaList.Rows[0]["var_sevikamaster_phoneno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_panno"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_panno"].ToString() != null)
                {
                    TxtPanNo.Text = dtSavikaList.Rows[0]["var_sevikamaster_panno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_aadharno"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_aadharno"].ToString() != null)
                {
                    txtAadharNo.Text = dtSavikaList.Rows[0]["var_sevikamaster_aadharno"].ToString();
                }
                if (dtSavikaList.Rows[0]["date_sevikamaster_joindate"].ToString() != "" && dtSavikaList.Rows[0]["date_sevikamaster_joindate"].ToString() != null)
                {
                    //DateTime dt = DateTime.Parse(dtSavikaList.Rows[0]["date_sevikamaster_joindate"].ToString());
                    //DtJoin.Text = string.Format("{0:dd/MM/yyyy}", dt);
                    DtJoin.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_joindate"]).ToString("dd/MM/yyyy");
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_orderno"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_orderno"].ToString() != null)
                {
                    TxtOrderNo.Text = dtSavikaList.Rows[0]["var_sevikamaster_orderno"].ToString();
                }
                if (dtSavikaList.Rows[0]["date_sevikamaster_orderdate"].ToString() != "" && dtSavikaList.Rows[0]["date_sevikamaster_orderdate"].ToString() != null)
                {
                    //DateTime dt = DateTime.Parse(dtSavikaList.Rows[0]["date_sevikamaster_orderdate"].ToString());
                    //DtOrder.Text = string.Format("{0:dd/MM/yyyy}", dt);
                    DtOrder.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_orderdate"]).ToString("dd/MM/yyyy");
                }
                if (dtSavikaList.Rows[0]["var_designation_desig"].ToString() != "" && dtSavikaList.Rows[0]["var_designation_desig"].ToString() != null)
                {
                    txtDesg.Text = dtSavikaList.Rows[0]["var_designation_desig"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_designation_flag"].ToString() != "" && dtSavikaList.Rows[0]["var_designation_flag"].ToString() != null)
                {
                    if (dtSavikaList.Rows[0]["var_designation_flag"].ToString() == "W")
                    {
                        txtDesgFlag.Text = "Worker";
                    }
                    if (dtSavikaList.Rows[0]["var_designation_flag"].ToString() == "H")
                    {
                        txtDesgFlag.Text = "Helper";
                    }
                    if (dtSavikaList.Rows[0]["var_designation_flag"].ToString() == "M")
                    {
                        txtDesgFlag.Text = "Mini-Anganwadi";
                    }
                }
                if (dtSavikaList.Rows[0]["var_payscal_payscal"].ToString() != "" && dtSavikaList.Rows[0]["var_payscal_payscal"].ToString() != null)
                {
                    txtPayScale.Text = dtSavikaList.Rows[0]["var_payscal_payscal"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_bank_bankname"].ToString() != "" && dtSavikaList.Rows[0]["var_bank_bankname"].ToString() != null)
                {
                    txtBank.Text = dtSavikaList.Rows[0]["var_bank_bankname"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_bankbranch_branchname"].ToString() != "" && dtSavikaList.Rows[0]["var_bankbranch_branchname"].ToString() != null)
                {
                    txtBranch.Text = dtSavikaList.Rows[0]["var_bankbranch_branchname"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_accno"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_accno"].ToString() != null)
                {
                    TxtAccNO.Text = dtSavikaList.Rows[0]["var_sevikamaster_accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_remark"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_remark"].ToString() != null)
                {
                    TxtRemark.Text = dtSavikaList.Rows[0]["var_sevikamaster_remark"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_active"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_active"].ToString() != null)
                {
                    rdbActive.SelectedValue = dtSavikaList.Rows[0]["var_sevikamaster_active"].ToString();
                    if (dtSavikaList.Rows[0]["var_sevikamaster_active"].ToString() == "N")
                    {
                        ddlReason.SelectedValue = dtSavikaList.Rows[0]["var_sevikamaster_reason"].ToString();
                        lblReason.Visible = true;
                        Label2.Visible = true;
                        ddlReason.Visible = true;
                    }
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_cpsmscode"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_cpsmscode"].ToString() != null)
                {
                    TxtCPSMSCode.Text = dtSavikaList.Rows[0]["var_sevikamaster_cpsmscode"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_religion_religname"].ToString() != "" && dtSavikaList.Rows[0]["var_religion_religname"].ToString() != null)
                {
                    txtReligion.Text = dtSavikaList.Rows[0]["var_religion_religname"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_cast_castname"].ToString() != "" && dtSavikaList.Rows[0]["var_cast_castname"].ToString() != null)
                {
                    txtCast.Text = dtSavikaList.Rows[0]["var_cast_castname"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_maritstat_maritstat"].ToString() != "" && dtSavikaList.Rows[0]["var_maritstat_maritstat"].ToString() != null)
                {
                    txtMaritStat.Text = dtSavikaList.Rows[0]["var_maritstat_maritstat"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_middlename"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_middlename"].ToString() != null)
                {
                    TxtMidName.Text = dtSavikaList.Rows[0]["var_sevikamaster_middlename"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_village"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_village"].ToString() != null)
                {
                    TxtVillage.Text = dtSavikaList.Rows[0]["var_sevikamaster_village"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_pincode"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_pincode"].ToString() != null)
                {
                    TxtPinCode.Text = dtSavikaList.Rows[0]["var_sevikamaster_pincode"].ToString();
                }

                //-------------added on 24-03-2018
                if (dtSavikaList.Rows[0]["var_experience_text"].ToString() != "" && dtSavikaList.Rows[0]["var_experience_text"].ToString() != null)
                {
                    txtExperience.Text = dtSavikaList.Rows[0]["var_experience_text"].ToString();
                }
                else
                {
                    txtExperience.Text = "";
                }
                //-------------

                if (dtSavikaList.Rows[0]["var_sevikanominee_nom1name"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom1name"].ToString() != null)
                {
                    txtNomNM1.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom1name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom1age"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom1age"].ToString() != null)
                {
                    txtNomAge1.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom1age"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom1address"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom1address"].ToString() != null)
                {
                    txtAddress1.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom1address"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_relation_relation1"].ToString() != "" && dtSavikaList.Rows[0]["var_relation_relation1"].ToString() != null)
                {
                    txtRel1.Text = dtSavikaList.Rows[0]["var_relation_relation1"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom2name"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom2name"].ToString() != null)
                {
                    txtNomNM2.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom2name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom2age"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom2age"].ToString() != null)
                {
                    txtNomAge2.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom2age"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom2address"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom2address"].ToString() != null)
                {
                    txtAddress2.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom2address"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_relation_relation2"].ToString() != "" && dtSavikaList.Rows[0]["var_relation_relation2"].ToString() != null)
                {
                    txtRel2.Text = dtSavikaList.Rows[0]["var_relation_relation2"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_bankbranch_ifsccode"].ToString() != "")
                {
                    txtIFSC.Text = dtSavikaList.Rows[0]["var_bankbranch_ifsccode"].ToString();
                }
            }
            #endregion
        }
        
        public int validation()
        {
            if (txtAngan.Text == "")
            {
                MessageAlert(" Anganwadi Name cannot be blank ", "");
                txtAngan.Focus();
                return 2;
            }
            if (txtName.Text == "")
            {
                MessageAlert(" Sevika Name cannot be blank ", "");
                txtName.Focus();
                return 2;
            }
            if (TxtMidName.Text == "")
            {
                MessageAlert(" Please Enter Middle Name ", "");
                TxtMidName.Focus();
                return 2;
            }
            if (TxtSevikaCode.Text == "")
            {
                MessageAlert(" Sevika Code cannot be blank ", "");
                TxtSevikaCode.Focus();
                return 2;
            }
            if (DtDob.Text == "")
            {
                MessageAlert(" Please Select Birth Date ", "");
                DtDob.Focus();
                return 2;
            }
            if (txtReligion.Text == "")
            {
                MessageAlert(" Please Enter Religion ", "");
                txtReligion.Focus();
                return 2;
            }
            if (txtEdu.Text== "")
            {
                MessageAlert(" Please Enter Qualification ", "");
                txtEdu.Focus();
                return 2;
            }
            if (txtMaritStat.Text == "")
            {
                MessageAlert(" Please Enter Martial Status ", "");
                txtMaritStat.Focus();
                return 2;
            }
            if (txtAadharNo.Text == "")
            {
                MessageAlert(" Please Enter Aadhar No. ", "");
                txtAadharNo.Focus();
                return 2;
            }
            if (txtAadharNo.Text != "")
            {
                if (txtAadharNo.Text.Length != 12)
                {
                    MessageAlert(" Please Enter Valid Addhar No. ", "");
                    txtAadharNo.Focus();
                    return 2;
                }
            }
            if (DtJoin.Text == "")
            {
                MessageAlert(" Please Select Join Date ", "");
                DtJoin.Focus();
                return 2;
            }
            if (txtRetireDt.Text == "")
            {
                MessageAlert(" Retirement Date not Set ", "");
                return 2;
            }
            if (txtDesg.Text == "")
            {
                MessageAlert(" Please Enter Designation ", "");
                txtDesg.Focus();
                return 2;
            }
            if (txtPayScale.Text == "")
            {
                MessageAlert(" Please Enter PayScale ", "");
                txtPayScale.Focus();
                return 2;
            }
            if (rdbActive.SelectedValue == "")
            {
                MessageAlert(" Please Select Active Type ", "");
                rdbActive.Focus();
                return 2;
            }
            else
            {
                return 1;
            }
        }

        private void disable()
        {
            txtSevikaId.Enabled = false;
            txtAngan.Enabled = false;
            txtName.Enabled = false;
            TxtMidName.Enabled = false;
            txtAddr.Enabled = false;
            TxtVillage.Enabled = false;
            TxtPinCode.Enabled = false;
            txtphone.Enabled = false;
            TxtMobNo.Enabled = false;
            txtReligion.Enabled = false;
            txtCast.Enabled = false;
            txtEdu.Enabled = false;
            txtMaritStat.Enabled = false;
            txtAadharNo.Enabled = false;
            TxtPanNo.Enabled = false;
            TxtSevikaCode.Enabled = false;
            TxtOrderNo.Enabled = false;
            txtRetireDt.Enabled = false;
            txtDesg.Enabled = false;
            txtPayScale.Enabled = false;
            txtBank.Enabled = false;
            txtIFSC.Enabled = false;
            txtBranch.Enabled = false;
            TxtAccNO.Enabled = false;
            TxtCPSMSCode.Enabled = false;
            TxtRemark.Enabled = false;
            rdbActive.Enabled = false;
            txtNomNM1.Enabled = false;
            txtRel1.Enabled = false;
            txtNomAge1.Enabled = false;
            txtAddress1.Enabled = false;
            txtNomNM2.Enabled = false;
            txtRel2.Enabled = false;
            txtNomAge2.Enabled = false;
            txtAddress2.Enabled = false;
            DtDob.DisableControl();
            //DtJoin.DisableControl();
            DtOrder.DisableControl();
        }
    }
}