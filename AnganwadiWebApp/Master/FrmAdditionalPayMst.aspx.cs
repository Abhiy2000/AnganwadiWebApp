using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace AnganwadiWebApp.Master
{
    public partial class FrmAdditionalPayMst : System.Web.UI.Page
    {
        AnganwadiLib.Business.BoAdditionalPay objBoAdditionalPay;
        int Mode = 1;
        String Status = "A";
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
                LblGrdHead.Text = Session["LblGrdHead"].ToString();//"Aganwadi Master"; //

                dtFrom.PostBackControl();
                CheckBoxChangeEvent();

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
                            dtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            dtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            SetFirstDateAndLastDate();
                            Mode = 1;
                        }
                        else if (Request.QueryString["@"].ToString() == "2")
                        {
                            Mode = 2;
                            if (!String.IsNullOrEmpty(Session["UniqueId"].ToString()) && !String.IsNullOrEmpty(Session["AadharNo"].ToString()))
                            {
                                EnableDisableConrols('D');
                                btnSearch.Enabled = false;
                                FetchRecord(Session["UniqueId"].ToString(), Session["AadharNo"].ToString());
                                btnSubmit.Text = "Update";
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/master/FrmAdditionalPayList.aspx");
                }
            }
            Control c = GetPostBackControl(this.Page);

            if (c != null)
            {
                if (c.ClientID == "ContentPlaceHolder1_dtFrom_txtDate")
                {
                    chkMaternity_CheckedChanged(null, null);
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

        private void SetFirstDateAndLastDate()
        {
            DateTime dtFromFirstDay = Convert.ToDateTime(dtTo.Value);
            DateTime FirstdtOfMonth = (new DateTime(dtFromFirstDay.Year, dtFromFirstDay.Month, 1)).Date;
            dtFrom.Text = FirstdtOfMonth.ToString("dd/MM/yyyy");
            DateTime sixMonthAfterFromdt = Convert.ToDateTime(dtFrom.Value);
            //DateTime afterSixMonth = (new DateTime(sixMonthAfterFromdt.Year, sixMonthAfterFromdt.Month, 1).AddMonths(1).AddDays(-1)).Date;
            DateTime afterSixMonth = (new DateTime(sixMonthAfterFromdt.Year, sixMonthAfterFromdt.Month, 1).AddDays(180)).Date;
            dtTo.Text = afterSixMonth.ToString("dd/MM/yyyy");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAdharNo.Text != "")
            {
                FetchRecord("", txtAdharNo.Text);
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

            if (dtFrom.Value > dtTo.Value)
            {
                MessageAlert("from Date can't be greater than To Date", "");
                return;
            }

            if (chkAdditionalPay.Checked != false || chkMaternity.Checked != false || chkTribal.Checked != false)
            {

                if (chkMaternity.Checked == true)
                {
                    DateTime dtFromFirstDay = Convert.ToDateTime(dtTo.Value);
                    int Firstdt = (new DateTime(dtFromFirstDay.Year, dtFromFirstDay.Month, 1)).Day;

                    DateTime dtToLastDay = Convert.ToDateTime(dtTo.Value);
                    int maxdt = (new DateTime(dtToLastDay.Year, dtToLastDay.Month, 1).AddMonths(1).AddDays(-1)).Day;

                    if (Convert.ToInt32(Convert.ToDateTime(dtFrom.Value).ToString("dd")) != 1)
                    {
                        MessageAlert("From Date must be start or first date of the month", "");
                        dtFrom.Focus();
                        return;
                    }

                    //if (Convert.ToInt32(Convert.ToDateTime(dtTo.Value).ToString("dd")) != maxdt)
                    //{
                    //    MessageAlert("To Date must be End date of the month", "");
                    //    dtTo.Focus();
                    //    return;
                    //}
                }

                if (chkAdditionalPay.Checked == true)
                {
                    //int monthDiference = Convert.ToDateTime(dtFrom.Value).Month - Convert.ToDateTime(dtTo.Value).Month;
                    //if (!(monthDiference <= 5 && monthDiference >= 0)) //if month difference is not minimum 1 and max is 6
                    //{

                    var frmdate = Convert.ToDateTime(Convert.ToDateTime(dtFrom.Value));
                    var todate = Convert.ToDateTime(Convert.ToDateTime(dtTo.Value));

                    int months = ((todate.Year * 12) + todate.Month) - ((frmdate.Year * 12) + frmdate.Month);


                   // int monthDiference = Convert.ToDateTime(dtFrom.Value).Month - Convert.ToDateTime(dtTo.Value).Month;
                    if (!(months <= 5 && months >= 0)) //if month difference is not minimum 1 and max is 6
                    {
                        MessageAlert("For AdditionalPay Min Month Diff. is 1 & Max 6", "");
                        dtFrom.Focus();
                        return;
                    }
                }

                String PayType = "";
                if (chkMaternity.Checked == true)
                {
                    PayType = "M";
                }
                else if (chkTribal.Checked == true)
                {
                    PayType = "T";
                }
                else if (chkAdditionalPay.Checked == true)
                {
                    PayType = "A";
                }
                objBoAdditionalPay = new AnganwadiLib.Business.BoAdditionalPay();
                objBoAdditionalPay.COMPID = Convert.ToInt32(txtCompId.Text.Trim());
                objBoAdditionalPay.FROMDATE = Convert.ToDateTime(dtFrom.Value);
                if (Request.QueryString["@"].ToString() == "2" & sender.ToString() != "delete")
                {
                    objBoAdditionalPay.Mode = 2;
                }
                else if (Request.QueryString["@"].ToString() == "1")
                {
                    objBoAdditionalPay.Mode = 1;
                }

                else if (sender.ToString() == "delete")
                {
                    objBoAdditionalPay.Mode = 3;
                }

                if (Request.QueryString["@"].ToString() == "2" || sender.ToString() == "delete")
                {
                    if (Session["UniqueId"].ToString() != null)
                    {
                        objBoAdditionalPay.AddPayUniqID = Convert.ToInt32(Session["UniqueId"].ToString());
                    }
                }
                else
                {
                    objBoAdditionalPay.AddPayUniqID = 0;
                }
                objBoAdditionalPay.SEVIKAID = Convert.ToInt32(hdnSavikaId.Value);
                objBoAdditionalPay.STATUS = Status;
                objBoAdditionalPay.TODATE = Convert.ToDateTime(dtTo.Value); ;
                objBoAdditionalPay.TYPE = PayType;
                objBoAdditionalPay.UserName = Session["UserId"].ToString();
                objBoAdditionalPay.InsertAdditionPay();

                if (objBoAdditionalPay.ErrorCode == -100)
                {
                    MessageAlert(objBoAdditionalPay.ErrorMessage, "../master/FrmAdditionalPayList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objBoAdditionalPay.ErrorCode + objBoAdditionalPay.ErrorMessage, "");
                    return;
                }

            }
            else
            {
                MessageAlert("Please choose one pay Type", "");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }

        protected void chkAdditionalPay_CheckedChanged(object sender, EventArgs e)
        {
            chkTribal.Checked = false;
            chkMaternity.Checked = false;
            CheckBoxChangeEvent();

        }

        protected void chkMaternity_CheckedChanged(object sender, EventArgs e)
        {
            chkTribal.Checked = false;
            chkAdditionalPay.Checked = false;
            CheckBoxChangeEvent();

        }

        protected void chkTribal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTribal.Checked == true)
            {

                chkAdditionalPay.Checked = false;
                chkMaternity.Checked = false;
                dtTo.EnableControl();
            }
        }

        protected void FetchRecord(String uniqueId, string Aadharno)
        {
            string str = " select a.num_addpay_compid,c.divname,c.distname,c.cdponame,c.bitbitname,s.var_sevikamaster_name savikaName, s.num_sevikamaster_compid, s.num_sevikamaster_sevikaid SevikaId,var_sevikamaster_cpsmscode schemeId,a.var_addpay_type pay_type, ";
            str += " a.date_addpay_fromdate fromdate,a.date_addpay_todate todate, a.num_addpay_uniqueid uniqueid  from  corpinfo c ";
            str += " inner join aoup_sevikamaster_def s on bitid=num_sevikamaster_compid  ";
            str += " left join Aoup_AddPay_Def a on a.num_addpay_compid=s.num_sevikamaster_compid and a.num_addpay_sevikaid=s.num_sevikamaster_sevikaid ";
            str += " where var_sevikamaster_aadharno='" + Aadharno + "'";

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
                txtSchemeSpecificId.Text = dtTbl2.Rows[0]["schemeId"].ToString();

                if (Request.QueryString["@"].ToString() == "2")
                {
                    if (dtTbl2.Rows[0]["pay_type"].ToString() == "M")
                    {
                        chkMaternity.Checked = true;
                        dtTo.DisableControl();
                    }
                    else if (dtTbl2.Rows[0]["pay_type"].ToString() == "A")
                    {
                        chkAdditionalPay.Checked = true;
                        dtTo.EnableControl();
                    }
                    else if (dtTbl2.Rows[0]["pay_type"].ToString() == "T")
                    {
                        chkAdditionalPay.Checked = true;
                        dtTo.EnableControl();
                    }
                    dtFrom.Text = Convert.ToDateTime(dtTbl2.Rows[0]["fromdate"]).ToString("dd/MM/yyyy");
                    dtTo.Text = Convert.ToDateTime(dtTbl2.Rows[0]["todate"]).ToString("dd/MM/yyyy");

                }

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
                txtSchemeSpecificId.Enabled = false;

            }
            if (Input == 'E')
            {
                //if any control need to be enabled
            }
        }

        public void CheckBoxChangeEvent()
        {

            if (chkMaternity.Checked == true)
            {
                dtTo.DisableControl();
                DateTime sixMonthAfterFromdt = Convert.ToDateTime(dtFrom.Value);
                //DateTime afterSixMonth = (new DateTime(sixMonthAfterFromdt.Year, sixMonthAfterFromdt.Month, 1).AddMonths(7).AddDays(-1)).Date;
                DateTime afterSixMonth = (new DateTime(sixMonthAfterFromdt.Year, sixMonthAfterFromdt.Month, 1).AddDays(179)).Date;
                dtTo.Text = afterSixMonth.ToString("dd/MM/yyyy");
                chkAdditionalPay.Checked = false;
                chkTribal.Checked = false;
            }
            if (chkAdditionalPay.Checked == true)
            {
                chkMaternity.Checked = false;
                DateTime sixMonthAfterFromdt = Convert.ToDateTime(dtFrom.Value);
                DateTime afterSixMonth = (new DateTime(sixMonthAfterFromdt.Year, sixMonthAfterFromdt.Month, 1).AddMonths(6).AddDays(-1)).Date;
                dtTo.Text = afterSixMonth.ToString("dd/MM/yyyy");
                dtTo.EnableControl();
                chkTribal.Checked = false;

                if (dtFrom.Value > dtTo.Value)
                {
                    MessageAlert("from Date can't be greater than To Date", "");
                    return;
                }

                

                //int daysDiference = Convert.ToDateTime(dtTo.Value).Day - Convert.ToDateTime(dtFrom.Value).Day;
                //if (!(daysDiference <= 180 && daysDiference >= 31)) //if month difference is not minimum 1 and max is 6
                //{
                //int monthDiference = Convert.ToDateTime(dtTo.Value).Month - Convert.ToDateTime(dtFrom.Value).Month;
                //int monthDiference = Convert.ToDateTime(dtTo.Value).Month - Convert.ToDateTime(dtFrom.Value).Month;
                //if (!(monthDiference <= 5 && monthDiference >= 0)) //if month difference is not minimum 1 and max is 6
                //{
                //    SetFirstDateAndLastDate();
                //    MessageAlert("For AdditionalPay Min Month Diff. is 1 & Max 6", "");
                //    dtFrom.Focus();
                //    return;
                //}
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            btnSubmit_Click("delete", null);
        }




    }
}