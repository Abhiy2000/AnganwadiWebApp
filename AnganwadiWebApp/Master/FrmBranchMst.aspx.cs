using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnganwadiLib.Business;
using System.Data;
using AnganwadiLib.Methods;

namespace AnganwadiLib.Masters
{
    public partial class FrmBranchMst : System.Web.UI.Page
    {
        BoBranch objBranch = new BoBranch();

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
            
            String MenuRights = MstMethods.ChkRights(Session["UserId"].ToString(), "../Master/FrmBranchRef.aspx");

            if (MenuRights == "N")
            {
                MessageAlert(" Sorry you have no access for this page ", "../HomePage/FrmDashboard.aspx");
                return;
            }

            LblGrdHead.Text = "Master" + " >> " + "Branch Master";// Session["LblGrdHead"].ToString();
            if (!IsPostBack)
            {
                //if (Session["UserName"] == null)
                //{
                //    Response.Redirect("~/FrmSessionLogOut.aspx?@=2");
                //}
                MstMethods.Dropdown.Fill(ddlBankname, "aoup_Bank_def order by trim(var_bank_bankname)", "var_bank_bankname", "num_bank_bankid ", "", "");
                if (Request.QueryString["@"] == "1")
                {
                    Session["Mode"] = 1;
                    ddlBankname.Visible = true;
                    ddlBankname.Enabled = true;
                    //  MstMethods.Dropdown.Fill(ddlBankname, "aopr_bankmst_def", "var_bankmst_bankname", "num_bankmst_bankid", "", "");
                    // MstMethods.Dropdown.Fill(ddlBankname, "aoup_Bank_def  ", "var_bank_bankname", "num_bank_bankid ", "", "");

                    txtBranch.Focus();
                }
                else
                {
                    Session["Mode"] = 2;
                    txtBranch.Enabled = true;
                    txtIFSC.Enabled = true;
                    ddlBankname.Visible = true;
                    ddlBankname.Enabled = false;
                    //txtBankname.Visible = true;
                    grdBranchList_set();
                }
            }
        }

        protected void btnSave_Cilck(object sender, EventArgs e)
        {
            try
            {
                if (ddlBankname.SelectedIndex == 0)
                {
                    MessageAlert("|| Bank cannot be blank  ||", "");
                    ddlBankname.Focus();
                    return;
                }

                //if (Request.QueryString["@"] == "2")
                //{
                //    if (txtBankname.Text.Trim() == "")
                //    {
                //        MessageAlert("|| Bank cannot be blank ||", "");
                //        txtBankname.Focus();
                //        return;
                //    }
                //}
                if (txtIFSC.Text.Trim() == "")
                {
                    MessageAlert(" || Please insert IFSC Code || ", "");
                    return;
                }
                if (txtIFSC.Text.Length != 11)
                {
                    MessageAlert(" IFSC Code should be 11 characters ", "");
                    return;
                }

                if (txtBranch.Text.Trim() == "")
                {
                    MessageAlert("||Branch cannot be blank||", "");
                    txtBranch.Focus();
                    return;
                }
                Int32 branch = 0;

                //objBranch.UserId = Session["UserName"].ToString();
                objBranch.UserId = Session["UserId"].ToString();
                objBranch.Branchname = txtBranch.Text.Trim();
                objBranch.Ifscnumber = txtIFSC.Text.Trim();

                if (Request.QueryString["@"] == "1")
                {
                    objBranch.Mode = 1;
                    objBranch.Branchid = branch;
                    //objBranch.Bankid = Convert.ToInt32(Session["bankid"]);
                    objBranch.Bankid = Convert.ToInt32(ddlBankname.SelectedValue);
                }
                else
                {
                    objBranch.Mode = 2;
                    objBranch.Branchid = Convert.ToInt32(Session["branchid"]);
                    objBranch.Bankid = Convert.ToInt32(Session["bankid"]);
                }

                objBranch.BoBranch_1();

                if (objBranch.ErrorCode == -100)
                {
                    MessageAlert(objBranch.ErrorMsg, "../Master/FrmBranchList.aspx");
                    return;
                }
                else
                {
                    MessageAlert(objBranch.ErrorMsg, "");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAlert(ex.Message, "");
            }
        }

        protected void btnCancel_Cilck(object sender, EventArgs e)
        {
            Response.Redirect("../Master/FrmBranchList.aspx");
            return;
        }

        private void grdBranchList_set()
        {
            #region "Load Grid"

            String query = "select a.num_bankbranch_branchid branchid,a.var_bankbranch_branchname branchname,a.num_bankbranch_bankid bankid,a.var_bankbranch_IFSCcode ifsccode, ";
            query += " b.var_bank_bankname bankname from aoup_BankBranch_def a inner join aoup_Bank_def b on a.num_bankbranch_bankid=b.num_bank_bankid ";
            query += " where a.num_bankbranch_branchid='" + Session["branchid"] + "' order by a.num_bankbranch_branchid";

            DataTable dtBranchList = (DataTable)AnganwadiLib.Methods.MstMethods.Query2DataTable.GetResult(query);

            if (dtBranchList.Rows.Count > 0)
            {
                //if (dtBranchList.Rows[0]["bankname"].ToString() != "" && dtBranchList.Rows[0]["bankname"].ToString() != null)
                //{
                //    txtBankname.Text = dtBranchList.Rows[0]["bankname"].ToString();
                //}
                if (dtBranchList.Rows[0]["bankid"].ToString() != "" && dtBranchList.Rows[0]["bankid"].ToString() != null)
                {
                    ddlBankname.SelectedValue = dtBranchList.Rows[0]["bankid"].ToString();
                }
                if (dtBranchList.Rows[0]["branchname"].ToString() != "" && dtBranchList.Rows[0]["branchname"].ToString() != null)
                {
                    txtBranch.Text = dtBranchList.Rows[0]["branchname"].ToString();
                }
                if (dtBranchList.Rows[0]["ifsccode"].ToString() != "" && dtBranchList.Rows[0]["ifsccode"].ToString() != null)
                {
                    txtIFSC.Text = dtBranchList.Rows[0]["ifsccode"].ToString();
                }
                Session["bankid"] = Convert.ToInt32(dtBranchList.Rows[0]["bankid"]);
            }
            #endregion
        }
    }
}