using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using System.Web.Services;
using System.Globalization;
using AnganwadiLib.Business;
using System.Drawing;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmSevikaMaster : System.Web.UI.Page
    {
        Cls_Business_SavikaMast objSavikaMast = new Cls_Business_SavikaMast();
        int days = 0;
        DateTime doj;
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
            String UserLevel = "";
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
            }

            if (!IsPostBack)
            {
                txtAadharNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnTextBoxEventHandler, ""));
                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";     
                ddlReason.Visible = false;
                lblReason.Visible = false;
                Label2.Visible = false;
                lblExitDate.Visible = false;
                Label4.Visible = false;
                DtExitDT.Visible = false;

                string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];

                TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

                bindcmb();

                String cdpo = " select cdpocode from corpinfo where bitid=" + Session["GrdLevel"];

                DataTable Tblcdpo = (DataTable)MstMethods.Query2DataTable.GetResult(cdpo);
                if (Tblcdpo.Rows.Count > 0)
                {
                    ViewState["CDPO"] = Tblcdpo.Rows[0]["cdpocode"].ToString();
                }

                MstMethods.Dropdown.Fill(ddlExperience, "aoup_experience_def", "var_experience_text", "num_experience_id", " var_experience_status ='A' order by num_experience_id", "");
                if (Request.QueryString["@"] == "1")
                {
                    Session["Mode"] = 1;
                    ddlAnganID.Focus();
                    MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
                    if (TblGetHOid.Rows.Count > 0)
                    {
                        MstMethods.Dropdown.Fill(ddlPayscaleID, "aoup_payscal_def", "var_payscal_payscal", "num_payscal_payscalid", " num_payscal_compid ='" + TblGetHOid.Rows[0]["hoid"].ToString() + "' and var_payscal_active='Y' order by trim(var_payscal_payscal)", "");
                    }

                    //String cdpo = " select cdpocode from corpinfo where bitid=" + Session["GrdLevel"];

                    //DataTable Tblcdpo = (DataTable)MstMethods.Query2DataTable.GetResult(cdpo);
                    //if (Tblcdpo.Rows.Count > 0)
                    //{
                    //    ViewState["CDPO"] = Tblcdpo.Rows[0]["cdpocode"].ToString();
                    //}
                    txtAadharNo.Enabled = true;
                    TxtCPSMSCode.Enabled = false;
                    BtnDate.Visible = true;
                }
                else
                {
                    Session["Mode"] = 2;
                    MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
                    if (TblGetHOid.Rows.Count > 0)
                    {
                        MstMethods.Dropdown.Fill(ddlPayscaleID, "aoup_payscal_def", "var_payscal_payscal", "num_payscal_payscalid", " num_payscal_compid ='" + TblGetHOid.Rows[0]["hoid"].ToString() + "'  order by trim(var_payscal_payscal)", "");
                    }

                    ddlAnganID.Focus();
                    grdSavikaList_set();

                    UserLevel = Session["brcategory"].ToString();
                    if (UserLevel == "0" || UserLevel == "11" || UserLevel == "1")
                    {
                        txtAadharNo.Enabled = true;
                        TxtCPSMSCode.Enabled = true;
                        txtJoinDate.Enabled = true;
                        //BtnDate.Visible = false;
                        //DtDob.DisableControl();
                    }
                    else
                    {
                        txtAadharNo.Enabled = false;
                        TxtCPSMSCode.Enabled = false;
                        txtJoinDate.Enabled = false;
                        BtnDate.Visible = false;
                        DtDob.DisableControl();
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);

                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (rdbActive.SelectedItem.Selected == true)
            {
                if (rdbActive.SelectedItem.Value == "N")
                {
                    popMsg1.Show();
                }
                else
                {
                    CallProc();
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Transaction/FrmSevikaMasterList.aspx");
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            String qry = "select var_bankbranch_branchname || '    ' || var_bankbranch_ifsccode,num_bankbranch_branchid from aoup_bankbranch_def ";
            qry += " where num_bankbranch_bankid=" + ddlBank.SelectedValue;
            MstMethods.Dropdown.Fill(ddlBranch, "", "", "", "", qry);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void btnTextBoxEventHandler_Click(object sender, EventArgs e)
        {
            if (txtAadharNo.Text != "")
            {
                if (txtAadharNo.Text.Length != 12)
                {
                    MessageAlert("Aadhar no is Invalid", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                    return;
                }

                Aadhar_Chk();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            }
            else
            {
                MessageAlert(" Please Enter Aadhar No. First ", "");
                txtAadharNo.Focus();
            }
        }

        protected void lnkSetRetireDate_Click(object sender, EventArgs e)
        {
            DateTime dt = System.DateTime.Now;
            if (DtDob.Text != "")
            {
                dt = Convert.ToDateTime(DtDob.Value).AddYears(65);

                DateTime Days = Convert.ToDateTime(dt);
                int month = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("MM")));
                String month1 = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Days.Month);
                int year = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("yyyy")));
                days = DateTime.DaysInMonth(year, month);
                String lstdate = Convert.ToString(days + "-" + month1 + "-" + year);

                txtRetireDt.Text = lstdate;
            }
            else
            {
                MessageAlert(" Please Select Birth Date First ", "");
                DtDob.Focus();
            }
        }

        protected void LinkSerchBankBranch_OnClick(object sender, EventArgs e)
        {
            if (ddlBank.SelectedValue.ToString() == "" || ddlBank.SelectedValue.ToString() == "0")
            {
                MessageAlert("Select Bank first from the list", "");
                ddlBank.Focus();
                return;
            }

            if (txtIFSC.Text == "")
            {
                MessageAlert("IFSC code can not be blank", "");
                txtIFSC.Focus();
                return;
            }

            String str = "select var_bankbranch_branchname branchname,num_bankbranch_branchid branchid from aoup_bankbranch_def where num_bankbranch_bankid = " + ddlBank.SelectedValue.ToString() + " and ";
            str += "UPPER(var_bankbranch_ifsccode) = UPPER('" + txtIFSC.Text + "')";

            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblResult.Rows.Count > 0)
            {
                ddlBranch.SelectedValue = TblResult.Rows[0]["branchid"].ToString();
                txtBankBranch.Text = TblResult.Rows[0]["branchname"].ToString();
            }

            else
            {
                ddlBranch.DataSource = "";
                ddlBranch.DataBind();
                txtBankBranch.Text = "";

                MessageAlert("Bank branch not found", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                txtIFSC.Focus();
                return;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void BtnNextTab1_OnClick(object sender, EventArgs e)
        {
            if (ddlAnganID.SelectedValue == "")
            {
                MessageAlert(" Please Select Anganwadi", "");
                return;
            }
            if (txtName.Text == "")
            {
                MessageAlert(" Sevika Name cannot be blank ", "");
                txtName.Focus();
                return;
            }
            if (TxtMidName.Text == "")
            {
                MessageAlert(" Please Enter Middle Name ", "");
                TxtMidName.Focus();
                return;
            }
            if (DtDob.Text == "")
            {
                MessageAlert(" Please Select Birth Date ", "");
                DtDob.Focus();
                return;
            }
            if (DtDob.Text != "")
            {
                if (Convert.ToDateTime(DtDob.Value) > DateTime.Now)
                {
                    MessageAlert("Please enter Date of Birth Properly.", "");
                    DtDob.Focus();
                    return;
                }
            }
            if (ddlReligion.SelectedValue == "")
            {
                MessageAlert(" Please Select Religion ", "");
                ddlReligion.Focus();
                return;
            }
            if (ddlmaritstat.SelectedValue == "")
            {
                MessageAlert(" Please Select Martial Status ", "");
                ddlmaritstat.Focus();
                return;
            }
            if (TxtPinCode.Text != "")
            {
                if (TxtPinCode.Text.Length != 6)
                {
                    MessageAlert(" PIN Code is Invalid ", "");
                    TxtPinCode.Focus();
                    return;
                }
            }
            if (TxtMobNo.Text != "")
            {
                if (TxtMobNo.Text.Length != 10)
                {
                    MessageAlert(" Mobile No should be 10 digit ", "");
                    TxtMobNo.Focus();
                    return;
                }
            }
            if (txtphone.Text != "")
            {
                if (txtphone.Text.Length != 10)
                {
                    MessageAlert(" Phone No should be 10 digit ", "");
                    txtphone.Focus();
                    return;
                }
            }
            if (rdbActive.SelectedValue != "")
            {
                if (rdbActive.SelectedValue == "N")
                {
                    if (ddlReason.SelectedIndex == 0)
                    {
                        MessageAlert("Please select Reason", "");
                        return;
                    }
                }
            }
            RetirementDate();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void BtnBackTab2_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);
        }

        protected void BtnNextTab2_OnClick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (txtJoinDate.Text != "")
            {
                int Exp = (System.DateTime.Now.Year - dt.Year);
                int month = (System.DateTime.Now.Month - dt.Month);

                int totalMonth = (Exp * 12) + month;

                MstMethods.Dropdown.Fill(ddlExperience, "aoup_experience_def", "var_experience_text", "num_experience_id", " var_experience_status ='A' order by num_experience_id", "");

                if (totalMonth <= 120)
                {
                    ddlExperience.SelectedValue = "1";
                }
                else if (totalMonth > 120 && totalMonth <= 240)
                {
                    ddlExperience.SelectedValue = "2";
                }
                else if (totalMonth > 240 && totalMonth <= 360)
                {
                    ddlExperience.SelectedValue = "3";
                }
                else
                {
                    ddlExperience.SelectedValue = "4";
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            }

            if (txtAadharNo.Text == "")
            {
                MessageAlert(" Please Enter Aadhar No. ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (TxtSevikaCode.Text == "")
            {
                MessageAlert(" Scheme Specific Code cannot be blank ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (txtAadharNo.Text != "")
            {
                if (txtAadharNo.Text.Length != 12)
                {
                    MessageAlert(" Please Enter Valid Addhar No. ", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                    return;
                }
                Aadhar_Chk();
            }
            if (Session["RejoinFlag"].ToString() == "Y")
            {
                String str = " select num_sevikamaster_sevikaid from aoup_sevikamaster_def where var_sevikamaster_aadharno='" + txtAadharNo.Text.ToString() + "' AND  var_sevikamaster_active='Y' ";

                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                if (TblResult.Rows.Count > 0)
                {
                    MessageAlert("Sevika already active cannot rejoin", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify();</script>", false);
                    txtAadharNo.Focus();
                    return;
                }
            }
            if (txtJoinDate.Text == "")
            {
                MessageAlert(" Please Select Join Date ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (txtJoinDate.Text != "")
            {
                DateTime dt3 = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (dt3 > System.DateTime.Now)
                {
                    MessageAlert("Join date can not be grater than today's date", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                    return;
                }
                DateTime dt1 = DateTime.ParseExact(DtDob.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dt2 = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //int days = (dt2 - dt1).Days;
                //Console.WriteLine(days);

                //double month = (dt2 - dt1).Days / 30;
                //Console.WriteLine(month);
                double year = (dt2 - dt1).Days / 365;
                //Console.WriteLine(year);

                if (year < 14)
                {
                    MessageAlert("Need 14 years difference between DOB and Join Date", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                    return;
                }
            }
            if (DtOrder.Text != "")
            {
                DateTime dt1 = DateTime.ParseExact(txtJoinDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt2 = DateTime.ParseExact(DtOrder.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (dt2 > dt1)
                {
                    MessageAlert("Order date is must be less than Join Date", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                    return;
                }
            }
            if (txtRetireDt.Text == "")
            {
                MessageAlert(" Retirement Date not Set ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (ddldesigID.SelectedValue == "")
            {
                MessageAlert(" Please Select Designation ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (ddlPayscaleID.SelectedValue == "")
            {
                MessageAlert(" Please Select PayScale ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (ddlEduID.SelectedValue == "")
            {
                MessageAlert(" Please Select Qualification ", "");
                ddlEduID.Focus();
                return;
            }
            //if (ddlBank.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Bank ", "");
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            //    return;
            //}
            //if (ddlBranch.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Branch ", "");
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            //    return;
            //}
            //if (TxtAccNO.Text == "")
            //{
            //    MessageAlert(" Please Enter Account No. ", "");
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
            //    return;
            //}
            if (rdbActive.SelectedValue == "")
            {
                MessageAlert(" Please Select Active Type ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            if (ddlExperience.SelectedValue == "")
            {
                MessageAlert(" Please Select Experience ", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void BtnBackTab3_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            CallProc();
        }

        public void CallProc()
        {
            int Mode = 0;

            if (txtAddress1.Text != "")
            {
                if (txtAddress1.Text.Length > 100)
                {
                    MessageAlert("Nominee 1 Address length should be less than 100 characters", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
                    return;
                }
            }
            if (txtAddress2.Text != "")
            {
                if (txtAddress2.Text.Length > 100)
                {
                    MessageAlert("Nominee 2 Address length should be less than 100 characters", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
                    return;
                }
            }

            if (txtAddress3.Text != "")
            {
                if (txtAddress3.Text.Length > 100)
                {
                    MessageAlert("Nominee 3 Address length should be less than 100 characters", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
                    return;
                }
            }

            if (validation() == 1)
            {
                if (Request.QueryString["@"] == "1")
                {
                    objSavikaMast.Mode = 1;
                }
                else
                {
                    objSavikaMast.Mode = 2;
                }

                objSavikaMast.CompID = Convert.ToInt32(Session["GrdLevel"].ToString());
                if (txtSevikaId.Text != "" && Request.QueryString["@"] == "2")
                {
                    objSavikaMast.SevikaID = Convert.ToInt32(txtSevikaId.Text.Trim());
                }
                objSavikaMast.UserID = Session["UserId"].ToString();
                objSavikaMast.AnganId = Convert.ToInt32(ddlAnganID.SelectedValue);
                objSavikaMast.Name = txtName.Text.Trim();
                objSavikaMast.SevikaCode = TxtSevikaCode.Text.Trim();

                if (ddlEduID.SelectedValue == "")
                {
                    objSavikaMast.EducId = 0;
                }
                else
                {
                    objSavikaMast.EducId = Convert.ToInt32(ddlEduID.SelectedValue);
                }
                if (DtDob.Text.Trim() != "")
                {
                    //DateTime DOBdt = DateTime.Parse(DtDob.Text.Trim());
                    //objSavikaMast.BirthDate = Convert.ToDateTime(string.Format("{0:dd-MMM-yyyy}", DOBdt));
                    objSavikaMast.BirthDate = Convert.ToDateTime(DtDob.Value);
                }
                else
                {
                    objSavikaMast.BirthDate = DateTime.MinValue;
                }
                String UrDate = txtRetireDt.Text;
                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";
                DateTime validDate = Convert.ToDateTime(UrDate, dateInfo);
                objSavikaMast.RetireDate = validDate;

                objSavikaMast.Address = txtAddr.Text.Trim();
                objSavikaMast.MobileNo = TxtMobNo.Text.Trim();
                objSavikaMast.PhoneNo = txtphone.Text.Trim();
                objSavikaMast.PanNo = TxtPanNo.Text.Trim();
                objSavikaMast.AadharNo = txtAadharNo.Text.Trim();
                if (txtJoinDate.Text.Trim() != "")
                {
                    objSavikaMast.JoinDate = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    objSavikaMast.JoinDate = DateTime.MinValue;
                }
                objSavikaMast.OrderNo = TxtOrderNo.Text.Trim();
                if (DtOrder.Text.Trim() != "")
                {
                    //DateTime orderdt = DateTime.Parse(DtOrder.Text.Trim());
                    //objSavikaMast.Orderdate = Convert.ToDateTime(string.Format("{0:dd-MMM-yyyy}", orderdt));
                    objSavikaMast.Orderdate = Convert.ToDateTime(DtOrder.Value);
                }
                else
                {
                    objSavikaMast.Orderdate = DateTime.MinValue;
                }

                if (ddldesigID.SelectedValue == "")
                {
                    objSavikaMast.DesigID = 0;
                }
                else
                {
                    objSavikaMast.DesigID = Convert.ToInt32(ddldesigID.SelectedValue);
                }
                if (ddlPayscaleID.SelectedValue == "")
                {
                    objSavikaMast.PayScalID = 0;
                }
                else
                {
                    objSavikaMast.PayScalID = Convert.ToInt32(ddlPayscaleID.SelectedValue);
                }

                if (ddlBranch.SelectedValue == "")
                {
                    objSavikaMast.BranchId = 0;
                }
                else
                {
                    objSavikaMast.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
                }

                objSavikaMast.AccNo = TxtAccNO.Text.Trim();
                objSavikaMast.Remark = TxtRemark.Text.Trim();

                if (rdbActive.SelectedItem.Selected == true)
                {
                    //if (rdbActive.SelectedItem.Value == "N")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>alert(' You are saving Sevika in InActive Mode ');</script>", false);
                    //}
                    objSavikaMast.Active = rdbActive.SelectedItem.Value;
                }
                objSavikaMast.CPSMSCode = TxtCPSMSCode.Text.Trim();

                if (ddlReligion.SelectedValue == "")
                {
                    objSavikaMast.ReligID = 0;
                }
                else
                {
                    objSavikaMast.ReligID = Convert.ToInt32(ddlReligion.SelectedValue);
                }

                if (ddlCast.SelectedValue == "")
                {
                    objSavikaMast.CastID = 0;
                }
                else
                {
                    objSavikaMast.CastID = Convert.ToInt32(ddlCast.SelectedValue);
                }

                objSavikaMast.MiddleName = TxtMidName.Text.Trim();
                objSavikaMast.Village = TxtVillage.Text.Trim();
                objSavikaMast.PinCode = TxtPinCode.Text.Trim();

                if (ddlmaritstat.SelectedValue == "")
                {
                    objSavikaMast.Maritstatid = 0;
                }
                else
                {
                    objSavikaMast.Maritstatid = Convert.ToInt32(ddlmaritstat.SelectedValue);
                }
                if (rdbActive.SelectedValue == "N")
                {
                    objSavikaMast.Reason = ddlReason.SelectedValue;
                }
                else
                {
                    objSavikaMast.Reason = "";
                }
                //-------------added on 24-03-18
                if (ddlExperience.SelectedValue == "")
                {
                    objSavikaMast.Experience = 0;
                }
                else
                {
                    objSavikaMast.Experience = Convert.ToInt32(ddlExperience.SelectedValue);
                }
                //--------------------------------

                objSavikaMast.Nom1Name = txtNomNM1.Text.Trim();
                if (ddlrelation1.SelectedValue == "")
                {
                    objSavikaMast.Nom1RelatnId = 0;
                }
                else
                {
                    objSavikaMast.Nom1RelatnId = Convert.ToInt32(ddlrelation1.SelectedValue);
                }
                if (txtNomAge1.Text == "")
                {
                    objSavikaMast.Nom1Age = 0;
                }
                else
                {
                    objSavikaMast.Nom1Age = Convert.ToInt32(txtNomAge1.Text);
                }
                objSavikaMast.Nom1Address = txtAddress1.Text.Trim();
                objSavikaMast.Nom2Name = txtNomNM2.Text.Trim();
                if (ddlrelation2.SelectedValue != "")
                {
                    objSavikaMast.Nom2RelationId = Convert.ToInt32(ddlrelation2.SelectedValue);
                }
                else
                {
                    objSavikaMast.Nom2RelationId = 0;
                }
                if (txtNomAge2.Text != "")
                {
                    objSavikaMast.Nom2Age = Convert.ToInt32(txtNomAge2.Text);
                }
                else
                {
                    objSavikaMast.Nom2Age = 0;
                }
                objSavikaMast.Nom2Address = txtAddress2.Text.Trim();

                //--------Added on 18-04-19

                if (ddlBranchN1.SelectedValue == "")
                {
                    objSavikaMast.Nom1BrID = 0;
                }
                else
                {
                    objSavikaMast.Nom1BrID = Convert.ToInt32(ddlBranchN1.SelectedValue);
                }

                if (TxtAccNON1.Text == "")
                {
                    objSavikaMast.Nom1Accno = null;
                }
                else
                {
                    objSavikaMast.Nom1Accno = TxtAccNON1.Text.Trim();
                }

                if (TxtRatioN1.Text == "")
                {
                    objSavikaMast.Nom1Ratio = 0;
                }
                else
                {
                    objSavikaMast.Nom1Ratio = Convert.ToInt32(TxtRatioN1.Text);
                }

                if (ddlBranchN2.SelectedValue == "")
                {
                    objSavikaMast.Nom2BrID = 0;
                }
                else
                {
                    objSavikaMast.Nom2BrID = Convert.ToInt32(ddlBranchN2.SelectedValue);
                }

                if (TxtAccNON2.Text == "")
                {
                    objSavikaMast.Nom2Accno = null;
                }
                else
                {
                    objSavikaMast.Nom2Accno = TxtAccNON2.Text.Trim();
                }

                if (TxtRatioN2.Text == "")
                {
                    objSavikaMast.Nom2Ratio = 0;
                }
                else
                {
                    objSavikaMast.Nom2Ratio = Convert.ToInt32(TxtRatioN2.Text);
                }

                objSavikaMast.Nom3name = txtNomNM3.Text.Trim();
                if (ddlrelation3.SelectedValue == "")
                {
                    objSavikaMast.Nom3relaid = 0;
                }
                else
                {
                    objSavikaMast.Nom3relaid = Convert.ToInt32(ddlrelation3.SelectedValue);
                }
                if (txtNomAge3.Text == "")
                {
                    objSavikaMast.Nom3age = 0;
                }
                else
                {
                    objSavikaMast.Nom3age = Convert.ToInt32(txtNomAge3.Text);
                }
                objSavikaMast.Nom3address = txtAddress3.Text.Trim();

                if (ddlBranchN3.SelectedValue == "")
                {
                    objSavikaMast.Nom3BrID = 0;
                }
                else
                {
                    objSavikaMast.Nom3BrID = Convert.ToInt32(ddlBranchN3.SelectedValue);
                }

                if (TxtAccNON3.Text == "")
                {
                    objSavikaMast.Nom3Accno = null;
                }
                else
                {
                    objSavikaMast.Nom3Accno = TxtAccNON3.Text.Trim();
                }

                if (TxtRatioN3.Text == "")
                {
                    objSavikaMast.Nom3Ratio = 0;
                }
                else
                {
                    objSavikaMast.Nom3Ratio = Convert.ToInt32(TxtRatioN3.Text);
                }
                if (DtExitDT.Text.Trim() != "")
                {
                    objSavikaMast.ExitDate = Convert.ToDateTime(DtExitDT.Value);
                }
                else
                {
                    objSavikaMast.ExitDate = DateTime.MinValue;
                }
                objSavikaMast.rejoinflag = Session["RejoinFlag"].ToString();
                objSavikaMast.UpDate();

                if (objSavikaMast.ErrorCode == -100)
                {
                    MessageAlert(objSavikaMast.ErrorMessage, "../Transaction/FrmSevikaMasterList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objSavikaMast.ErrorMessage, "");
                    return;
                }
            }
        }

        private void bindcmb()
        {
            //MstMethods.Dropdown.Fill(ddldesigID, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "", "");
            MstMethods.Dropdown.Fill(ddlBank, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
            //MstMethods.Dropdown.Fill(ddlBranch, "aoup_bankbranch_def", "var_bankbranch_branchname", "num_bankbranch_branchid", "", "");
            MstMethods.Dropdown.Fill(ddlEduID, "aoup_education_def order by trim(var_education_educname)", "var_education_educname", "num_education_educid", "", "");
            MstMethods.Dropdown.Fill(ddlCast, "aoup_Cast_def order by trim(var_cast_castname)", "var_cast_castname", "num_cast_castid", "", "");
            MstMethods.Dropdown.Fill(ddlmaritstat, "aoup_Maritstat_def order by trim(var_maritstat_maritstat)", "var_maritstat_maritstat", "num_maritstat_maritstatid", "", "");
            MstMethods.Dropdown.Fill(ddlReligion, "aoup_religion_def order by trim(var_religion_religname)", "var_religion_religname", "num_religion_religid", "", "");
            MstMethods.Dropdown.Fill(ddlrelation1, "aoup_relation_def", "var_relation_relation", "num_relation_relationid", "", "");
            MstMethods.Dropdown.Fill(ddlrelation2, "aoup_relation_def", "var_relation_relation", "num_relation_relationid", "", "");
            MstMethods.Dropdown.Fill(ddlrelation3, "aoup_relation_def", "var_relation_relation", "num_relation_relationid", "", "");
            MstMethods.Dropdown.Fill(ddlBankN1, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
            MstMethods.Dropdown.Fill(ddlBankN2, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
            MstMethods.Dropdown.Fill(ddlBankN3, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
        }

        public int validation()
        {
            if (ddlAnganID.SelectedValue == "")
            {
                MessageAlert(" Please select Angan Wadi ", "");
                ddlAnganID.Focus();
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
            if (DtDob.Text == "")
            {
                MessageAlert(" Please Select Birth Date ", "");
                DtDob.Focus();
                return 2;
            }
            if (ddlReligion.SelectedValue == "")
            {
                MessageAlert(" Please Select Religion ", "");
                ddlReligion.Focus();
                return 2;
            }
            if (ddlEduID.SelectedValue == "")
            {
                MessageAlert(" Please Select Qualification ", "");
                ddlEduID.Focus();
                return 2;
            }
            if (ddlmaritstat.SelectedValue == "")
            {
                MessageAlert(" Please Select Martial Status ", "");
                ddlmaritstat.Focus();
                return 2;
            }
            if (TxtSevikaCode.Text == "")
            {
                MessageAlert(" Scheme Specific Code cannot be blank ", "");
                TxtSevikaCode.Focus();
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
                //---------------------------------------------------------------------Added for rejoin-------------------------------------------------------//
                if (Session["RejoinFlag"].ToString() == "Y")
                {
                    String str = " select num_sevikamaster_sevikaid from aoup_sevikamaster_def where var_sevikamaster_aadharno='" + txtAadharNo.Text.ToString() + "' AND  var_sevikamaster_active='Y' ";

                    DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                    if (TblResult.Rows.Count > 0)
                    {
                        MessageAlert("Sevika already active cannot rejoin", "");
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify();</script>", false);
                        txtAadharNo.Focus();
                        return 2;
                    }
                }
                //---------------------------------------------------------------------Added for rejoin-------------------------------------------------------//

                ////-----------------

                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "", false);

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "validate();", true);
                MstMethods.aadharcard aad = new MstMethods.aadharcard();
                bool isValidnumber = MstMethods.aadharcard.validateVerhoeff(txtAadharNo.Text);
                //aadharcard.validateVerhoeff("num");
                lblmsg.Text = txtAadharNo.Text + "valid number";
                if (isValidnumber)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Valid";
                    TxtSevikaCode.Text = ViewState["CDPO"].ToString() + txtAadharNo.Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                }
                else
                {
                    lblmsg.ForeColor = Color.Red;
                    lblmsg.Text = "Invalid";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                    return 2;
                }

                ////-----------------
            }
            if (txtJoinDate.Text == "")
            {
                MessageAlert(" Please Select Join Date ", "");
                //DtJoin.Focus();
                return 2;
            }
            if (txtRetireDt.Text == "")
            {
                MessageAlert(" Retirement Date not Set ", "");
                return 2;
            }
            if (ddldesigID.SelectedValue == "")
            {
                MessageAlert(" Please Select Designation ", "");
                ddldesigID.Focus();
                return 2;
            }
            if (ddlPayscaleID.SelectedValue == "")
            {
                MessageAlert(" Please Select PayScale ", "");
                ddlPayscaleID.Focus();
                return 2;
            }
            //if (ddlBank.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Bank ", "");
            //    ddlBank.Focus();
            //    return 2;
            //}
            //if (ddlBranch.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Branch ", "");
            //    ddlBranch.Focus();
            //    return 2;
            //}
            //if (TxtAccNO.Text == "")
            //{
            //    MessageAlert(" Please Enter Account No. ", "");
            //    TxtAccNO.Focus();
            //    return 2;
            //}
            if (rdbActive.SelectedValue == "")
            {
                MessageAlert(" Please Select Active Type ", "");
                rdbActive.Focus();
                return 2;
            }
            if (ddlExperience.SelectedValue == "")
            {
                MessageAlert(" Please Select Experience ", "");
                ddlExperience.Focus();
                return 2;
            }
            else
            {
                return 1;
            }
        }

        public void RetirementDate()
        {
            DateTime dt = System.DateTime.Now;
            if (DtDob.Text != "")
            {
                dt = Convert.ToDateTime(DtDob.Value).AddYears(65);

                DateTime Days = Convert.ToDateTime(dt);
                int month = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("MM")));
                String month1 = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Days.Month);
                int year = Convert.ToInt32(DateTime.Now.ToString(Days.ToString("yyyy")));
                days = DateTime.DaysInMonth(year, month);
                String lstdate = Convert.ToString(days + "-" + month1 + "-" + year);

                txtRetireDt.Text = lstdate;
            }
            else
            {
                MessageAlert(" Please Select Birth Date First ", "");
                DtDob.Focus();
            }
        }

        private void grdSavikaList_set()
        {
            #region "Load Grid"

            String query = " SELECT a.num_sevikamaster_sevikaid,a.num_sevakmaster_anganid, a.var_sevikamaster_name,a.var_sevikamaster_sevikacode, a.num_sevikamaster_educid, ";
            query += " a.date_sevikamaster_birthdate,a.date_sevikamaster_retiredate, a.var_sevikamaster_address,a.num_sevikamaster_mobileno, a.var_sevikamaster_phoneno,num_bank_bankid, b.num_bankbranch_branchid, ";
            query += " b.var_bankbranch_ifsccode, ";
            query += " a.var_sevikamaster_panno, a.var_sevikamaster_aadharno,a.date_sevikamaster_joindate, a.var_sevikamaster_orderno, ";
            query += " a.date_sevikamaster_orderdate, a.num_sevikamaster_desigid,a.num_sevikamaster_payscalid,b.var_bankbranch_branchname branchnm, a.num_sevikamaster_branchid, ";
            query += " a.var_sevikamaster_accno, a.var_sevikamaster_remark,a.var_sevikamaster_active, ";
            query += " a.var_sevikamaster_cpsmscode,h.var_religion_religname,i.var_cast_castname,a.var_sevikamaster_middlename, a.var_sevikamaster_village, ";
            query += " a.var_sevikamaster_pincode, j.var_maritstat_maritstat , a.num_sevikamaster_religid, a.num_sevikamaster_castid,a.num_sevikamaster_maritstatid,a.var_sevikamaster_reason, ";
            query += " k.var_sevikanominee_nom1name,k.var_sevikanominee_nom1relaid,k.var_sevikanominee_nom1age,k.var_sevikanominee_nom1address, ";
            query += " k.var_sevikanominee_nom2name,k.var_sevikanominee_nom2relaid,k.var_sevikanominee_nom2age,k.var_sevikanominee_nom2address, ";
            query += " k.var_sevikanominee_nom3name, k.var_sevikanominee_nom3relaid,k.var_sevikanominee_nom3age, k.var_sevikanominee_nom3address, ";
            query += " br1.num_bankbranch_bankid N1BankId,br2.num_bankbranch_bankid N2BankId,br3.num_bankbranch_bankid N3BankId, ";
            query += " k.num_sevikanominee_nom1brid,br1.var_bankbranch_branchname N1Branch,br1.var_bankbranch_ifsccode N1ifsccode,k.num_sevikanominee_nom1accno,k.num_sevikanominee_nom1ratio, ";
            query += " k.num_sevikanominee_nom2brid,br2.var_bankbranch_branchname N2Branch,br2.var_bankbranch_ifsccode N2ifsccode,k.num_sevikanominee_nom2accno,k.num_sevikanominee_nom2ratio, ";
            query += " k.num_sevikanominee_nom3brid,br3.var_bankbranch_branchname N3Branch,br3.var_bankbranch_ifsccode N3ifsccode,k.num_sevikanominee_nom3accno,k.num_sevikanominee_nom3ratio, ";
            query += " a.num_sevikamaster_experience,var_designation_flag, date_promo_promotedt,a.date_sevikamaster_exitdate ";
            query += " FROM aoup_sevikamaster_def a  left join aoup_bankbranch_def b on a.num_sevikamaster_branchid=b.num_bankbranch_branchid ";
            query += " left join aoup_bank_def g on b.num_bankbranch_bankid=g.num_bank_bankid ";
            query += " left join aoup_religion_def h on num_religion_religid=num_sevikamaster_religid  ";
            query += " left join aoup_cast_def i on num_cast_castid=num_sevikamaster_castid ";
            query += " left join aoup_maritstat_def j on  num_maritstat_maritstatid=num_sevikamaster_maritstatid ";
            query += " left join aoup_sevikanominee_def k on a.num_sevikamaster_sevikaid=k.num_sevikanominee_sevikaid ";
            query += " left join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";//-------------14-05-18
            query += " left join aoup_bankbranch_def br1 on  br1.num_bankbranch_branchid=k.num_sevikanominee_nom1brid ";
            query += " left join aoup_bankbranch_def br2 on  br2.num_bankbranch_branchid=k.num_sevikanominee_nom2brid ";
            query += " left join aoup_bankbranch_def br3 on  br3.num_bankbranch_branchid=k.num_sevikanominee_nom3brid ";
            query += " left join aoup_promotion_def on num_promo_sevikaid=num_sevikamaster_sevikaid ";
            query += " where num_sevikamaster_compid='" + Session["GrdLevel"].ToString() + "' and num_sevikamaster_sevikaid='" + Session["SevikaId"] + "'";

            DataTable dtSavikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtSavikaList.Rows.Count > 0)
            {
                if (dtSavikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString() != null)
                {
                    txtSevikaId.Text = dtSavikaList.Rows[0]["num_sevikamaster_sevikaid"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevakmaster_anganid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevakmaster_anganid"].ToString() != null)
                {
                    ddlAnganID.SelectedValue = dtSavikaList.Rows[0]["num_sevakmaster_anganid"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_name"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_name"].ToString() != null)
                {
                    txtName.Text = dtSavikaList.Rows[0]["var_sevikamaster_name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_sevikacode"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_sevikacode"].ToString() != null)
                {
                    TxtSevikaCode.Text = dtSavikaList.Rows[0]["var_sevikamaster_sevikacode"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikamaster_educid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_educid"].ToString() != null)
                {
                    ddlEduID.SelectedValue = dtSavikaList.Rows[0]["num_sevikamaster_educid"].ToString();
                    ddlEduID_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlEduID.SelectedValue = "0";
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
                    if (dtSavikaList.Rows[0]["date_promo_promotedt"].ToString() != "" && dtSavikaList.Rows[0]["date_promo_promotedt"].ToString() != null)
                    {
                        txtPromoteDt.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_promo_promotedt"]).ToString("dd/MM/yyyy");
                    }
                    txtJoinDate.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_joindate"]).ToString("dd/MM/yyyy");
                    CalcExp();

                    //DateTime dt = DateTime.Parse(dtSavikaList.Rows[0]["date_sevikamaster_joindate"].ToString());
                    //DtJoin.Text = string.Format("{0:dd/MM/yyyy}", dt);
                    //DtJoin.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_joindate"]).ToString("dd/MM/yyyy");

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
                if (dtSavikaList.Rows[0]["var_designation_flag"].ToString() != "" && dtSavikaList.Rows[0]["var_designation_flag"].ToString() != null)
                {
                    ddlFlag.SelectedValue = dtSavikaList.Rows[0]["var_designation_flag"].ToString();
                    ddlFlag_SelectedIndexChanged(null, null);
                }
                if (dtSavikaList.Rows[0]["num_sevikamaster_desigid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_desigid"].ToString() != null)
                {
                    ddldesigID.SelectedValue = dtSavikaList.Rows[0]["num_sevikamaster_desigid"].ToString();
                }
                else
                {
                    ddldesigID.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_sevikamaster_payscalid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_payscalid"].ToString() != null)
                {
                    ddlPayscaleID.SelectedValue = dtSavikaList.Rows[0]["num_sevikamaster_payscalid"].ToString();
                }
                else
                {
                    ddlPayscaleID.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_bank_bankid"].ToString() != "" && dtSavikaList.Rows[0]["num_bank_bankid"].ToString() != null)
                {
                    ddlBank.SelectedValue = dtSavikaList.Rows[0]["num_bank_bankid"].ToString();
                    ddlBank_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlBank.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_bankbranch_branchid"].ToString() != "" && dtSavikaList.Rows[0]["num_bankbranch_branchid"].ToString() != null)
                {
                    ddlBranch.SelectedValue = dtSavikaList.Rows[0]["num_bankbranch_branchid"].ToString();
                }
                else
                {
                    ddlBranch.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["branchnm"].ToString() != "" && dtSavikaList.Rows[0]["branchnm"].ToString() != null)
                {
                    txtBankBranch.Text = dtSavikaList.Rows[0]["branchnm"].ToString();
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
                        ddlReason.Visible = true;
                        lblReason.Visible = true;
                        Label2.Visible = true;
                        lblExitDate.Visible = true;
                        Label4.Visible = true;
                        DtExitDT.Visible = true;
                    }
                }
                if (dtSavikaList.Rows[0]["var_sevikamaster_cpsmscode"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikamaster_cpsmscode"].ToString() != null)
                {
                    TxtCPSMSCode.Text = dtSavikaList.Rows[0]["var_sevikamaster_cpsmscode"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikamaster_religid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_religid"].ToString() != null)
                {
                    ddlReligion.SelectedValue = dtSavikaList.Rows[0]["num_sevikamaster_religid"].ToString();
                }
                else
                {
                    ddlReligion.SelectedValue = "0";
                }

                if (dtSavikaList.Rows[0]["num_sevikamaster_castid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_castid"].ToString() != null)
                {
                    ddlCast.SelectedValue = dtSavikaList.Rows[0]["num_sevikamaster_castid"].ToString();
                }
                else
                {
                    ddlCast.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_sevikamaster_maritstatid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikamaster_maritstatid"].ToString() != null)
                {
                    ddlmaritstat.SelectedValue = dtSavikaList.Rows[0]["num_sevikamaster_maritstatid"].ToString();
                }
                else
                {
                    ddlmaritstat.SelectedValue = "0";
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
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom1relaid"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom1relaid"].ToString() != null)
                {
                    ddlrelation1.SelectedValue = dtSavikaList.Rows[0]["var_sevikanominee_nom1relaid"].ToString();
                }
                else
                {
                    ddlrelation1.SelectedValue = "0";
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
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom2relaid"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom2relaid"].ToString() != null)
                {
                    ddlrelation2.SelectedValue = dtSavikaList.Rows[0]["var_sevikanominee_nom2relaid"].ToString();
                }
                else
                {
                    ddlrelation2.SelectedValue = "0";
                }

                if (dtSavikaList.Rows[0]["var_bankbranch_ifsccode"].ToString() != "")
                {
                    txtIFSC.Text = dtSavikaList.Rows[0]["var_bankbranch_ifsccode"].ToString();
                }
                // --- Added on 19-04-19
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom3name"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom3name"].ToString() != null)
                {
                    txtNomNM3.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom3name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom3age"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom3age"].ToString() != null)
                {
                    txtNomAge3.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom3age"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom3address"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom3address"].ToString() != null)
                {
                    txtAddress3.Text = dtSavikaList.Rows[0]["var_sevikanominee_nom3address"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_sevikanominee_nom3relaid"].ToString() != "" && dtSavikaList.Rows[0]["var_sevikanominee_nom3relaid"].ToString() != null)
                {
                    ddlrelation3.SelectedValue = dtSavikaList.Rows[0]["var_sevikanominee_nom3relaid"].ToString();
                }
                else
                {
                    ddlrelation3.SelectedValue = "0";
                }

                if (dtSavikaList.Rows[0]["N1BankId"].ToString() != "" && dtSavikaList.Rows[0]["N1BankId"].ToString() != null)
                {
                    ddlBankN1.SelectedValue = dtSavikaList.Rows[0]["N1BankId"].ToString();
                    ddlBankN1_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlBankN1.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom1brid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom1brid"].ToString() != null)
                {
                    ddlBranchN1.SelectedValue = dtSavikaList.Rows[0]["num_sevikanominee_nom1brid"].ToString();
                }
                else
                {
                    ddlBranchN1.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["N1Branch"].ToString() != "" && dtSavikaList.Rows[0]["N1Branch"].ToString() != null)
                {
                    txtBankBranchN1.Text = dtSavikaList.Rows[0]["N1Branch"].ToString();
                }
                if (dtSavikaList.Rows[0]["N1ifsccode"].ToString() != "" && dtSavikaList.Rows[0]["N1ifsccode"].ToString() != null)
                {
                    txtIFSCN1.Text = dtSavikaList.Rows[0]["N1ifsccode"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom1accno"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom1accno"].ToString() != null)
                {
                    TxtAccNON1.Text = dtSavikaList.Rows[0]["num_sevikanominee_nom1accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom1ratio"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom1ratio"].ToString() != null)
                {
                    TxtRatioN1.Text = dtSavikaList.Rows[0]["num_sevikanominee_nom1ratio"].ToString();
                }

                if (dtSavikaList.Rows[0]["N2BankId"].ToString() != "" && dtSavikaList.Rows[0]["N2BankId"].ToString() != null)
                {
                    ddlBankN2.SelectedValue = dtSavikaList.Rows[0]["N2BankId"].ToString();
                    ddlBankN2_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlBankN2.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom2brid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom2brid"].ToString() != null)
                {
                    ddlBranchN2.SelectedValue = dtSavikaList.Rows[0]["num_sevikanominee_nom2brid"].ToString();
                }
                else
                {
                    ddlBranchN2.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["N2Branch"].ToString() != "" && dtSavikaList.Rows[0]["N2Branch"].ToString() != null)
                {
                    txtBankBranchN2.Text = dtSavikaList.Rows[0]["N2Branch"].ToString();
                }
                if (dtSavikaList.Rows[0]["N2ifsccode"].ToString() != "" && dtSavikaList.Rows[0]["N2ifsccode"].ToString() != null)
                {
                    txtIFSCN2.Text = dtSavikaList.Rows[0]["N2ifsccode"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom2accno"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom2accno"].ToString() != null)
                {
                    TxtAccNON2.Text = dtSavikaList.Rows[0]["num_sevikanominee_nom2accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom2ratio"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom2ratio"].ToString() != null)
                {
                    TxtRatioN2.Text = dtSavikaList.Rows[0]["num_sevikanominee_nom2ratio"].ToString();
                }

                if (dtSavikaList.Rows[0]["N3BankId"].ToString() != "" && dtSavikaList.Rows[0]["N3BankId"].ToString() != null)
                {
                    ddlBankN3.SelectedValue = dtSavikaList.Rows[0]["N3BankId"].ToString();
                    ddlBankN3_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlBankN3.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom3brid"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom3brid"].ToString() != null)
                {
                    ddlBranchN3.SelectedValue = dtSavikaList.Rows[0]["num_sevikanominee_nom3brid"].ToString();
                }
                else
                {
                    ddlBranchN3.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["N3Branch"].ToString() != "" && dtSavikaList.Rows[0]["N3Branch"].ToString() != null)
                {
                    txtBankBranchN3.Text = dtSavikaList.Rows[0]["N3Branch"].ToString();
                }
                if (dtSavikaList.Rows[0]["N3ifsccode"].ToString() != "" && dtSavikaList.Rows[0]["N3ifsccode"].ToString() != null)
                {
                    txtIFSCN3.Text = dtSavikaList.Rows[0]["N3ifsccode"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom3accno"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom3accno"].ToString() != null)
                {
                    TxtAccNON3.Text = dtSavikaList.Rows[0]["num_sevikanominee_nom3accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_sevikanominee_nom3ratio"].ToString() != "" && dtSavikaList.Rows[0]["num_sevikanominee_nom3ratio"].ToString() != null)
                {
                    TxtRatioN3.Text = dtSavikaList.Rows[0]["num_sevikanominee_nom3ratio"].ToString();
                }
                if (dtSavikaList.Rows[0]["date_sevikamaster_exitdate"].ToString() != "" && dtSavikaList.Rows[0]["date_sevikamaster_exitdate"].ToString() != null)
                {
                    DtExitDT.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_sevikamaster_exitdate"]).ToString("dd/MM/yyyy");
                }

            }
            #endregion
        }

        public void Aadhar_Chk()
        {
            ////-----------------

            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "", false);

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "validate();", true);
            MstMethods.aadharcard aad = new MstMethods.aadharcard();
            bool isValidnumber = MstMethods.aadharcard.validateVerhoeff(txtAadharNo.Text);
            //aadharcard.validateVerhoeff("num");
            lblmsg.Text = txtAadharNo.Text + "valid number";
            if (isValidnumber)
            {
                lblmsg.ForeColor = Color.Green;
                lblmsg.Text = "Valid";
                TxtSevikaCode.Text = ViewState["CDPO"].ToString() + txtAadharNo.Text;
            }
            else
            {
                lblmsg.ForeColor = Color.Red;
                lblmsg.Text = "Invalid";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
                return;
            }

            ////-----------------
        }

        public void CalcExp()
        {
            if (txtJoinDate.Text != "")
            {
                if (txtPromoteDt.Text != "")
                {
                    ddlExperience.SelectedValue = "1";
                }
                else
                {
                    DateTime dt = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    int Exp = (System.DateTime.Now.Year - dt.Year);
                    int month = (System.DateTime.Now.Month - dt.Month);

                    int totalMonth = (Exp * 12) + month;

                    MstMethods.Dropdown.Fill(ddlExperience, "aoup_experience_def", "var_experience_text", "num_experience_id", " var_experience_status ='A' order by num_experience_id", "");

                    if (totalMonth <= 120)
                    {
                        ddlExperience.SelectedValue = "1";
                    }
                    else if (totalMonth > 120 && totalMonth <= 240)
                    {
                        ddlExperience.SelectedValue = "2";
                    }
                    else if (totalMonth > 240 && totalMonth <= 360)
                    {
                        ddlExperience.SelectedValue = "3";
                    }
                    else
                    {
                        ddlExperience.SelectedValue = "4";
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);

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
                        desg += " and num_experience_id= " + ddlExperience.SelectedValue + " and var_designation_flag='" + ddlFlag.SelectedValue + "'  and var_payscal_active='Y' ";

                        MstMethods.Dropdown.Fill(ddldesigID, "", "", "", "", desg);

                        DataTable Tbldesg = (DataTable)MstMethods.Query2DataTable.GetResult(desg);

                        if (Tbldesg.Rows.Count > 0)
                        {
                            ddldesigID.SelectedValue = Tbldesg.Rows[0]["num_designation_desigid"].ToString();
                        }
                    }
                    else
                    {
                        ddldesigID.SelectedValue = "";
                    }
                }
                else
                {
                    ddldesigID.SelectedValue = "";
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
        }

        protected void ddldesigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPayScale();
        }

        protected void rdbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbActive.SelectedValue == "Y")
            {
                ddlReason.Visible = false;
                lblReason.Visible = false;
                Label2.Visible = false;
                lblExitDate.Visible = false;
                Label4.Visible = false;
                DtExitDT.Visible = false;
            }
            if (rdbActive.SelectedValue == "N")
            {
                ddlReason.Visible = true;
                lblReason.Visible = true;
                Label2.Visible = true;
                lblExitDate.Visible = true;
                Label4.Visible = true;
                DtExitDT.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);
        }

        protected void ddlFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlag.SelectedValue != "")
            {
                //MstMethods.Dropdown.Fill(ddldesigID, "aoup_designation_def", "var_designation_desig", "num_designation_desigid", "var_designation_flag='" + ddlFlag.SelectedValue + "'", "");
                FillDesig();
                setPayScale();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void lnkCalcExp_Click(object sender, EventArgs e)
        {
            CalcExp();
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
                            Pay += " and num_experience_id=" + ddlExperience.SelectedValue;
                            Pay += " order by trim(var_payscal_payscal) ";

                            MstMethods.Dropdown.Fill(ddlPayscaleID, "", "", "", "", Pay);

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

        protected void ddlEduID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDesig();
            setPayScale();
        }

        protected void ddlExperience_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDesig();
            setPayScale();
        }

        protected void dummybtn_Click(object sender, EventArgs e)
        {
            string dt = Request.Form[txtJoinDate.UniqueID];
            txtJoinDate.Text = dt;
            CalcExp();
            FillDesig();
            setPayScale();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
        }

        protected void ddlBankN1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String qryBN1 = "select var_bankbranch_branchname || '    ' || var_bankbranch_ifsccode,num_bankbranch_branchid from aoup_bankbranch_def ";
            qryBN1 += " where num_bankbranch_bankid=" + ddlBankN1.SelectedValue;
            MstMethods.Dropdown.Fill(ddlBranchN1, "", "", "", "", qryBN1);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void ddlBankN2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String qryBN2 = "select var_bankbranch_branchname || '    ' || var_bankbranch_ifsccode,num_bankbranch_branchid from aoup_bankbranch_def ";
            qryBN2 += " where num_bankbranch_bankid=" + ddlBankN2.SelectedValue;
            MstMethods.Dropdown.Fill(ddlBranchN2, "", "", "", "", qryBN2);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void ddlBankN3_SelectedIndexChanged(object sender, EventArgs e)
        {
            String qryBN3 = "select var_bankbranch_branchname || '    ' || var_bankbranch_ifsccode,num_bankbranch_branchid from aoup_bankbranch_def ";
            qryBN3 += " where num_bankbranch_bankid=" + ddlBankN3.SelectedValue;
            MstMethods.Dropdown.Fill(ddlBranchN3, "", "", "", "", qryBN3);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void LinkSerchBankBranchN1_OnClick(object sender, EventArgs e)
        {
            if (ddlBankN1.SelectedValue.ToString() == "" || ddlBankN1.SelectedValue.ToString() == "0")
            {
                MessageAlert("Select Bank first from the list", "");
                ddlBankN1.Focus();
                return;
            }

            if (txtIFSCN1.Text == "")
            {
                MessageAlert("IFSC code can not be blank", "");
                txtIFSCN1.Focus();
                return;
            }

            String str = "select var_bankbranch_branchname branchname,num_bankbranch_branchid branchid from aoup_bankbranch_def where num_bankbranch_bankid = " + ddlBankN1.SelectedValue.ToString() + " and ";
            str += "UPPER(var_bankbranch_ifsccode) = UPPER('" + txtIFSCN1.Text + "')";

            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblResult.Rows.Count > 0)
            {
                ddlBranchN1.SelectedValue = TblResult.Rows[0]["branchid"].ToString();
                txtBankBranchN1.Text = TblResult.Rows[0]["branchname"].ToString();
            }

            else
            {
                ddlBranchN1.DataSource = "";
                ddlBranchN1.DataBind();
                txtBankBranchN1.Text = "";

                MessageAlert("Bank branch not found", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
                txtIFSCN1.Focus();
                return;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void LinkSerchBankBranchN2_OnClick(object sender, EventArgs e)
        {
            if (ddlBankN2.SelectedValue.ToString() == "" || ddlBankN2.SelectedValue.ToString() == "0")
            {
                MessageAlert("Select Bank first from the list", "");
                ddlBankN2.Focus();
                return;
            }

            if (txtIFSCN2.Text == "")
            {
                MessageAlert("IFSC code can not be blank", "");
                txtIFSCN2.Focus();
                return;
            }

            String str = "select var_bankbranch_branchname branchname,num_bankbranch_branchid branchid from aoup_bankbranch_def where num_bankbranch_bankid = " + ddlBankN2.SelectedValue.ToString() + " and ";
            str += "UPPER(var_bankbranch_ifsccode) = UPPER('" + txtIFSCN2.Text + "')";

            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblResult.Rows.Count > 0)
            {
                ddlBranchN2.SelectedValue = TblResult.Rows[0]["branchid"].ToString();
                txtBankBranchN2.Text = TblResult.Rows[0]["branchname"].ToString();
            }

            else
            {
                ddlBranchN2.DataSource = "";
                ddlBranchN2.DataBind();
                txtBankBranchN2.Text = "";

                MessageAlert("Bank branch not found", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
                txtIFSCN2.Focus();
                return;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void LinkSerchBankBranchN3_OnClick(object sender, EventArgs e)
        {
            if (ddlBankN3.SelectedValue.ToString() == "" || ddlBankN3.SelectedValue.ToString() == "0")
            {
                MessageAlert("Select Bank first from the list", "");
                ddlBankN3.Focus();
                return;
            }

            if (txtIFSCN3.Text == "")
            {
                MessageAlert("IFSC code can not be blank", "");
                txtIFSCN3.Focus();
                return;
            }

            String str = "select var_bankbranch_branchname branchname,num_bankbranch_branchid branchid from aoup_bankbranch_def where num_bankbranch_bankid = " + ddlBankN3.SelectedValue.ToString() + " and ";
            str += "UPPER(var_bankbranch_ifsccode) = UPPER('" + txtIFSCN3.Text + "')";

            DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

            if (TblResult.Rows.Count > 0)
            {
                ddlBranchN3.SelectedValue = TblResult.Rows[0]["branchid"].ToString();
                txtBankBranchN3.Text = TblResult.Rows[0]["branchname"].ToString();
            }

            else
            {
                ddlBranchN3.DataSource = "";
                ddlBranchN3.DataBind();
                txtBankBranchN3.Text = "";

                MessageAlert("Bank branch not found", "");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
                txtIFSCN3.Focus();
                return;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify1();</script>", false);
        }

        protected void txtAadharNo_TextChanged(object sender, EventArgs e)
        {
            if (txtAadharNo.Text.ToString() != "")
            {
                if (txtAadharNo.Text.Length.ToString() == "12")
                {
                    if (Session["RejoinFlag"].ToString() == "Y")
                    {
                        String str = " select num_sevikamaster_sevikaid from aoup_sevikamaster_def where var_sevikamaster_aadharno='" + txtAadharNo.Text.ToString() + "' AND  var_sevikamaster_active='Y' ";

                        DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);

                        if (TblResult.Rows.Count > 0)
                        {
                            MessageAlert("Sevika already active cannot rejoin", "");
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify();</script>", false);
                            txtAadharNo.Focus();
                            return;
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>ShowModify();</script>", false);
        }
    }
}