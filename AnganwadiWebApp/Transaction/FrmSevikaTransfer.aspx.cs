using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Methods;
using System.Data;
using AnganwadiLib.Business;
using System.Drawing;

namespace AnganwadiWebApp.Transaction
{
    public partial class FrmSevikaTransfer : System.Web.UI.Page
    {
        BoTransferSevika objTrfSev = new BoTransferSevika();

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

            LblGrdHead.Text = Session["LblGrdHead"].ToString();// "Savika Master";           

            if (!IsPostBack)
            {
                trAadhar.Visible = true;
                PnlDetail.Visible = false;

                String str = "select brcategory, parentid from companyview where brid = " + Session["GrdLevel"].ToString();
                DataTable TblResult = (DataTable)MstMethods.Query2DataTable.GetResult(str);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAadharNo.Text == "")
            {
                MessageAlert("Please Enter Aadhar No", "");
                return;
            }
            if (txtAadharNo.Text != "")
            {
                if (txtAadharNo.Text.Length != 12)
                {
                    MessageAlert("Aadhar no is Invalid", "");
                    return;
                }
                Aadhar_Chk();
            }
            try
            {
                string GetHOid = "select hoid from companyview where brid=" + Session["GrdLevel"];

                DataTable TblGetHOid = (DataTable)MstMethods.Query2DataTable.GetResult(GetHOid);

                String det = " select num_sevikamaster_sevikaid,var_sevikamaster_name,var_designation_desig,var_payscal_payscal,a.divname OldDiv,a.distname OldDist, ";
                det += " a.cdponame OldCdpo,a.bitbitname OldBit, ";
                det += " b.divname NewDiv,b.distname NewDist,b.cdponame NewCdpo,b.bitbitname NewBit, ";
                det += " var_angnwadimst_angnname,a.bitid OldBitId,b.bitid NewBitId,num_sevakmaster_anganid from aoup_sevikamaster_def ";
                det += " inner join corpinfo a on a.bitid=num_sevikamaster_compid inner join corpinfo b on b.bitid=" + Session["GrdLevel"];
                det += " left join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                det += " left join aoup_payscal_def on num_payscal_compid=a.stateid and num_payscal_payscalid=num_sevikamaster_payscalid ";
                det += " left join aoup_angnwadimst_def on num_angnwadimst_compid=num_sevikamaster_compid and num_angnwadimst_angnid=num_sevakmaster_anganid ";
                det += " where a.stateid=" + TblGetHOid.Rows[0]["hoid"].ToString();
                det += " and var_sevikamaster_aadharno='" + txtAadharNo.Text.Trim() + "'";

                DataTable dtInfo = (DataTable)MstMethods.Query2DataTable.GetResult(det);

                if (dtInfo.Rows.Count > 0)
                {
                    PnlDetail.Visible = true;
                    txtSevikaName.Text = dtInfo.Rows[0]["var_sevikamaster_name"].ToString();
                    txtDesg.Text = dtInfo.Rows[0]["var_designation_desig"].ToString();
                    txtPayscale.Text = dtInfo.Rows[0]["var_payscal_payscal"].ToString();
                    txtCurrDiv.Text = dtInfo.Rows[0]["OldDiv"].ToString();
                    txtCurrDist.Text = dtInfo.Rows[0]["OldDist"].ToString();
                    txtCurrCDPO.Text = dtInfo.Rows[0]["OldCdpo"].ToString();
                    txtCurrBIT.Text = dtInfo.Rows[0]["OldBit"].ToString();
                    txtCurrAnganwadi.Text = dtInfo.Rows[0]["var_angnwadimst_angnname"].ToString();

                    txtDiv.Text = dtInfo.Rows[0]["NewDiv"].ToString();
                    txtDist.Text = dtInfo.Rows[0]["NewDist"].ToString();
                    txtCDPO.Text = dtInfo.Rows[0]["NewCdpo"].ToString();
                    txtBIT.Text = dtInfo.Rows[0]["NewBit"].ToString();

                    ViewState["Old_BIT_Id"] = dtInfo.Rows[0]["OldBitId"].ToString();
                    ViewState["New_BIT_Id"] = dtInfo.Rows[0]["NewBitId"].ToString();
                    ViewState["ANGAN_Id"] = dtInfo.Rows[0]["num_sevakmaster_anganid"].ToString();
                    ViewState["SEVIKA_ID"] = dtInfo.Rows[0]["num_sevikamaster_sevikaid"].ToString();

                    MstMethods.Dropdown.Fill(ddlNewAnganwadi, "aoup_AngnwadiMst_def", "var_angnwadimst_angnname", "num_angnwadimst_angnid", " num_angnwadimst_compid ='" + ViewState["New_BIT_Id"] + "' order by trim(var_angnwadimst_angnname)", "");
                }
                else
                {
                    MessageAlert("Record Not Found", "");
                    PnlDetail.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlNewAnganwadi.SelectedValue == "")
            {
                MessageAlert("Please select Anganwadi", "");
                return;
            }
            try
            {
                objTrfSev.OldBitId = Convert.ToInt32(ViewState["Old_BIT_Id"].ToString());
                objTrfSev.NewBitId = Convert.ToInt32(ViewState["New_BIT_Id"].ToString());
                objTrfSev.OldAnganwadiId = Convert.ToInt32(ViewState["ANGAN_Id"].ToString());
                objTrfSev.NewAnganwadiId = Convert.ToInt32(ddlNewAnganwadi.SelectedValue);
                objTrfSev.AadharNo = txtAadharNo.Text.Trim();
                objTrfSev.SevikaId = Convert.ToInt32(ViewState["SEVIKA_ID"].ToString());
                objTrfSev.UserId = Session["UserId"].ToString();
                objTrfSev.BoTransferSevika_1();
                if (objTrfSev.ErrorCode == -100)
                {
                    MessageAlert(objTrfSev.ErrMsg, "../Transaction/FrmSevikaTransfer.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objTrfSev.ErrMsg, "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
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
            }
            else
            {
                lblmsg.ForeColor = Color.Red;
                lblmsg.Text = "Invalid";
                return;
            }

            ////-----------------
        }
    }
}