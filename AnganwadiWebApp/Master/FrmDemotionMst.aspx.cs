using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;
using System.Globalization;
namespace AnganwadiWebApp.Master
{
    public partial class FrmDemotionMst : System.Web.UI.Page
    {
        AnganwadiLib.Business.BoPromotion objPromotion = new AnganwadiLib.Business.BoPromotion();
        int Mode = 0;
        String Status = "A";
        DataTable TblGetHOid = new DataTable();
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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();


                String ddlEdu = "select var_education_educname, num_education_educid from aoup_education_def  order by trim(var_education_educname) "; // where num_education_educid=" + Convert.ToInt32(Session["EduId"]) + "
                MstMethods.Dropdown.Fill(ddlEduID, "", "", "", "", ddlEdu);

                DataTable Tbledu = (DataTable)MstMethods.Query2DataTable.GetResult(ddlEdu);

                if (Tbledu.Rows.Count > 0)
                {
                    ddlEduID.SelectedValue = Tbledu.Rows[0]["num_education_educid"].ToString();
                    txtQuali.Text = ddlEduID.SelectedItem.Text;
                }

                
                MstMethods.Dropdown.Fill(ddlExperience, "aoup_experience_def", "var_experience_text", "num_experience_id", " var_experience_status ='A' order by num_experience_id", "");


                if (Request.QueryString.HasKeys())
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["@"].ToString()))
                    {
                        if (Request.QueryString["@"].ToString() == "1")
                        {
                            EnableDisableConrols('D');
                            btnSearch.Enabled = true;
                            btnDelete.Visible = false;
                            btnDelete.Enabled = false;

                            Mode = 1;
                        }
                        else if (Request.QueryString["@"].ToString() == "2")
                        {
                            Mode = 2;
                            if (!String.IsNullOrEmpty(Session["AadharNo"].ToString()))
                            {
                                EnableDisableConrols('D');
                                btnSearch.Enabled = false;
                                FetchRecord(Session["AadharNo"].ToString());
                                btnSubmit.Text = "Demotion";


                                ddlFlag.SelectedValue = "H";
                                txtDesgin.Text = ddlFlag.SelectedItem.Text;
                                //txtQuali.Text = Session["EduName"].ToString();

                                ddlExperience_SelectedIndexChanged(null, null);
                                //ddlEduID_SelectedIndexChanged(null, null);
                                ddlFlag_SelectedIndexChanged(null, null);
                                ddldesigID_SelectedIndexChanged(null, null);

                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Master/FrmDemotionList.aspx");
                }
            }

        }

        public static Control GetPostBackControl(Page page)
        {
            Control control = null;
            string ctrlname = page.Request.Params.Get("__EVENTTARGET");
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = page.FindControl(ctrlname);

            }
            else
            {
                foreach (string ctl in page.Request.Form)
                {
                    Control c = page.FindControl(ctl);
                    if (c is System.Web.UI.WebControls.Button)
                    {
                        control = c;
                        break;
                    }
                }

            }
            return control;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAdharNo.Text != "")
            {
                FetchRecord(txtAdharNo.Text);
                //string str = " select a.num_addpay_compid,c.divname,c.distname,c.cdponame,c.bitbitname,s.var_sevikamaster_name savikaName,var_sevikamaster_cpsmscode schemeId";
                //str += " from  corpinfo c ";
                //str += " inner join aoup_sevikamaster_def s on bitid=num_sevikamaster_compid  ";
                //str += " inner join Aoup_AddPay_Def a on a.num_addpay_compid=s.num_sevikamaster_compid and a.num_addpay_sevikaid=s.num_sevikamaster_sevikaid ";
                //str += " where var_sevikamaster_aadharno='" + txtAdharNo.Text + "' ";
                //DataTable dtTbl2 = new DataTable();
                //dtTbl2 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                //if (dtTbl2.Rows.Count > 0)
                //{
                //    txtBitCompId.Text = dtTbl2.Rows[0]["num_addpay_compid"].ToString();
                //    txtCDPO.Text = dtTbl2.Rows[0]["cdponame"].ToString();
                //    txtDistrict.Text = dtTbl2.Rows[0]["distname"].ToString();
                //    txtDivision.Text = dtTbl2.Rows[0]["divname"].ToString();
                //    txtName.Text = dtTbl2.Rows[0]["savikaName"].ToString();
                //    txtSchemeSpecificId.Text = dtTbl2.Rows[0]["schemeId"].ToString();
                //}
                //else
                //{
                //    MessageAlert("No Record Found", "");
                //    return;
                //}
            }
            else
            {
                MessageAlert("please input Aadhar Number", "");
                txtAdharNo.Focus();
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CallProc();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }


        protected void FetchRecord(string Aadharno)
        {
            string str = " SELECT  c.divname, c.distname, c.cdponame, c.bitbitname, s.num_sevikamaster_compid, s.num_sevikamaster_sevikaid SevikaId,s.var_sevikamaster_name savikaName,date_sevikamaster_joindate joindate, ";
            str += " var_experience_text Exp,num_sevikamaster_desigid desigid,var_designation_desig desig,NUM_PAYSCAL_PAYSCALID PAYSCALID, var_payscal_payscal payscal,var_education_educname educname,var_designation_flag ";
            str += " FROM corpinfo c  INNER JOIN aoup_sevikamaster_def s ON bitid = num_sevikamaster_compid ";
            str += " inner join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
            str += " inner join aoup_payscal_def on num_payscal_payscalid=num_sevikamaster_payscalid ";
            str += " inner join aoup_education_def on num_education_educid=num_sevikamaster_educid ";
            str += " inner join aoup_experience_def on num_experience_id=num_sevikamaster_experience ";
            str += " WHERE  var_sevikamaster_aadharno = '" + Aadharno + "' and var_payscal_active='Y' ";

            DataTable dtTbl2 = new DataTable();
            dtTbl2 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);


            if (dtTbl2.Rows.Count > 0)
            {
                txtAdharNo.Text = Aadharno;
                txtBitCompId.Text = dtTbl2.Rows[0]["bitbitname"].ToString();
                txtCompId.Text = dtTbl2.Rows[0]["num_sevikamaster_compid"].ToString();
                txtCDPO.Text = dtTbl2.Rows[0]["cdponame"].ToString();
                txtDistrict.Text = dtTbl2.Rows[0]["distname"].ToString();
                txtDivision.Text = dtTbl2.Rows[0]["divname"].ToString();
                txtName.Text = dtTbl2.Rows[0]["savikaName"].ToString();

                txtExperience.Text = dtTbl2.Rows[0]["Exp"].ToString();
                txtDesignation.Text = dtTbl2.Rows[0]["desig"].ToString();
                txtDesigID.Text = dtTbl2.Rows[0]["desigid"].ToString();
                txtQualification.Text = dtTbl2.Rows[0]["educname"].ToString();
                txtPayscale.Text = dtTbl2.Rows[0]["payscal"].ToString();
                txtOldPayscale.Text = dtTbl2.Rows[0]["PAYSCALID"].ToString();
                ddlDesgFlag.SelectedValue = dtTbl2.Rows[0]["var_designation_flag"].ToString();
                txtJoinDt.Text = Convert.ToDateTime(dtTbl2.Rows[0]["joindate"]).ToString("dd/MM/yyyy");
                //if (Request.QueryString["@"].ToString() == "2")
                //{
                //    dtJoinDt.Text = Convert.ToDateTime(dtTbl2.Rows[0]["joindate"]).ToString("dd/MM/yyyy");
                //}

                hdnSavikaId.Value = dtTbl2.Rows[0]["SevikaId"].ToString();

            }
            else
            {
                MessageAlert("No Record Found", "");
                return;
            }
        }

        private void EnableDisableConrols(Char Input)
        {
            if (Input == 'D')
            {
                if (Request.QueryString["@"].ToString() == "1")
                {
                    txtAdharNo.Enabled = true;
                }
                else
                {
                    txtAdharNo.Enabled = false;
                }
                txtBitCompId.Enabled = false;
                txtCDPO.Enabled = false;
                txtDistrict.Enabled = false;
                txtDivision.Enabled = false;
                txtName.Enabled = false;
                txtExperience.Enabled = false;
                // txtDesignation.Enabled = false;
                txtQualification.Enabled = false;
                // txtPayscale.Enabled = false;
                // txtSchemeSpecificId.Enabled = false;

            }
            if (Input == 'E')
            {
                //if any control need to be enabled
            }
        }


        protected void dummybtn_Click(object sender, EventArgs e)
        {
            string dt = Request.Form[txtPromoteDate.UniqueID];
            txtPromoteDate.Text = dt;
            CalcExp();
            FillDesig();
            setPayScale();
            // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }


        public void CalcExp()
        {
            if (txtPromoteDate.Text != "")
            {
                DateTime dt = DateTime.ParseExact(txtPromoteDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int Exp = (System.DateTime.Now.Year - dt.Year);
                int month = (System.DateTime.Now.Month - dt.Month);

                int totalMonth = (Exp * 12) + month;

                MstMethods.Dropdown.Fill(ddlExperience, "aoup_experience_def", "var_experience_text", "num_experience_id", " var_experience_status ='A' order by num_experience_id", "");

                if (totalMonth <= 120)
                {
                    ddlExperience.SelectedValue = "1";
                    txtExp.Text = ddlExperience.SelectedItem.Text;
                }
                else if (totalMonth > 120 && totalMonth <= 240)
                {
                    ddlExperience.SelectedValue = "2";
                    txtExp.Text = ddlExperience.SelectedItem.Text;
                }
                else if (totalMonth > 240 && totalMonth <= 360)
                {
                    ddlExperience.SelectedValue = "3";
                    txtExp.Text = ddlExperience.SelectedItem.Text;
                }
                else
                {
                    ddlExperience.SelectedValue = "4";
                    txtExp.Text = ddlExperience.SelectedItem.Text;
                }
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            }
        }
        public void FillDesig()
        {
            try
            {
                if (ddlExperience.SelectedValue != "")
                {
                    if (ddlEduID.SelectedValue != "")
                    {
                        string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];
                        TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

                        String desg = " select var_designation_desig,num_designation_desigid from aoup_designation_def left join aoup_payscal_def on num_payscal_desigid=num_designation_desigid ";
                        desg += " left join aoup_experience_def on num_payscal_expfrom=num_experience_from and num_payscal_expto=num_experience_to ";
                        desg += " where num_payscal_compid=" + TblGetHOid.Rows[0]["hoid"].ToString() + " and num_payscal_educid= " + ddlEduID.SelectedValue;
                        desg += " and var_payscal_active='Y' "; //and num_experience_id= " + ddlExperience.SelectedValue + " and var_designation_flag='" + ddlFlag.SelectedValue + "'  

                        MstMethods.Dropdown.Fill(ddldesigID, "", "", "", "", desg);

                        DataTable Tbldesg = (DataTable)MstMethods.Query2DataTable.GetResult(desg);

                        if (Tbldesg.Rows.Count > 0)
                        {
                            ddldesigID.SelectedValue = Tbldesg.Rows[0]["num_designation_desigid"].ToString();
                            txtDesgID.Text = ddldesigID.SelectedItem.Text;
                        }
                    }
                    else
                    {
                        ddldesigID.SelectedValue = "";
                        txtDesgID.Text = "";
                    }
                }
                else
                {
                    ddldesigID.SelectedValue = "";
                    txtDesgID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
        }

        public void setPayScale()
        {
            string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];

            TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

            if (ddldesigID.SelectedValue != "")
            {
                if (ddlExperience.SelectedValue != "")
                {
                    if (ddlEduID.SelectedValue != "")
                    {
                        if (TblGetHOid.Rows.Count > 0)
                        {
                            //MstMethods.Dropdown.Fill(ddlPayscaleID, "aoup_payscal_def", "var_payscal_payscal", "num_payscal_payscalid", " num_payscal_compid ='" + TblGetHOid.Rows[0]["hoid"].ToString() + "' and num_payscal_desigid =" + ddldesigID.SelectedValue + " and num_payscal_educid=" + ddlEduID.SelectedValue + " and var_payscal_active='Y' order by trim(var_payscal_payscal)", "");

                            string Pay = "select var_payscal_payscal, num_payscal_payscalid from aoup_payscal_def ";
                            Pay += " inner join aoup_experience_def on num_experience_from= num_payscal_expfrom and num_experience_to=num_payscal_expto ";
                            Pay += " where  num_payscal_compid ='" + TblGetHOid.Rows[0]["hoid"].ToString() + "' and num_payscal_desigid =" + ddldesigID.SelectedValue + " and num_payscal_educid=" + ddlEduID.SelectedValue + " and var_payscal_active='Y' ";
                           // Pay += " and num_experience_id=" + ddlExperience.SelectedValue;
                            Pay += " order by trim(var_payscal_payscal) ";

                            if (Request.QueryString["@"].ToString() == "2")
                            {
                                MstMethods.Dropdown.FillddlPayScale(ddlPayscaleID, "", "", "", "", Pay);
                            }
                            else
                            {
                                MstMethods.Dropdown.Fill(ddlPayscaleID, "", "", "", "", Pay);
                            }

                            DataTable TblPay = (DataTable)MstMethods.Query2DataTable.GetResult(Pay);

                            if (TblPay.Rows.Count > 0)
                            {
                                ddlPayscaleID.SelectedValue = TblPay.Rows[0]["num_payscal_payscalid"].ToString();
                            }
                        }
                    }
                    else
                    {
                        ddlPayscaleID.SelectedValue = "";
                    }
                }
                else
                {
                    ddlPayscaleID.SelectedValue = "";
                }
            }
            else
            {
                ddlPayscaleID.SelectedValue = "";
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }


        protected void ddlExperience_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDesig();
            setPayScale();
        }

        protected void ddlEduID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDesig();
            setPayScale();
        }

        protected void ddlFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlag.SelectedValue != "")
            {
                //MstMethods.Dropdown.Fill(ddldesigID, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag='" + ddlFlag.SelectedValue + "'", "");
                FillDesig();
                setPayScale();
            }
            // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void ddldesigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPayScale();
        }

        public void CallProc()
        {

            objPromotion.COMPID = Convert.ToInt32(txtCompId.Text.Trim());
            objPromotion.SEVIKAID = Convert.ToInt32(hdnSavikaId.Value);

            if (txtPromoteDate.Text.Trim() != "")
            {
                objPromotion.PromoteDT = DateTime.ParseExact(txtPromoteDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                objPromotion.PromoteDT = DateTime.MinValue;
            }

            if (Request.QueryString["@"].ToString() == "2")
            {
                objPromotion.Mode = 2;
            }
            else if (Request.QueryString["@"].ToString() == "1")
            {
                objPromotion.Mode = 1;
            }

            //else if (sender.ToString() == "delete")
            //{
            //    objPromotion.Mode = 3;
            //}

            objPromotion.OldQualification = txtQualification.Text;
            objPromotion.NewQualification = ddlEduID.SelectedValue;
            objPromotion.OldDesFlag = ddlDesgFlag.SelectedValue;
            objPromotion.NewDesFlag = ddlFlag.SelectedValue;
            objPromotion.OldDesID = txtDesigID.Text;
            objPromotion.NewDesID = ddldesigID.SelectedValue;
            objPromotion.OldPayscale = txtPayscale.Text; //txtOldPayscale
            objPromotion.NewPayscale = ddlPayscaleID.SelectedValue;
            objPromotion.UserName = Session["UserId"].ToString();

            objPromotion.OldExp = txtExperience.Text;
            objPromotion.NewExpID = Convert.ToInt32(ddlExperience.SelectedValue);

            objPromotion.InsertDemotion();

            if (objPromotion.ErrorCode == -100)
            {
                MessageAlert(objPromotion.ErrorMessage, "../Master/FrmDemotionList.aspx");
                return;
            }
            else
            {
                MessageAlert(objPromotion.ErrorCode + objPromotion.ErrorMessage, "");
                return;
            }


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // btnSubmit_Click("delete", null);
        }




    }
}