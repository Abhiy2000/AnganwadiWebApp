using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiWebApp.Master
{
    public partial class FrmBankMaster : System.Web.UI.Page
    {
        AnganwadiLib.Business.BankBrConfig objBankBrConfig = new AnganwadiLib.Business.BankBrConfig();

        Int32 Inmode = 1;
        Int32 BankId = 0;

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
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmBankRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LblGrdHead.Text = "Master" + " >> " + "Bank Master"; //Session["LblGrdHead"].ToString(); //
                txtBankCode.Enabled = false;
                System.Drawing.Color backcolor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                System.Drawing.Color forecolor = System.Drawing.ColorTranslator.FromHtml("#808080");
                txtBankCode.BackColor = backcolor;
                txtBankCode.ForeColor = forecolor;
                if (Request.QueryString["@"] == "1")
                {
                    Inmode = 1;
                    BankId = 0;
                }
                if (Request.QueryString["@"] == "2")
                {
                    Inmode = 2;
                    grdBankList_set();
                    //BankId = Convert.ToInt32(Session["BankId"]);
                    //String str = "select var_bank_bankname from aoup_Bank_def where  num_bank_bankid=" + BankId;

                    //DataTable TblBankId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(str);

                    //if (TblBankId.Rows.Count > 0)
                    //{
                    //    txtBankCode.Text = Session["BankId"].ToString();
                    //    txtBankName.Text = TblBankId.Rows[0]["var_bank_bankname"].ToString();
                    //}
                }
            }              
        }

        private void grdBankList_set()
        {
            #region "Load Grid"

            String query = "select num_bank_bankid bankid,var_bank_bankname bankname from aoup_Bank_def where num_bank_bankid='" + Session["BankId"] + "' order by num_bank_bankid";

           // DataTable dtBankList = (DataTable)HRPayRollClass.Methods.MstMethods.Query2DataTable.GetResult(query);
            DataTable TblBankId = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (TblBankId.Rows.Count > 0)
            {
                if (TblBankId.Rows[0]["bankid"].ToString() != "" && TblBankId.Rows[0]["bankid"].ToString() != null)
                {
                    txtBankCode.Text = TblBankId.Rows[0]["bankid"].ToString();
                }
                if (TblBankId.Rows[0]["bankname"].ToString() != "" && TblBankId.Rows[0]["bankname"].ToString() != null)
                {
                    txtBankName.Text = TblBankId.Rows[0]["bankname"].ToString();
                }
            }
            #endregion
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (txtBankName.Text == "")
            {
                MessageAlert("Bank Name cannot be blank", "");
                return;
            }
         
            if (Request.QueryString["@"] == "1")
            {
                objBankBrConfig.Mode = 1;
                objBankBrConfig.BankId = BankId;
            }
            else
            {
                objBankBrConfig.Mode = 2;
                objBankBrConfig.BankId = Convert.ToInt32(txtBankCode.Text.Trim());
            }
            objBankBrConfig.UserId = Session["UserId"].ToString();
            objBankBrConfig.Bank = txtBankName.Text.Trim();
            objBankBrConfig.Insert();

            if (objBankBrConfig.ErrCode == -100)
            {
                MessageAlert(objBankBrConfig.ErrMsg, "../Master/FrmBankList.aspx");
                return;
            }
            else
            {
                MessageAlert(objBankBrConfig.ErrMsg, "");
                return;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmBankList.aspx");
        }
    }
}