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
    public partial class FrmTransferAuth : System.Web.UI.Page
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
                setInfo();
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtRemark.Text == "")
            {
                MessageAlert("Please Enter Remark", "");
                return;
            }
            if (txtRemark.Text != "")
            {
                if (txtRemark.Text.Length > 100)
                {
                    MessageAlert("Remark length should be 100 Characters", "");
                    return;
                }
            }
            objTrfSev.Status = "A";
            CallProc();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtRemark.Text == "")
            {
                MessageAlert("Please Enter Remark", "");
                return;
            }
            if (txtRemark.Text != "")
            {
                if (txtRemark.Text.Length > 100)
                {
                    MessageAlert("Remark length should be 100 Characters", "");
                    return;
                }
            }
            objTrfSev.Status = "R";
            CallProc();
        }

        public void setInfo()
        {
            try
            {
                String det = " select num_sevikamaster_sevikaid,var_sevikamaster_name,var_designation_desig,var_payscal_payscal,a.divname OldDiv,a.distname OldDist, ";
                det += " a.cdponame OldCdpo,a.bitbitname OldBit, ";
                det += " b.divname NewDiv,b.distname NewDist,b.cdponame NewCdpo,b.bitbitname NewBit, ";
                det += " c.var_angnwadimst_angnname OldAngan,d.var_angnwadimst_angnname NewAngan,num_transfer_oldbitid,num_transfer_newbitid,num_sevakmaster_anganid,num_transfer_newanganwadiid,num_transfer_oldanganwadiid from aoup_transfer_det ";
                det += " inner Join aoup_sevikamaster_def on num_sevikamaster_sevikaid=num_transfer_sevikaid and var_sevikamaster_aadharno=var_transfer_aadharno ";
                det += " inner join corpinfo a on num_transfer_oldbitid=a.bitid inner join corpinfo b on b.bitid=num_transfer_newbitid ";
                det += " left join aoup_designation_def on num_designation_desigid=num_sevikamaster_desigid ";
                det += " left join aoup_payscal_def on num_payscal_compid=a.stateid and num_payscal_payscalid=num_sevikamaster_payscalid ";
                det += " left join aoup_angnwadimst_def c on c.num_angnwadimst_compid=num_transfer_oldbitid and num_transfer_oldanganwadiid=c.num_angnwadimst_angnid ";
                det += " left join aoup_angnwadimst_def d on d.num_angnwadimst_compid=num_transfer_newbitid and num_transfer_newanganwadiid=d.num_angnwadimst_angnid ";
                det += " where var_sevikamaster_aadharno='" + Session["AadharNo"] + "' and var_transfer_status = 'P' and b.cdpoid=" + Session["GrdLevel"];
                det += " and TRUNC(dat_transfer_transferdate)=TO_DATE('" + Session["TransferDate"] + "','dd-mm-yyyy') ";

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
                    txtCurrAnganwadi.Text = dtInfo.Rows[0]["OldAngan"].ToString();

                    txtDiv.Text = dtInfo.Rows[0]["NewDiv"].ToString();
                    txtDist.Text = dtInfo.Rows[0]["NewDist"].ToString();
                    txtCDPO.Text = dtInfo.Rows[0]["NewCdpo"].ToString();
                    txtBIT.Text = dtInfo.Rows[0]["NewBit"].ToString();
                    txtNewAnganwadi.Text = dtInfo.Rows[0]["NewAngan"].ToString();
                    txtAadharNo.Text = Session["AadharNo"].ToString();

                    ViewState["Old_BIT_Id"] = dtInfo.Rows[0]["num_transfer_oldbitid"].ToString();
                    ViewState["New_BIT_Id"] = dtInfo.Rows[0]["num_transfer_newbitid"].ToString();
                    ViewState["Old_ANGAN_Id"] = dtInfo.Rows[0]["num_transfer_oldanganwadiid"].ToString();
                    ViewState["New_ANGAN_Id"] = dtInfo.Rows[0]["num_transfer_newanganwadiid"].ToString();
                    ViewState["SEVIKA_ID"] = dtInfo.Rows[0]["num_sevikamaster_sevikaid"].ToString();
                }
                else
                {
                    PnlDetail.Visible = false;
                    MessageAlert("Record Not Found", "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
        }

        public void CallProc()
        {
            try
            {
                objTrfSev.OldBitId = Convert.ToInt32(ViewState["Old_BIT_Id"].ToString());
                objTrfSev.NewBitId = Convert.ToInt32(ViewState["New_BIT_Id"].ToString());
                objTrfSev.OldAnganwadiId = Convert.ToInt32(ViewState["Old_ANGAN_Id"].ToString());
                objTrfSev.NewAnganwadiId = Convert.ToInt32(ViewState["New_ANGAN_Id"].ToString());
                objTrfSev.AadharNo = Session["AadharNo"].ToString();
                objTrfSev.SevikaId = Convert.ToInt32(ViewState["SEVIKA_ID"].ToString());
                objTrfSev.TransferDate = Session["TransferDate"].ToString();
                objTrfSev.Remark = txtRemark.Text.Trim();
                objTrfSev.UserId = Session["UserId"].ToString();
                objTrfSev.Authorized();
                if (objTrfSev.ErrorCode == -100)
                {
                    MessageAlert(objTrfSev.ErrMsg, "../Transaction/FrmTransferAuthList.aspx");
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
    }
}