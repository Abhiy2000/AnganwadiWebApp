using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmAdditionalChargePay : System.Web.UI.Page
    {
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
                divResult.Visible = false;
                //dtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //dtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                FetchRecord("", txtAdharNo.Text);
            }
            else
            {
                MessageAlert("please input Aadhar Number", "");
                txtAdharNo.Focus();
                return;
            }
        }

        protected void FetchRecord(String uniqueId, string Aadharno)
        {
            string str = " SELECT c.cdponame, c.bitid,c.bitbitname, s.var_sevikamaster_name savikaname,s.num_sevikamaster_compid compid, ";
            str += " s.num_sevikamaster_sevikaid sevikaid,s.num_sevakmaster_anganid anganid, ag.var_angnwadimst_angnname angnname FROM corpinfo c ";
            str += " INNER JOIN aoup_sevikamaster_def s ON bitid = num_sevikamaster_compid ";
            str += " inner join aoup_angnwadimst_def ag on num_angnwadimst_angnid = num_sevakmaster_anganid ";
            str += " where var_sevikamaster_aadharno='" + Aadharno + "'";

            DataTable dtTbl2 = new DataTable();
            dtTbl2 = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);


            if (dtTbl2.Rows.Count > 0)
            {
                txtBitCompId.Text = dtTbl2.Rows[0]["bitbitname"].ToString();
                txtCompId.Text = dtTbl2.Rows[0]["compid"].ToString();
                txtCDPO.Text = dtTbl2.Rows[0]["cdponame"].ToString();

                txtName.Text = dtTbl2.Rows[0]["savikaName"].ToString();
                ViewState["AnganwadiID"] = dtTbl2.Rows[0]["anganid"].ToString();
                txtExistAnganwadi.Text = dtTbl2.Rows[0]["angnname"].ToString();
                hdnSavikaId.Value = dtTbl2.Rows[0]["SevikaId"].ToString();
                string bitid = dtTbl2.Rows[0]["bitid"].ToString();

                MstMethods.Dropdown.Fill(ddlAnganID, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + bitid + "' order by trim(var_angnwadimst_angnname)", "");

                divResult.Visible = true;
            }
            else
            {
                divResult.Visible = false;
                MessageAlert("No Record Found", "");
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAnganID.SelectedIndex == 0)
                {
                    MessageAlert("Please Select Anganwadi", "");
                    ddlAnganID.Focus();
                    return;
                }

                if (dtFrom.Text == "")
                {
                    MessageAlert("Please Select From Date", "");
                    dtFrom.Focus();
                    return;
                }

                if (dtTo.Text == "")
                {
                    MessageAlert("Please Select To Date", "");
                    dtTo.Focus();
                    return;
                }

                if (dtFrom.Value > dtTo.Value)
                {
                    MessageAlert("from Date can't be greater than To Date", "");
                    return;
                }

                var frmdate = Convert.ToDateTime(Convert.ToDateTime(dtFrom.Value));
                var todate = Convert.ToDateTime(Convert.ToDateTime(dtTo.Value));

                int months = ((todate.Year * 12) + todate.Month) - ((frmdate.Year * 12) + frmdate.Month);

                // int monthDiference = Convert.ToDateTime(dtFrom.Value).Month - Convert.ToDateTime(dtTo.Value).Month;
                if (!(months <= 5 && months >= 0)) //if month difference is not minimum 1 and max is 6
                {
                    MessageAlert("Min Month Difference is 1 & Max 6", "");
                    dtFrom.Focus();
                    return;
                }

                if (txtReason.Text == "")
                {
                    MessageAlert("Please Enter Reason", "");
                    txtReason.Focus();
                    return;
                }

                AnganwadiLib.Business.BoAdditionalCharge objAdditionalCharge;
                objAdditionalCharge = new AnganwadiLib.Business.BoAdditionalCharge();

                objAdditionalCharge.UserName = Session["UserId"].ToString();
                objAdditionalCharge.COMPID = Convert.ToInt32(txtCompId.Text.Trim());
                objAdditionalCharge.BITCODE = txtBitCompId.Text;
                objAdditionalCharge.ANGANID = Convert.ToInt32(ViewState["AnganwadiID"]);
                objAdditionalCharge.SEVIKAID = Convert.ToInt32(hdnSavikaId.Value);
                objAdditionalCharge.NEWANGANID = Convert.ToInt32(ddlAnganID.SelectedValue);
                objAdditionalCharge.FROMDATE = Convert.ToDateTime(dtFrom.Value);
                objAdditionalCharge.TODATE = Convert.ToDateTime(dtTo.Value);
                objAdditionalCharge.REASON = txtReason.Text;

                objAdditionalCharge.Insert();

                if (objAdditionalCharge.ErrorCode == -100)
                {
                    MessageAlert(objAdditionalCharge.ErrorMessage, "../Transaction/FrmAdditionalChargePay.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objAdditionalCharge.ErrorCode + objAdditionalCharge.ErrorMessage, "");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage/FrmDashboard.aspx");
        }
    }
}