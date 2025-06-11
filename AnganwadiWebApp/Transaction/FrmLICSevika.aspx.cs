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
using System.Drawing;
using System.IO;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmLICSevika : System.Web.UI.Page
    {
        BoLIC_Sevika objSavikaMast = new BoLIC_Sevika();
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

                LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";     

                string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];

                TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

                bindcmb();

                String cdpo = " select cdpocode from corpinfo where bitid=" + Session["GrdLevel"];

                DataTable Tblcdpo = (DataTable)MstMethods.Query2DataTable.GetResult(cdpo);
                if (Tblcdpo.Rows.Count > 0)
                {
                    ViewState["CDPO"] = Tblcdpo.Rows[0]["cdpocode"].ToString();
                }

                if (Request.QueryString["@"] == "1")
                {
                    Session["Mode"] = 1;
                    ddlAnganID.Focus();
                    MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
                }
                else
                {
                    Session["Mode"] = 2;
                    MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + Session["GrdLevel"].ToString() + "' order by trim(var_angnwadimst_angnname)", "");
                    ddlAnganID.Focus();
                    grdSavikaList_set();

                    UserLevel = Session["brcategory"].ToString();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);

                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            CallProc();
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Transaction/FrmLICSevikaList.aspx");
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            String qry = "select var_bankbranch_branchname || '    ' || var_bankbranch_ifsccode,num_bankbranch_branchid from aoup_bankbranch_def ";
            qry += " where num_bankbranch_bankid=" + ddlBank.SelectedValue;
            MstMethods.Dropdown.Fill(ddlBranch, "", "", "", "", qry);

            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify();</script>", false);
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

                //txtRetireDt.Text = lstdate;
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

            if (dtJoinDate.Text == "")
            {
                MessageAlert("Please select Join Date ", "");
                dtJoinDate.Focus();
                return;
            }

            //if (dtRetirement.Text == "")
            //{
            //    MessageAlert("Please select Retirement Date ", "");
            //    dtRetirement.Focus();
            //    return;
            //}

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
            if (rdbActive.SelectedValue == "")
            {
                MessageAlert(" Please Select Designation ", "");
                rdbActive.Focus();
                return;
            }

            if (ddlBank.SelectedValue == "")
            {
                MessageAlert(" Please Select Bank ", "");
                ddlBank.Focus();
                return;
            }
            //if (ddlBranch.SelectedValue == "")
            //{
            //    MessageAlert(" Please Select Branch ", "");
            //    ddlBranch.Focus();
            //    return;
            //}
            if (TxtAccNO.Text == "")
            {
                MessageAlert(" Please Enter Account No. ", "");
                TxtAccNO.Focus();
                return;
            }

            if (rdbactivesevika.SelectedValue == "N")
            {
                if (rdbactivesevika.SelectedValue == "0")
                {
                    MessageAlert(" Please Select Remark ", "");
                    rdbactivesevika.Focus();
                    return;
                }
            }

            if (DtExitDT.Text == "")
            {
                MessageAlert(" Please select Exit Date ", "");
                DtExitDT.Focus();
                return;
            }

            //if (txtSoftwareNo.Text == "")
            //{
            //    MessageAlert(" Please Enter Old Training Software No. ", "");
            //    txtSoftwareNo.Focus();
            //    return;
            //}

            if (Request.QueryString["@"] == "1")
            {
                string Query = "";
                Query += " select * from aoup_licsevika_def ";
                Query += " where num_licsevika_anganid = '" + ddlAnganID.SelectedValue.ToString() + "' and var_licsevika_name = '" + txtName.Text + "' and var_licsevika_middlename = '" + TxtMidName.Text + "' ";

                DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                if (dtSevikaList.Rows.Count > 0)
                {
                    lblPopupResponse.Text = " सदर सेविका/मदतनीस ह्यांचे एकरक्कमी लाभ मुख्यालयातर्फे सादर करण्यात आला असून, कृपया आयुक्तालय स्तरावर संपर्क साधावा ";
                    popMsg.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify1();</script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowModify1();</script>", false);
            }
        }

        protected void BtnBackTab3_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ShowAdd();</script>", false);
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

            if (DtDob.Text.Trim() != "")
            {
                objSavikaMast.BirthDate = Convert.ToDateTime(DtDob.Value);
            }
            else
            {
                objSavikaMast.BirthDate = DateTime.MinValue;
            }


            objSavikaMast.Address = txtAddr.Text.Trim();
            objSavikaMast.MobileNo = TxtMobNo.Text.Trim();
            objSavikaMast.PhoneNo = txtphone.Text.Trim();
            objSavikaMast.PanNo = "";// TxtPanNo.Text.Trim();
            objSavikaMast.AadharNo = ""; //txtAadharNo.Text.Trim();


            if (ddlBranch.SelectedValue == "")
            {
                objSavikaMast.BranchId = 0;
            }
            else
            {
                objSavikaMast.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            }

            objSavikaMast.AccNo = TxtAccNO.Text.Trim();
            objSavikaMast.Reason = ddlRemark.SelectedValue;
            objSavikaMast.MiddleName = TxtMidName.Text.Trim();
            objSavikaMast.Village = TxtVillage.Text.Trim();
            objSavikaMast.PinCode = TxtPinCode.Text.Trim();

            if (rdbActive.SelectedItem.Selected == true)
            {
                objSavikaMast.DesigID = rdbActive.SelectedItem.Value;
            }

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

            if (dtJoinDate.Text.Trim() != "")
            {
                objSavikaMast.JoinDate = Convert.ToDateTime(dtJoinDate.Value);
            }
            else
            {
                objSavikaMast.JoinDate = DateTime.MinValue;
            }

            //if (dtRetirement.Text.Trim() != "")
            //{
            //    objSavikaMast.RetireDate = Convert.ToDateTime(dtRetirement.Value);
            //}
            //else
            //{
            //    objSavikaMast.RetireDate = DateTime.MinValue;
            //}
            //added by arvind  20/12/2021
          

            if (txtlastSalary.Text == "")
            {
                objSavikaMast.LastSalary = 0;
            }
            else
            {
                objSavikaMast.LastSalary = Convert.ToInt32(txtlastSalary.Text);
            }

            objSavikaMast.Active = rdbactivesevika.SelectedValue;

            if (ViewState["LICDocImg"] == null)
            {
                MessageAlert("Please Upload Document", "");
                return;
            }
            if (DtExitDT.Text.Trim() != "")
            {
                objSavikaMast.ExitDate = Convert.ToDateTime(DtExitDT.Value);
            }
            else
            {
                objSavikaMast.ExitDate = DateTime.MinValue;
            }
            if (txtSoftwareNo.Text == "")
            {
                objSavikaMast.SoftwareNo = null;
            }
            else
            {
                objSavikaMast.SoftwareNo = txtSoftwareNo.Text;
            }
            objSavikaMast.Insert();

            if (objSavikaMast.ErrorCode == -100)
            {
                ViewState["OutSevikaID"] = objSavikaMast.OutSevikaID;

                if (ViewState["LICDocImg"] != null)
                {
                    try
                    {
                        string Query = "";
                        Query += " select num_licsevika_compid,num_licsevika_sevikaid SevikaID,var_licsevika_name from aoup_licsevika_def ";
                        Query += " where img_licsevika_document is null and num_licsevika_sevikaid = '" + Convert.ToInt32(ViewState["OutSevikaID"]) + "' ";

                        DataTable dtSevikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query);

                        if (dtSevikaList.Rows.Count > 0)
                        {
                            dtSevikaList.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
                            dtSevikaList.Rows[0]["ImageByte"] = ViewState["LICDocImg"];
                            ViewState["CurrentTable"] = dtSevikaList;
                        }
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dt = (DataTable)ViewState["CurrentTable"];
                            if (dt.Rows.Count > 0)
                            {
                                AnganwadiLib.Methods.MstMethods.UpdateLICDoc(dt);
                            }
                        }

                        string Query1 = "";
                        Query1 += "  select num_lic_compid,num_lic_sevikaid SevikaID from aoup_lic_def where var_lic_flag_insert = 'E' and img_lic_document is null and num_lic_sevikaid = '" + Convert.ToInt32(ViewState["OutSevikaID"]) + "' ";

                        DataTable dtSevikaList1 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(Query1);

                        if (dtSevikaList1.Rows.Count > 0)
                        {
                            dtSevikaList1.Columns.Add(new DataColumn("ImageByte", typeof(Byte[])));
                            dtSevikaList1.Rows[0]["ImageByte"] = ViewState["LICDocImg"];
                            ViewState["CurrentTableEx"] = dtSevikaList1;
                        }
                        if (ViewState["CurrentTableEx"] != null)
                        {
                            DataTable dt = (DataTable)ViewState["CurrentTableEx"];
                            if (dt.Rows.Count > 0)
                            {
                                AnganwadiLib.Methods.MstMethods.UpdateDocDetails(dt);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }

                MessageAlert(objSavikaMast.ErrorMessage, "../Transaction/FrmLICSevikaList.aspx");
                return;
            }
            else
            {
                MessageAlert(objSavikaMast.ErrorMessage, "");
                return;
            }

        }

        private void bindcmb()
        {
            MstMethods.Dropdown.Fill(ddlBank, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
            MstMethods.Dropdown.Fill(ddlrelation1, "aoup_relation_def", "var_relation_relation", "num_relation_relationid", "", "");
            MstMethods.Dropdown.Fill(ddlrelation2, "aoup_relation_def", "var_relation_relation", "num_relation_relationid", "", "");
            MstMethods.Dropdown.Fill(ddlrelation3, "aoup_relation_def", "var_relation_relation", "num_relation_relationid", "", "");
            MstMethods.Dropdown.Fill(ddlBankN1, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
            MstMethods.Dropdown.Fill(ddlBankN2, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
            MstMethods.Dropdown.Fill(ddlBankN3, "aoup_bank_def", "var_bank_bankname", "num_bank_bankid", "", "");
        }

        private void grdSavikaList_set()
        {
            #region "Load Grid"

            String query = " SELECT a.num_licsevika_sevikaid,a.num_licsevika_anganid, a.var_licsevika_name, a.date_licsevika_birthdate,a.var_licsevika_address, ";
            query += " a.num_licsevika_mobileno, a.var_licsevika_phoneno,num_bank_bankid, b.num_bankbranch_branchid,  b.var_bankbranch_ifsccode, ";
            query += " a.var_licsevika_panno, a.var_licsevika_aadharno,b.var_bankbranch_branchname branchnm, a.num_licsevika_branchid,a.var_licsevika_desigcode, ";
            query += " a.var_licsevika_accno, a.var_licsevika_remark,a.var_licsevika_middlename, a.var_licsevika_village, a.var_licsevika_pincode, ";
            query += " k.var_licsevikanomi_nom1name,k.var_licsevikanomi_nom1relaid,k.var_licsevikanomi_nom1age,k.var_licsevikanomi_nom1address, ";
            query += " k.var_licsevikanomi_nom2name,k.var_licsevikanomi_nom2relaid,k.var_licsevikanomi_nom2age,k.var_licsevikanomi_nom2address, ";
            query += " k.var_licsevikanomi_nom3name, k.var_licsevikanomi_nom3relaid,k.var_licsevikanomi_nom3age, k.var_licsevikanomi_nom3address, ";
            query += " br1.num_bankbranch_bankid N1BankId,br2.num_bankbranch_bankid N2BankId,br3.num_bankbranch_bankid N3BankId,k.num_licsevikanomi_nom1brid, ";
            query += " br1.var_bankbranch_branchname N1Branch,br1.var_bankbranch_ifsccode N1ifsccode,k.num_licsevikanomi_nom1accno,k.num_licsevikanomi_nom1ratio, ";
            query += " k.num_licsevikanomi_nom2brid,br2.var_bankbranch_branchname N2Branch,br2.var_bankbranch_ifsccode N2ifsccode,k.num_licsevikanomi_nom2accno, ";
            query += " k.num_licsevikanomi_nom2ratio,k.num_licsevikanomi_nom3brid,br3.var_bankbranch_branchname N3Branch,br3.var_bankbranch_ifsccode N3ifsccode, ";
            query += " k.num_licsevikanomi_nom3accno,k.num_licsevikanomi_nom3ratio,a.date_licsevika_joindate, a.date_licsevika_retirementdate, a.num_licsevika_lastsalary, ";
            query += " a.var_licsevika_active,a.date_licsevika_exitdate,a.VAR_LICSEVIKA_OLDTSOFTWARENO  ";
            query += " FROM aoup_LICsevika_def a  left join aoup_bankbranch_def b on a.num_licsevika_branchid=b.num_bankbranch_branchid ";
            query += " left join aoup_bank_def g on b.num_bankbranch_bankid=g.num_bank_bankid ";
            query += " left join aoup_licsevikanomi_def k on a.num_licsevika_sevikaid=k.num_licsevikanomi_sevikaid  ";

            query += " left join aoup_bankbranch_def br1 on  br1.num_bankbranch_branchid=k.num_licsevikanomi_nom1brid ";
            query += " left join aoup_bankbranch_def br2 on  br2.num_bankbranch_branchid=k.num_licsevikanomi_nom2brid ";
            query += " left join aoup_bankbranch_def br3 on  br3.num_bankbranch_branchid=k.num_licsevikanomi_nom3brid ";
            query += " where num_licsevika_compid='" + Session["GrdLevel"].ToString() + "' and num_licsevika_sevikaid='" + Session["SevikaId"] + "'";

            DataTable dtSavikaList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtSavikaList.Rows.Count > 0)
            {
                if (dtSavikaList.Rows[0]["num_licsevika_sevikaid"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevika_sevikaid"].ToString() != null)
                {
                    txtSevikaId.Text = dtSavikaList.Rows[0]["num_licsevika_sevikaid"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_licsevika_anganid"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevika_anganid"].ToString() != null)
                {
                    ddlAnganID.SelectedValue = dtSavikaList.Rows[0]["num_licsevika_anganid"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_name"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_name"].ToString() != null)
                {
                    txtName.Text = dtSavikaList.Rows[0]["var_licsevika_name"].ToString();
                }

                if (dtSavikaList.Rows[0]["date_licsevika_birthdate"].ToString() != "" && dtSavikaList.Rows[0]["date_licsevika_birthdate"].ToString() != null)
                {
                    DtDob.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_licsevika_birthdate"]).ToString("dd/MM/yyyy");
                }

                if (dtSavikaList.Rows[0]["var_licsevika_address"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_address"].ToString() != null)
                {
                    txtAddr.Text = dtSavikaList.Rows[0]["var_licsevika_address"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_licsevika_mobileno"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevika_mobileno"].ToString() != null)
                {
                    TxtMobNo.Text = dtSavikaList.Rows[0]["num_licsevika_mobileno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_phoneno"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_phoneno"].ToString() != null)
                {
                    txtphone.Text = dtSavikaList.Rows[0]["var_licsevika_phoneno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_panno"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_panno"].ToString() != null)
                {
                    //TxtPanNo.Text = dtSavikaList.Rows[0]["var_sevikamaster_panno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_aadharno"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_aadharno"].ToString() != null)
                {
                    //txtAadharNo.Text = dtSavikaList.Rows[0]["var_sevikamaster_aadharno"].ToString();
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
                if (dtSavikaList.Rows[0]["var_licsevika_accno"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_accno"].ToString() != null)
                {
                    TxtAccNO.Text = dtSavikaList.Rows[0]["var_licsevika_accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_desigcode"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_desigcode"].ToString() != null)
                {
                    rdbActive.SelectedValue = dtSavikaList.Rows[0]["var_licsevika_desigcode"].ToString();

                }

                if (dtSavikaList.Rows[0]["var_licsevika_middlename"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_middlename"].ToString() != null)
                {
                    TxtMidName.Text = dtSavikaList.Rows[0]["var_licsevika_middlename"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_village"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_village"].ToString() != null)
                {
                    TxtVillage.Text = dtSavikaList.Rows[0]["var_licsevika_village"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevika_pincode"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_pincode"].ToString() != null)
                {
                    TxtPinCode.Text = dtSavikaList.Rows[0]["var_licsevika_pincode"].ToString();
                }

                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom1name"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom1name"].ToString() != null)
                {
                    txtNomNM1.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom1name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom1age"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom1age"].ToString() != null)
                {
                    txtNomAge1.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom1age"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom1address"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom1address"].ToString() != null)
                {
                    txtAddress1.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom1address"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom1relaid"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom1relaid"].ToString() != null)
                {
                    ddlrelation1.SelectedValue = dtSavikaList.Rows[0]["var_licsevikanomi_nom1relaid"].ToString();
                }
                else
                {
                    ddlrelation1.SelectedValue = "0";
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom2name"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom2name"].ToString() != null)
                {
                    txtNomNM2.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom2name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom2age"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom2age"].ToString() != null)
                {
                    txtNomAge2.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom2age"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom2address"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom2address"].ToString() != null)
                {
                    txtAddress2.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom2address"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom2relaid"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom2relaid"].ToString() != null)
                {
                    ddlrelation2.SelectedValue = dtSavikaList.Rows[0]["var_licsevikanomi_nom2relaid"].ToString();
                }
                else
                {
                    ddlrelation2.SelectedValue = "0";
                }

                if (dtSavikaList.Rows[0]["var_bankbranch_ifsccode"].ToString() != "")
                {
                    txtIFSC.Text = dtSavikaList.Rows[0]["var_bankbranch_ifsccode"].ToString();
                }

                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom3name"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom3name"].ToString() != null)
                {
                    txtNomNM3.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom3name"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom3age"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom3age"].ToString() != null)
                {
                    txtNomAge3.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom3age"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom3address"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom3address"].ToString() != null)
                {
                    txtAddress3.Text = dtSavikaList.Rows[0]["var_licsevikanomi_nom3address"].ToString();
                }
                if (dtSavikaList.Rows[0]["var_licsevikanomi_nom3relaid"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevikanomi_nom3relaid"].ToString() != null)
                {
                    ddlrelation3.SelectedValue = dtSavikaList.Rows[0]["var_licsevikanomi_nom3relaid"].ToString();
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
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom1brid"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom1brid"].ToString() != null)
                {
                    ddlBranchN1.SelectedValue = dtSavikaList.Rows[0]["num_licsevikanomi_nom1brid"].ToString();
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
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom1accno"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom1accno"].ToString() != null)
                {
                    TxtAccNON1.Text = dtSavikaList.Rows[0]["num_licsevikanomi_nom1accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom1ratio"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom1ratio"].ToString() != null)
                {
                    TxtRatioN1.Text = dtSavikaList.Rows[0]["num_licsevikanomi_nom1ratio"].ToString();
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
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom2brid"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom2brid"].ToString() != null)
                {
                    ddlBranchN2.SelectedValue = dtSavikaList.Rows[0]["num_licsevikanomi_nom2brid"].ToString();
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
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom2accno"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom2accno"].ToString() != null)
                {
                    TxtAccNON2.Text = dtSavikaList.Rows[0]["num_licsevikanomi_nom2accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom2ratio"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom2ratio"].ToString() != null)
                {
                    TxtRatioN2.Text = dtSavikaList.Rows[0]["num_licsevikanomi_nom2ratio"].ToString();
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
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom3brid"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom3brid"].ToString() != null)
                {
                    ddlBranchN3.SelectedValue = dtSavikaList.Rows[0]["num_licsevikanomi_nom3brid"].ToString();
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
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom3accno"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom3accno"].ToString() != null)
                {
                    TxtAccNON3.Text = dtSavikaList.Rows[0]["num_licsevikanomi_nom3accno"].ToString();
                }
                if (dtSavikaList.Rows[0]["num_licsevikanomi_nom3ratio"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevikanomi_nom3ratio"].ToString() != null)
                {
                    TxtRatioN3.Text = dtSavikaList.Rows[0]["num_licsevikanomi_nom3ratio"].ToString();
                }

                if (dtSavikaList.Rows[0]["date_licsevika_joindate"].ToString() != "" && dtSavikaList.Rows[0]["date_licsevika_joindate"].ToString() != null)
                {
                    dtJoinDate.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_licsevika_joindate"]).ToString("dd/MM/yyyy");
                }

                //if (dtSavikaList.Rows[0]["date_licsevika_retirementdate"].ToString() != "" && dtSavikaList.Rows[0]["date_licsevika_retirementdate"].ToString() != null)
                //{
                //    dtRetirement.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_licsevika_retirementdate"]).ToString("dd/MM/yyyy");
                //}

                if (dtSavikaList.Rows[0]["num_licsevika_lastsalary"].ToString() != "" && dtSavikaList.Rows[0]["num_licsevika_lastsalary"].ToString() != null)
                {
                    txtlastSalary.Text = dtSavikaList.Rows[0]["num_licsevika_lastsalary"].ToString();
                }

                if (dtSavikaList.Rows[0]["var_licsevika_active"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_active"].ToString() != null)
                {
                    rdbactivesevika.SelectedValue = dtSavikaList.Rows[0]["var_licsevika_active"].ToString();
                }

                if (dtSavikaList.Rows[0]["var_licsevika_remark"].ToString() != "" && dtSavikaList.Rows[0]["var_licsevika_remark"].ToString() != null)
                {
                    ddlRemark.SelectedValue = dtSavikaList.Rows[0]["var_licsevika_remark"].ToString();
                }

                if (dtSavikaList.Rows[0]["date_licsevika_exitdate"].ToString() != "" && dtSavikaList.Rows[0]["date_licsevika_exitdate"].ToString() != null)
                {
                    DtExitDT.Text = Convert.ToDateTime(dtSavikaList.Rows[0]["date_licsevika_exitdate"]).ToString("dd/MM/yyyy");
                }

                if (dtSavikaList.Rows[0]["VAR_LICSEVIKA_OLDTSOFTWARENO"].ToString() != "" && dtSavikaList.Rows[0]["VAR_LICSEVIKA_OLDTSOFTWARENO"].ToString() != null)
                {
                    txtSoftwareNo.Text = dtSavikaList.Rows[0]["VAR_LICSEVIKA_OLDTSOFTWARENO"].ToString();
                }
            }
            #endregion
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

        public void BtnUpload_Click(object sender, EventArgs e)
        {
            string strFileName = Path.GetFileName(FileUploadDoc.PostedFile.FileName);
            string strExtension = Path.GetExtension(strFileName);
            if (FileUploadDoc.PostedFile.FileName.Length > 0)
            {
                //if (strExtension == ".jpeg" || strExtension == ".jpg" || strExtension == ".png")
                if (strExtension == ".PDF" || strExtension == ".pdf")
                {
                    HttpPostedFile PFileThumb = FileUploadDoc.PostedFile;
                    int lenthThumb = PFileThumb.ContentLength;
                    int KB = (lenthThumb / 2048000) + 1;

                    if (KB > 300)
                    {
                        MessageAlert("Document size can not be more than 2MB", "");
                        return;
                    }
                    Byte[] PropimageBytes = new byte[lenthThumb];
                    PFileThumb.InputStream.Read(PropimageBytes, 0, lenthThumb);

                    String strFilenameoc = Path.GetFileName(PFileThumb.FileName);
                    strFilenameoc = "LICSevika_" + System.DateTime.Now.Date.ToString("ddMMyyyymmhhss") + "_" + strFilenameoc;
                    String filePath = Server.MapPath("..\\ImageGarbage\\") + strFilenameoc;
                    FileUploadDoc.SaveAs(filePath);
                    ViewState["LICDocImg"] = PropimageBytes;
                    Label2.Text = "Upload status: File uploaded.";
                }
                else
                {
                    Label2.Text = "Upload status: only .pdf file are allowed!";
                    return;
                }
            }

        }
    }
}